using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;

public class EmployeeEducationInfoValidator : AbstractValidator<EmployeeEducationInfo>
{
    public EmployeeEducationInfoValidator()
    {
        RuleFor(x => x.InstituteName).NotEmpty().MaximumLength(250);
        RuleFor(x => x.Major).MaximumLength(50);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}
