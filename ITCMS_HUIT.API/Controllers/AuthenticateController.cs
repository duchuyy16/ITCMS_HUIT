using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly GiaoVienService _giaoVien;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthenticateController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, SignInManager<IdentityUser> signInManager, GiaoVienService giaoVien)
        {
            _giaoVien = giaoVien;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [HttpPost]
        [Route("dangxuat")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok(new { message = "Đăng xuất thành công." });
            }
            catch 
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi đăng xuất." });
            }
        }


        [HttpPost("dangnhap")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
						new Claim("idGiaoVien",user.Id),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new ApiResponse<string>
                {
                    Status = "Thành công",
                    Message = "Đăng nhập thành công",
                    Data = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Ok(new ApiResponse<string>
            {
                Status = "Lỗi",
                Message = "Đăng nhập không thành công. Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.",
                Data = null
            });
        }

        [HttpPost]
        [Route("dang-ky-giao-vien")]
        public async Task<IActionResult> RegisterTeacher([FromBody] Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = "Người dùng đã tồn tại!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = "Tạo người dùng không thành công! Vui lòng kiểm tra chi tiết người dùng và thử lại." });

            // Kiểm tra và tạo role Giáo viên nếu chưa tồn tại
            if (!await _roleManager.RoleExistsAsync(UserRoles.Teacher))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));

            // Gán role Giáo viên cho người dùng mới
            await _userManager.AddToRoleAsync(user, UserRoles.Teacher);

            var addTeacher = _giaoVien.Add(new GiaoVienDTO
            {
                IdgiaoVien = user.Id,
                TenGiaoVien=user.UserName!,
                ChungChi="",
                TrinhDo="",
                HoSoCaNhan="",
                HinhAnh="",
            });

            if (addTeacher == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = "Tạo giáo viên không thành công! Vui lòng kiểm tra chi tiết và thử lại." });
            }

            var apiResponse = new ApiResponse<Register>
            {
                Status = "Thành công",
                Message = "Người dùng đã được tạo thành công!",
                Data = model
            };

            return Ok(apiResponse);
        }


        [HttpPost]
        [Route("dangky-quantri")]
        public async Task<IActionResult> RegisterAdmin([FromBody] Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = "Người dùng đã tồn tại!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = "Tạo người dùng không thành công! Vui lòng kiểm tra chi tiết người dùng và thử lại." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            var apiResponse = new ApiResponse<Register>
            {
                Status = "Thành công",
                Message = "Người dùng đã được tạo thành công!",
                Data = model   
            };

            return Ok(apiResponse);
        }

        [HttpPost]
        [Route("thay-doi-mat-khau")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists == null)
                return StatusCode(StatusCodes.Status404NotFound, new { Status = "Lỗi", Message = "Người dùng không tồn tại!" });

            if (string.Compare(model.NewPassword, model.ConfirmNewPassword) != 0)
                return StatusCode(StatusCodes.Status400BadRequest, new { Status = "Lỗi", Message = "Mật khẩu mới và xác nhận mật khẩu mới không khớp!" });

            var result = await _userManager.ChangePasswordAsync(userExists, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new
                { Status = "Error", Message = "Something went wrong" });

            var apiResponse = new ApiResponse<ChangePassword>
            {
                Status = "Thành công",
                Message = "Đã đổi mật khẩu thành công!",
                Data = model
			};

            return Ok(apiResponse);
        }
    }
}
