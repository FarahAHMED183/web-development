using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Models;

namespace WebApplication2.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Address).IsRequired().HasMaxLength(250);
        builder.Property(e => e.Gender).IsRequired().HasMaxLength(10);

        builder
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(e => e.Projects)
            .WithMany(p => p.Employees)
            .UsingEntity<Dictionary<string, object>>(
                "EmployeeProject",
                j => j
                    .HasOne<Project>()
                    .WithMany()
                    .HasForeignKey("ProjectsP_No")
                    .OnDelete(DeleteBehavior.NoAction), 
                j => j
                    .HasOne<Employee>()
                    .WithMany()
                    .HasForeignKey("EmployeesId")
                    .OnDelete(DeleteBehavior.Cascade) 
            );
    }
}