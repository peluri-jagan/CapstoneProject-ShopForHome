// Backend/Controllers/CartController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHomeBackend.DTOs;
using ShopForHomeBackend.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue("id"));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetCartItems()
        {
            var userId = GetUserId();
            var items = await _cartService.GetCartItemsAsync(userId);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartDto cartDto)
        {
            var userId = GetUserId();
            await _cartService.AddToCartAsync(userId, cartDto.ProductId, cartDto.Quantity);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCart([FromBody] CartDto cartDto)
        {
            var userId = GetUserId();
            await _cartService.UpdateCartAsync(userId, cartDto.ProductId, cartDto.Quantity);
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = GetUserId();
            await _cartService.RemoveFromCartAsync(userId, productId);
            return Ok();
        }
    }
}
