using FluentValidation;
using Jogging_Times.Core.DTOs.JoggingTimesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogging_Times.Core.Validation
{
    public class FilterJoggingTimeDtoValidation: AbstractValidator<FilterJoggingTimeDto>
    {
        public FilterJoggingTimeDtoValidation()
        {
            RuleFor(j => j.FromDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("Start date  is required");

            RuleFor(j => j.ToDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("End date is required");
        }
    }
}
