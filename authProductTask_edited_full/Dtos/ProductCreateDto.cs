using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Operations.Dtos
{
    public class ProductCreateDto
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        // Optional image upload
        public IFormFile? Image { get; set; }
    }
}
