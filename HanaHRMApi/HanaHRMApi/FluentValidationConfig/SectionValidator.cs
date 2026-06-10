using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;

public class SectionValidator : AbstractValidator<Section>
{
    public SectionValidator()
    {
        RuleFor(x => x.SectionName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.SectionNameBangla).MaximumLength(100);
        RuleFor(x => x.ShortName).MaximumLength(50);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}
