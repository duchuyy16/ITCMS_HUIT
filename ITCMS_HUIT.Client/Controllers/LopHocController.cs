using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITCMS_HUIT.Client.Controllers
{
    public class LopHocController : Controller
    {
        public IActionResult ThongTinChiTietLopHoc(int id)
        {
            var url = string.Format(ConstantValues.LopHoc.ChiTietLopHoc, id);
            var chiTietLopHoc = Utilities.SendDataRequest<LopHocDTO>(url).Data;

            return View(chiTietLopHoc);
        }
    }
}
