using FluentValidation;
using CRUD_Operation.Models;

namespace CRUD_Operation.Validators
{
    public class CourseValidator : AbstractValidator<CourseEntity>
    {
        public CourseValidator()
        {
            RuleFor(x => x.Cname).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Hours).GreaterThan(0);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
        }
    }
}