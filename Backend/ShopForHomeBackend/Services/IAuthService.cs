// Backend/Services/IAuthService.cs
using ShopForHomeBackend.DTOs;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public interface IAuthService
    {
        Task<(bool IsSuccess, string Message)> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
}
