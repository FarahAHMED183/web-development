using FluentValidation;

namespace Project.Application.Features.Products.Commands.Update;

public class UpdateProductValidator: AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(p=>p.Id)
            .NotEmpty().WithMessage("Product ID is required.");
    }
    
}