using API.Contracts;
using API.DTOs.EducationDto;
using FluentValidation;

namespace API.Utilities.Validation.Educations
{
    public class NewEducationValidator : AbstractValidator<NewEducationDto>
    {
        private readonly IEducationRepository _educationRepository;

        public NewEducationValidator(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;

            RuleFor(e => e.Major).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Degree).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Gpa).NotEmpty();
            RuleFor(e => e.UniversityGuid).NotEmpty();
        }
    }
}
