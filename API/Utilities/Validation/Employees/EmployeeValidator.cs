using API.Contracts;
using API.DTOs.EmployeeDto;
using FluentValidation;

namespace API.Utilities.Validation.Employees
{
    public class EmployeeValidator : AbstractValidator<EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(e => e.FirstName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(e => e.BirthDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-10));
            RuleFor(e => e.Gender)
                .NotNull()
                .IsInEnum();
            RuleFor(e => e.HiringDate)
                .NotEmpty();
            RuleFor(e => e.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is not valid")
                .Must(IsDuplicateValue)
                .WithMessage("Email already exist");
            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20)
                .Matches(@"^\+[0-9]")
                .Must(IsDuplicateValue)
                .WithMessage("Phone Number already exist");
        }

        private bool IsDuplicateValue(string arg)
        {
            return _employeeRepository.IsNotExist(arg);
        }
    }
}
