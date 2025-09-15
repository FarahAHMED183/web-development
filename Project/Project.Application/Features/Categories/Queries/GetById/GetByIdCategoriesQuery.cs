using Project.Application.Abstractions.Messaging;
using Project.Application.Features.Categories.Dtos;

namespace Project.Application.Features.Categories.Queries.GetById;

public record GetByIdCategoriesQuery(Guid Id): IQuery<CategoryDto>;