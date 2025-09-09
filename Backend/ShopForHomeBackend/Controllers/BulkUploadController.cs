using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHomeBackend.Models;
using ShopForHomeBackend.Services;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Controllers
{
 [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
   [ApiController]

public class BulkUploadController : ControllerBase
{
        [HttpPost("products")]
[Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadProducts([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded" });
            }

            // Example: save the file to wwwroot/uploads
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var filePath = Path.Combine(uploadsPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { message = $"File {file.FileName} uploaded successfully" });
        }
}

}
