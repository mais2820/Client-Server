using API.DTOs.RoomDto;
using API.Models;
using API.Repositories;
using API.Utilities.Enums;

namespace API.Contracts
{
    public interface IBookingRepository : IGeneralRepository<Booking>
    {
    }
}
