using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.Repository.Interfaces
{
    public interface IRepo
    {
        ILopHocRepo LopHocRepo { get; }
        IChuongTrinhDaoTaoRepo ChuongTrinhDaoTaoRepo { get; }
        IKhoaHocRepo KhoaHocRepo { get; }
        IGiaoVienRepo GiaoVienRepo { get; }
        IThongTinHocVienRepo ThongTinHocVienRepo { get; }
        IHocVienRepo HocVienRepo { get; }
    }
}
