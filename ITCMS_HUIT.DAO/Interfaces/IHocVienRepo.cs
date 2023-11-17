using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.Repository.Interfaces
{
    public interface IHocVienRepo : IBaseRepository<HocVien>
    {
        HocVien GetById(int id);
        List<HocVien> Search(string keyword);
        List<HocVien> GetAll();
        bool IsExist(int id);
        int Count();
    }
}
