using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.Repository.Interfaces
{
    public interface IThongTinHocVienRepo: IBaseRepository<ThongTinHocVien>
    {
        List<ThongTinHocVien> GetAll();
        List<ThongTinHocVien> Search(string tenHocVien);
        List<ThongTinHocVien> GetAllByUserId(string id);
        ThongTinHocVien GetById(int idHocVien, int idLopHoc);
        ThongTinHocVien GetByIdHocVien(int idHocVien);
        List<ThongTinHocVien> GetAllByIdLopHoc(int idLopHoc);
        bool IsExist(int idHocVien,int idLopHoc);
    }
}
