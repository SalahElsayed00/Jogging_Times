using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogging_Times.Core.DTOs.JoggingTimesDto
{
    public class WeeklyAverageDto
    {
        public string? Average_Distance { get; set; }
        public string? Average_Time { get; set; }
        public string? Count_jogging { get; set; }
    }
}
