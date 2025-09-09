// Backend/DTOs/CategoryDto.cs
using System.ComponentModel.DataAnnotations;

namespace ShopForHomeBackend.DTOs
{
    public class CategoryDto
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
