using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Cart;
using Project.Domain.Responses;

namespace Project.Application.Features.Cart.Commands.Update;

public class UpdateCartCommandHandler(IMapper mapper,IRepository<CartItem> cartRepository):ICommandHandler<UpdateCartCommand,Guid>
{
    public async Task<Response<Guid>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByIdAsync(request.Id, cancellationToken);
        if (cart is null)
        {
            return Response<Guid>.NotFound("Category not found.");
        }
        
        mapper.Map(request, cart);
        await cartRepository.UpdateAsync(cart, cancellationToken);

        return Response<Guid>.Success(cart.Id);
    }}