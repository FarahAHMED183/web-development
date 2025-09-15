using FluentValidation;

namespace Project.Application.Features.Cart.Commands.Delete;

public class DeleteCartValidator:AbstractValidator<DeleteCartCommand>
{
    public DeleteCartValidator()
    {
        RuleFor(c=>c.Id).NotEmpty().
            WithMessage("Cart Item ID is required.");
    }
}