using FluentValidation;

namespace Project.Application.Features.Cart.Commands.Update;

public class UpdateCartValidator:AbstractValidator<UpdateCartCommand>
{
    public UpdateCartValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("cart cant be empty");
    }
}