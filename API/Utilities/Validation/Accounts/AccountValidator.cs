using API.Contracts;
using API.DTOs.AccountDto;
using FluentValidation;

namespace API.Utilities.Validation.Accounts
{
    public class AccountValidator : AbstractValidator<AccountDto>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

            RuleFor(a => a.Guid).NotEmpty();
            RuleFor(a => a.Password).NotEmpty().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$\");
            //Minimum eight characters, at least one uppercase letter, one lowercase letter and one number
            RuleFor(a => a.Otp).NotEmpty();
            RuleFor(a => a.IsUsed).NotEmpty();
            RuleFor(a => a.ExpiredTime).NotEmpty();
        }
    }
}
