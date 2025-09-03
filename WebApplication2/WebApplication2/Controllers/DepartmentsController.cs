using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.interfaces;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController(
    ApplicationDbContext context,
    IMapper mapper,
    IGenericRepository<Department> departmentRepository
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDepartment(DepartmentCreateDto dto, CancellationToken cancellationToken)
    {
        var department = mapper.Map<Department>(dto);
        departmentRepository.Add(department);
        await context.SaveChangesAsync(cancellationToken);
        return Ok("Department created successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartments(int pageNumber = 1, int pageSize = 10)
    {
        var query = departmentRepository.GetQueryable();

        var items = await query
            .OrderBy(d => d.D_No)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var departments = mapper.Map<IEnumerable<DepartmentReadDto>>(items);
        return Ok(departments);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateDepartment(int id, DepartmentCreateDto dto, CancellationToken cancellationToken)
    {
        var department = await departmentRepository.GetQueryable().FirstOrDefaultAsync(d => d.D_No == id, cancellationToken);
        if (department == null) return NotFound();

        mapper.Map(dto, department);

        departmentRepository.Update(department);
        await context.SaveChangesAsync(cancellationToken);

        return Ok("Department updated successfully");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteDepartment(int id, CancellationToken cancellationToken)
    {
        var department = await departmentRepository.GetQueryable().FirstOrDefaultAsync(d => d.D_No == id, cancellationToken);
        if (department == null) return NotFound();

        departmentRepository.Delete(department);
        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
