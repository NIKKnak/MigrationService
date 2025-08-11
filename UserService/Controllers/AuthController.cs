using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (await _context.Users.AnyAsync(u => u.Name == user.Name))
                return BadRequest("Пользователь уже существует");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("Регистрация успешна");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginUser)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Name == loginUser.Name && u.Password == loginUser.Password);

            if (user == null)
                return Unauthorized("Неверные имя или пароль");

            var token = _jwtService.GenerateToken(user.Name);
            return Ok(new { Token = token });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok("Вы вышли из системы");
        }
    }
}
