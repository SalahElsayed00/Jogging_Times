using Jogging_Times.Core.DTOs.JoggingTimeMangementDto;
using Jogging_Times.Core.DTOs.JoggingTimesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogging_Times.Core.Services
{
    public interface IjoggingTimeManagementService
    {
        Task<IEnumerable<JoggingTimeDetailsDto>> GetJoggingTimesAsync(string userId);
        Task<IEnumerable<JoggingTimeDetailsDto>> FilterJoggingTimeAsync(FilterJoggingTimeManagementDto filterTime);
        Task<JoggingTimeDetailsDto> CreateJoggingAsync(JoggingTimeManagementDto joggingTimeDto);
        Task<JoggingTimeDetailsDto> UpdateJoggingAsync(UpdateJoggingTimeManagementDto joggingTime);
        Task<JoggingTimeDetailsDto> DeleteJoggingAsync(int joggingTimeId);
        WeeklyAverageDto AverageCaculationReport(FilterJoggingTimeManagementDto filterTime);
    }
}
