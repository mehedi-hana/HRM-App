using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;

public class JobTypeValidator : AbstractValidator<JobType>
{
    public JobTypeValidator()
    {
        RuleFor(x => x.JobTypeName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.JobTypeBanglaName).MaximumLength(50);
        RuleFor(x => x.CreatedBy).MaximumLength(50);
    }
}
