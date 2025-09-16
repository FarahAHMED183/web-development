using FluentValidation;
using CRUD_Operation.Models;

namespace CRUD_Operation.Validators
{
    public class LearnValidator : AbstractValidator<LearnEntity>
    {
        public LearnValidator()
        {
            RuleFor(x => x.StudentId).GreaterThan(0);
            RuleFor(x => x.CourseId).GreaterThan(0);
        }
    }
}