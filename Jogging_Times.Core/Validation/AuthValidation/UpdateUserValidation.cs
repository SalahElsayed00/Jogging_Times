using FluentValidation;
using Jogging_Times.Core.DTOs;
using Jogging_Times.Core.DTOs.AuthenticationDto;

namespace Jogging_Times.Core.Validation
{
    public class UpdateUserValidation : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidation()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("User Id is required");

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

            RuleFor(x => x.Role)
                .NotNull()
                .NotEmpty()
                .WithMessage("Role is required");
        }
    }
}
