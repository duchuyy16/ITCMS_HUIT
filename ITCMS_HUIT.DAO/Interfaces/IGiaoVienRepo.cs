using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.Repository.Interfaces
{
    public interface IGiaoVienRepo : IBaseRepository<GiaoVien>
    {
        List<GiaoVien> GetAll();
        GiaoVien GetById(string id);
        bool IsExist(string id);
        int Count();
    }
}
