using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;


public class EmployeeFamilyInfoValidator : AbstractValidator<EmployeeFamilyInfo>
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
