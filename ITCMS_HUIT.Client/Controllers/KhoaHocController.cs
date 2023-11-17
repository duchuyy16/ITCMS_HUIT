using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITCMS_HUIT.Client.Controllers
{
    public class KhoaHocController : Controller
    {
        public IActionResult TimKiemKhoaHocTheoTen(string keyword)
        {
            ViewBag.Name = keyword;
            if (!string.IsNullOrEmpty(keyword))
            {
                var url = string.Format(ConstantValues.KhoaHoc.TimKiem, keyword);
                var dsKhoaHoc = Utilities.SendDataRequest<List<KhoaHocDTO>>(url);
                return View(dsKhoaHoc);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult ChiTietKhoaHoc(int id)
        {
            var url = string.Format(ConstantValues.KhoaHoc.ChiTietKhoaHoc, id);
            var chiTietKhoaHoc = Utilities.SendDataRequest<KhoaHocDTO>(url);
            return View(chiTietKhoaHoc);
        }
    }
}
