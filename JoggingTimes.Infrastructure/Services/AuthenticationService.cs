using AutoMapper;
using Jogging_Times.Core.Const;
using Jogging_Times.Core.DTOs.AuthenticationDto;
using Jogging_Times.Core.Helpers;
using Jogging_Times.Core.Models;
using Jogging_Times.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jogging_Times.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly Jwt _jwt;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
            IMapper mapper, IOptions<Jwt> jwt, IPasswordHasher<ApplicationUser> passwordHasher,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
            _jwt = jwt.Value;
        }

        public async Task<List<UsersDto>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users is null)
                return new List<UsersDto> { new UsersDto { Message = ResponseMessage.UserNotFoundMessage } };
            var result = _mapper.Map<List<UsersDto>>(users);

            return result;
        }

        public async Task<ResponseAuthDto> RegisterUserAsync(RegisterUserDto registerUserDto, string role)
        {
            if (await _userManager.FindByEmailAsync(registerUserDto.Email) is not null)
                return new ResponseAuthDto { Message = ResponseMessage.EmailErrorMessage };

            if (await _userManager.FindByNameAsync(registerUserDto.UserName) is not null)
                return new ResponseAuthDto { Message = ResponseMessage.UserNameErrorMessage };

            var loggedUserRole = GetUserRole();

            if (loggedUserRole == UserRoles.UserManager && role == UserRoles.Admin)
                return new ResponseAuthDto { Message = ResponseMessage.RegisterRoleErrorMessage };

            var user = _mapper.Map<ApplicationUser>(registerUserDto);
            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error.Description} ,{Environment.NewLine}";

                return new ResponseAuthDto { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, role.ToLower());

            var jwtSecurityToken = await CreateJwtToken(user);

            return new ResponseAuthDto
            {
                UserName = user.UserName,
                Email = user.Email,
                IsAuthenticated = true,
                Roles = new List<string> { role.ToLower() },
                ExpireDate = jwtSecurityToken.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };
        }

        public async Task<ResponseAuthDto> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(updateUserDto.UserId);

            if (user is null)
                return new ResponseAuthDto { Message = ResponseMessage.UserNotFoundMessage };

            user.UserName = updateUserDto.UserName;
            user.Email = updateUserDto.Email;
            user.PasswordHash = _passwordHasher.HashPassword(user, updateUserDto.Password);
            var result = await _userManager.UpdateAsync(user);

            var oldRoleName = await _userManager.GetRolesAsync(user);
            foreach (var role in oldRoleName)
            {
                if (role != updateUserDto.Role)
                    await _userManager.AddToRoleAsync(user, updateUserDto.Role);
                else
                    return new ResponseAuthDto { Message = ResponseMessage.RoleExistMessage };
            }


            if (!result.Succeeded)
                return new ResponseAuthDto { Message = ResponseMessage.ErrorUpdateMessage };

            return new ResponseAuthDto
            {
                IsAuthenticated = true,
                Message = ResponseMessage.SuccessUpdateMessage,
                Email = user.Email,
                UserName = user.UserName,
                Roles = new List<string> { updateUserDto.Role }
            };
        }

        public async Task<ResponseAuthDto> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return new ResponseAuthDto { Message = ResponseMessage.UserNotFoundMessage };

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return new ResponseAuthDto { Message = ResponseMessage.ErrorDeleteUserMessage };

            return new ResponseAuthDto { Message = ResponseMessage.SuccessDeleteUserMessage, IsAuthenticated = true };
        }

        public async Task<ResponseAuthDto> LoginUserAsync(LogInUserDto logInUserDto)
        {
            var user = await _userManager.FindByEmailAsync(logInUserDto.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, logInUserDto.Password))
                return new ResponseAuthDto { Message = ResponseMessage.UserNamePasswordMessage };

            var role = await _userManager.GetRolesAsync(user);
            var jwtSecurityToken = await CreateJwtToken(user);

            return new ResponseAuthDto
            {
                UserName = user.UserName,
                Email = user.Email,
                IsAuthenticated = true,
                Roles = role.ToList(),
                ExpireDate = jwtSecurityToken.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private string GetUserRole()
        {
            // Retrieve the current user's identity from the HttpContext
            ClaimsIdentity identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            string role = "";
            // Check if the user is authenticated
            if (identity != null && identity.IsAuthenticated)
            {
                // Retrieve the user's role from the identity
                role += identity.FindFirst(ClaimTypes.Role)?.Value;
            }
            return role;
        }
    }
}
