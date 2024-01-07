using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITCMS_HUIT.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckToken("Teacher,Admin")]
    public class ChartsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public List<KhoaHocTheoChuongTrinhDTO> GetCourseCountsByProgram()
        {
            var courseCounts = Utilities.SendDataRequest<List<KhoaHocTheoChuongTrinhDTO>>(ConstantValues.Chart.GetCourseCountsByProgram);
            return courseCounts.Data!;
        }

        [HttpGet]
        public List<ThongKeDoiTuongDangKyDTO> ThongKeDoiTuongDangKy()
        {
            var doiTuongStats = Utilities.SendDataRequest<List<ThongKeDoiTuongDangKyDTO>>(ConstantValues.Chart.ThongKeDoiTuongDangKy);
            return doiTuongStats.Data!;
        }
    }
}
