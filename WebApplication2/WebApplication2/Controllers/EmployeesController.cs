using Microsoft.AspNetCore.Mvc;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.Data;
using WebApplication2.Dto;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUpload _fileUpload;

        public EmployeesController(ApplicationDbContext context, IFileUpload fileUpload)
        {
            _context = context;
            _fileUpload = fileUpload;
        }

        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] EmployeeCreateDto dto)
        {
            if (dto == null) return BadRequest("Employee data is required.");

            var employee = new Employee
            {
                Name = dto.Name,
                Address = dto.Address,
                Dob = dto.Dob,
                Doj = dto.Doj,
                Gender = dto.Gender,
                DepartmentId = dto.DepartmentId
            };

            if (dto.Image != null)
            {
                var path = await _fileUpload.UploadFileAsync(dto.Image, "employees");
                employee.ImagePath = path;
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _context.Employees.ToList();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
