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
public class DependentsController(
    ApplicationDbContext context,
    IMapper mapper,
    IGenericRepository<Dependent> dependentRepository
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDependent(DependentCreateDto dependentDto, CancellationToken cancellationToken)
    {
        var dependent = mapper.Map<Dependent>(dependentDto);
        dependentRepository.Add(dependent);
        await context.SaveChangesAsync(cancellationToken);
        return Ok("Dependent created successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetDependents(int pageNumber = 1, int pageSize = 10)
    {
        var query = dependentRepository.GetQueryable()
            .Include(d => d.Employee);

        var items = await query.OrderBy(d => d.EmployeeId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var dependentDtos = mapper.Map<IEnumerable<DepartmentReadDto>>(items);
        return Ok(dependentDtos);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateDependent(int id, DependentCreateDto updatedDto, CancellationToken cancellationToken)
    {
        var dependent = await dependentRepository.GetQueryable().FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        if (dependent == null) return NotFound();

        mapper.Map(updatedDto, dependent);
        dependentRepository.Update(dependent);
        await context.SaveChangesAsync(cancellationToken);

        return Ok("Dependent updated successfully");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteDependent(int id, CancellationToken cancellationToken)
    {
        var dependent = await dependentRepository.GetQueryable().FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        if (dependent == null) return NotFound();

        dependentRepository.Delete(dependent);
        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
