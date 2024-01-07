using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITCMS_HUIT.Client.ViewComponents
{
    [ViewComponent(Name = "GiaoVien")]
    public class GiaoVien : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var dsGiaoVien = Utilities.SendDataRequest<List<GiaoVienDTO>>
               (ConstantValues.GiaoVien.DanhSach).Data!.Take(8);
            return View(dsGiaoVien);
        }
    }
}
