using API.Models;
using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs.BookingDto
{
    public class BookingDto
    {
        public Guid Guid { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }

        public static implicit operator Booking(BookingDto bookingDto)
        {
            return new Booking
            {
                Guid = bookingDto.Guid,
                RoomGuid = bookingDto.RoomGuid,
                EmployeeGuid = bookingDto.EmployeeGuid,
                StartDate = bookingDto.StartDate,
                EndDate = bookingDto.EndDate,
                Status = bookingDto.Status,
                Remarks = bookingDto.Remarks,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator BookingDto(Booking booking)
        {
            return new BookingDto
            {
                Guid = booking.Guid,
                RoomGuid = booking.RoomGuid,
                EmployeeGuid = booking.EmployeeGuid,
                StartDate= booking.StartDate,
                EndDate= booking.EndDate,
                Status = booking.Status,
                Remarks = booking.Remarks
            };
        }
    }
}
