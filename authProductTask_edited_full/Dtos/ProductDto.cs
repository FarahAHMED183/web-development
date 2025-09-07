using CRUD_Operations.Models;
using System;

namespace CRUD_Operations.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
