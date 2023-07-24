using API.Contracts;
using API.DTOs.EmployeeDto;
using API.DTOs.UniversityDto;
using FluentValidation;

namespace API.Utilities.Validation.Universities
{
    public class NewUniversityValidator : AbstractValidator<NewUniversityDto>
    {
        private readonly IUniversityRepository _universityRepository;

        public NewUniversityValidator(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;

            RuleFor(u => u.Code)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(u => u.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
