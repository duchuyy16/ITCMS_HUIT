using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace ITCMS_HUIT.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckToken("Admin")]
    public class CoSoDuLieusController : Controller
    {
        public ActionResult Index(int pageNo = 1)
        {
            try
            {
                var backupFiles = Utilities.SendDataRequest<List<string>>(ConstantValues.CoSoDuLieu.BackupFiles).Data;
                var pagedList = backupFiles.ToPagedList(pageNo, 5);
                return View(pagedList);
            }
            catch 
            {
                ViewBag.ErrorMessage = "Lỗi truy xuất tập tin sao lưu. Vui lòng thử lại sau.";
                return View();
            }
        }

        public ActionResult Backup()
        {
            try
            {
                var backup = Utilities.SendDataRequest<List<string>>(ConstantValues.CoSoDuLieu.Backup);
				if (backup!=null)
				{
					TempData["BackupSuccessMessage"] = "Sao lưu thành công!";
				}
				else
				{
					TempData["BackupErrorMessage"] = "Sao lưu thất bại. Vui lòng thử lại sau.";
				}
				return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorMessage = "Lỗi khi thực hiện sao lưu. Vui lòng thử lại sau.";
                return View();
            }
        }

        public ActionResult Restore(string backupFileName)
        {
            try
            {      
                var url = string.Format(ConstantValues.CoSoDuLieu.Restore, backupFileName);
                var restore = Utilities.SendDataRequest<bool>(url).Data;
				if (restore)
				{
					TempData["RestoreSuccessMessage"] = "Phục hồi thành công!";
				}
				else
				{
					TempData["RestoreErrorMessage"] = "Phục hồi thất bại. Vui lòng thử lại sau.";
				}
				return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.ErrorMessage = "Lỗi khôi phục bản sao lưu. Vui lòng thử lại sau.";
                return View();
            }
        }

    }
}
