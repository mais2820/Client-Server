using API.Contracts;
using API.DTOs.BookingDto;
using API.Models;

namespace API.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IEnumerable<BookingDto> GetAll()
        {
            var bookings = _bookingRepository.GetAll();
            if (!bookings.Any())
            {
                return Enumerable.Empty<BookingDto>(); // Booking is null or not found;
            }

            var bookingDtos = new List<BookingDto>();
            foreach (var booking in bookings)
            {
                bookingDtos.Add((BookingDto)booking);
            }

            return bookingDtos; // Booking is found;
        }

        public BookingDto? GetByGuid(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return null; // Booking is null or not found;
            }

            return (BookingDto)booking; // Booking is found;
        }

        public BookingDto? Create(NewBookingDto newBookingDto)
        {
            var booking = _bookingRepository.Create(newBookingDto);
            if (booking is null)
            {
                return null; // Booking is null or not found;
            }

            return (BookingDto)booking; // Booking is found;
        }

        public int Update(BookingDto bookingDto)
        {
            var booking = _bookingRepository.GetByGuid(bookingDto.Guid);
            if (booking is null)
            {
                return -1; // Booking is null or not found;
            }

            Booking toUpdate = bookingDto;
            toUpdate.CreatedDate = booking.CreatedDate;
            var result = _bookingRepository.Update(toUpdate);

            return result ? 1 // Booking is updated;
                : 0; // Booking failed to update;
        }

        public int Delete(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return -1; // Booking is null or not found;
            }

            var result = _bookingRepository.Delete(booking);

            return result ? 1 // Booking is deleted;
                : 0; // Booking failed to delete;
        }
    }
}
