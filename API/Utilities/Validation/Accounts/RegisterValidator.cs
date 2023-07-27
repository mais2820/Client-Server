using API.Contracts;
using API.DTOs.AccountDto;
using FluentValidation;

namespace API.Utilities.Validation.Accounts
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public RegisterValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(l => l.FirstName)
                .NotEmpty();
            RuleFor(l => l.BirthDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-10));
            RuleFor(l => l.Gender)
                .NotNull()
                .IsInEnum();
            RuleFor(l => l.HiringDate)
                .NotEmpty();
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid")
                .Must(IsDuplicateValue).WithMessage("Email already exists"); ;
            RuleFor(l => l.PhoneNumber).NotEmpty()
                .Must(IsDuplicateValue).WithMessage("Phone Number already exists");
            RuleFor(l => l.Major)
                .NotEmpty();
            RuleFor(l => l.Degree)
                .NotEmpty();
            RuleFor(l => l.GPA)
                .LessThanOrEqualTo(0)
                .GreaterThanOrEqualTo(4)
                .NotEmpty();
            RuleFor(l => l.UniversityCode)
                .NotEmpty(); 
            RuleFor(l => l.UniversityName)
                .NotEmpty();
            RuleFor(l => l.Password)
                .NotEmpty()
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$\");
        }

        private bool IsDuplicateValue(string value)
        {
            return _employeeRepository.IsNotExist(value);
        }
    }
}
