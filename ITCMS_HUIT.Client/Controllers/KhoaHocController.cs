using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using X.PagedList;

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

            chiTietKhoaHoc.Mota = HttpUtility.HtmlDecode(chiTietKhoaHoc.Mota)!;

            var uRL = string.Format(ConstantValues.KhoaHoc.DanhSachKhoaHocTheoChuongTrinh, id);
            var dsKhoaHoc = Utilities.SendDataRequest<List<KhoaHocDTO>>(uRL).Take(4);

            ViewBag.KhoaHocs=dsKhoaHoc;

            return View(chiTietKhoaHoc);
        }

        public IActionResult DanhSachKhoaHoc(int pageNo=1)
        {
            var dsKhoaHoc = Utilities.SendDataRequest<List<KhoaHocDTO>>(ConstantValues.KhoaHoc.DanhSach);
            var pagedList = dsKhoaHoc.ToPagedList(pageNo, 5);
            return View(pagedList);
        }

    }
}
