// Models/UploadProductsRequest.cs
using Microsoft.AspNetCore.Http;

namespace ShopForHomeBackend.Models
{
    public class UploadProductsRequest
    {
        public IFormFile File { get; set; }
        
    }
}
