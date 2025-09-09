// Backend/DTOs/CouponDto.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace ShopForHomeBackend.DTOs
{
    public class CouponDto
    {
        public int? Id { get; set; }

        [Required]
        public string Code { get; set; }

        public decimal DiscountAmount { get; set; }

        public double DiscountPercentage { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public bool IsActive { get; set; }
        public decimal Discount { get; set; }
         public DateTime ExpiryDate { get; set; }
    }
}
