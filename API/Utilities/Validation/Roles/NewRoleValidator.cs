using API.Contracts;
using API.DTOs.RoleDto;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace API.Utilities.Validation.Roles
{
    public class NewRoleValidator : AbstractValidator<NewRoleDto>
    {
        private readonly IRoleRepository _roleRepository;

        public NewRoleValidator(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;

            RuleFor(r => r.Name).NotEmpty().MaximumLength(100);            
        }
    }
}
