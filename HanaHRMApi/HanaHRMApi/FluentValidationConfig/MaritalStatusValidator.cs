using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;


public class MaritalStatusValidator : AbstractValidator<MaritalStatus>
{
    public MaritalStatusValidator()
    {
        RuleFor(x => x.MaritalStatusName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}
