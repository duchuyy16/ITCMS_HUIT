using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.Repository.Interfaces
{
    public interface ILopHocRepo : IBaseRepository<LopHoc>
    {
        List<LopHoc> GetAll();
        List<LopHoc> GetAllByUserId(int userId);
        List<LopHoc> Search(string keyword);
        LopHoc GetById(int id);
        LopHoc GetByUserId(int userId);
        bool IsExist(int id);
        int Count();
    }
}
