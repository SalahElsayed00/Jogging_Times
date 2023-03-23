using Jogging_Times.Core.DTOs.JoggingTimesDto;
using Jogging_Times.Core.Models;
using System.Linq.Expressions;

namespace Jogging_Times.Core.Services
{
    public interface IJoggingTimeService
    {
        Task<IEnumerable<JoggingTimeDetailsDto>> GetJoggingTimesAsync();
        Task<IEnumerable<JoggingTimeDetailsDto>> FilterJoggingTimeAsync(FilterJoggingTimeDto filterTime);
        Task<JoggingTimeDetailsDto> CreateJoggingAsync(JoggingTimeDto joggingTimeDto);
        Task<JoggingTimeDetailsDto> UpdateJoggingAsync(UpdateJoggingTimeDto joggingTime);
        Task<JoggingTimeDetailsDto> DeleteJoggingAsync(int joggingTimeId);
        WeeklyAverageDto AverageCaculationReport(FilterJoggingTimeDto filterTime);
    }
}
