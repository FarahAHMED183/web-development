using Ardalis.Specification;
using Project.Domain.Models.Cart;

public class CartsSpec : Specification<CartItem>
{
    public CartsSpec(string? name, int pageSize, int pageNumber)
    {
        Query.Include(x => x.Product); 

        if (!string.IsNullOrWhiteSpace(name))
        {
            Query.Where(x => x.Product.Name.Contains(name));
        }

        Query.Skip(pageSize * (pageNumber - 1));
        Query.Take(pageSize);
    }
}