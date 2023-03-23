using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogging_Times.Core.DTOs.JoggingTimeMangementDto
{
    public class JoggingTimeManagementDto
    {
        public string? UserId { get; set; }
        public string? Date { get; set; }
        public string? Distance { get; set; }
        public string? Time { get; set; }
    }
}
