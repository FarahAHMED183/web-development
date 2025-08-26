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
    public class EmployeesController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeCreateDto dto, CancellationToken cancellationToken)
        {
            var employee = mapper.Map<Employee>(dto);

            await context.Employees.AddAsync(employee, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var employeeReadDto = mapper.Map<EmolyeeDto>(employee);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employeeReadDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmolyeeDto>>> GetEmployees(CancellationToken cancellationToken)
        {
            var employees = await context.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .Include(e => e.Login)
                .ToListAsync(cancellationToken);

            return mapper.Map<List<EmolyeeDto>>(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmolyeeDto>> GetEmployeeById(int id, CancellationToken cancellationToken)
        {
            var employee = await context.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .Include(e => e.Login)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            if (employee == null) return NotFound();

            return mapper.Map<EmolyeeDto>(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeCreateDto dto, CancellationToken cancellationToken)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (employee == null) return NotFound();

            mapper.Map(dto, employee);

            await context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (employee == null) return NotFound();

            context.Employees.Remove(employee);
            await context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
