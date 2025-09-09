// Backend/Controllers/SearchController.cs
using Microsoft.AspNetCore.Mvc;
using ShopForHomeBackend.DTOs;
using ShopForHomeBackend.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IProductService _productService;

        public SearchController(IProductService productService)
        {
            _productService = productService;
        }

        // Search with filtering query parameters: categoryId, priceMin, priceMax, ratingMin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> SearchProducts(
            [FromQuery] int? categoryId,
            [FromQuery] decimal? priceMin,
            [FromQuery] decimal? priceMax,
            [FromQuery] double? ratingMin)
        {
            var products = await _productService.SearchProductsAsync(categoryId, priceMin, priceMax, ratingMin);
            return Ok(products);
        }
    }
}
