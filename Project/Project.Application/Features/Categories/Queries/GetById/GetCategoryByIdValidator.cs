using FluentValidation;

namespace Project.Application.Features.Categories.Queries.GetById;

public class GetCategoryByIdValidator:AbstractValidator<GetByIdCategoriesQuery>
{
    public GetCategoryByIdValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Category ID is required.");
    }
    
}