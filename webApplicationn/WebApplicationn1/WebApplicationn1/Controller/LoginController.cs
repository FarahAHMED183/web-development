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
    public class LoginsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LoginsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginDto>>> GetAll(CancellationToken cancellationToken)
        {
            var logins = await _context.Logins.ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<LoginDto>>(logins));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoginDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var login = await _context.Logins.FindAsync(new object[] { id }, cancellationToken);
            if (login == null) return NotFound();

            return Ok(_mapper.Map<LoginDto>(login));
        }

        [HttpPost]
        public async Task<ActionResult<LoginDto>> Create(LoginDto dto, CancellationToken cancellationToken)
        {
            var login = _mapper.Map<Login>(dto);
            _context.Logins.Add(login);
            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = login.Id }, _mapper.Map<LoginDto>(login));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LoginDto dto, CancellationToken cancellationToken)
        {
            var login = await _context.Logins.FindAsync(new object[] { id }, cancellationToken);
            if (login == null) return NotFound();

            _mapper.Map(dto, login);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var login = await _context.Logins.FindAsync(new object[] { id }, cancellationToken);
            if (login == null) return NotFound();

            _context.Logins.Remove(login);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
