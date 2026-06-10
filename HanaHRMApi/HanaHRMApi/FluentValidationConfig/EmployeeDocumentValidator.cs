using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;

public class EmployeeDocumentValidator : AbstractValidator<EmployeeDocument>
{
    public EmployeeDocumentValidator()
    {
        RuleFor(x => x.DocumentName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.FileName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.UploadedFileExtention).MaximumLength(10);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}
