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
    public class AccountController : Controller
    {
        public ActionResult Index(int pageNo = 1)
        {
            try
            {
                var dsNguoiDung = Utilities.SendDataRequest<List<UserViewDTO>>(ConstantValues.Account.DanhSachNguoiDung);
                var pagedList = dsNguoiDung.Data.ToPagedList(pageNo, 5);
                return View(pagedList);
            }
            catch
            {
                return NotFound();
            }
        }

		public ActionResult Edit(string? userId)
		{
			try
			{
				if (userId == null)
					return NotFound();

				var url = string.Format(ConstantValues.Account.ChiTietNguoiDung, userId);
				var user = Utilities.SendDataRequest<UserViewDTO>(url);

				if (user == null)
					return NotFound();

				return View(user);
			}
			catch
			{
				TempData["UpdatedUnsuccessfully"] = "Có lỗi xảy ra khi truy cập trang chỉnh sửa.";
				return BadRequest();
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(string? userId, [Bind("UserId,UserName,Email,Roles")] UserViewDTO model)
		{
			try
			{
				if (userId != model.UserId)
					return NotFound();

				List<string> originalRoles = model.Roles != null ? new List<string>(model.Roles) : new List<string>();

				string newRole = Request.Form["newRole"];

				if (!string.IsNullOrEmpty(newRole))
					model.Roles!.AddRange(newRole.Split(',').Select(role => role.Trim()).ToList());

				if (ModelState.IsValid)
				{
					var success = Utilities.SendDataRequest<List<string>>(ConstantValues.Account.CapNhat, model);
					if (success != null)
					{
						TempData["UpdatedSuccessfully"] = "Cập nhật thành công!";
						return RedirectToAction(nameof(Index));
					}
					else
					{
						model.Roles = originalRoles;
						ModelState.AddModelError("Lỗi_" + Guid.NewGuid(), "Danh sách người dùng mới không hợp lệ!");
						TempData["UpdatedUnsuccessfully"] = "Cập nhật không thành công. Đã xảy ra lỗi.";
					}
				}

				return View(model);
			}
			catch
			{
				TempData["UpdatedUnsuccessfully"] = "Có lỗi xảy ra khi thực hiện cập nhật người dùng.";
				return BadRequest();
			}
		}


	}
}
