using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Cart;
using Project.Domain.Responses;

namespace Project.Application.Features.Cart.Commands.Delete;

public class DeleteCartCommandHandler(IRepository<CartItem> cartRepository):ICommandHandler<DeleteCartCommand,Guid>
{
    public async Task<Response<Guid>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByIdAsync(request.Id, cancellationToken);
        if (cart is null)
        {
            return Response<Guid>.NotFound("Cart Item not found.");
        }

        await cartRepository.DeleteAsync(cart, cancellationToken);
        return Response<Guid>.Success(request.Id);
    }
}