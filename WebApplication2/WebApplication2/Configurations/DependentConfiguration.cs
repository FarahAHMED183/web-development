using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Models;

namespace WebApplication2.Configurations;

public class DependentConfiguration : IEntityTypeConfiguration<Dependent>
{
    public void Configure(EntityTypeBuilder<Dependent> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.D_Name).IsRequired();
        builder.Property(d => d.Gender).IsRequired();
        builder.Property(d => d.Relationship).IsRequired();

        // Dependent ↔ Employee
        builder.HasOne(d => d.Employee)
            .WithMany(e => e.Dependents)
            .HasForeignKey(d => d.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}