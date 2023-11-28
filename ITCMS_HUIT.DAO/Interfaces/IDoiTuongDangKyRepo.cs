using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.Repository.Interfaces
{
    public interface IDoiTuongDangKyRepo: IBaseRepository<DoiTuongDangKy>
    {
        List<DoiTuongDangKy> GetAll();
    }
}
