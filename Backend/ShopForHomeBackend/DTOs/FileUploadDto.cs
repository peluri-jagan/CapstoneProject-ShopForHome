// DTO
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHomeBackend.Services;

public class FileUploadDto
{
    [FromForm(Name = "file")]
    public IFormFile File { get; set; }
}

// Controller
[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class BulkUploadController : ControllerBase
{
    private readonly IBulkUploadService _bulkUploadService;

    public BulkUploadController(IBulkUploadService bulkUploadService)
    {
        _bulkUploadService = bulkUploadService;
    }

    [HttpPost("products")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadProducts([FromForm] FileUploadDto request)
    {
        if (request.File == null || request.File.Length == 0)
        {
            return BadRequest(new { message = "No file uploaded" });
        }

        var result = await _bulkUploadService.UploadProductsFromCsvAsync(request.File);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Message });

        return Ok(new { message = "Products uploaded successfully", created = result.CreatedCount });
    }
}
