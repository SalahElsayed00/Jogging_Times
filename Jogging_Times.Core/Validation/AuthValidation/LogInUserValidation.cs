using FluentValidation;
using Jogging_Times.Core.DTOs;
using Jogging_Times.Core.DTOs.AuthenticationDto;

namespace Jogging_Times.Core.Validation
{
    public class LogInUserValidation : AbstractValidator<LogInUserDto>
    {
        public LogInUserValidation()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("User Name is required");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Your password cannot be empty");
        }
    }
}
