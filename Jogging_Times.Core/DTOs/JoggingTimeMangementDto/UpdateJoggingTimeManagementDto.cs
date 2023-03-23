using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogging_Times.Core.DTOs.JoggingTimeMangementDto
{
    public class UpdateJoggingTimeManagementDto
    {
        public int Id { get; set; }
        public double Distance { get; set; }
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public string? UserId { get; set; }
    }
}
