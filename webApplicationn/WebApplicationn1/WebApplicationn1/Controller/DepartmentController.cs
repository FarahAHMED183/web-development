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
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> AddDepartment([FromBody] DepartmentDto dto, CancellationToken cancellationToken)
        {
            var department = _mapper.Map<Department>(dto);
            await _context.Departments.AddAsync(department, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<DepartmentDto>(department);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments(CancellationToken cancellationToken)
        {
            var departments = await _context.Departments
                .Include(d => d.Employees)
                .ToListAsync(cancellationToken);

            return Ok(_mapper.Map<IEnumerable<DepartmentDto>>(departments));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentById(int id, CancellationToken cancellationToken)
        {
            var department = await _context.Departments
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

            if (department == null) return NotFound();
            return Ok(_mapper.Map<DepartmentDto>(department));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto dto, CancellationToken cancellationToken)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
            if (department == null) return NotFound();

            _mapper.Map(dto, department);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id, CancellationToken cancellationToken)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
            if (department == null) return NotFound();

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
