using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.BookingDto
{
    public class NewBookingDto
    {
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }

        public static implicit operator Booking(NewBookingDto newBookingDto)
        {
            return new Booking
            {
                Guid = Guid.NewGuid(),
                RoomGuid = newBookingDto.RoomGuid,
                EmployeeGuid = newBookingDto.EmployeeGuid,
                StartDate = newBookingDto.StartDate,
                EndDate = newBookingDto.EndDate,
                Status = newBookingDto.Status,
                Remarks = newBookingDto.Remarks,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewBookingDto(Booking booking)
        {
            return new NewBookingDto
            {
                RoomGuid = booking.RoomGuid,
                EmployeeGuid = booking.EmployeeGuid,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status,
                Remarks = booking.Remarks
            };
        }
    }
}
