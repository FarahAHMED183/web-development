using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Cart;
using Project.Domain.Responses;

namespace Project.Application.Features.Cart.Commands.Add;

public class AddToCartCommandHandler(IMapper mapper,IRepository<CartItem> cartRepository):ICommandHandler<AddToCartCommand,Guid>
{
    public async Task<Response<Guid>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var cart = mapper.Map<CartItem>(request);
        await cartRepository.AddAsync(cart, cancellationToken);
        return Response<Guid>.Created(cart.Id);
    }
}