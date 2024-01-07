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
            var token = Utilities.SendDataRequest<string>(ConstantValues.Authenticate.Login, model).Data;

            if (string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("", "Đăng nhập không hợp lệ. Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.");
                return View(); 
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
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("","Đăng ký không hợp lệ");
                    return View(model);
                }

                var result = Utilities.SendDataRequest<RegisterModel>(ConstantValues.Authenticate.Register, model);

                if (result != null)
                {
                    if(result.Status=="Thành công")
                    {
                        TempData["RegisterSuccessful"] = result.Message;
                        return RedirectToAction("Login", "Auth");
                    }
                    ModelState.AddModelError("", result.Message!);
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "Đăng ký thất bại.");
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
				var response = Utilities.SendDataRequest<ChangePassword>(ConstantValues.Authenticate.ChangePassword, model);

				if (response != null)
				{
                    if (response.Status == "Thành công")
                    {
                        TempData["ChangedPasswordSuccessful"] = response.Message;
                        return RedirectToAction("Index", "Admin");
                    }
                    ModelState.AddModelError("", response.Message!);
                    return View(model);      
				}
				else
				{
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
