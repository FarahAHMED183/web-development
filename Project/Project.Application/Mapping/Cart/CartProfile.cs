using Project.Application.Features.Cart.Commands.Add;
using Project.Application.Features.Cart.Commands.Update;
using Project.Application.Features.Cart.Dtos;
using Project.Domain.Models.Cart;

namespace Project.Application.Mapping.Cart;

public class CartProfile:AutoMapper.Profile
{
    public CartProfile()
    {
        CreateMap<AddToCartCommand, CartItem>();
        CreateMap<UpdateCartCommand, CartItem>();
        CreateMap<CartItem, CartDto>()
            .ForCtorParam("CartId", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("ProductName", opt => opt.MapFrom(src => src.Product.Name))
            .ForCtorParam("Quantity", opt => opt.MapFrom(src => src.Quantity));    }
}