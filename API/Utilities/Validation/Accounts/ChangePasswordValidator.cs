using API.Contracts;
using API.DTOs.AccountDto;
using FluentValidation;

namespace API.Utilities.Validation.Accounts
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public ChangePasswordValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(e => e.Email)
                .NotEmpty()
                .WithMessage("Email is required");
            RuleFor(Accounts => Accounts.OTP)
                .NotEmpty()
                .WithMessage("OTP is Required");
            RuleFor(Accounts => Accounts.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");
            RuleFor(Accounts => Accounts.NewPassword)
                .NotEmpty();
            
        }
    }
}
