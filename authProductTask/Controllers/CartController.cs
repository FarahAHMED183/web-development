using System.Linq;
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
    public class CartController : ControllerBase
    {
        private readonly ICartService _cart;
        private readonly IFileUrlResolver _url;

        public CartController(ICartService cart, IFileUrlResolver url)
        {
            _cart = cart;
            _url = url;
        }

        [HttpPost("items")]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> Add(AddToCartDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _cart.AddAsync(userId, dto);
            return Ok(new { message = "Added to cart" });
        }

        [HttpGet("items")]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> MyCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var items = await _cart.GetMyCartAsync(userId);
            var result = items.Select(x => new {
                x.item.Id,
               
                x.item.Quantity,
                Product = new {
                    x.product.Id,
                    x.product.Name,
                    x.product.Price,
                    ImageUrl = _url.Resolve(x.product.ImagePath)
                }
            });
            return Ok(result);
        }
    }
}
