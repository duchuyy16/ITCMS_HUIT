using Microsoft.AspNetCore.Mvc;

namespace ITCMS_HUIT.Client.Areas.Admin.Controllers
{
    [Area("Admin")] 
    [Route("Admin/trangchu")] 
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
