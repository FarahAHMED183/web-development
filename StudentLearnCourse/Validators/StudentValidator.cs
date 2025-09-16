using FluentValidation;
using CRUD_Operation.Models;

namespace CRUD_Operation.Validators
{
    public class StudentValidator : AbstractValidator<StudentEntity>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Sname).NotEmpty().MaximumLength(200);
            RuleFor(x => x.SID).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Age).GreaterThan(0);
        }
    }
}