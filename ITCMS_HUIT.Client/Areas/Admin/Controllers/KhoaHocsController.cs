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
        public ActionResult Index(int? IdchuongTrinh, int pageNo = 1)
        {
            try
            {
                var dsChuongTrinhDaoTao = Utilities.SendDataRequest<List<ChuongTrinhDaoTaoDTO>>
                    (ConstantValues.ChuongTrinhDaoTao.DanhSachChuongTrinhDaoTao).Data;
                dsChuongTrinhDaoTao!.Insert(0, new ChuongTrinhDaoTaoDTO { IdchuongTrinh = 0, TenChuongTrinh = "----------Chọn tên chương trình----------" });
                ViewBag.IdChuongTrinh = new SelectList(dsChuongTrinhDaoTao, "IdchuongTrinh", "TenChuongTrinh", IdchuongTrinh);

                if (IdchuongTrinh == null)
                {
                    var dsKhoaHoc = Utilities.SendDataRequest<List<KhoaHocDTO>>(ConstantValues.KhoaHoc.DanhSach).Data;
                    var pagedList = dsKhoaHoc.ToPagedList(pageNo, 5);
                    return View(pagedList);
                }
                else
                {
                    var dsKhoaHoc = Utilities.SendDataRequest<List<KhoaHocDTO>>(ConstantValues.KhoaHoc.DanhSach).Data;
                    var pagedList = dsKhoaHoc!.Where(c=>c.IdchuongTrinh==IdchuongTrinh).ToPagedList(pageNo, 5);
                    return View(pagedList);
                }    
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
                ViewData["IdChuongTrinh"] = new SelectList(Utilities.SendDataRequest<List<ChuongTrinhDaoTaoDTO>>
                    (ConstantValues.ChuongTrinhDaoTao.DanhSachChuongTrinhDaoTao).Data, "IdchuongTrinh", "TenChuongTrinh");
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("TenKhoaHoc,SoGio,SoTuan,HocPhi,Mota,HinhAnh,IdchuongTrinh")] KhoaHocDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<KhoaHocDTO>(ConstantValues.KhoaHoc.Them, model);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdChuongTrinh"] = new SelectList(Utilities.SendDataRequest<List<ChuongTrinhDaoTaoDTO>>
                    (ConstantValues.ChuongTrinhDaoTao.DanhSachChuongTrinhDaoTao).Data,model.IdchuongTrinh);
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
                
                var url = string.Format(ConstantValues.KhoaHoc.ChiTietKhoaHoc, id);
                var khoaHoc = Utilities.SendDataRequest<KhoaHocDTO>(url).Data;

                khoaHoc!.Mota = HttpUtility.HtmlDecode(khoaHoc.Mota)!;

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

        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.KhoaHoc.ChiTietKhoaHoc, id);
                var khoaHoc = Utilities.SendDataRequest<KhoaHocDTO>(url).Data;
                if (khoaHoc == null)
                {
                    return NotFound();
                }

                ViewData["IdChuongTrinh"] = new SelectList(Utilities.SendDataRequest<List<ChuongTrinhDaoTaoDTO>>
                    (ConstantValues.ChuongTrinhDaoTao.DanhSachChuongTrinhDaoTao).Data, "IdchuongTrinh", "TenChuongTrinh");
                return View(khoaHoc);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("IdkhoaHoc,TenKhoaHoc,SoGio,SoTuan,HocPhi,Mota,HinhAnh,IdchuongTrinh")] KhoaHocDTO model)
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
                    (ConstantValues.ChuongTrinhDaoTao.DanhSachChuongTrinhDaoTao).Data, "IdchuongTrinh", "TenChuongTrinh", model.IdchuongTrinh);
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

                var url = string.Format(ConstantValues.KhoaHoc.ChiTietKhoaHoc, id);
                var khoaHoc = Utilities.SendDataRequest<KhoaHocDTO>(url).Data;
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
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var url = string.Format(ConstantValues.KhoaHoc.ChiTietKhoaHoc, id);
                var khoaHoc = Utilities.SendDataRequest<KhoaHocDTO>(url).Data;
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
            var khoaHoc = Utilities.SendDataRequest<bool>(url).Data;
            if (khoaHoc != true) return false;
            else return true;
        }
    }
}
