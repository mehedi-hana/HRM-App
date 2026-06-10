using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;
    public class WeekOffValidator : AbstractValidator<WeekOff>
    {
        public WeekOffValidator()
        {
            RuleFor(x => x.WeekOffDay).NotEmpty().MaximumLength(3);
            RuleFor(x => x.CreatedBy).MaximumLength(50);
        }
    }





























