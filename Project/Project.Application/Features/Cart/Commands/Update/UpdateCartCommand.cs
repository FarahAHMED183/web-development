using System.Text.Json.Serialization;
using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Cart.Commands.Update;

public record UpdateCartCommand(Guid ProductId, int Quantity) : ICommand<Guid>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}