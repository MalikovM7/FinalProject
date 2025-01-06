//using FinalProjectMVC.ViewModels.Reservation;
//using FinalProjectMVC.ViewModels.Testimonials;
//using FluentValidation;

//namespace FinalProjectMVC.FluentValidation.ReserveValidation
//{
//    public class RentValidation : AbstractValidator<CarReservationViewModel>
//    {

//        public RentValidation()
//        {
//            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");

//            RuleFor(x => x.CarId).GreaterThan(0).WithMessage("Car ID is required.");

//            RuleFor(x => x.StartDate)
//                .LessThan(x => x.EndDate)
//                .WithMessage("Start date must be earlier than the end date.")
//                .GreaterThanOrEqualTo(DateTime.Today)
//                .WithMessage("Start date cannot be in the past.");

//            RuleFor(x => x.EndDate)
//                .GreaterThan(x => x.StartDate)
//                .WithMessage("End date must be after the start date.");

//            RuleFor(x => x.TotalPrice)
//                .GreaterThan(0)
//                .WithMessage("Total price must be greater than zero.");

//            RuleFor(x => x.PhoneNumber)
//                .NotEmpty().WithMessage("Phone number is required.")
//                .Matches(@"^\+994(?:50|51|55|77|70|10)[1-9]\d{6}$")
//                .WithMessage("Please enter a valid phone number in the format: +994 followed by 50, 51, 55, 77, 70, or 10, and then 7 digits not starting with 0.");

//            RuleFor(x => x.DrivingLicense)
//                .NotNull().WithMessage("Driving license upload is required.");
                
//        }
//    }

//}
