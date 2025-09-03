using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Models;

namespace WebApplication2.Configurations;

public class ProjectEmployeeConfiguration : IEntityTypeConfiguration<ProjectEmployee>
{
    public void Configure(EntityTypeBuilder<ProjectEmployee> builder)
    {
        builder.HasKey(pe => new { pe.EmployeeId, pe.ProjectId });

        builder.HasOne(pe => pe.Employee)
            .WithMany(e => e.ProjectEmployees)
            .HasForeignKey(pe => pe.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction); 

        builder.HasOne(pe => pe.Project)
            .WithMany(p => p.ProjectEmployees)
            .HasForeignKey(pe => pe.ProjectId)
            .OnDelete(DeleteBehavior.NoAction); 
    }
}