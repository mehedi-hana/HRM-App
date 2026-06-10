using FluentValidation;
using HanaHRMApi.Models;

namespace HanaHRMApi.FluentValidationConfig;
    public class EducationExaminationValidator : AbstractValidator<EducationExamination>
    {
        public EducationExaminationValidator()
        {
            RuleFor(x => x.ExamName).NotEmpty().MaximumLength(250);
            RuleFor(x => x.CreatedBy).MaximumLength(50);
        }
    }
