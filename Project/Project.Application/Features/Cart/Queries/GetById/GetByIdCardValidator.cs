using FluentValidation;

namespace Project.Application.Features.Cart.Queries.GetById;

public class GetByIdCardValidator:AbstractValidator<GetByIdCartsQuery>
{
    public GetByIdCardValidator()
    {
        RuleFor(c => c.Id).NotEmpty()
            .WithMessage("Cart ID is required. ");
    }
}