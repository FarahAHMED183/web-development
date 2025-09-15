using System.Text.Json.Serialization;
using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Categories.Commands.Update;

public record UpdateCategoryCommand(string Name) : ICommand<Guid>

{
    [JsonIgnore]
    public Guid Id { get; set; }
}