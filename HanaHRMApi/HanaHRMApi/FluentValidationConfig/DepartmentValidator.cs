using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;
    public class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.DepartName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.DepartNameBangla).MaximumLength(100);
            RuleFor(x => x.CreatedBy).MaximumLength(50);
        }
    }
