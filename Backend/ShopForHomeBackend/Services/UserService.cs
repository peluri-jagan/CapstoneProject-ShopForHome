// Backend/Services/UserService.cs
using Microsoft.EntityFrameworkCore;
using ShopForHomeBackend.Data;
using ShopForHomeBackend.DTOs;
using ShopForHomeBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role
                }).ToListAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<bool> UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.Email = userUpdateDto.Email;
            user.Role = userUpdateDto.Role;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
