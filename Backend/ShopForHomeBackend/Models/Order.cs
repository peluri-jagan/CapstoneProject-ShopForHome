// Backend/Models/Order.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopForHomeBackend.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
