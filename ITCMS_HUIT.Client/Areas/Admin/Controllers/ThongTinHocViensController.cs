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
        public ActionResult Index(string id,int pageNo=1)
        {
            try
            {
                var url = string.Format(ConstantValues.ThongTinHocVien.DanhSachHocVienTheoGiaoVien, id);
                var thongTinHocViens = Utilities.SendDataRequest<List<ThongTinHocVienDTO>>(url).Data;
                var pagedList = thongTinHocViens.ToPagedList(pageNo, 5);
                return View(pagedList);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public ActionResult IndexAdmin(int pageNo = 1)
        {
            try
            {
                var thongTinHocViens = Utilities.SendDataRequest<List<ThongTinHocVienDTO>>(ConstantValues.ThongTinHocVien.DanhSach).Data;
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
                ValueTuple<MemoryStream,string> file = Utilities.SendDataRequestExcel(ConstantValues.ThongTinHocVien.Export);
                ValueTuple<MemoryStream, string> fileName = Utilities.SendDataRequestExcel(ConstantValues.ThongTinHocVien.Export);

                return new FileStreamResult(file.Item1, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    
                    FileDownloadName = file.Item2,
                };

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public ActionResult Import(string fileName)
        {
            try
            {
                var url = string.Format(ConstantValues.ThongTinHocVien.Import, fileName);
                var import = Utilities.SendDataRequest<bool>(url);

                if (import.Data)
                {
                    TempData["ImportSuccessMessage"] = "Import thành công!";
                }
                else
                {
                    TempData["ImportErrorMessage"] = "Import thất bại. Vui lòng thử lại sau.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ImportErrorMessage"] = "Lỗi khôi phục bản sao lưu. Vui lòng thử lại sau.";
                return View();
            }
        }

        public ActionResult TimKiemHocVienTheoTenHocVien(string tenHocVien, int pageNo=1)
        {
            if (tenHocVien != null)
            {
                var url = string.Format(ConstantValues.ThongTinHocVien.TimKiem, tenHocVien);
                var thongTinHocViens = Utilities.SendDataRequest<List<ThongTinHocVienDTO>>(url).Data;
                var pagedList = thongTinHocViens.ToPagedList(pageNo, 5);
                return View(pagedList);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Create()
        {
            ViewData["IdHocVien"] = new SelectList(Utilities.SendDataRequest<List<HocVienDTO>>
                   (ConstantValues.HocVien.DanhSach).Data, "IdhocVien", "TenHocVien");
            ViewData["IdLopHoc"] = new SelectList(Utilities.SendDataRequest<List<LopHocDTO>>
                (ConstantValues.LopHoc.DanhSach).Data, "IdlopHoc", "TenLopHoc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("IdhocVien,IdlopHoc,Diem,NgayThongBao,TrangThaiThongBao,SoLanVangMat,LyDoThongBao,HocPhi,NgayGioGiaoDich,TrangThaiThanhToan")] ThongTinHocVienDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<ThongTinHocVienDTO>(ConstantValues.ThongTinHocVien.Them, model);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdHocVien"] = new SelectList(Utilities.SendDataRequest<List<HocVienDTO>>
                  (ConstantValues.HocVien.DanhSach).Data, "IdhocVien", "TenHocVien");
                ViewData["IdLopHoc"] = new SelectList(Utilities.SendDataRequest<List<LopHocDTO>>
                    (ConstantValues.LopHoc.DanhSach).Data, "IdlopHoc", "TenLopHoc");
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
                var thongTinHocVien = Utilities.SendDataRequest<ThongTinHocVienDTO>(url).Data;
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
                var thongTinHocVien = Utilities.SendDataRequest<ThongTinHocVienDTO>(url).Data;


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
        public ActionResult Edit(int IdhocVien, int IdlopHoc, [Bind("IdhocVien,IdlopHoc,Diem,NgayThongBao,TrangThaiThongBao,SoLanVangMat,LyDoThongBao,HocPhi,NgayGioGiaoDich,TrangThaiThanhToan")] ThongTinHocVienDTO model)
        {
            try
            {
                if (IdhocVien != model.IdhocVien || IdlopHoc != model.IdlopHoc)
                {
                    return NotFound();
                }
                var url = string.Format(ConstantValues.ThongTinHocVien.ThongTinChiTiet, IdhocVien, IdlopHoc);
                var thongTinHocVien = Utilities.SendDataRequest<ThongTinHocVienDTO>(url).Data;

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
                var khoaHoc = Utilities.SendDataRequest<ThongTinHocVienDTO>(url).Data;
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
                var thongTinHocVien = Utilities.SendDataRequest<ThongTinHocVienDTO>(url).Data;
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
            var thongTinHocVien = Utilities.SendDataRequest<bool>(url).Data;
            if (thongTinHocVien != true) return false;
            else return true;
        }
    }
}
