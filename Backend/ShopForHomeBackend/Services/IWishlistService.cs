// Backend/Services/IWishlistService.cs
using ShopForHomeBackend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public interface IWishlistService
    {
        Task<List<WishlistDto>> GetWishlistAsync(int userId);
        Task AddToWishlistAsync(int userId, int productId);
        Task RemoveFromWishlistAsync(int userId, int productId);
    }
}
