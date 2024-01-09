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
        public ActionResult Index(string id,int pageNo = 1, int? idKhoaHoc = null)
        {
            try
            {
                var dsKhoaHoc = Utilities.SendDataRequest<List<KhoaHocDTO>>(ConstantValues.KhoaHoc.DanhSach).Data;
                dsKhoaHoc!.Insert(0, new KhoaHocDTO { IdkhoaHoc = 0, TenKhoaHoc = "----------Chọn tên khóa học----------" });
                ViewBag.IdKhoaHoc = new SelectList(dsKhoaHoc, "IdkhoaHoc", "TenKhoaHoc", idKhoaHoc);

                var url = string.Format(ConstantValues.LopHoc.DanhSachLopHocTheoGiaoVien, id);
                var dsLopHoc = Utilities.SendDataRequest<List<LopHocDTO>>(url).Data;
                if (idKhoaHoc.HasValue)
                {
                    dsLopHoc = dsLopHoc!.Where(c => c.IdkhoaHoc == idKhoaHoc).ToList();
                }

                var pagedList = dsLopHoc.ToPagedList(pageNo, 5);

                return View(pagedList);
            }
            catch
            {
                return NotFound();
            }
        }

		public ActionResult IndexAdmin(int pageNo = 1, int? idKhoaHoc = null)
		{
			try
			{
				var dsKhoaHoc = Utilities.SendDataRequest<List<KhoaHocDTO>>(ConstantValues.KhoaHoc.DanhSach);
				dsKhoaHoc.Data!.Insert(0, new KhoaHocDTO { IdkhoaHoc = 0, TenKhoaHoc = "----------Chọn tên khóa học----------" });
				ViewBag.IdKhoaHoc = new SelectList(dsKhoaHoc.Data, "IdkhoaHoc", "TenKhoaHoc", idKhoaHoc);

				var dsLopHoc = Utilities.SendDataRequest<List<LopHocDTO>>(ConstantValues.LopHoc.DanhSach).Data;
				if (idKhoaHoc.HasValue)
				{
					dsLopHoc = dsLopHoc!.Where(c => c.IdkhoaHoc == idKhoaHoc).ToList();
				}

				var pagedList = dsLopHoc.ToPagedList(pageNo, 5);

				return View(pagedList);
			}
			catch
			{
				return NotFound();
			}
		}

		public ActionResult Create()
		{
			try
			{
				ViewData["IdGiaoVien"] = new SelectList(Utilities.SendDataRequest<List<GiaoVienDTO>>(
					ConstantValues.GiaoVien.DanhSach).Data, "IdgiaoVien", "TenGiaoVien");
				ViewData["IdKhoaHoc"] = new SelectList(Utilities.SendDataRequest<List<KhoaHocDTO>>(
					ConstantValues.KhoaHoc.DanhSach).Data, "IdkhoaHoc", "TenKhoaHoc");
				return View();
			}
			catch (Exception)
			{
				TempData["AddedUnsuccessfully"] = "Có lỗi xảy ra khi thêm thông tin lớp học.";
				return BadRequest();
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind("TenLopHoc,ThoiGian,NgayBatDau,NgayKetThuc,DiaDiem,IdkhoaHoc,IdgiaoVien,PhongHoc")] LopHocDTO model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Utilities.SendDataRequest<LopHocDTO>(ConstantValues.LopHoc.Them, model);
					TempData["AddedSuccessfully"] = "Lớp học đã được thêm thành công.";
					return RedirectToAction(nameof(Index));
				}

				ViewData["IdGiaoVien"] = new SelectList(Utilities.SendDataRequest<List<GiaoVienDTO>>(
					ConstantValues.GiaoVien.DanhSach).Data, "IdgiaoVien", "TenGiaoVien", model.IdgiaoVien);
				ViewData["IdKhoaHoc"] = new SelectList(Utilities.SendDataRequest<List<KhoaHocDTO>>(
					ConstantValues.KhoaHoc.DanhSach).Data, "IdkhoaHoc", "TenKhoaHoc", model.IdkhoaHoc);

				TempData["AddedUnsuccessfully"] = "Có lỗi xảy ra khi thêm thông tin lớp học.";
				return View(model);
			}
			catch (Exception)
			{
				TempData["AddedUnsuccessfully"] = "Có lỗi xảy ra khi thêm thông tin lớp học.";
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

                var url = string.Format(ConstantValues.LopHoc.ChiTietLopHoc, id);
                var LopHoc = Utilities.SendDataRequest<LopHocDTO>(url).Data;
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

		public ActionResult Edit(int? id)
		{
			try
			{
				if (id == null)
				{
					return NotFound();
				}

				var url = string.Format(ConstantValues.LopHoc.ChiTietLopHoc, id);
				var lopHoc = Utilities.SendDataRequest<LopHocDTO>(url).Data;
				if (lopHoc == null)
				{
					return NotFound();
				}

				ViewData["IdKhoaHoc"] = new SelectList(Utilities.SendDataRequest<List<KhoaHocDTO>>(
					ConstantValues.KhoaHoc.DanhSach).Data, "IdkhoaHoc", "TenKhoaHoc");
				ViewData["IdGiaoVien"] = new SelectList(Utilities.SendDataRequest<List<GiaoVienDTO>>(
					ConstantValues.GiaoVien.DanhSach).Data, "IdgiaoVien", "TenGiaoVien");

				return View(lopHoc);
			}
			catch (Exception)
			{
				TempData["UpdatedUnsuccessfully"] = "Có lỗi xảy ra khi cập nhật thông tin lớp học.";
				return BadRequest();
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, [Bind("IdlopHoc,TenLopHoc,ThoiGian,NgayBatDau,NgayKetThuc,DiaDiem,IdkhoaHoc,IdgiaoVien,PhongHoc")] LopHocDTO model)
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
						TempData["UpdatedSuccessfully"] = "Thông tin lớp học đã được cập nhật thành công.";
						return RedirectToAction("IndexAdmin");
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
				}

				ViewData["IdKhoaHoc"] = new SelectList(Utilities.SendDataRequest<List<KhoaHocDTO>>(
					ConstantValues.KhoaHoc.DanhSach).Data, "IdkhoaHoc", "TenKhoaHoc", model.IdkhoaHoc);
				ViewData["IdGiaoVien"] = new SelectList(Utilities.SendDataRequest<List<GiaoVienDTO>>(
					ConstantValues.GiaoVien.DanhSach).Data, "IdgiaoVien", "TenGiaoVien", model.IdgiaoVien);

				TempData["UpdatedUnsuccessfully"] = "Có lỗi xảy ra khi cập nhật thông tin lớp học.";
				return View(model);
			}
			catch (Exception)
			{
				TempData["UpdatedUnsuccessfully"] = "Có lỗi xảy ra khi cập nhật thông tin lớp học.";
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

				var url = string.Format(ConstantValues.LopHoc.ChiTietLopHoc, id);
				var lopHoc = Utilities.SendDataRequest<LopHocDTO>(url).Data;
				if (lopHoc == null)
				{
					return NotFound();
				}

				return View(lopHoc);
			}
			catch (Exception)
			{
				TempData["DeletedUnsuccessfully"] = "Có lỗi xảy ra khi xóa thông tin lớp học.";
				return BadRequest();
			}
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			try
			{
				var url = string.Format(ConstantValues.LopHoc.ChiTietLopHoc, id);
				var lopHoc = Utilities.SendDataRequest<LopHocDTO>(url).Data;
				Utilities.SendDataRequest<bool>(ConstantValues.LopHoc.Xoa, lopHoc);

				TempData["DeletedSuccessfully"] = "Lớp học đã được xóa thành công.";
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				TempData["DeletedUnsuccessfully"] = "Có lỗi xảy ra khi xóa thông tin lớp học.";
				return BadRequest();
			}
		}


		private bool IsExist(int id)
        {
            var url = string.Format(ConstantValues.LopHoc.KiemTraTonTai, id);
            var lopHoc = Utilities.SendDataRequest<bool>(url).Data;
            if (lopHoc != true) return false;
            else return true;
        }
    }
}
