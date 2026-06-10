using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;

public class ReligionValidator : AbstractValidator<Religion>
{
    public ReligionValidator()
    {
        RuleFor(x => x.ReligionName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}
