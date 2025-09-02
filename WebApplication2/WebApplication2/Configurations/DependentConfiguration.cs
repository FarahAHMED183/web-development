using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Models;

namespace WebApplication2.Configurations
{
    public class DependentConfiguration : IEntityTypeConfiguration<Dependent>
    {
        public void Configure(EntityTypeBuilder<Dependent> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.D_Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Gender)
                .HasMaxLength(10);

            builder.Property(d => d.Relationship)
                .HasMaxLength(50);

            builder.HasOne(d => d.Employee)
                .WithMany(e => e.Dependents)
                .HasForeignKey(d => d.EmployeeId);
        }
    }
}