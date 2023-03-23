using FluentValidation;
using Jogging_Times.Core.DTOs.JoggingTimeMangementDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogging_Times.Core.Validation.JoggingTimeManagementDtoValidation
{
    public class FilterJoggingTimeManagementDtoValidation:AbstractValidator<FilterJoggingTimeManagementDto>
    {
        public FilterJoggingTimeManagementDtoValidation()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("User Id is required");

            RuleFor(x => x.FromDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("start date is required");

            RuleFor(x => x.ToDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("end date is required");
        }
    }
}
