using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment([FromBody] DepartmentCreateDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = department.D_No }, department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentCreateDto departmentDto)
        {
            var existingDepartment = await _context.Departments.FindAsync(id);
            if (existingDepartment == null)
            {
                return NotFound();
            }

            _mapper.Map(departmentDto, existingDepartment);

            _context.Entry(existingDepartment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
