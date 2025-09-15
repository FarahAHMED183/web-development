using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models.Cart;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {

        builder.Property(ci => ci.Quantity)
            .IsRequired()
            .HasDefaultValue(1);

        builder.HasOne(ci => ci.Product)
            .WithMany() // product doesn’t need to know about cart
            .HasForeignKey(ci => ci.ProductId);
        
        builder.HasQueryFilter(ci => !ci.Product.IsDeleted);

    }
}