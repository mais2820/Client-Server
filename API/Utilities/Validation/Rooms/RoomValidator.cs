using API.Contracts;
using API.DTOs.RoomDto;
using FluentValidation;

namespace API.Utilities.Validation.Rooms
{
    public class RoomValidator : AbstractValidator<RoomDto>
    {
        private readonly IRoomRepository _roomRepository;
        public RoomValidator(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;

            RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
            RuleFor(r => r.Floor).NotEmpty();
            RuleFor(r => r.Capacity).NotEmpty();
        }
    }
}
