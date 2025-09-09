// Backend/Models/Category.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopForHomeBackend.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
