using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Cart.Dtos;
using Project.Application.Features.Cart.Queries.GetAll;
using Project.Application.Features.Cart.Queries;
using Project.Application.Features.Categories.Dtos;
using Project.Application.Features.Categories.Queries.GetAll;
using Project.Application.Features.Categories.Specifications;
using Project.Domain.Models.Cart;
using Project.Domain.Responses;

namespace Project.Application.Features.Cart.Queries.GetAll;

public class GetAllCartsQueryHandler(IMapper mapper,IReadRepository<CartItem> cartRepository):
    IQueryHandler<GetAllCarts,PaginatedResult<CartDto>>
{
    public async Task<Response<PaginatedResult<CartDto>>> Handle(GetAllCarts request, CancellationToken cancellationToken)
    {
        var carts = await cartRepository
            .ListAsync(new CartsSpec(request.Name,
                request.PageSize,
                request.PageNumber), cancellationToken);
        
        var cartsCount = await cartRepository
            .CountAsync(new CartsSpec(request.Name,
                request.PageSize,
                request.PageNumber), cancellationToken);
        var mappedCarts = mapper.Map<IEnumerable<CartDto>>(carts);
        return Response<CartDto>.GetData(mappedCarts, request.PageNumber, request.PageSize, cartsCount);
    }}