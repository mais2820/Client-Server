using API.Contracts;
using API.Data;
using API.DTOs.RoomDto;
using API.Models;
using API.Utilities.Enums;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingDbContext context) : base(context) {
           
        }
    }
}
