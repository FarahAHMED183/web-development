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
public class ProjectEmployeesController(
    ApplicationDbContext context,
    IMapper mapper,
    IGenericRepository<ProjectEmployee> repository
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProjectEmployee(ProjectEmployeeDto dto, CancellationToken cancellationToken)
    {
        var projectEmployee = mapper.Map<ProjectEmployee>(dto);
        repository.Add(projectEmployee);
        await context.SaveChangesAsync(cancellationToken);
        return Ok("ProjectEmployee created successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetProjectEmployees(int pageNumber = 1, int pageSize = 10)
    {
        var query = repository.GetQueryable()
            .Include(ep => ep.Employee)
            .Include(ep => ep.Project);

        var items = await query.OrderBy(ep => ep.EmployeeId)
            .ThenBy(ep => ep.ProjectId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var dtos = mapper.Map<IEnumerable<ProjectEmployeeRead>>(items);
        return Ok(dtos);
    }

    [HttpDelete("{employeeId:int}/{projectId:int}")]
    public async Task<IActionResult> DeleteProjectEmployee(int employeeId, int projectId, CancellationToken cancellationToken)
    {
        var entity = await repository.GetQueryable()
            .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId && ep.ProjectId == projectId, cancellationToken);

        if (entity == null) return NotFound();

        repository.Delete(entity);
        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpPut("{employeeId:int}/{projectId:int}")]
    public async Task<IActionResult> UpdateProjectEmployee(int employeeId, int projectId, ProjectEmployeeDto dto, CancellationToken cancellationToken)
    {
        var entity = await repository.GetQueryable()
            .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId && ep.ProjectId == projectId, cancellationToken);

        if (entity == null) return NotFound();

        mapper.Map(dto, entity);

        repository.Update(entity);
        await context.SaveChangesAsync(cancellationToken);

        return Ok("ProjectEmployee updated successfully");
    }
}
