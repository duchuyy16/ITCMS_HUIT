using ITCMS_HUIT.Client.Common;
using ITCMS_HUIT.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITCMS_HUIT.Client.Components
{
    [ViewComponent(Name ="Menu")]
    public class Menu:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var dsChuongTrinh = Utilities.SendDataRequest<List<ChuongTrinhDaoTaoDTO>>
               (ConstantValues.ChuongTrinhDaoTao.DanhSachChuongTrinhDaoTao);
            return View(dsChuongTrinh);
        }
    }
}
