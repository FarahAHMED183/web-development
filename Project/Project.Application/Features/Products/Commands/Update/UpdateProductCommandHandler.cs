using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Products;
using Project.Domain.Responses;

namespace Project.Application.Features.Products.Commands.Update;

public class DeleteProductCommandHandler(IMapper mapper,IRepository<Product> productRepository) :
    ICommandHandler<UpdateProductCommand, Guid>
    
{
    
    public async Task<Response<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            return Response<Guid>.NotFound("Product not found.");
        }
        
        mapper.Map(request, product);
        await productRepository.UpdateAsync(product, cancellationToken);

        return Response<Guid>.Success(product.Id);
    }
}