using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    IGenericRepository<Project> projectRepository) : ControllerBase
{
    [HttpGet]
    public IActionResult GetProjects(int pageNumber = 1, int pageSize = 10)
    {
        var projects = projectRepository.GetAll()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return Ok(mapper.Map<IEnumerable<ProjectDto>>(projects));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetProjectById(int id)
    {
        var project = projectRepository.GetById(id);
        if (project == null) return NotFound();

        return Ok(mapper.Map<ProjectDto>(project));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(ProjectDto dto, CancellationToken cancellationToken)
    {
        var project = mapper.Map<Project>(dto);

        projectRepository.Add(project);
        await context.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetProjectById), new { id = project.P_No }, mapper.Map<ProjectDto>(project));
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateProject(int id, ProjectDto dto)
    {
        var project = projectRepository.GetById(id);
        if (project == null) return NotFound();

        mapper.Map(dto, project);
        projectRepository.Update(project);
        context.SaveChanges();

        return Ok(mapper.Map<ProjectDto>(project));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteProject(int id)
    {
        var project = projectRepository.GetById(id);
        if (project == null) return NotFound();

        projectRepository.Delete(project);
        context.SaveChanges();

        return NoContent();
    }
}
