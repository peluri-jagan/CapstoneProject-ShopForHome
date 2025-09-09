// Backend/DTOs/CouponUserAssignDto.cs
using System.Collections.Generic;

namespace ShopForHomeBackend.DTOs
{
    public class CouponUserAssignDto
    {
        public int CouponId { get; set; }
        public List<int> UserIds { get; set; }
    }
}
