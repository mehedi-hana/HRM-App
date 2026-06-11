using FluentValidation;
using HanaHRMApi.DTOs.Employee;

namespace HanaHRMApi.FluentValidationConfig;

public class EmployeeValidator : AbstractValidator<EmployeeDto>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.EmployeeName).NotEmpty().MaximumLength(250);
        RuleFor(x => x.EmployeeNameBangla).MaximumLength(250);
        RuleFor(x => x.FatherName).MaximumLength(250);
        RuleFor(x => x.MotherName).MaximumLength(250);
        RuleFor(x => x.ContactNo).MaximumLength(250);
        RuleFor(x => x.NationalIdentificationNumber).MaximumLength(30);
        RuleFor(x => x.Address).MaximumLength(250);
        RuleFor(x => x.PresentAddress).MaximumLength(250);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}