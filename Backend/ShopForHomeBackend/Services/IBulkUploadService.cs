// Backend/Services/IBulkUploadService.cs
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public interface IBulkUploadService
    {
        Task<(bool IsSuccess, string Message, int CreatedCount)> UploadProductsFromCsvAsync(IFormFile file);
    }
}
