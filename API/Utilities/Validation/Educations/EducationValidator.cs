using API.Contracts;
using API.DTOs.EducationDto;
using FluentValidation;

namespace API.Utilities.Validation.Educations
{
    public class EducationValidator : AbstractValidator<EducationDto>
    {
        private readonly IEducationRepository _educationRepository;

        public EducationValidator(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;

            RuleFor(e => e.Major).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Degree).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Gpa).NotEmpty();
            RuleFor(e => e.UniversityGuid).NotEmpty();
        }
    }
}
