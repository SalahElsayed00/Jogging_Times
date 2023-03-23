using AutoMapper;
using Jogging_Times.Core.Const;
using Jogging_Times.Core.DTOs.JoggingTimeMangementDto;
using Jogging_Times.Core.DTOs.JoggingTimesDto;
using Jogging_Times.Core.Models;
using Jogging_Times.Core.Services;
using JoggingTimes.Infrastructure.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JoggingTimes.Infrastructure.Services
{
    public class JoggingTimeManagementService:IjoggingTimeManagementService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JoggingTimeManagementService(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<JoggingTimeDetailsDto>> GetJoggingTimesAsync(string userId)
        {
            var joggingTimes = await _context.JoggingTimes
                .Where(j => j.UserId == userId)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            var JoggingTimeDto = _mapper.Map<IEnumerable<JoggingTimeDetailsDto>>(joggingTimes);

            return JoggingTimeDto;
        }

        public async Task<IEnumerable<JoggingTimeDetailsDto>> FilterJoggingTimeAsync(FilterJoggingTimeManagementDto filterTime)
        {
            var joggingTimeFilteration = await _context.JoggingTimes
                .AsNoTracking()
                .Where(j => j.UserId == filterTime.UserId &&
                            j.Date >= filterTime.FromDate.Value.Date &&
                            j.Date <= filterTime.ToDate.Value.Date)
                .ToListAsync();

            var joggingTimeDetailsDto = _mapper.Map<IEnumerable<JoggingTimeDetailsDto>>(joggingTimeFilteration);
            return joggingTimeDetailsDto;
        }

        public WeeklyAverageDto AverageCaculationReport(FilterJoggingTimeManagementDto filterTime)
        {
            var joggingTime = _context.JoggingTimes
                .Where(j => j.UserId == filterTime.UserId &&
                       j.Date >= filterTime.FromDate.Value.Date &&
                       j.Date <= filterTime.ToDate.Value.Date);

            var average = new WeeklyAverageDto
            {
                Average_Time = $"{Math.Round(joggingTime.Average(x => x.Time), 2)} H",
                Average_Distance = $"{Math.Round(joggingTime.Average(x => x.Distance), 2)} M",
                Count_jogging = $"{joggingTime.Count()}"
            };

            return average;
        }

        public async Task<JoggingTimeDetailsDto> CreateJoggingAsync(JoggingTimeManagementDto joggingTimeDto)
        {
            if (joggingTimeDto is null)
                new JoggingTimeDetailsDto { Message = ResponseMessage.JoggingNullErrorMessage };

            var userExist = await GetUser(joggingTimeDto.UserId);

            if (!userExist)
                return new JoggingTimeDetailsDto { Message = ResponseMessage.UserNotFoundMessage };

            var joggingTime = _mapper.Map<JoggingTime>(joggingTimeDto);

            await _context.JoggingTimes.AddAsync(joggingTime);
            await _context.SaveChangesAsync();

            var joggingTimeDetailsDto = _mapper.Map<JoggingTimeDetailsDto>(joggingTime);

            joggingTimeDetailsDto.Message = ResponseMessage.JoggingTimeCreatedMessage;
            return joggingTimeDetailsDto;
        }

        public async Task<JoggingTimeDetailsDto> UpdateJoggingAsync(UpdateJoggingTimeManagementDto updatejoggingTimeDto)
        {
            if (updatejoggingTimeDto.Id <= 0)
                return new JoggingTimeDetailsDto { Message = ResponseMessage.joggingNotFoundMessage };

            var joggingTimeExist = await _context.JoggingTimes.FindAsync(updatejoggingTimeDto.Id);

            if (joggingTimeExist is null)
                return new JoggingTimeDetailsDto { Message = ResponseMessage.joggingNotFoundMessage };

            var userExist = await GetUser(updatejoggingTimeDto.UserId);

            if (!userExist)
                return new JoggingTimeDetailsDto { Message = ResponseMessage.UserNotFoundMessage };

            var joggingTime = _mapper.Map<JoggingTime>(updatejoggingTimeDto);

            _context.Entry(joggingTimeExist).CurrentValues.SetValues(joggingTime);

            await _context.SaveChangesAsync();

            var UpdatedjoggingTime = _mapper.Map<JoggingTimeDetailsDto>(joggingTime);
            UpdatedjoggingTime.Message = ResponseMessage.JoggingTimeUpdatedMessage;
            return UpdatedjoggingTime;
        }

        public async Task<JoggingTimeDetailsDto> DeleteJoggingAsync(int joggingTimeId)
        {
            var joggingTime = await _context.JoggingTimes.FindAsync(joggingTimeId);

            if (joggingTime is null)
                return new JoggingTimeDetailsDto { Message = ResponseMessage.joggingNotFoundMessage };

            _context.JoggingTimes.Remove(joggingTime);
            await _context.SaveChangesAsync();
            return new JoggingTimeDetailsDto { Message = ResponseMessage.JoggingTimeDeletedMessage };
        }

        private async Task<bool> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return false;

            return true;
        }

        private string GetCurrentUserId()
        {
            // Retrieve the current user's identity from the HttpContext
            ClaimsIdentity identity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
            string userId = identity.FindFirst("uid")?.Value;
            return userId;
        }
    }
}
