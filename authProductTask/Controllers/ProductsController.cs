using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CRUD_Operations.Dtos;
using CRUD_Operations.Services.Interfaces;
using System.Security.Claims;

namespace CRUD_Operations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _products;

        public ProductsController(IProductService products)
        {
            _products = products;
        }

        // productCreator can create products
        [HttpPost]
        [Authorize(Roles = "productCreator")]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _products.CreateAsync(userId, dto);
            return Ok(result);
        }

        [HttpPost("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _products.ApproveAsync(id, adminId);
            if (result == null) return NotFound();
            return Ok(result);
        }

       
        [HttpPost("{id}/reject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(int id)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _products.RejectAsync(id, adminId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // approved only
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetApproved() => Ok(await _products.GetApprovedAsync());

        // all
        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAll() => Ok(await _products.GetAllAsync());
    }
}
