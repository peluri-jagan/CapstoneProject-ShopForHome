using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopForHomeBackend.Models
{
    public class UploadProductCsvRequest
    {
        [FromForm(Name = "File")]
        public IFormFile File { get; set; }
    }
}
