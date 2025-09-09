using ShopForHomeBackend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
