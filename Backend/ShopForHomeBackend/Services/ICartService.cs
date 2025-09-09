// Backend/Services/ICartService.cs
using ShopForHomeBackend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartDto>> GetCartItemsAsync(int userId);
        Task AddToCartAsync(int userId, int productId, int quantity);
        Task UpdateCartAsync(int userId, int productId, int quantity);
        Task RemoveFromCartAsync(int userId, int productId);
    }
}
