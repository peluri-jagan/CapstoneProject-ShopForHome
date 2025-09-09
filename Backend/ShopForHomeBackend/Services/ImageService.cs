using System.IO;
using System.Threading.Tasks;
using ShopForHomeBackend.Services;
namespace ShopForHomeBackend.Services
{
    public class ImageService : IImageService
    {
        public async Task<string> UploadAsync(byte[] fileBytes, string fileName)
        {
            // Save files into "Uploads" folder in project root
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var filePath = Path.Combine(uploadPath, fileName);
            await File.WriteAllBytesAsync(filePath, fileBytes);

            // Return relative path (can be replaced with a URL if serving files)
            return $"Uploads/{fileName}";
        }
    }
}
