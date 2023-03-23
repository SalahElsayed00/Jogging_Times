namespace Jogging_Times.Core.DTOs.AuthenticationDto
{
    public class UpdateUserDto
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Password { get; set; }

    }
}
