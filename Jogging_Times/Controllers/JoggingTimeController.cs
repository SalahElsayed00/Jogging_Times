using Jogging_Times.Core.Const;
using Jogging_Times.Core.DTOs.JoggingTimesDto;
using Jogging_Times.Core.Models;
using Jogging_Times.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jogging_Times.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JoggingTimeController : ControllerBase
    {
        private readonly IJoggingTimeService _joggingService;

        public JoggingTimeController(IJoggingTimeService joggingService)
        {
            _joggingService = joggingService;
        }

        [HttpGet("GetAllJoggingTime/{userId}")]
        public async Task<IActionResult> GetAllAsync(string userId)
        {
            var jogginTimes = await _joggingService.GetJoggingTimesAsync(userId);

            if (!ModelState.IsValid)
                BadRequest(ModelState);

            return Ok(jogginTimes);
        }

        [HttpPost("CreateJoggingTime")]
        public async Task<IActionResult> CreateAsync(JoggingTimeDto joggingTimeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var joggingTime = await _joggingService.CreateJoggingAsync(joggingTimeDto);

            if (string.IsNullOrEmpty(joggingTime.Distance))
                return BadRequest(joggingTime.Message);

            return Ok(joggingTime);
        }

        [HttpPut("UpdateJoggingTime")]
        public async Task<IActionResult> UpdateAsync(UpdateJoggingTimeDto UpdatejoggingTimeDto)
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
    }
}
