using FluentValidation;
using Jogging_Times.Core.DTOs.JoggingTimesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogging_Times.Core.Validation.JoggingTimeValidation
{
    public class UpdateJoggingTimeDtoValidation:AbstractValidator<UpdateJoggingTimeDto>
    {
        public UpdateJoggingTimeDtoValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Id is required");

            RuleFor(x => x.Distance)
                .NotNull()
                .NotEmpty()
                .WithMessage("Distance is required");

            RuleFor(x => x.Time)
                .NotNull()
                .NotEmpty()
                .WithMessage("Time is required");

            RuleFor(x => x.Date)
                .NotNull()
                .NotEmpty()
                .WithMessage("Date is required");
        }
    }
}
