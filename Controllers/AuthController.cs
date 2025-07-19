using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ArthvaTech.API.Repository;
using BCrypt.Net;


namespace ArthvaTech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly JwtService _jwtService;
        private readonly UserRepository _repo;

        public AuthController(IConfiguration config, JwtService jwtService, UserRepository repo)
        {
            _config = config;
            _jwtService = jwtService;
            _repo = repo;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _repo.GetUserByEmailAsync(model.Email);
            if (user!= null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                token,
                user = new { user.Email, user.Role, user.UserName }
            });
                
            }
            return Unauthorized("Invalid credentials.");    
            
        }

        // 🔐 Protected endpoint to test role
        [HttpGet("admin-only")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdminData()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            return Ok(new
            {
                message = $"Welcome Admin {userEmail}!",
                dashboardData = new { TotalUsers = 50, TotalCategories = 12 }
            });
        }

        // 🔐 Protected endpoint for any logged in user
        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetProfile()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var role = User.FindFirstValue(ClaimTypes.Role);

            return Ok(new { Email = email, Role = role });
        }
    }
}
