using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Models;

namespace WebApplication2.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.D_No);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}