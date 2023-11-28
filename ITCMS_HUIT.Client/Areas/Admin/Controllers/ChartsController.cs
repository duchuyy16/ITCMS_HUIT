using ITCMS_HUIT.Client.Common;
using Microsoft.AspNetCore.Mvc;

namespace ITCMS_HUIT.Client.Areas.Admin.Controllers
{
    public class ChartsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //public List<StatisticModel> Statistic()
        //{
        //    var productStatistics = Utilities.SendDataRequest<List<StatisticModel>>(ConstantValues.Product.ProductStatistics);
        //    return productStatistics;
        //}
    }
}
