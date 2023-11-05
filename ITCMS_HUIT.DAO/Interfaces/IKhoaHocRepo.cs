using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.Repository.Interfaces
{
    public interface IKhoaHocRepo: IBaseRepository<KhoaHoc>
    {
        KhoaHoc GetById(int id);
        List<KhoaHoc> GetByIdCTDD(int chuongTrinhID);
        List<KhoaHoc> GetAll();
        List<KhoaHoc> Search(string keyword);
    }
}
