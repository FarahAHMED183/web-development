using FluentValidation;

namespace Project.Application.Features.Categories.Commands.Update;

public class UpdateCategoryValidator: AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(c=>c.Id)
            .NotEmpty().WithMessage("Category ID is required.");
    }
}