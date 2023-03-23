//using FluentValidation;
//using Jogging_Times.Core.Models;

//namespace Jogging_Times.Core.Validation
//{
//    public class JoggingTimeValidaion : AbstractValidator<IJoggingTime>
//    {
//        public JoggingTimeValidaion()
//        {
//            RuleFor(x => x.Id)
//                .NotNull()
//                .NotEmpty()
//                .WithMessage("JoggingTime id is required");


//            RuleFor(x => x.Distance)
//                .NotNull()
//                .NotEmpty()
//                .WithMessage("Distance is required");

//            RuleFor(x => x.Time)
//                .NotNull()
//                .NotEmpty()
//                .WithMessage("Time is required");

//            RuleFor(x => x.Date)
//                .NotNull()
//                .NotEmpty()
//                .WithMessage("Date is required");

//            RuleFor(x => x.UserId)
//                .NotNull()
//                .NotEmpty()
//                .WithMessage("User id is required");
//        }
//    }
//}
