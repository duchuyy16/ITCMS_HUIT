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
    [CheckToken("Teacher,Admin")]
    public class LopHocsController : Controller
    {
        public IActionResult Index(int pageNo = 1)
        {
            try
            {
                var dsLopHoc = Utilities.SendDataRequest<List<LopHocDTO>>(ConstantValues.LopHoc.DanhSach);
                var pagedList = dsLopHoc.ToPagedList(pageNo, 5);
                return View(pagedList);
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
                ViewData["IdGiaoVien"] = new SelectList(Utilities.SendDataRequest<List<GiaoVienDTO>>
                    (ConstantValues.GiaoVien.DanhSach), "IdgiaoVien", "TenGiaoVien");
                ViewData["IdKhoaHoc"] = new SelectList(Utilities.SendDataRequest<List<KhoaHocDTO>>
                    (ConstantValues.KhoaHoc.DanhSach), "IdkhoaHoc", "TenKhoaHoc");
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TenLopHoc,ThoiGian,NgayBatDau,NgayKetThuc,DiaDiem,IdkhoaHoc,IdgiaoVien")] LopHocDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<LopHocDTO>(ConstantValues.LopHoc.Them, model);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdGiaoVien"] = new SelectList(Utilities.SendDataRequest<List<GiaoVienDTO>>
                    (ConstantValues.GiaoVien.DanhSach), model.IdgiaoVien);
                ViewData["IdKhoaHoc"] = new SelectList(Utilities.SendDataRequest<List<KhoaHocDTO>>
                    (ConstantValues.KhoaHoc.DanhSach), model.IdkhoaHoc);
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

                var url = string.Format(ConstantValues.LopHoc.ChiTietLopHoc, id);
                var LopHoc = Utilities.SendDataRequest<LopHocDTO>(url);
                if (LopHoc == null)
                {
                    return NotFound();
                }

                return View(LopHoc);
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

                var url = string.Format(ConstantValues.LopHoc.ChiTietLopHoc, id);
                var lopHoc = Utilities.SendDataRequest<LopHocDTO>(url);
                if (lopHoc == null)
                {
                    return NotFound();
                }
                ViewData["IdKhoaHoc"] = new SelectList(Utilities.SendDataRequest<List<KhoaHocDTO>>
                   (ConstantValues.KhoaHoc.DanhSach), "IdkhoaHoc", "TenKhoaHoc");
                ViewData["IdGiaoVien"] = new SelectList(Utilities.SendDataRequest<List<GiaoVienDTO>>
                     (ConstantValues.GiaoVien.DanhSach), "IdgiaoVien", "TenGiaoVien");        
                return View(lopHoc);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdlopHoc,TenLopHoc,ThoiGian,NgayBatDau,NgayKetThuc,DiaDiem,IdkhoaHoc,IdgiaoVien")] LopHocDTO model)
        {
            try
            {
                if (id != model.IdlopHoc)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        Utilities.SendDataRequest<bool>(ConstantValues.LopHoc.CapNhat, model);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!IsExist(model.IdlopHoc))
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
                ViewData["IdKhoaHoc"] = new SelectList(Utilities.SendDataRequest<List<KhoaHocDTO>>
                   (ConstantValues.KhoaHoc.DanhSach), "IdkhoaHoc", "TenKhoaHoc", model.IdkhoaHoc);
                ViewData["IdGiaoVien"] = new SelectList(Utilities.SendDataRequest<List<GiaoVienDTO>>
                      (ConstantValues.GiaoVien.DanhSach), "IdgiaoVien", "TenGiaoVien", model.IdgiaoVien);
               
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

                var url = string.Format(ConstantValues.LopHoc.ChiTietLopHoc, id);
                var lopHoc = Utilities.SendDataRequest<LopHocDTO>(url);
                if (lopHoc == null)
                {
                    return NotFound();
                }

                return View(lopHoc);
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
                var url = string.Format(ConstantValues.LopHoc.ChiTietLopHoc, id);
                var lopHoc = Utilities.SendDataRequest<LopHocDTO>(url);
                Utilities.SendDataRequest<bool>(ConstantValues.LopHoc.Xoa, lopHoc);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        private bool IsExist(int id)
        {
            var url = string.Format(ConstantValues.LopHoc.KiemTraTonTai, id);
            var lopHoc = Utilities.SendDataRequest<bool>(url);
            if (lopHoc != true) return false;
            else return true;
        }
    }
}
