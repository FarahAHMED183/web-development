using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    IGenericRepository<Dependent> dependentRepository) : ControllerBase
{
    [HttpGet]
    public IActionResult GetDependents(int pageNumber = 1, int pageSize = 10)
    {
        var dependents = dependentRepository.GetAll()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return Ok(mapper.Map<IEnumerable<DependentDto>>(dependents));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetDependentById(int id)
    {
        var dependent = dependentRepository.GetById(id);
        if (dependent == null) return NotFound();

        return Ok(mapper.Map<DependentDto>(dependent));
    }

    [HttpPost]
    public async Task<IActionResult> CreateDependent(DependentDto dto, CancellationToken cancellationToken)
    {
        var dependent = mapper.Map<Dependent>(dto);

        dependentRepository.Add(dependent);
        await context.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetDependentById), new { id = dependent.Id }, mapper.Map<DependentDto>(dependent));
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateDependent(int id, DependentDto dto)
    {
        var dependent = dependentRepository.GetById(id);
        if (dependent == null) return NotFound();

        mapper.Map(dto, dependent);
        dependentRepository.Update(dependent);
        context.SaveChanges();

        return Ok(mapper.Map<DependentDto>(dependent));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteDependent(int id)
    {
        var dependent = dependentRepository.GetById(id);
        if (dependent == null) return NotFound();

        dependentRepository.Delete(dependent);
        context.SaveChanges();

        return NoContent();
    }
}
