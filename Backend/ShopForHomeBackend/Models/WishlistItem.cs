// Backend/Models/WishlistItem.cs
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopForHomeBackend.Models
{
    public class WishlistItem
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
