using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web;
using System;
using X.PagedList;

namespace ITCMS_HUIT.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckToken("Teacher,Admin")]
    public class KhoaHocsController : Controller
    {
        public IActionResult Index(int? IdchuongTrinh, int pageNo = 1)
        {
            try
            {
                var dsChuongTrinhDaoTao = Utilities.SendDataRequest<List<ChuongTrinhDaoTaoDTO>>
                    (ConstantValues.ChuongTrinhDaoTao.DanhSachChuongTrinhDaoTao);
                dsChuongTrinhDaoTao.Insert(0, new ChuongTrinhDaoTaoDTO { IdchuongTrinh = 0, TenChuongTrinh = "----------Chọn tên chương trình----------" });
                ViewBag.IdChuongTrinh = new SelectList(dsChuongTrinhDaoTao, "IdchuongTrinh", "TenChuongTrinh", IdchuongTrinh);

                if (IdchuongTrinh == null)
                {
                    var dsKhoaHoc = Utilities.SendDataRequest<List<KhoaHocDTO>>(ConstantValues.KhoaHoc.DanhSach);
                    var pagedList = dsKhoaHoc.ToPagedList(pageNo, 5);
                    return View(pagedList);
                }
                else
                {
                    var dsKhoaHoc = Utilities.SendDataRequest<List<KhoaHocDTO>>(ConstantValues.KhoaHoc.DanhSach);
                    var pagedList = dsKhoaHoc.Where(c=>c.IdchuongTrinh==IdchuongTrinh).ToPagedList(pageNo, 5);
                    return View(pagedList);
                }    
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
                ViewData["IdChuongTrinh"] = new SelectList(Utilities.SendDataRequest<List<ChuongTrinhDaoTaoDTO>>
                    (ConstantValues.ChuongTrinhDaoTao.DanhSachChuongTrinhDaoTao), "IdchuongTrinh", "TenChuongTrinh");
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TenKhoaHoc,SoGio,SoTuan,HocPhi,Mota,HinhAnh,IdchuongTrinh")] KhoaHocDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<KhoaHocDTO>(ConstantValues.KhoaHoc.Them, model);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdChuongTrinh"] = new SelectList(Utilities.SendDataRequest<List<ChuongTrinhDaoTaoDTO>>
                    (ConstantValues.ChuongTrinhDaoTao.DanhSachChuongTrinhDaoTao),model.IdchuongTrinh);
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
                
                var url = string.Format(ConstantValues.KhoaHoc.ChiTietKhoaHoc, id);
                var khoaHoc = Utilities.SendDataRequest<KhoaHocDTO>(url);

                khoaHoc.Mota = HttpUtility.HtmlDecode(khoaHoc.Mota)!;

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

        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.KhoaHoc.ChiTietKhoaHoc, id);
                var khoaHoc = Utilities.SendDataRequest<KhoaHocDTO>(url);
                if (khoaHoc == null)
                {
                    return NotFound();
                }

                ViewData["IdChuongTrinh"] = new SelectList(Utilities.SendDataRequest<List<ChuongTrinhDaoTaoDTO>>
                    (ConstantValues.ChuongTrinhDaoTao.DanhSachChuongTrinhDaoTao), "IdchuongTrinh", "TenChuongTrinh");
                return View(khoaHoc);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdkhoaHoc,TenKhoaHoc,SoGio,SoTuan,HocPhi,Mota,HinhAnh,IdchuongTrinh")] KhoaHocDTO model)
        {
            try
            {
                if (id != model.IdkhoaHoc)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        model.Mota = HttpUtility.HtmlEncode(model.Mota)!;                      
                        Utilities.SendDataRequest<bool>(ConstantValues.KhoaHoc.CapNhat, model);       
                        

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!IsExist(model.IdkhoaHoc))
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

                ViewData["IdChuongTrinh"] = new SelectList(Utilities.SendDataRequest<List<ChuongTrinhDaoTaoDTO>>
                    (ConstantValues.ChuongTrinhDaoTao.DanhSachChuongTrinhDaoTao), "IdchuongTrinh", "TenChuongTrinh", model.IdchuongTrinh);
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

                var url = string.Format(ConstantValues.KhoaHoc.ChiTietKhoaHoc, id);
                var khoaHoc = Utilities.SendDataRequest<KhoaHocDTO>(url);
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
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var url = string.Format(ConstantValues.KhoaHoc.ChiTietKhoaHoc, id);
                var khoaHoc = Utilities.SendDataRequest<KhoaHocDTO>(url);
                Utilities.SendDataRequest<bool>(ConstantValues.KhoaHoc.Xoa, khoaHoc);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        private bool IsExist(int id)
        {
            var url = string.Format(ConstantValues.KhoaHoc.KiemTraTonTai, id);
            var khoaHoc = Utilities.SendDataRequest<bool>(url);
            if (khoaHoc != true) return false;
            else return true;
        }
    }
}
