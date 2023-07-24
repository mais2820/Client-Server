using API.Contracts;
using API.DTOs.UniversityDto;
using FluentValidation;

namespace API.Utilities.Validation.Universities
{
    public class UniversityValidator : AbstractValidator<UniversityDto>
    {
        private readonly IUniversityRepository _universityRepository;

        public UniversityValidator(IUniversityRepository universityRepository)
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
