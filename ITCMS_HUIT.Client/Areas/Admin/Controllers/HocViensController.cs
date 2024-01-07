﻿using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ITCMS_HUIT.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckToken("Admin")]
    public class HocViensController : Controller
    {
        public ActionResult Index(int pageNo = 1)
        {
            try
            {
                var dsHocVien = Utilities.SendDataRequest<List<HocVienDTO>>(ConstantValues.HocVien.DanhSach).Data;
                var pagedList = dsHocVien.ToPagedList(pageNo, 5);
                return View(pagedList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        public ActionResult Create()
        {
            try
            {
                ViewData["IdDoiTuong"] = new SelectList(Utilities.SendDataRequest<List<DoiTuongDangKyDTO>>
                    (ConstantValues.DoiTuongDangKy.DanhSachDoiTuongDangKy).Data, "IddoiTuong", "DoiTuongDangKy1");
                ViewData["IdTrangThai"] = new SelectList(Utilities.SendDataRequest<List<TrangThaiHocVienDTO>>
                   (ConstantValues.TrangThaiHocVien.DanhSachTrangThaiHocVien).Data, "IdtrangThai", "TenTrangThai");
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("TenHocVien,NgaySinh,Email,Sdt,DiaChi,NgayDangKy,IddoiTuong,IdtrangThai")] HocVienDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<HocVienDTO>(ConstantValues.HocVien.Them, model);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdDoiTuong"] = new SelectList(Utilities.SendDataRequest<List<DoiTuongDangKyDTO>>
                    (ConstantValues.DoiTuongDangKy.DanhSachDoiTuongDangKy).Data, model.IddoiTuong);
                ViewData["IdTrangThai"] = new SelectList(Utilities.SendDataRequest<List<TrangThaiHocVienDTO>>
                   (ConstantValues.TrangThaiHocVien.DanhSachTrangThaiHocVien).Data, model.IdtrangThai);
                return View(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.HocVien.ChiTietHocVien, id);
                var hocVien = Utilities.SendDataRequest<HocVienDTO>(url).Data;
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

        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.HocVien.ChiTietHocVien, id);
                var HocVien = Utilities.SendDataRequest<HocVienDTO>(url).Data;
                if (HocVien == null)
                {
                    return NotFound();
                }

                ViewData["IdDoiTuong"] = new SelectList(Utilities.SendDataRequest<List<DoiTuongDangKyDTO>>
                  (ConstantValues.DoiTuongDangKy.DanhSachDoiTuongDangKy).Data, "IddoiTuong", "DoiTuongDangKy1");
                ViewData["IdTrangThai"] = new SelectList(Utilities.SendDataRequest<List<TrangThaiHocVienDTO>>
                   (ConstantValues.TrangThaiHocVien.DanhSachTrangThaiHocVien).Data, "IdtrangThai", "TenTrangThai");

                return View(HocVien);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("IdhocVien,TenHocVien,NgaySinh,Email,Sdt,DiaChi,NgayDangKy,IddoiTuong,IdtrangThai")] HocVienDTO model)
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
                  (ConstantValues.DoiTuongDangKy.DanhSachDoiTuongDangKy).Data, "IddoiTuong", "DoiTuongDangKy1",model.IddoiTuong);
                ViewData["IdTrangThai"] = new SelectList(Utilities.SendDataRequest<List<TrangThaiHocVienDTO>>
                   (ConstantValues.TrangThaiHocVien.DanhSachTrangThaiHocVien).Data, "IdtrangThai", "TenTrangThai",model.IdtrangThai);
                return View(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.HocVien.ChiTietHocVien, id);
                var HocVien = Utilities.SendDataRequest<HocVienDTO>(url).Data;
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
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var url = string.Format(ConstantValues.HocVien.ChiTietHocVien, id);
                var HocVien = Utilities.SendDataRequest<HocVienDTO>(url).Data;
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
            var HocVien = Utilities.SendDataRequest<bool>(url).Data;
            if (HocVien != true) return false;
            else return true;
        }
    }
}
