using Microsoft.AspNetCore.Mvc;
using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        // Constructor - Dependency Injection
        public UserController(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // ✅ Lấy tất cả người dùng
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllUsersAsync(cancellationToken);
            return Ok(users);
        }

        // ✅ Lấy người dùng theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("ID must be greater than zero.");

            var user = await _userService.GetUserByIdAsync(id, cancellationToken);
            if (user == null) return NotFound();

            return Ok(user);
        }

        // ✅ Thêm một người dùng mới
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user, CancellationToken cancellationToken)
        {
            if (user == null)
                return BadRequest("User data is required.");

            var createdUser = await _userService.AddUserAsync(user, cancellationToken);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        // ✅ Cập nhật thông tin người dùng
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user, CancellationToken cancellationToken)
        {
            if (id <= 0 || user == null || id != user.Id)
                return BadRequest("Invalid user data.");

            var updatedUser = await _userService.UpdateUserAsync(user, cancellationToken);
            if (updatedUser == null) return NotFound();

            return Ok(updatedUser);
        }

        // ✅ Xóa một người dùng
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("ID must be greater than zero.");

            var deleted = await _userService.DeleteUserAsync(id, cancellationToken);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
