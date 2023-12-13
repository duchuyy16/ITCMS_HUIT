using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

#nullable disable

namespace ITCMS_HUIT.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckToken("Teacher,Admin")]
    public class ThongTinHocViensController : Controller
    {
        public ActionResult Index(int pageNo=1)
        {
            try
            {
                var thongTinHocViens = Utilities.SendDataRequest<List<ThongTinHocVienDTO>>(ConstantValues.ThongTinHocVien.DanhSach);
                var pagedList = thongTinHocViens.ToPagedList(pageNo, 5);
                return View(pagedList);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public ActionResult ExportExcel()
        {
            try
            {
                MemoryStream file = Utilities.SendDataRequest<MemoryStream>(ConstantValues.ThongTinHocVien.Export);
                return new FileStreamResult(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = "DanhSachThongTinHocVien.xlsx"
                };

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public ActionResult TimKiemHocVienTheoId(int? IdhocVien)
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

        public ActionResult Create()
        {
            ViewData["IdHocVien"] = new SelectList(Utilities.SendDataRequest<List<HocVienDTO>>
                   (ConstantValues.HocVien.DanhSach), "IdhocVien", "TenHocVien");
            ViewData["IdLopHoc"] = new SelectList(Utilities.SendDataRequest<List<LopHocDTO>>
                (ConstantValues.LopHoc.DanhSach), "IdlopHoc", "TenLopHoc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("IdhocVien,IdlopHoc,Diem,NgayThongBao,TrangThaiThongBao,SoLanVangMat,LyDoThongBao,HocPhi,NgayGioGiaoDich,TrangThaiThongBao")] ThongTinHocVienDTO model)
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

        public ActionResult Details(int? IdhocVien, int? IdlopHoc)
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

        public ActionResult Edit(int? IdhocVien, int? IdlopHoc)
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
        public ActionResult Edit(int IdhocVien, int IdlopHoc, [Bind("IdhocVien,IdlopHoc,Diem,NgayThongBao,TrangThaiThongBao,SoLanVangMat,LyDoThongBao,HocPhi,NgayGioGiaoDich,TrangThaiThongBao")] ThongTinHocVienDTO model)
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

        public ActionResult Delete(int? IdhocVien, int? IdlopHoc)
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
        public ActionResult DeleteConfirmed(int IdhocVien, int IdlopHoc)
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
