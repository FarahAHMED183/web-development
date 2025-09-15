using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Cart.Commands.Delete;

public record DeleteCartCommand(Guid Id):ICommand<Guid>;