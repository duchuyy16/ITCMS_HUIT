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
        LopHoc GetById(int id);
        List<LopHoc> GetAllByUserId(int userId);
        LopHoc GetByUserId(int userId);
        List<LopHoc> Search(string keyword);
    }
}
