namespace Jogging_Times.Core.DTOs.AuthenticationDto
{
    public class ResponseAuthDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
