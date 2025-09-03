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
public class ProjectsController(
    ApplicationDbContext context,
    IMapper mapper,
    IGenericRepository<Project> projectRepository
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProject(ProjectCreateDto dto, CancellationToken cancellationToken)
    {
        var project = mapper.Map<Project>(dto);
        projectRepository.Add(project);
        await context.SaveChangesAsync(cancellationToken);
        return Ok("Project created successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects(int pageNumber = 1, int pageSize = 10)
    {
        var query = projectRepository.GetQueryable()
            .Include(p => p.Department);

        var items = await query
            .OrderBy(p => p.P_No)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var projects = mapper.Map<IEnumerable<ProjectReadDto>>(items);
        return Ok(projects);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProject(int id, ProjectCreateDto dto, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetQueryable().FirstOrDefaultAsync(p => p.P_No == id, cancellationToken);
        if (project == null) return NotFound();

        mapper.Map(dto, project);

        projectRepository.Update(project);
        await context.SaveChangesAsync(cancellationToken);

        return Ok("Project updated successfully");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProject(int id, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetQueryable().FirstOrDefaultAsync(p => p.P_No == id, cancellationToken);
        if (project == null) return NotFound();

        projectRepository.Delete(project);
        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
