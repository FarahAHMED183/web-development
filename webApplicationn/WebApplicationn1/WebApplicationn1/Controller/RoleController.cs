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
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RolesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll(CancellationToken cancellationToken)
        {
            var roles = await _context.Roles.ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<RoleDto>>(roles));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FindAsync(new object[] { id }, cancellationToken);
            if (role == null) return NotFound();

            return Ok(_mapper.Map<RoleDto>(role));
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> Create(RoleDto dto, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<Role>(dto);
            _context.Roles.Add(role);
            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = role.Id }, _mapper.Map<RoleDto>(role));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RoleDto dto, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FindAsync(new object[] { id }, cancellationToken);
            if (role == null) return NotFound();

            _mapper.Map(dto, role);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FindAsync(new object[] { id }, cancellationToken);
            if (role == null) return NotFound();

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
