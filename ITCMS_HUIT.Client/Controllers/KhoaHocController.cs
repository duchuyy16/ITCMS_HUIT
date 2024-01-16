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
            if (!string.IsNullOrEmpty(keyword))
            {
                var url = string.Format(ConstantValues.KhoaHoc.TimKiem, keyword);
                var dsKhoaHoc = Utilities.SendDataRequest<List<KhoaHocDTO>>(url).Data;
                return View(dsKhoaHoc); 
            }

            return NotFound();
        }


        public IActionResult ChiTietKhoaHoc(int id)
        {
            var url = string.Format(ConstantValues.KhoaHoc.ChiTietKhoaHoc, id);
            var chiTietKhoaHoc = Utilities.SendDataRequest<KhoaHocDTO>(url).Data;

            chiTietKhoaHoc!.Mota = HttpUtility.HtmlDecode(chiTietKhoaHoc.Mota)!;

            var uRL = string.Format(ConstantValues.KhoaHoc.DanhSachKhoaHocTheoChuongTrinh, id);
            var dsKhoaHoc = Utilities.SendDataRequest<List<KhoaHocDTO>>(uRL).Data!.Take(4);

            ViewBag.KhoaHocs=dsKhoaHoc;

            return View(chiTietKhoaHoc);
        }

        public IActionResult DanhSachKhoaHoc(int pageNo=1)
        {
            var dsKhoaHoc = Utilities.SendDataRequest<List<KhoaHocDTO>>(ConstantValues.KhoaHoc.DanhSach).Data;
            var pagedList = dsKhoaHoc.ToPagedList(pageNo, 5);
            return View(pagedList);
        }

    }
}
