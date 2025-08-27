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
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<EmolyeeDto>> AddEmployee([FromBody] EmployeeCreateDto dto, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(dto);
            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var employeeReadDto = _mapper.Map<EmolyeeDto>(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employeeReadDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmolyeeDto>>> GetEmployees(CancellationToken cancellationToken)
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .Include(e => e.Login)
                .ToListAsync(cancellationToken);

            return Ok(_mapper.Map<IEnumerable<EmolyeeDto>>(employees));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmolyeeDto>> GetEmployeeById(int id, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .Include(e => e.Login)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            if (employee == null) return NotFound();
            return Ok(_mapper.Map<EmolyeeDto>(employee));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeCreateDto dto, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (employee == null) return NotFound();

            _mapper.Map(dto, employee);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (employee == null) return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
