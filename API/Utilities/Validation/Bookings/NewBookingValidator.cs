using API.Contracts;
using API.DTOs.BookingDto;
using FluentValidation;

namespace API.Utilities.Validation.Bookings
{
    public class NewBookingValidator : AbstractValidator<NewBookingDto>
    {
        private readonly IBookingRepository _bookingRepository;
        public NewBookingValidator(IBookingRepository bookingRepository) 
        { 
            _bookingRepository = bookingRepository;

            RuleFor(b => b.RoomGuid).NotEmpty();
            RuleFor(b => b.EmployeeGuid).NotEmpty();
            RuleFor(b => b.StartDate).NotEmpty().GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(b => b.EndDate).NotEmpty().GreaterThanOrEqualTo(b => b.StartDate.AddDays(1));
            RuleFor(b => b.Status).NotEmpty();
            RuleFor(b => b.Remarks).NotEmpty();
        }
    }
}
