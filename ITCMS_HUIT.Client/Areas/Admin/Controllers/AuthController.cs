using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ITCMS_HUIT.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {

            var token = Utilities.SendDataRequest<string>(ConstantValues.Authenticate.Login, model);

            if (string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("", "Đăng nhập không hợp lệ. Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.");
                return RedirectToAction("Login", "Auth");
            }

            // Lưu token vào session
            HttpContext.Session.Set("Token", token);

            return RedirectToAction("Index", "Admin", new { area = "Admin" });

        }




        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                var registrationResult = Utilities.SendDataRequest<RegisterModel>(ConstantValues.Authenticate.Register, model);

                if (registrationResult != null)
                {
                    TempData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập ngay bây giờ.";
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception)
            {
                return View(model);
            }
        }


        public IActionResult ChangedPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangedPassword(ChangePassword model)
        {
            try
            {
                var response = Utilities.SendDataRequest<ChangePassword>(ConstantValues.Authenticate.ChangePassword, model);

                if (response != null)
                {
                    TempData["SuccessMessage"] = "Thay đổi mật khẩu thành công!";
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }
        }


        public IActionResult Logout()
        {
            var response = Utilities.SendDataRequest<object>(ConstantValues.Authenticate.Logout);

            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Auth");
        }
    }
}
