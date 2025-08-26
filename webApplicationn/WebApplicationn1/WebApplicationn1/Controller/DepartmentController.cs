using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationn1.Data;
using WebApplicationn1.Dto;
using WebApplicationn1.Models;

namespace WebApplicationn1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentDto dto, CancellationToken cancellationToken)
        {
            var department = mapper.Map<Department>(dto);

            await context.Departments.AddAsync(department, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<DepartmentDto>(department);

            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments(CancellationToken cancellationToken)
        {
            var departments = await context.Departments
                .Include(d => d.Employees) 
                .ToListAsync(cancellationToken);

            return mapper.Map<List<DepartmentDto>>(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentById(int id, CancellationToken cancellationToken)
        {
            var department = await context.Departments
                .Include(d => d.Employees) 
                .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

            if (department == null) return NotFound();

            return mapper.Map<DepartmentDto>(department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto dto, CancellationToken cancellationToken)
        {
            var department = await context.Departments.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
            if (department == null) return NotFound();

            mapper.Map(dto, department);
            await context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id, CancellationToken cancellationToken)
        {
            var department = await context.Departments.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
            if (department == null) return NotFound();

            context.Departments.Remove(department);
            await context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
