using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Categories.Dtos;
using Project.Domain.Models.Categories;
using Project.Domain.Responses;

namespace Project.Application.Features.Categories.Queries.GetById;

public class GetByIdCategoriesQueryHandler 
    : IQueryHandler<GetByIdCategoriesQuery, CategoryDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Category> _categoryRepository;

    public GetByIdCategoriesQueryHandler(
        IMapper mapper,
        IRepository<Category> categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<Response<CategoryDto>> Handle(GetByIdCategoriesQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (category == null)
        {
            return Response<CategoryDto>.Failure("Category not found.");
        }

        var mappedCategory = _mapper.Map<CategoryDto>(category);

        return Response<CategoryDto>.Success(mappedCategory);
    }
}