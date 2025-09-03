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
public class EmployeesController(
    ApplicationDbContext context,
    IMapper mapper,
    IGenericRepository<Employee> employeeRepository
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromForm] EmployeeCreateDto dto, CancellationToken cancellationToken)
    {
        var employee = mapper.Map<Employee>(dto);

        if (dto.Image != null)
        {
            var fileName = $"{Guid.NewGuid()}_{dto.Image.FileName}";
            var filePath = Path.Combine("wwwroot/images", fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await dto.Image.CopyToAsync(stream, cancellationToken);

            employee.ImagePath = $"/images/{fileName}";
        }

        employeeRepository.Add(employee);
        await context.SaveChangesAsync(cancellationToken);

        return Ok("Employee created successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees(int pageNumber = 1, int pageSize = 10)
    {
        var query = employeeRepository.GetQueryable()
            .Include(e => e.Department);

        var items = await query
            .OrderBy(e => e.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var employees = mapper.Map<IEnumerable<EmployeeDto>>(items);
        return Ok(employees);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromForm] EmployeeCreateDto dto, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetQueryable().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (employee == null) return NotFound();

        mapper.Map(dto, employee);

        if (dto.Image != null)
        {
            var fileName = $"{Guid.NewGuid()}_{dto.Image.FileName}";
            var filePath = Path.Combine("wwwroot/images", fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await dto.Image.CopyToAsync(stream, cancellationToken);

            employee.ImagePath = $"/images/{fileName}";
        }

        employeeRepository.Update(employee);
        await context.SaveChangesAsync(cancellationToken);

        return Ok("Employee updated successfully");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetQueryable().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (employee == null) return NotFound();

        employeeRepository.Delete(employee);
        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
