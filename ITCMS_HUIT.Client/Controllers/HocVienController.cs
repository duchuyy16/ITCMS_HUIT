using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITCMS_HUIT.Client.Controllers
{
	public class HocVienController : Controller
	{
        public IActionResult ThemHocVien()
        {
            try
            {
                ViewData["IdDoiTuong"] = new SelectList(Utilities.SendDataRequest<List<DoiTuongDangKyDTO>>
                    (ConstantValues.DoiTuongDangKy.DanhSachDoiTuongDangKy), "IddoiTuong", "DoiTuongDangKy1");
                
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemHocVien([Bind("TenHocVien,NgaySinh,Email,Sdt,DiaChi,IddoiTuong")] HocVienDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var idLopHoc = Request.RouteValues["idLopHoc"];
                    var url = string.Format(ConstantValues.HocVien.Them, idLopHoc);
                    var themHocVien = Utilities.SendDataRequest<HocVienDTO>(url,model);
            
                    TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng kiểm tra email để xác nhận.";
                    return RedirectToAction("Index", "Home");
                }
                ViewData["IdDoiTuong"] = new SelectList(Utilities.SendDataRequest<List<DoiTuongDangKyDTO>>
                    (ConstantValues.DoiTuongDangKy.DanhSachDoiTuongDangKy), model.IddoiTuong);

                return View("ThemHocVien", model);
            }
            catch 
            {
                return BadRequest();
            }
            
        }
        
    }
    
}
