using Jogging_Times.Core.DTOs.JoggingTimesDto;
using Jogging_Times.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jogging_Times.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportingController : ControllerBase
    {
        private readonly IJoggingTimeService _joggingService;

        public ReportingController(IJoggingTimeService joggingService)
        {
            _joggingService = joggingService;
        }

        [HttpPost("JoggingTimeByDate")]
        public async Task<IActionResult> FilterAsync(FilterJoggingTimeDto filterTime)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filterTimes = await _joggingService.FilterJoggingTimeAsync(filterTime);

            return Ok(filterTimes);
        }

        [HttpPost("AverageCaculationReport")]
        public IActionResult ReportAsync(FilterJoggingTimeDto filterTime)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var average = _joggingService.AverageCaculationReport(filterTime);

            return Ok(average);
        }
    }
}
