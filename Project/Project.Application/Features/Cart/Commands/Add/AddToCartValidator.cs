using FluentValidation;

namespace Project.Application.Features.Cart.Commands.Add;

public class AddToCartValidator:AbstractValidator<AddToCartCommand>
{
    public AddToCartValidator()
    {
        RuleFor(c => c.Quantity)
            .NotEmpty().WithMessage("Quantity can't be zero");

    }
}