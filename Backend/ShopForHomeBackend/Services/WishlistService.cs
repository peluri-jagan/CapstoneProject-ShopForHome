// Backend/Services/WishlistService.cs
using Microsoft.EntityFrameworkCore;
using ShopForHomeBackend.Data;
using ShopForHomeBackend.DTOs;
using ShopForHomeBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly AppDbContext _context;
        public WishlistService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<WishlistDto>> GetWishlistAsync(int userId)
        {
            return await _context.WishlistItems
                .Where(w => w.UserId == userId)
                .Select(w => new WishlistDto { ProductId = w.ProductId })
                .ToListAsync();
        }

        public async Task AddToWishlistAsync(int userId, int productId)
        {
            var exists = await _context.WishlistItems.FindAsync(userId, productId);
            if (exists == null)
            {
                _context.WishlistItems.Add(new WishlistItem
                {
                    UserId = userId,
                    ProductId = productId
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFromWishlistAsync(int userId, int productId)
        {
            var existing = await _context.WishlistItems.FindAsync(userId, productId);
            if (existing != null)
            {
                _context.WishlistItems.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
