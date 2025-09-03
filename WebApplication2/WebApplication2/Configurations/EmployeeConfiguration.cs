using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Models;

namespace WebApplication2.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.Gender).IsRequired();
        builder.Property(e => e.Address).IsRequired();

        // Employee ↔ Department
        builder.HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Employee ↔ Dependents
        builder.HasMany(e => e.Dependents)
            .WithOne(d => d.Employee)
            .HasForeignKey(d => d.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Employee ↔ ProjectEmployees
        builder.HasMany(e => e.ProjectEmployees)
            .WithOne(pe => pe.Employee)
            .HasForeignKey(pe => pe.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}