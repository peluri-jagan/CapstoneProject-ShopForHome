using CsvHelper;
using Microsoft.AspNetCore.Http;
using ShopForHomeBackend.Data;
using ShopForHomeBackend.Models;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace ShopForHomeBackend.Services
{
    public class BulkUploadService : IBulkUploadService
    {
        private readonly AppDbContext _context;

        public BulkUploadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool IsSuccess, string Message, int CreatedCount)> UploadProductsFromCsvAsync(IFormFile file)
        {
            try
            {
                using var stream = file.OpenReadStream();
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var records = csv.GetRecords<ProductCsvRecord>().ToList();

                // Filter out invalid records
                var products = records
                    .Where(r => !string.IsNullOrWhiteSpace(r.Name) && r.Price > 0)
                    .Select(r => new Product
                    {
                        Name = r.Name,
                        Description = r.Description,
                        Price = r.Price,
                        StockQuantity = r.StockQuantity,
                        Rating = r.Rating,
                        ImageUrl = r.ImageUrl,
                        CategoryId = r.CategoryId
                    })
                    .ToList();

                if (!products.Any())
                    return (false, "No valid products found in CSV.", 0);

                await _context.Products.AddRangeAsync(products);
                var count = await _context.SaveChangesAsync();

                return (true, "Upload successful", count);
            }
            catch (Exception ex)
            {
                return (false, $"CSV upload failed: {ex.Message}", 0);
            }
        }
    }
}
