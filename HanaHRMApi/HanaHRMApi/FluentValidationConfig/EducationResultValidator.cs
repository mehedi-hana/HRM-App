using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;

public class EducationResultValidator : AbstractValidator<EducationResult>
{
    public EducationResultValidator()
    {
        RuleFor(x => x.ResultName).NotEmpty().MaximumLength(250);
        RuleFor(x => x.Description).MaximumLength(250);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}
