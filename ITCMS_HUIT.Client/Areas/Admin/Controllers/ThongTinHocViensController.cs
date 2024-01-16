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
                var url1 = string.Format(ConstantValues.ThongTinHocVien.DanhSachHocVienTheoGiaoVien, id);
                ViewData["DanhSachLopHoc"] = Utilities.SendDataRequest<List<ThongTinHocVienDTO>>(url1).Data
                    .Select(m => new LopHocDTO { IdlopHoc = m.IdlopHoc, TenLopHoc = m.IdlopHocNavigation.TenLopHoc }).Distinct().ToList();

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
                ViewData["DanhSachLopHoc"] = Utilities.SendDataRequest<List<LopHocDTO>>
                     (ConstantValues.LopHoc.DanhSach).Data
                     .Select(m => new LopHocDTO { IdlopHoc = m.IdlopHoc, TenLopHoc = m.TenLopHoc }).Distinct().ToList();
                var thongTinHocViens = Utilities.SendDataRequest<List<ThongTinHocVienDTO>>(ConstantValues.ThongTinHocVien.DanhSach).Data;
                var pagedList = thongTinHocViens.ToPagedList(pageNo, 5);
                return View(pagedList);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //public ActionResult ExportExcel()
        //{
        //    try
        //    {
        //        ValueTuple<MemoryStream, string> result = Utilities.SendDataRequestExcel(ConstantValues.ThongTinHocVien.Export);

        //        if (result.Item1 != null && !string.IsNullOrEmpty(result.Item2))
        //        {
        //            return new FileStreamResult(result.Item1, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        //            {
        //                FileDownloadName = result.Item2,
        //            };
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        public ActionResult ExportExcelById(int id)
        {
            try
            {
                var url = string.Format(ConstantValues.ThongTinHocVien.ExportById, id);
                var result = Utilities.SendDataRequestExcel(url);
                
                if (result.Item1 != null && !string.IsNullOrEmpty(result.Item2))
                {
                    return new FileStreamResult(result.Item1, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = result.Item2,
                    };
                }
                else
                {
                    return BadRequest("Không thể xuất Excel cho ID đã cho.");
                }
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
                return ReturnToIndex();
            }
        }

        //public ActionResult Create()
        //{
        //    ViewData["IdHocVien"] = new SelectList(Utilities.SendDataRequest<List<HocVienDTO>>
        //           (ConstantValues.HocVien.DanhSach).Data, "IdhocVien", "TenHocVien");
        //    ViewData["IdLopHoc"] = new SelectList(Utilities.SendDataRequest<List<LopHocDTO>>
        //        (ConstantValues.LopHoc.DanhSach).Data, "IdlopHoc", "TenLopHoc");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind("IdhocVien,IdlopHoc,Diem,NgayThongBao,TrangThaiThongBao,SoLanVangMat,LyDoThongBao,HocPhi,NgayGioGiaoDich,TrangThaiThanhToan")] ThongTinHocVienDTO model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            Utilities.SendDataRequest<ThongTinHocVienDTO>(ConstantValues.ThongTinHocVien.Them, model);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        ViewData["IdHocVien"] = new SelectList(Utilities.SendDataRequest<List<HocVienDTO>>
        //          (ConstantValues.HocVien.DanhSach).Data, "IdhocVien", "TenHocVien");
        //        ViewData["IdLopHoc"] = new SelectList(Utilities.SendDataRequest<List<LopHocDTO>>
        //            (ConstantValues.LopHoc.DanhSach).Data, "IdlopHoc", "TenLopHoc");
        //        return View(model);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}

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

        public RedirectToActionResult ReturnToIndex()
        {
            var sessionToken = Common.AppContext.Current!.Session.Get<string>("Token");
            var idGiaoVien = sessionToken!.GetTeacherIdFromJwt();
            var userRoles = sessionToken!.GetRolesFromJwt();
              
            if(userRoles.Contains("Admin"))
                return RedirectToAction(nameof(IndexAdmin));

            
            return RedirectToAction(nameof(Index), new {id=idGiaoVien});
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
				TempData["UpdatedUnsuccessfully"] = "Có lỗi xảy ra khi cập nhật thông tin học viên.";
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
						TempData["UpdatedSuccessfully"] = "Thông tin học viên đã được cập nhật thành công.";

                        return ReturnToIndex();

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
				}

				TempData["UpdatedUnsuccessfully"] = "Có lỗi xảy ra khi cập nhật thông tin học viên.";
				return View(model);
			}
			catch (Exception)
			{
				TempData["UpdatedUnsuccessfully"] = "Có lỗi xảy ra khi cập nhật thông tin học viên.";
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
				var thongTinHocVien = Utilities.SendDataRequest<ThongTinHocVienDTO>(url).Data;
				if (thongTinHocVien == null)
				{
					return NotFound();
				}

				return View(thongTinHocVien);
			}
			catch (Exception)
			{
				TempData["DeletedUnsuccessfully"] = "Có lỗi xảy ra khi xóa học viên.";
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

				TempData["DeletedSuccessfully"] = "Học viên đã được xóa thành công.";
                return ReturnToIndex();
            }
			catch (Exception)
			{
				TempData["DeletedUnsuccessfully"] = "Có lỗi xảy ra khi xóa học viên.";
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
