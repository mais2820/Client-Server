using API.Contracts;
using API.DTOs.RoleDto;
using FluentValidation;

namespace API.Utilities.Validation.Roles
{
    public class RoleValidator : AbstractValidator<RoleDto>
    {
        private readonly IRoleRepository _roleRepository;

        public RoleValidator(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;

            RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
        }
    }
}
