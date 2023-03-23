using Jogging_Times.Core.DTOs.AuthenticationDto;

namespace Jogging_Times.Core.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseAuthDto> RegisterUserAsync(RegisterUserDto registerUserDto,string role);
        Task<ResponseAuthDto> LoginUserAsync(LogInUserDto logInUserDto);
        Task<ResponseAuthDto> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<ResponseAuthDto> DeleteUserAsync(string userId);
        Task<List<UsersDto>> GetAllUsers();
    }
}
