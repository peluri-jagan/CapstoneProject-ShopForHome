// Backend/Services/ICouponService.cs
using ShopForHomeBackend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public interface ICouponService
    {
        Task<List<CouponDto>> GetActiveCouponsAsync();
        Task<CouponDto> CreateCouponAsync(CouponDto couponDto);
        Task AssignCouponToUsersAsync(int couponId, List<int> userIds);
    }
}
