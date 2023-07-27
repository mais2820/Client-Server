using API.Contracts;
using API.DTOs.BookingDto;
using API.DTOs.RoomDto;
using API.Models;
using API.Repositories;
using API.Utilities.Enums;

namespace API.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository, IEmployeeRepository employeeRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _employeeRepository = employeeRepository;
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

        public IEnumerable<RoomDto> FreeRoomToday()
        {
            List<RoomDto> roomDtos = new List<RoomDto>();
            var bookings = GetAll();
            var freeBookings = bookings.Where(b => b.Status == StatusLevel.Done);
            var freeBookingsToday = freeBookings.Where(b => b.EndDate < DateTime.Now);
            foreach (var booking in freeBookingsToday)
            {
                var roomGuid = booking.RoomGuid;
                var room = _roomRepository.GetByGuid(roomGuid);
                RoomDto roomDto = new RoomDto()
                {
                    Guid = roomGuid,
                    Capacity = room.Capacity,
                    Floor = room.Floor,
                    Name = room.Name
                };
                roomDtos.Add(roomDto);
            }

            if (!roomDtos.Any())
            {
                return null; // No free room today
            }

            return roomDtos; // free room today
        }

        public IEnumerable<BookingLengthDto> BookingLength()
        {
            List<BookingLengthDto> listBookingLength = new List<BookingLengthDto>();
            TimeSpan workingHour = new TimeSpan(8, 30, 0);
            var timeSpan = new TimeSpan();
            var bookings = GetAll();
            foreach (var booking in bookings)
            {
                var currentDate = booking.StartDate;
                var endDate = booking.EndDate;

                while (currentDate <= endDate)
                {
                    // Memeriksa apakah hari saat ini adalah Sabtu atau Minggu
                    if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        // Hari kerja, menghitung waktu kerja dengan memperhitungkan jam
                        DateTime openRoom = currentDate.Date.AddHours(9); // Misalnya, waktu kerja dimulai pada pukul 09:00
                        DateTime closeRoom = currentDate.Date.AddHours(17).AddMinutes(30); // Misalnya, waktu kerja selesai pada pukul 17:30

                        TimeSpan dayTime = closeRoom - openRoom;
                        timeSpan += dayTime;
                    }

                    currentDate = currentDate.AddDays(1); // Pindah ke hari berikutnya
                }

                var room = _roomRepository.GetByGuid(booking.RoomGuid);

                var bookingLengthDto = new BookingLengthDto()
                {
                    RoomGuid = booking.RoomGuid,
                    RoomName = room.Name,
                    BookingLength = timeSpan.TotalHours
                };
                listBookingLength.Add(bookingLengthDto);
            }

            if (!listBookingLength.Any())
            {
                return null;
            }

            return listBookingLength;
        }

        public IEnumerable<DetailBookingDto> getDetailAll()
        {
            var resultBooking = _bookingRepository.GetAll();
            if (!resultBooking.Any())
            {
                return Enumerable.Empty<DetailBookingDto>();
            }

            var detailDtos = new List<DetailBookingDto>();
            foreach (var result in resultBooking)
            {
                var resultEmployee = _employeeRepository.GetByGuid(result.EmployeeGuid);
                if (resultEmployee is null)
                {
                    return Enumerable.Empty<DetailBookingDto>();
                }

                var resultRoom = _roomRepository.GetByGuid(result.RoomGuid);
                if (resultRoom is null)
                {
                    return Enumerable.Empty<DetailBookingDto>();
                }

                var toDto = new DetailBookingDto()
                {
                    BookingGuid = result.Guid,
                    BookedNik = resultEmployee.NIK,
                    BookedBy = resultEmployee.FirstName + " " + resultEmployee.LastName,
                    RoomName = resultRoom.Name,
                    StartDate = result.StartDate,
                    EndDate = result.EndDate,
                    Status = result.Status,
                    Remarks = result.Remarks
                };
                detailDtos.Add(toDto);
            }

            return detailDtos;
        }

        public DetailBookingDto? getDetailByGuid(Guid guid)
        {
            var resultBooking = _bookingRepository.GetByGuid(guid);
            if (resultBooking is null)
            {
                return null;
            }

            var resultEmployee = _employeeRepository.GetByGuid(resultBooking.EmployeeGuid);
            if (resultEmployee is null)
            {
                return null;
            }

            var resultRoom = _roomRepository.GetByGuid(resultBooking.RoomGuid);
            if (resultRoom is null)
            {
                return null;
            }

            var toDto = new DetailBookingDto()
            {
                BookingGuid = resultBooking.Guid,
                BookedNik = resultEmployee.NIK,
                BookedBy = resultEmployee.FirstName + " " + resultEmployee.LastName,
                RoomName = resultRoom.Name,
                StartDate = resultBooking.StartDate,
                EndDate = resultBooking.EndDate,
                Status = resultBooking.Status,
                Remarks = resultBooking.Remarks
            };

            return toDto;
        }
    }
}
