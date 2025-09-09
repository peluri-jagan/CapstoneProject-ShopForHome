// Backend/Controllers/WishlistController.cs
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
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue("id"));
        }

        [HttpGet]
        public async Task<ActionResult<List<WishlistDto>>> GetWishlist()
        {
            var userId = GetUserId();
            var items = await _wishlistService.GetWishlistAsync(userId);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist([FromBody] WishlistDto wishlistDto)
        {
            var userId = GetUserId();
            await _wishlistService.AddToWishlistAsync(userId, wishlistDto.ProductId);
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveFromWishlist(int productId)
        {
            var userId = GetUserId();
            await _wishlistService.RemoveFromWishlistAsync(userId, productId);
            return Ok();
        }
    }
}
