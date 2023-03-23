using FluentValidation;
using Jogging_Times.Core.DTOs;
using Jogging_Times.Core.DTOs.AuthenticationDto;

namespace Jogging_Times.Core.Validation
{
    public class RegisterUserValidation : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidation()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("User Name is required");


            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email is required");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Your password cannot be empty");
        }
    }
}
