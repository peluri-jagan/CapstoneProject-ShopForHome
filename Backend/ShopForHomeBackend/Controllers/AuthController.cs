// Backend/Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using ShopForHomeBackend.DTOs;
using ShopForHomeBackend.Services;
using System.Threading.Tasks;

namespace ShopForHomeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(new { Message = "Registration successful." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine(loginDto.Username);
                Console.WriteLine(loginDto.Password);
                return Unauthorized("Invalid username or password.");
            }
            return Ok(new { Token = token });
        }
    }
}
