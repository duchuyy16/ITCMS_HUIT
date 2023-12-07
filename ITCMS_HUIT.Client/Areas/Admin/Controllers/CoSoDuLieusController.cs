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
        public IActionResult Index(int pageNo = 1)
        {
            try
            {
                var backupFiles = Utilities.SendDataRequest<List<string>>(ConstantValues.CoSoDuLieu.BackupFiles);
                var pagedList = backupFiles.ToPagedList(pageNo, 5);
                return View(pagedList);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Lỗi truy xuất tập tin sao lưu. Vui lòng thử lại sau.";
                return View();
            }
        }

        public IActionResult Backup()
        {
            try
            {
                var backup = Utilities.SendDataRequest<List<string>>(ConstantValues.CoSoDuLieu.Backup);
                return View(backup);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Lỗi khi thực hiện sao lưu. Vui lòng thử lại sau.";
                return View();
            }
        }

        public IActionResult Restore(string backupFileName)
        {
            try
            {
                
                var url = string.Format(ConstantValues.CoSoDuLieu.Restore, backupFileName);
                var restore = Utilities.SendDataRequest<bool>(url);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Lỗi khôi phục bản sao lưu. Vui lòng thử lại sau.";
                return View();
            }
        }

    }
}
