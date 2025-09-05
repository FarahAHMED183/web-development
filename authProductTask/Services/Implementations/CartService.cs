using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Operations.Data;
using CRUD_Operations.Dtos;
using CRUD_Operations.Models;
using CRUD_Operations.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operations.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _db;

        public CartService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(string userId, AddToCartDto dto)
        {
            // Only approved products can be added
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == dto.ProductId && p.Status == ProductStatus.Approved);
            if (product == null)
                throw new System.Exception("Product not found or not approved.");

            var existing = await _db.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == dto.ProductId);
            if (existing != null)
            {
                existing.Quantity += dto.Quantity;
            }
            else
            {
                _db.CartItems.Add(new CartItem
                {
                    UserId = userId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                });
            }
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<(CartItem item, Product product)>> GetMyCartAsync(string userId)
        {
            var items = await _db.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            return items.Select(i => (i, i.Product!));
        }
    }
}
