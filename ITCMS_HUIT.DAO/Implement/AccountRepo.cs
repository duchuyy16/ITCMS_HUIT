using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.Repository.Implement
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountRepo(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {

            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public async Task<string> ChangedPassword(ChangePassword model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);

            if (userExists == null)
                return "Không tìm thấy người dùng.";
            
            if (string.Compare(model.NewPassword, model.ConfirmNewPassword) != 0)
                return "Mật khẩu mới và mật khẩu xác nhận không khớp.";
            
            var result = await _userManager.ChangePasswordAsync(userExists, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
                return "Không thể thay đổi mật khẩu.";
            
            return "Mật khẩu đã được thay đổi thành công.";
        }


        public async Task<string> LogIn(Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return string.Empty;           

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public Task LogOut()
        {
            return _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterAdmin(Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                var error = new IdentityError { Description = "Tên người dùng đã tồn tại." };
                return IdentityResult.Failed(error);
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var error = new IdentityError { Description = "Tạo người dùng không thành công! Vui lòng kiểm tra chi tiết người dùng và thử lại." };
                return IdentityResult.Failed(error);
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            return result;
        }

        public async Task<IdentityResult> RegisterTeacher(Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                var error = new IdentityError { Description = "Tên người dùng đã tồn tại." };
                return IdentityResult.Failed(error);
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var error = new IdentityError { Description = "Tạo người dùng không thành công! Vui lòng kiểm tra chi tiết người dùng và thử lại." };
                return IdentityResult.Failed(error);
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Teacher))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));
            await _userManager.AddToRoleAsync(user, UserRoles.Teacher);

            return result;
        }
    }
}
