using AutoMapper;
using Jogging_Times.Core.DTOs.AuthenticationDto;
using Jogging_Times.Core.DTOs.JoggingTimeMangementDto;
using Jogging_Times.Core.DTOs.JoggingTimesDto;
using Jogging_Times.Core.Models;

namespace Jogging_Times.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserDto, ApplicationUser>();
            CreateMap<ApplicationUser, UsersDto>();
            CreateMap<ApplicationUser, LogInUserDto>();
            CreateMap<JoggingTimeDto, JoggingTime>();
            CreateMap<UpdateJoggingTimeDto, JoggingTime>();
            CreateMap<JoggingTime, JoggingTimeDetailsDto>();
            CreateMap<UpdateJoggingTimeManagementDto, JoggingTime>();
            CreateMap<JoggingTimeManagementDto, JoggingTime>();
            CreateMap<JoggingTime, JoggingTimeDetailsDto>()
                    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("MM-dd-yyyy")))
                    .ForMember(dest => dest.Distance, opt => opt.MapFrom(src => src.Distance + " M"))
                    .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time + " H"));

        }
    }
}
