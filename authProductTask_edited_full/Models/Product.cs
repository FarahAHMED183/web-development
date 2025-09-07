using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Operations.Models
{
    public enum ProductStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        // Relative file path stored in DB (e.g., "uploads/products/abc.jpg")
        [MaxLength(400)]
        public string? ImagePath { get; set; }

        // Derived at read-time via URL resolver, not mapped to DB
        [NotMapped]
        public string? ImageUrl { get; set; }

        public ProductStatus Status { get; set; } = ProductStatus.Pending;

        // Who created it
        [Required]
        public string CreatorId { get; set; } = string.Empty;

        [ForeignKey(nameof(CreatorId))]
        public ApplicationUser? Creator { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ApprovedAt { get; set; }
        public string? ApprovedById { get; set; }
    }
}
