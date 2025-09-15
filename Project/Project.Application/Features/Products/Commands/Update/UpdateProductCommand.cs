using System.Text.Json.Serialization;
using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Products.Commands.Update;

public record UpdateProductCommand(string Name) : ICommand<Guid>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}
