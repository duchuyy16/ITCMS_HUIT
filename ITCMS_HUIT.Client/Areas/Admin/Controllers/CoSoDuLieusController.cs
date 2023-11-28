using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITCMS_HUIT.Client.Areas.Admin.Controllers
{
    public class CoSoDuLieusController : Controller
    {
        public IActionResult Index()
        {
            var backupFiles= Utilities.SendDataRequest<List<BackupConfiguration>>(ConstantValues.CoSoDuLieu.BackupFiles);
            return View(backupFiles);
        }

        public IActionResult Backup()
        {
            var backup = Utilities.SendDataRequest<string>(ConstantValues.CoSoDuLieu.Backup);
            return View(backup);
        }

        public IActionResult Restore(string backupFileName)
        {
            var url = string.Format(ConstantValues.CoSoDuLieu.Restore, backupFileName);
            var restore = Utilities.SendDataRequest<bool>(url);
            return View(restore);
        }
    }
}
