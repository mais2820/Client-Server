using API.Contracts;
using API.DTOs.BookingDto;
using FluentValidation;

namespace API.Utilities.Validation.Bookings
{
    public class BookingValidator : AbstractValidator<BookingDto>
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingValidator(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;

            RuleFor(b => b.RoomGuid).NotEmpty();
            RuleFor(b => b.EmployeeGuid).NotEmpty();
            RuleFor(b => b.StartDate).NotEmpty();
            RuleFor(b => b.EndDate).NotEmpty();
            RuleFor(b => b.Status).NotEmpty();
            RuleFor(b => b.Remarks).NotEmpty();
        }
    }
}
