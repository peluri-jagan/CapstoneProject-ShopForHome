// Backend/DTOs/ProductDto.cs
using System.ComponentModel.DataAnnotations;

namespace ShopForHomeBackend.DTOs
{
    public class ProductDto
    {
        public int? Id { get; set; } // Nullable for creation

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public double Rating { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
