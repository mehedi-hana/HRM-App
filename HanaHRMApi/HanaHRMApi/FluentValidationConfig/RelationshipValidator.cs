using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;

public class RelationshipValidator : AbstractValidator<Relationship>
{
    public RelationshipValidator()
    {
        RuleFor(x => x.RelationName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}
