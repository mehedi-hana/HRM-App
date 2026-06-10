using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;

public class GenderValidator : AbstractValidator<Gender>
{
    public GenderValidator()
    {
        RuleFor(x => x.GenderName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}
