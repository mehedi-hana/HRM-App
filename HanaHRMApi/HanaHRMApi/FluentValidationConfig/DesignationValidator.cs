using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;
    public class DesignationValidator : AbstractValidator<Designation>
    {
        public DesignationValidator()
        {
            RuleFor(x => x.DesignationName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.DesignationNameBangla).MaximumLength(100);
            RuleFor(x => x.CreatedBy).MaximumLength(50);
        }
    }
