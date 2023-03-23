namespace Jogging_Times.Core.Helpers
{
    public class Jwt
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? SecretKey { get; set; }
        public double DurationInDays { get; set; }
    }
}
