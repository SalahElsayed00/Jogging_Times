using Jogging_Times.Core.Const;
using Jogging_Times.Core.DTOs.AuthenticationDto;
using Jogging_Times.Core.Models;
using Jogging_Times.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Jogging_Times.Controllers
{
    [Authorize(Roles = $"{UserRoles.Admin}")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public UserManagementController(IAuthenticationService authService)
        {
            _authService = authService;
        }


        [HttpGet("AllUser")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await _authService.GetAllUsers();
            return Ok(result);
        }

        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.UserManager}")]
        [HttpPost("CreateUserAsync/{role}")]
        public async Task<IActionResult> CreateUserAsync(RegisterUserDto registerUserDto, string role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterUserAsync(registerUserDto, role);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.UpdateUserAsync(updateUserDto);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new { userName = result.UserName, email = result.Email, message = result.Message });
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest(ResponseMessage.RequiredUserIdMessage);

            var result = await _authService.DeleteUserAsync(userId);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new { message = result.Message });
        }

    }
}
