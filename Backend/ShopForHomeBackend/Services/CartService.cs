// Backend/Services/CartService.cs
using Microsoft.EntityFrameworkCore;
using ShopForHomeBackend.Data;
using ShopForHomeBackend.DTOs;
using ShopForHomeBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        public CartService(AppDbContext context)
        {
            _context = context;
        }

       public async Task<IEnumerable<CartDto>> GetCartItemsAsync(int userId)
{
    return await _context.CartItems
        .Where(c => c.UserId == userId)
        .Include(c => c.Product) // Include product details
        .Select(c => new CartDto
        {
            ProductId = c.ProductId,
            Name = c.Product.Name,
            Description = c.Product.Description,
            Price = c.Product.Price,
            ImageUrl = c.Product.ImageUrl,
            Quantity = c.Quantity
        })
        .ToListAsync();
}

        public async Task AddToCartAsync(int userId, int productId, int quantity)
        {
            var existing = await _context.CartItems.FindAsync(userId, productId);
            if (existing != null)
            {
                existing.Quantity += quantity;
                _context.CartItems.Update(existing);
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartAsync(int userId, int productId, int quantity)
        {
            var existing = await _context.CartItems.FindAsync(userId, productId);
            if (existing != null)
            {
                existing.Quantity = quantity;
                _context.CartItems.Update(existing);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFromCartAsync(int userId, int productId)
        {
            var existing = await _context.CartItems.FindAsync(userId, productId);
            if (existing != null)
            {
                _context.CartItems.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
