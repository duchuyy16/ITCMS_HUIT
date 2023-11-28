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
    public class GiaoViensController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var dsGiaoVien = Utilities.SendDataRequest<List<GiaoVienDTO>>(ConstantValues.GiaoVien.DanhSach);
                return View(dsGiaoVien);
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
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdGiaoVien,TenGiaoVien,TrinhDo,ChungChi,HinhAnh,HoSoCaNhan")] GiaoVienDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<GiaoVienDTO>(ConstantValues.GiaoVien.Them, model);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult Details(string? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.GiaoVien.ChiTietGiaoVien, id);
                var giaoVien = Utilities.SendDataRequest<GiaoVienDTO>(url);
                if (giaoVien == null)
                {
                    return NotFound();
                }

                return View(giaoVien);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult Edit(string? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.GiaoVien.ChiTietGiaoVien, id);
                var giaoVien = Utilities.SendDataRequest<GiaoVienDTO>(url);
                if (giaoVien == null)
                {
                    return NotFound();
                }

                return View(giaoVien);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("IdGiaoVien,TenGiaoVien,TrinhDo,ChungChi,HinhAnh,HoSoCaNhan")] GiaoVienDTO model)
        {
            try
            {
                if (id != model.IdgiaoVien)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        Utilities.SendDataRequest<bool>(ConstantValues.GiaoVien.CapNhat, model);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!IsExist(model.IdgiaoVien))
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

        public IActionResult Delete(string? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.GiaoVien.ChiTietGiaoVien, id);
                var giaoVien = Utilities.SendDataRequest<GiaoVienDTO>(url);
                if (giaoVien == null)
                {
                    return NotFound();
                }

                return View(giaoVien);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string? id)
        {
            try
            {
                var url = string.Format(ConstantValues.GiaoVien.ChiTietGiaoVien, id);
                var giaoVien = Utilities.SendDataRequest<GiaoVienDTO>(url);
                Utilities.SendDataRequest<bool>(ConstantValues.GiaoVien.Xoa, giaoVien);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        private bool IsExist(string id)
        {
            var url = string.Format(ConstantValues.GiaoVien.KiemTraTonTai, id);
            var giaoVien = Utilities.SendDataRequest<bool>(url);
            if (giaoVien != true) return false;
            else return true;
        }
    }
}
