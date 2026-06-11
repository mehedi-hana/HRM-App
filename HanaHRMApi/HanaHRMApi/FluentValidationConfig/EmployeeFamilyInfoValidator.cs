using FluentValidation;
using HanaHRMApi.DTOs.Employee;

namespace HanaHRMApi.FluentValidationConfig;


public class EmployeeFamilyInfoValidator : AbstractValidator<EmployeeFamilyInfoDto>
{
    public EmployeeFamilyInfoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.ContactNo).MaximumLength(50);
        RuleFor(x => x.CurrentAddress).MaximumLength(500);
        RuleFor(x => x.PermanentAddress).MaximumLength(500);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}
