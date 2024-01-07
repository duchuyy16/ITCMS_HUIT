using ITCMS_HUIT.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Data;
using System.Linq;
using System.Net.WebSockets;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("danh-sach-nguoi-dung")]
        public async Task<IActionResult> GetAspNetUsersWithRoles()
        {
            var users = await _userManager.Users.ToListAsync();

            var usersWithRoles = new List<UserViewDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userWithRoles = new UserViewDTO
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = (List<string>)roles
                };

                usersWithRoles.Add(userWithRoles);
            }

            var apiResponse = new ApiResponse<List<UserViewDTO>>
            {
                Status = "Thành công",
                Message = "Cập nhật thành công!",
                Data = usersWithRoles
            };

            return Ok(apiResponse);
        }


        [HttpPost]
        [Route("nguoi-dung/{userId}")]
        public async Task<IActionResult> GetUserId(string userId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u=>u.Id==userId);
            var roles = await _userManager.GetRolesAsync(user!);

            var userView = new UserViewDTO
            {
                UserId = user!.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = (List<string>)roles
            };

            var apiResponse = new ApiResponse<UserViewDTO>
            {
                Status = "Thành công",
                Message = "Thông tin người dùng!",
                Data = userView
            };

            return Ok(apiResponse);
        }

        [HttpPost]
        [Route("cap-nhat-quyen")]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UserViewDTO model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)               
                    return NotFound(new ApiResponse<UserViewDTO> { Status = "Lỗi", Message = "Người dùng không tồn tại." });
                
                var existingRoles = await _userManager.GetRolesAsync(user);

                var roles = _roleManager.Roles.Select(x => x.Name).ToList();

                var newRoles = model.Roles!.Except(existingRoles).Where(role=> roles.Contains(role)).ToList();

                if (!newRoles.Any())
                    return BadRequest(new ApiResponse<UserViewDTO> { Status = "Lỗi", Message = "Danh sách quyền mới không hợp lệ." });
                                    
               await _userManager.AddToRolesAsync(user, newRoles);
                
                var apiResponse = new ApiResponse<List<string>>
                {
                    Status = "Thành công",
                    Message = "Cập nhật quyền người dùng thành công!",
                    Data = newRoles
                };
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");

                return StatusCode(500, new ApiResponse<UserViewDTO> { Status = "Lỗi", Message = "Đã xảy ra lỗi trong quá trình xử lý yêu cầu." });
            }
        }
    }
}
