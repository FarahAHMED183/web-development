using System.Collections.Generic;
using System.Threading.Tasks;
using CRUD_Operations.Dtos;
using CRUD_Operations.Models;

namespace CRUD_Operations.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateAsync(string creatorId, ProductCreateDto dto);
        Task<ProductDto?> ApproveAsync(int id, string adminId);
        Task<ProductDto?> RejectAsync(int id, string adminId);
        Task<IEnumerable<ProductDto>> GetApprovedAsync();
        Task<IEnumerable<ProductDto>> GetAllAsync(); 
    }
}
