// Backend/Controllers/CouponsController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHomeBackend.DTOs;
using ShopForHomeBackend.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponsController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<CouponDto>>> GetCoupons()
        {
            var coupons = await _couponService.GetActiveCouponsAsync();
            return Ok(coupons);
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CouponDto couponDto)
        {
            var created = await _couponService.CreateCouponAsync(couponDto);
            return CreatedAtAction(nameof(GetCoupons), new { id = created.Id }, created);
        }

        [Authorize(Roles="Admin")]
        [HttpPost("assign")]
        public async Task<IActionResult> AssignCouponToUsers([FromBody] CouponUserAssignDto assignDto)
        {
            await _couponService.AssignCouponToUsersAsync(assignDto.CouponId, assignDto.UserIds);
            return Ok();
        }
    }
}
