using API.Contracts;
using API.DTOs.AccountDto;
using API.DTOs.AccountRoleDto;
using FluentValidation;

namespace API.Utilities.Validation.AccountRoles
{
    public class AccountRoleValidator : AbstractValidator<AccountRoleDto>
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountRoleValidator(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;

            RuleFor(a => a.AccountGuid).NotEmpty();
            RuleFor(a => a.RoleGuid).NotEmpty();
        }
    }
}
