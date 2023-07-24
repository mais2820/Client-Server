using API.Contracts;
using API.DTOs.RoomDto;
using FluentValidation;

namespace API.Utilities.Validation.Rooms
{
    public class NewRoomValidator : AbstractValidator<NewRoomDto>
    {
        private readonly IRoomRepository _roomRepository;
        public NewRoomValidator(IRoomRepository roomRepository) 
        {
            _roomRepository = roomRepository;

            RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
            RuleFor(r => r.Floor).NotEmpty();
            RuleFor(r => r.Capacity).NotEmpty();
        }
    }
}
