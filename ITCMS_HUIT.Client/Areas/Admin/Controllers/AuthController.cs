using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ITCMS_HUIT.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                if (model.Username == null || model.Email == null || model.Password == null)
                    return NotFound();

                var result = Utilities.SendDataRequest<RegisterModel>(ConstantValues.Authenticate.Register, model);

                if (result != null)
                {
                    TempData["RegisterSuccessful"] = "Bạn có thể đăng nhập ngay bây giờ.";
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


        public ActionResult ChangedPassword()
        {
            return View();
        }

		[HttpPost]
		public ActionResult ChangedPassword(ChangePassword model)
		{
			try
			{
				// Gửi yêu cầu thay đổi mật khẩu đến API
				var response = Utilities.SendDataRequest<ChangePassword>(ConstantValues.Authenticate.ChangePassword, model);

				if (response != null)
				{
					TempData["ChangedPasswordSuccess"] = "Đã thay đổi mật khẩu thành công!";
					return RedirectToAction("Index", "Admin");
				}
				else
				{
					// Xử lý khi API trả về không thành công
					ModelState.AddModelError("", "Thay đổi mật khẩu không thành công. Vui lòng kiểm tra thông tin và thử lại.");
					return View(model);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return View(model);
			}
		}



		public ActionResult Logout()
        {
            var response = Utilities.SendDataRequest<object>(ConstantValues.Authenticate.Logout);

            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Auth");
        }
    }
}
