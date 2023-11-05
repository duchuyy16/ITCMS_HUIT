using Microsoft.AspNetCore.Mvc;

namespace ITCMS_HUIT.Client.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
