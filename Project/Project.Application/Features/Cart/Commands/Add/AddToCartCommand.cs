using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Cart.Commands.Add;

public record AddToCartCommand(Guid ProductId,int Quantity):ICommand<Guid>;