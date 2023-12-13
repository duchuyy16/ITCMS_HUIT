using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ITCMS_HUIT.Client.Common.ConstantValues;

namespace ITCMS_HUIT.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/trangchu")]
    [CheckToken("Teacher,Admin")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            var dashboard = Utilities.SendDataRequest<ITCMS_HUIT.Client.Models.Dashboard>(ConstantValues.Count.Dashboard);
            ViewBag.LopHoc = dashboard.LopHoc;
            ViewBag.HocVien = dashboard.HocVien;
            ViewBag.ChuongTrinhDaoTao = dashboard.ChuongTrinhDaoTao;
            ViewBag.GiaoVien = dashboard.GiaoVien;           
            return View();
        }
    }
}
