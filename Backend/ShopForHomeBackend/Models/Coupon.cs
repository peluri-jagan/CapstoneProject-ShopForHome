// Backend/Models/Coupon.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopForHomeBackend.Models
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Code { get; set; }

        public decimal DiscountAmount { get; set; } // fixed amount discount

        public double DiscountPercentage { get; set; } // or percentage discount

        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
         public decimal Discount { get; set; }
         public DateTime ExpiryDate { get; set; }

        public ICollection<User> AssignedUsers { get; set; } // Many-to-many

        public bool IsActive { get; set; } = true;
    }
}
