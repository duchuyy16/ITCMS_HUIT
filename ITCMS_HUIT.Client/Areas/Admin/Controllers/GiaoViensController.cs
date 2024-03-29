﻿using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using X.PagedList;

namespace ITCMS_HUIT.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckToken("Teacher,Admin")]
    public class GiaoViensController : Controller
    {
        public ActionResult Index(int pageNo = 1)
        {
            try
            {
                var dsGiaoVien = Utilities.SendDataRequest<List<GiaoVienDTO>>(ConstantValues.GiaoVien.DanhSach).Data;
                var pagedList = dsGiaoVien.ToPagedList(pageNo, 5);
                return View(pagedList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
		public ActionResult TimKiemGiaoVien(string tenGiaoVien, int pageNo = 1)
		{
			if (tenGiaoVien != null)
			{
				var url = string.Format(ConstantValues.GiaoVien.TimKiem, tenGiaoVien);
				var giaoVien = Utilities.SendDataRequest<List<GiaoVienDTO>>(url).Data;
				var pagedList = giaoVien.ToPagedList(pageNo, 5);
				return View(pagedList);
			}
			else
			{
                return RedirectToAction(nameof(Index));
            }
		}

		//public ActionResult Create()
  //      {
  //          try
  //          {
  //              return View();
  //          }
  //          catch (Exception)
  //          {
  //              return BadRequest();
  //          }

  //      }

  //      [HttpPost]
  //      [ValidateAntiForgeryToken]
  //      public ActionResult Create([Bind("IdgiaoVien,TenGiaoVien,TrinhDo,ChungChi,HinhAnh,HoSoCaNhan")] GiaoVienDTO model)
  //      {
  //          try
  //          {
  //              if (ModelState.IsValid)
  //              {
  //                  if (IsExist(model.IdgiaoVien!))
  //                  {
  //                      ModelState.AddModelError("IdgiaoVien", "Mã giáo viên đã tồn tại. Vui lòng nhập mã khác.");
  //                      return View(model);
  //                  }

  //                  Utilities.SendDataRequest<GiaoVienDTO>(ConstantValues.GiaoVien.Them, model);
  //                  return RedirectToAction(nameof(Index));
  //              }
  //              return View(model);
  //          }
  //          catch (Exception)
  //          {
  //              return BadRequest();
  //          }
  //      }

        //[HttpGet]
        //[Route("Details/{id?}")]
        public ActionResult Details(string? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.GiaoVien.ChiTietGiaoVien, id);
                var giaoVien = Utilities.SendDataRequest<GiaoVienDTO>(url).Data;
                if (giaoVien == null)
                {
                    return NotFound();
                }

                //return StatusCode((int)HttpStatusCode.Conflict);

                return View(giaoVien);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

		public ActionResult Edit(string? id)
		{
			try
			{
				if (id == null)
				{
					return NotFound();
				}

				var url = string.Format(ConstantValues.GiaoVien.ChiTietGiaoVien, id);
				var giaoVien = Utilities.SendDataRequest<GiaoVienDTO>(url).Data;
				if (giaoVien == null)
				{
					return NotFound();
				}

				return View(giaoVien);
			}
			catch (Exception)
			{
				TempData["UpdatedUnsuccessfully"] = "Có lỗi xảy ra khi cập nhật thông tin giáo viên.";
				return BadRequest();
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(string id, [Bind("IdgiaoVien,TenGiaoVien,TrinhDo,ChungChi,HinhAnh,HoSoCaNhan")] GiaoVienDTO model, IFormFile? imageFile)
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
						Utilities.FromData<bool>(ConstantValues.GiaoVien.CapNhat, model, imageFile);
						TempData["UpdatedSuccessfully"] = "Thông tin giáo viên đã được cập nhật thành công.";
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

				TempData["UpdatedUnsuccessfully"] = "Có lỗi xảy ra khi cập nhật thông tin giáo viên.";
				return View(model);
			}
			catch (Exception)
			{
				TempData["UpdatedUnsuccessfully"] = "Có lỗi xảy ra khi cập nhật thông tin giáo viên.";
				return BadRequest();
			}
		}


		public ActionResult Delete(string? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.GiaoVien.ChiTietGiaoVien, id);
                var giaoVien = Utilities.SendDataRequest<GiaoVienDTO>(url).Data;
                if (giaoVien == null)
                {
                    return NotFound();
                }

                return View(giaoVien);
            }
            catch (Exception)
            {
                TempData["DeletedUnsuccessfully"] = "Xóa không thành công. Đã xảy ra lỗi.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string? id)
        {
            try
            {
                var url = string.Format(ConstantValues.GiaoVien.ChiTietGiaoVien, id);
                var giaoVien = Utilities.SendDataRequest<GiaoVienDTO>(url).Data;
                Utilities.SendDataRequest<bool>(ConstantValues.GiaoVien.Xoa, giaoVien);

                TempData["DeletedSuccessfully"] = "Xóa thành công.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["DeletedUnsuccessfully"] = "Xóa không thành công. Đã xảy ra lỗi.";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool IsExist(string id)
        {
            var url = string.Format(ConstantValues.GiaoVien.KiemTraTonTai, id);
            var giaoVien = Utilities.SendDataRequest<bool>(url).Data;
            if (giaoVien != true) return false;
            else return true;
        }
    }
}
