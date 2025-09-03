using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Models;

namespace WebApplication2.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.P_No);

        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Location).IsRequired();

        // Project ↔ Department
        builder.HasOne(p => p.Department)
            .WithMany(d => d.Projects)
            .HasForeignKey(p => p.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Project ↔ ProjectEmployees
        builder.HasMany(p => p.ProjectEmployees)
            .WithOne(pe => pe.Project)
            .HasForeignKey(pe => pe.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}