using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ITCMS_HUIT.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongTinHocViensController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var thongTinHocViens = Utilities.SendDataRequest<List<ThongTinHocVienDTO>>(ConstantValues.ThongTinHocVien.DanhSach);
                return View(thongTinHocViens);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult TimKiemHocVienTheoId(int? IdhocVien)
        {
            if (IdhocVien!=null)
            {
                var url = string.Format(ConstantValues.ThongTinHocVien.TimKiem, IdhocVien);
                var thongTinHocViens = Utilities.SendDataRequest<List<ThongTinHocVienDTO>>(url);
                return View(thongTinHocViens);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Create()
        {
            ViewData["IdHocVien"] = new SelectList(Utilities.SendDataRequest<List<HocVienDTO>>
                   (ConstantValues.HocVien.DanhSach), "IdhocVien", "TenHocVien");
            ViewData["IdLopHoc"] = new SelectList(Utilities.SendDataRequest<List<LopHocDTO>>
                (ConstantValues.LopHoc.DanhSach), "IdlopHoc", "TenLopHoc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdhocVien,IdlopHoc,Diem,NgayThongBao,TrangThaiThongBao,SoLanVangMat,LyDoThongBao,HocPhi,NgayGioGiaoDich,TrangThaiThongBao")] ThongTinHocVienDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<ThongTinHocVienDTO>(ConstantValues.ThongTinHocVien.Them, model);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdHocVien"] = new SelectList(Utilities.SendDataRequest<List<HocVienDTO>>
                  (ConstantValues.HocVien.DanhSach), "IdhocVien", "TenHocVien");
                ViewData["IdLopHoc"] = new SelectList(Utilities.SendDataRequest<List<LopHocDTO>>
                    (ConstantValues.LopHoc.DanhSach), "IdlopHoc", "TenLopHoc");
                return View(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult Details(int? IdhocVien, int? IdlopHoc)
        {
            try
            {
                if (IdhocVien == null && IdlopHoc == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.ThongTinHocVien.ThongTinChiTiet, IdhocVien, IdlopHoc);
                var thongTinHocVien = Utilities.SendDataRequest<ThongTinHocVienDTO>(url);
                if (thongTinHocVien == null)
                {
                    return NotFound();
                }

                return View(thongTinHocVien);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult Edit(int? IdhocVien, int? IdlopHoc)
        {
            try
            {
                if (IdhocVien == null || IdlopHoc == null)
                {
                    return NotFound();
                }
                var url = string.Format(ConstantValues.ThongTinHocVien.ThongTinChiTiet, IdhocVien, IdlopHoc);
                var thongTinHocVien = Utilities.SendDataRequest<ThongTinHocVienDTO>(url);


                if (thongTinHocVien == null)
                {
                    return NotFound();
                }

                return View(thongTinHocVien);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int IdhocVien, int IdlopHoc, [Bind("IdhocVien,IdlopHoc,Diem,NgayThongBao,TrangThaiThongBao,SoLanVangMat,LyDoThongBao,HocPhi,NgayGioGiaoDich,TrangThaiThongBao")] ThongTinHocVienDTO model)
        {
            try
            {
                if (IdhocVien != model.IdhocVien || IdlopHoc != model.IdlopHoc)
                {
                    return NotFound();
                }
                var url = string.Format(ConstantValues.ThongTinHocVien.ThongTinChiTiet, IdhocVien, IdlopHoc);
                var thongTinHocVien = Utilities.SendDataRequest<ThongTinHocVienDTO>(url);

                model.IdhocVienNavigation = thongTinHocVien.IdhocVienNavigation;

                if (ModelState.IsValid)
                {
                    try
                    {
                        Utilities.SendDataRequest<ThongTinHocVienDTO>(ConstantValues.ThongTinHocVien.CapNhat, model);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!IsExist(model.IdhocVien, model.IdlopHoc))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
             
                return View(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult Delete(int? IdhocVien, int? IdlopHoc)
        {
            try
            {
                if (IdhocVien == null || IdlopHoc == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.ThongTinHocVien.ThongTinChiTiet, IdhocVien, IdlopHoc);
                var khoaHoc = Utilities.SendDataRequest<ThongTinHocVienDTO>(url);
                if (khoaHoc == null)
                {
                    return NotFound();
                }

                return View(khoaHoc);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int IdhocVien, int IdlopHoc)
        {
            try
            {
                var url = string.Format(ConstantValues.ThongTinHocVien.ThongTinChiTiet, IdhocVien, IdlopHoc);
                var thongTinHocVien = Utilities.SendDataRequest<ThongTinHocVienDTO>(url);
                Utilities.SendDataRequest<bool>(ConstantValues.ThongTinHocVien.Xoa, thongTinHocVien);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        private bool IsExist(int IdhocVien, int IdlopHoc)
        {
            var url = string.Format(ConstantValues.ThongTinHocVien.KiemTraTonTai, IdhocVien, IdlopHoc);
            var thongTinHocVien = Utilities.SendDataRequest<bool>(url);
            if (thongTinHocVien != true) return false;
            else return true;
        }
    }
}
