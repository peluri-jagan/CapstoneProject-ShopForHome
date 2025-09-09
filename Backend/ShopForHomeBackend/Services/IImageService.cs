using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public interface IImageService
    {
        Task<string> UploadAsync(byte[] fileBytes, string fileName);
    }
}
