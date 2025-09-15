using Project.Domain.Models.Base;
using Project.Domain.Models.Products;

namespace Project.Domain.Models.Cart;

public class CartItem:Entity
{
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }

    public int Quantity { get; set; }

   
}