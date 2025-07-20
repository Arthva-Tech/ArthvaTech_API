using Microsoft.AspNetCore.Mvc;
using ArthvaTech.API.Models;
using ArthvaTech.API.Repository;
using Microsoft.AspNetCore.Authorization;
namespace ArthvaTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repo;

        public UserController(UserRepository repo) => _repo = repo;

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> Get() => Ok(await _repo.GetAllUsersAsync());
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles() => Ok(await _repo.GetRolesAsync());

        [HttpPost("AddUser")]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            var result = await _repo.AddUserAsync(user);
            return result > 0 ? Ok("User created") : BadRequest("Failed");
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("update")]
        public async Task<IActionResult> Update(UserViewModel user)
        {
            var result = await _repo.UpdateUserAsync(user);
            return result > 0 ? Ok("User updated") : BadRequest("Failed");
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _repo.DeleteUserAsync(id);
            return result > 0 ? Ok("User deleted") : BadRequest("Failed");
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(Guid id, [FromBody] string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                return BadRequest("Password cannot be empty.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            var result = await _repo.ResetPasswordAsync(id, hashedPassword);

            return result > 0 ? Ok("Password reset successfully") : NotFound("User not found");
        }

    }

}