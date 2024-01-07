using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITCMS_HUIT.Client.ViewComponents
{
    [ViewComponent(Name = "MenuCourse")]
    public class MenuCourse : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var dsChuongTrinh = Utilities.SendDataRequest<List<ChuongTrinhDaoTaoDTO>>
               (ConstantValues.ChuongTrinhDaoTao.DanhSachChuongTrinhDaoTao).Data;
            return View(dsChuongTrinh);
        }
    }
}
