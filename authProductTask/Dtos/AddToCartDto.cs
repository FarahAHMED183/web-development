using System.ComponentModel.DataAnnotations;

namespace CRUD_Operations.Dtos
{
    public class AddToCartDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;
    }
}
