using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;

public class EducationLevelValidator : AbstractValidator<EducationLevel>
{
    public EducationLevelValidator()
    {
        RuleFor(x => x.EducationLevelName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(250);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}
