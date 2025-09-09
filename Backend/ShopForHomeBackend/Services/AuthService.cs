// Backend/Services/AuthService.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopForHomeBackend.Data;
using ShopForHomeBackend.DTOs;
using ShopForHomeBackend.Helpers;
using ShopForHomeBackend.Models;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _jwtHelper = new JwtHelper(config);
        }

        public async Task<(bool IsSuccess, string Message)> RegisterAsync(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
                return (false, "Username already exists.");

            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return (false, "Email already registered.");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = PasswordHasher.HashPassword(dto.Password),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return (true, "User registered successfully.");
        }

        public async Task<string> LoginAsync(LoginDto dto)
{
    var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == dto.Username);
    if (user == null) return null;

    // Use only your custom PasswordHasher
    bool valid = PasswordHasher.VerifyPassword(user.PasswordHash, dto.Password);
    if (!valid) return null;

    // Generate JWT
    var token = _jwtHelper.GenerateJwtToken(user);
    return token;
}

    }
}
