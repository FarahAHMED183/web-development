using System.Collections.Generic;
using System.Threading.Tasks;
using CRUD_Operations.Dtos;
using CRUD_Operations.Models;

namespace CRUD_Operations.Services.Interfaces
{
    public interface ICartService
    {
        Task AddAsync(string userId, AddToCartDto dto);
        Task<IEnumerable<(CartItem item, Product product)>> GetMyCartAsync(string userId);
    }
}
