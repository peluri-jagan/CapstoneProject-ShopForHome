// Backend/Services/IProductService.cs
using ShopForHomeBackend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductDto productDto);
        Task<bool> UpdateProductAsync(int id, ProductDto productDto);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(int? categoryId, decimal? priceMin, decimal? priceMax, double? ratingMin);

    }
}
