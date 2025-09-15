using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Cart.Dtos;
using Project.Domain.Models.Cart;
using Project.Domain.Responses;

namespace Project.Application.Features.Cart.Queries.GetById;

public class GetByIdCartsHandler 
    : IQueryHandler<GetByIdCartsQuery, CartDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<CartItem> _cartRepository;

    public GetByIdCartsHandler(
        IMapper mapper,
        IRepository<CartItem> cartRepository)
    {
        _mapper = mapper;
        _cartRepository = cartRepository;
    }

    public async Task<Response<CartDto>> Handle(GetByIdCartsQuery request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);

        if (cart == null)
        {
            return Response<CartDto>.Failure("Cart not found.");
        }

        var mappedCart = _mapper.Map<CartDto>(cart);

        return Response<CartDto>.Success(mappedCart);
    }
}