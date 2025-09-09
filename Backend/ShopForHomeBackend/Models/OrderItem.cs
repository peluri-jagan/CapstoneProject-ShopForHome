// Backend/Models/OrderItem.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopForHomeBackend.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName="decimal(10,2)")]
        public decimal UnitPrice { get; set; }
    }
}
