using System.ComponentModel.DataAnnotations;

namespace Jogging_Times.Core.Models
{
    public class JoggingTime
    {
        public int Id { get; set; }
        public double Distance { get; set; }
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}
