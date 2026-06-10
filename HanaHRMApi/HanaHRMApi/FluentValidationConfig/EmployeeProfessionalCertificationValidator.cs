using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;


public class EmployeeProfessionalCertificationValidator : AbstractValidator<EmployeeProfessionalCertification>
{
    public EmployeeProfessionalCertificationValidator()
    {
        RuleFor(x => x.CertificationTitle).NotEmpty().MaximumLength(255);
        RuleFor(x => x.CertificationInstitute).MaximumLength(250);
        RuleFor(x => x.InstituteLocation).MaximumLength(250);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}
