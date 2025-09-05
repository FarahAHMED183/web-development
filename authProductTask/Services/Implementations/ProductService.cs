using System;
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
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileStorage _storage;
        private readonly IFileUrlResolver _url;

        public ProductService(ApplicationDbContext db, IFileStorage storage, IFileUrlResolver url)
        {
            _db = db;
            _storage = storage;
            _url = url;
        }

        public async Task<ProductDto> CreateAsync(string creatorId, ProductCreateDto dto)
        {
            var path = await _storage.SaveAsync(dto.Image, "uploads/products");
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImagePath = path,
                CreatorId = creatorId,
                Status = ProductStatus.Pending
            };
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            product.ImageUrl = _url.Resolve(product.ImagePath);
            return ToDto(product);
        }

        public async Task<ProductDto?> ApproveAsync(int id, string adminId)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return null;
            product.Status = ProductStatus.Approved;
            product.ApprovedAt = DateTime.UtcNow;
            product.ApprovedById = adminId;
            await _db.SaveChangesAsync();
            product.ImageUrl = _url.Resolve(product.ImagePath);
            return ToDto(product);
        }

        public async Task<ProductDto?> RejectAsync(int id, string adminId)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return null;
            product.Status = ProductStatus.Rejected;
            product.ApprovedAt = DateTime.UtcNow;
            product.ApprovedById = adminId;
            await _db.SaveChangesAsync();
            product.ImageUrl = _url.Resolve(product.ImagePath);
            return ToDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetApprovedAsync()
        {
            var items = await _db
                .Products
                .Where(p => p.Status == ProductStatus.Approved)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            foreach (var p in items)
                p.ImageUrl = _url.Resolve(p.ImagePath);

            return items.Select(ToDto);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var items = await _db
                .Products
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
            foreach (var p in items)
                p.ImageUrl = _url.Resolve(p.ImagePath);
            return items.Select(ToDto);
        }

        private static ProductDto ToDto(Product p) => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            Status = p.Status,
            CreatedAt = p.CreatedAt
        };
    }
}
