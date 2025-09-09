// Backend/Services/ProductService.cs
using Microsoft.EntityFrameworkCore;
using ShopForHomeBackend.Data;
using ShopForHomeBackend.DTOs;
using ShopForHomeBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    Rating = p.Rating,
                    ImageUrl = p.ImageUrl,
                    CategoryId = p.CategoryId
                }).ToListAsync();
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var p = await _context.Products.FindAsync(id);
            if (p == null) return null;

            return new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                Rating = p.Rating,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId
            };
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                Rating = productDto.Rating,
                ImageUrl = productDto.ImageUrl,
                CategoryId = productDto.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            productDto.Id = product.Id;
            return productDto;
        }

        public async Task<bool> UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.StockQuantity = productDto.StockQuantity;
            product.Rating = productDto.Rating;
            product.ImageUrl = productDto.ImageUrl;
            product.CategoryId = productDto.CategoryId;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
        // Backend/Services/ProductService.cs - Add SearchProductsAsync implementation
public async Task<IEnumerable<ProductDto>> SearchProductsAsync(int? categoryId, decimal? priceMin, decimal? priceMax, double? ratingMin)
{
    var query = _context.Products.AsQueryable();

    if (categoryId.HasValue)
        query = query.Where(p => p.CategoryId == categoryId);

    if (priceMin.HasValue)
        query = query.Where(p => p.Price >= priceMin);

    if (priceMax.HasValue)
        query = query.Where(p => p.Price <= priceMax);

    if (ratingMin.HasValue)
        query = query.Where(p => p.Rating >= ratingMin);

    var products = await query.Select(p => new ProductDto
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        Price = p.Price,
        StockQuantity = p.StockQuantity,
        Rating = p.Rating,
        ImageUrl = p.ImageUrl,
        CategoryId = p.CategoryId
    }).ToListAsync();

    return products;
}

    }
}
