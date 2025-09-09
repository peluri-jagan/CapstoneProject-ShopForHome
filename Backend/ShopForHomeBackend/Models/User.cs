
// Backend/Models/User.cs
using System.ComponentModel.DataAnnotations;

namespace ShopForHomeBackend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required, MaxLength(256)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required, MaxLength(20)]
        public string Role { get; set; } // "User" or "Admin"

        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Coupon> Coupons { get; set; }

    }
}
