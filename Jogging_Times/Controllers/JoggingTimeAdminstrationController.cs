using Jogging_Times.Core.Const;
using Jogging_Times.Core.DTOs.JoggingTimeMangementDto;
using Jogging_Times.Core.Models;
using Jogging_Times.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jogging_Times.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class JoggingTimeAdminstrationController : ControllerBase
    {
        private readonly IjoggingTimeManagementService _joggingService;

        public JoggingTimeAdminstrationController(IjoggingTimeManagementService joggingService)
        {
            _joggingService = joggingService;
        }

        [HttpGet("GetAllJoggingTimeByUserId/{userId}")]
        public async Task<IActionResult> GetAllAsync(string userId)
        {
            var jogginTimes = await _joggingService.GetJoggingTimesAsync(userId);

            if (!ModelState.IsValid)
                BadRequest(ModelState);

            return Ok(jogginTimes);
        }

        [HttpPost("CreateJoggingTime")]
        public async Task<IActionResult> CreateAsync(JoggingTimeManagementDto joggingTimeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var joggingTime = await _joggingService.CreateJoggingAsync(joggingTimeDto);

            if (string.IsNullOrEmpty(joggingTime.Distance))
                return BadRequest(joggingTime.Message);

            return Ok(joggingTime);
        }

        [HttpPut("UpdateJoggingTime")]
        public async Task<IActionResult> UpdateAsync(UpdateJoggingTimeManagementDto UpdatejoggingTimeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var joggingTime = await _joggingService.UpdateJoggingAsync(UpdatejoggingTimeDto);

            if (string.IsNullOrEmpty(joggingTime.Distance))
                return BadRequest(joggingTime.Message);

            return Ok(joggingTime);
        }

        [HttpDelete("DeleteJoggingTime/{joggingTimeId}")]
        public async Task<IActionResult> DeleteAsync(int joggingTimeId)
        {
            if (joggingTimeId <= 0)
                return BadRequest(ResponseMessage.joggingNotFoundMessage);

            var joggingTime = await _joggingService.DeleteJoggingAsync(joggingTimeId);

            if (string.IsNullOrEmpty(joggingTime.Distance))
                return BadRequest(joggingTime.Message);

            return Ok(joggingTime);
        }

        [HttpPost("JoggingTimeByDate")]
        public async Task<IActionResult> FilterAsync(FilterJoggingTimeManagementDto filterTime)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filterTimes = await _joggingService.FilterJoggingTimeAsync(filterTime);

            return Ok(filterTimes);
        }

        [HttpPost("AverageCaculationReport")]
        public IActionResult ReportAsync(FilterJoggingTimeManagementDto filterTime)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var average = _joggingService.AverageCaculationReport(filterTime);

            return Ok(average);
        }
    }
}
