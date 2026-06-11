using FluentValidation;
using HanaHRMApi.DTOs.Employee;

namespace HanaHRMApi.FluentValidationConfig;


public class EmployeeProfessionalCertificationValidator : AbstractValidator<EmployeeProfessionalCertificationDto>
{
    public EmployeeProfessionalCertificationValidator()
    {
        RuleFor(x => x.CertificationTitle).NotEmpty().MaximumLength(255);
        RuleFor(x => x.CertificationInstitute).MaximumLength(250);
        RuleFor(x => x.InstituteLocation).MaximumLength(250);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}
