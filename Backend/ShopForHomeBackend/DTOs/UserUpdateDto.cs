// Backend/DTOs/UserUpdateDto.cs
using System.ComponentModel.DataAnnotations;

namespace ShopForHomeBackend.DTOs
{
    public class UserUpdateDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; } // "User" or "Admin"
    }
}
