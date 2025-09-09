using ShopForHomeBackend.DTOs;
using ShopForHomeBackend.Models;
using ShopForHomeBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public class CouponService : ICouponService
    {
        private readonly AppDbContext _context;

        public CouponService(AppDbContext context)
        {
            _context = context;
        }

        // Get all active coupons
        public async Task<List<CouponDto>> GetActiveCouponsAsync()
        {
            var coupons = await _context.Coupons
                .Where(c => c.IsActive)
                .Select(c => new CouponDto
                {
                    Id = c.Id,
                    Code = c.Code,
                    Discount = c.Discount,
                    ExpiryDate = c.ExpiryDate,
                    IsActive = c.IsActive
                })
                .ToListAsync();

            return coupons;
        }

        // Create a new coupon
        public async Task<CouponDto> CreateCouponAsync(CouponDto couponDto)
        {
            var coupon = new Coupon
            {
                Code = couponDto.Code,
                Discount = couponDto.Discount,
                ExpiryDate = couponDto.ExpiryDate,
                IsActive = true
            };

            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();

            couponDto.Id = coupon.Id;
            couponDto.IsActive = coupon.IsActive;
            return couponDto;
        }

        // Assign a coupon to specific users
        public async Task AssignCouponToUsersAsync(int couponId, List<int> userIds)
        {
            var coupon = await _context.Coupons
                .Include(c => c.AssignedUsers)
                .FirstOrDefaultAsync(c => c.Id == couponId);

            if (coupon == null)
                throw new KeyNotFoundException("Coupon not found");

            // Clear existing assignments or add new users to the coupon
            var users = await _context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();

            foreach (var user in users)
            {
                if (!coupon.AssignedUsers.Contains(user))
                {
                    coupon.AssignedUsers.Add(user);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
