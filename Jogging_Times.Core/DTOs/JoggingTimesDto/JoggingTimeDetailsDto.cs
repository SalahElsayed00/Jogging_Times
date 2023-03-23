using Jogging_Times.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Jogging_Times.Core.DTOs.JoggingTimesDto
{
    public class JoggingTimeDetailsDto
    {
        public string? Date { get; set; }
        public string? Distance { get; set; }
        public string? Time { get; set; }
        public string? Message { get; set; }
    }
}
