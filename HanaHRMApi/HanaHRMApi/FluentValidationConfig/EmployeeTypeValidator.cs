using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;

public class EmployeeTypeValidator : AbstractValidator<EmployeeType>
{
    public EmployeeTypeValidator()
    {
        RuleFor(x => x.TypeName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}
