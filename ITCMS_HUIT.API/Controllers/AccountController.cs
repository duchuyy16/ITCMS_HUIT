using ITCMS_HUIT.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

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

        [HttpGet]
        [Route("danh-sach-nguoi-dung")]
        public IActionResult GetAspNetUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }

        [HttpPost]
        [Route("cap-nhat-quyen")]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesDTO model)
        {
            var userExists = await _userManager.FindByIdAsync(model.UserId);
            if (userExists == null)
                return NotFound(new { Status = "Lỗi", Message = "Người dùng không tồn tại!" });

            var isTeacher = await _userManager.IsInRoleAsync(userExists, UserRoles.Teacher);

            if (isTeacher)
            {
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    await _userManager.AddToRoleAsync(userExists, UserRoles.Admin);
            }

            var apiResponse = new ApiResponse<string>
            {
                Status = "Thành công",
                Message = "Cập nhật thành công!",
                Data = null
            };

            return Ok(apiResponse);
        }


    }
}
