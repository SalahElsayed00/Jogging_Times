using Microsoft.AspNetCore.Identity;

namespace Jogging_Times.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<JoggingTime> joggingTime { get; set; }
    }
}
