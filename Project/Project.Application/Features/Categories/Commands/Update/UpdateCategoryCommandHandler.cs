using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Categories;
using Project.Domain.Responses;

namespace Project.Application.Features.Categories.Commands.Update;

public class UpdateCategoryCommandHandler(IMapper mapper,IRepository<Category> categoryRepository):ICommandHandler<UpdateCategoryCommand, Guid>
{
    public async Task<Response<Guid>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
        {
            return Response<Guid>.NotFound("Category not found.");
        }
        
        mapper.Map(request, category);
        await categoryRepository.UpdateAsync(category, cancellationToken);

        return Response<Guid>.Success(category.Id);
    }
}