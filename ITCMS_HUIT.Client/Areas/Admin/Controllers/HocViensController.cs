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
    public class HocViensController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var dsHocVien = Utilities.SendDataRequest<List<HocVienDTO>>(ConstantValues.HocVien.DanhSach);
                return View(dsHocVien);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        public IActionResult Create()
        {
            try
            {
                ViewData["IdDoiTuong"] = new SelectList(Utilities.SendDataRequest<List<DoiTuongDangKyDTO>>
                    (ConstantValues.DoiTuongDangKy.DanhSachDoiTuongDangKy), "IddoiTuong", "DoiTuongDangKy1");
                ViewData["IdTrangThai"] = new SelectList(Utilities.SendDataRequest<List<TrangThaiHocVienDTO>>
                   (ConstantValues.TrangThaiHocVien.DanhSachTrangThaiHocVien), "IdtrangThai", "TenTrangThai");
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdHocVien,TenHocVien,NgaySinh,Email,SDT,DiaChi,NgayDangKy,IddoiTuong,IdtrangThai")] HocVienDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<HocVienDTO>(ConstantValues.HocVien.Them, model);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdDoiTuong"] = new SelectList(Utilities.SendDataRequest<List<DoiTuongDangKyDTO>>
                    (ConstantValues.DoiTuongDangKy.DanhSachDoiTuongDangKy), model.IddoiTuong);
                ViewData["IdTrangThai"] = new SelectList(Utilities.SendDataRequest<List<TrangThaiHocVienDTO>>
                   (ConstantValues.TrangThaiHocVien.DanhSachTrangThaiHocVien), model.IdtrangThai);
                return View(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.HocVien.ChiTietHocVien, id);
                var hocVien = Utilities.SendDataRequest<HocVienDTO>(url);
                if (hocVien == null)
                {
                    return NotFound();
                }

                return View(hocVien);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.HocVien.ChiTietHocVien, id);
                var HocVien = Utilities.SendDataRequest<HocVienDTO>(url);
                if (HocVien == null)
                {
                    return NotFound();
                }

                ViewData["IdDoiTuong"] = new SelectList(Utilities.SendDataRequest<List<DoiTuongDangKyDTO>>
                  (ConstantValues.DoiTuongDangKy.DanhSachDoiTuongDangKy), "IddoiTuong", "DoiTuongDangKy1");
                ViewData["IdTrangThai"] = new SelectList(Utilities.SendDataRequest<List<TrangThaiHocVienDTO>>
                   (ConstantValues.TrangThaiHocVien.DanhSachTrangThaiHocVien), "IdtrangThai", "TenTrangThai");

                return View(HocVien);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdHocVien,TenHocVien,NgaySinh,Email,SDT,DiaChi,NgayDangKy,IddoiTuong,IdtrangThai")] HocVienDTO model)
        {
            try
            {
                if (id != model.IdhocVien)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        Utilities.SendDataRequest<bool>(ConstantValues.HocVien.CapNhat, model);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!IsExist(model.IdhocVien))
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

                ViewData["IdDoiTuong"] = new SelectList(Utilities.SendDataRequest<List<DoiTuongDangKyDTO>>
                  (ConstantValues.DoiTuongDangKy.DanhSachDoiTuongDangKy), "IddoiTuong", "DoiTuongDangKy1",model.IddoiTuong);
                ViewData["IdTrangThai"] = new SelectList(Utilities.SendDataRequest<List<TrangThaiHocVienDTO>>
                   (ConstantValues.TrangThaiHocVien.DanhSachTrangThaiHocVien), "IdtrangThai", "TenTrangThai",model.IdtrangThai);
                return View(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.HocVien.ChiTietHocVien, id);
                var HocVien = Utilities.SendDataRequest<HocVienDTO>(url);
                if (HocVien == null)
                {
                    return NotFound();
                }

                return View(HocVien);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var url = string.Format(ConstantValues.HocVien.ChiTietHocVien, id);
                var HocVien = Utilities.SendDataRequest<HocVienDTO>(url);
                Utilities.SendDataRequest<bool>(ConstantValues.HocVien.Xoa, HocVien);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        private bool IsExist(int id)
        {
            var url = string.Format(ConstantValues.HocVien.KiemTraTonTai, id);
            var HocVien = Utilities.SendDataRequest<bool>(url);
            if (HocVien != true) return false;
            else return true;
        }
    }
}
