using API.Contracts;
using API.DTOs.AccountRoleDto;
using FluentValidation;

namespace API.Utilities.Validation.AccountRoles
{
    public class NewAccountRoleValidator : AbstractValidator<NewAccountRoleDto>
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public NewAccountRoleValidator(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;

            RuleFor(a => a.AccountGuid).NotEmpty();
            RuleFor(a => a.RoleGuid).NotEmpty();
        }
    }
}
