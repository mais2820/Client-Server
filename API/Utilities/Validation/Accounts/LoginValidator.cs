using API.Contracts;
using API.DTOs.AccountDto;
using FluentValidation;

namespace API.Utilities.Validation.Accounts
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public LoginValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(l => l.Email).NotEmpty().EmailAddress();
            RuleFor(l => l.Password).NotEmpty().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");
        }
    }
}
