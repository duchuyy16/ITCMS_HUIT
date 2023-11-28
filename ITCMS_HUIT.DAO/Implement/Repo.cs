using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.Repository.Implement
{
    public class Repo : IRepo
    {
        private readonly ITCMS_HUITContext _context;
        public Repo(ITCMS_HUITContext context)
        {
            _context = context;
        }

        public ILopHocRepo LopHocRepo => new LopHocRepo(_context);

        public IChuongTrinhDaoTaoRepo ChuongTrinhDaoTaoRepo => new ChuongTrinhDaoTaoRepo(_context);
        
        public IKhoaHocRepo KhoaHocRepo => new KhoaHocRepo(_context);

        public IGiaoVienRepo GiaoVienRepo => new GiaoVienRepo(_context);

        public IThongTinHocVienRepo ThongTinHocVienRepo => new ThongTinHocVienRepo(_context);

        public IHocVienRepo HocVienRepo => new HocVienRepo(_context);

        public IDoiTuongDangKyRepo DoiTuongDangKyRepo => new DoiTuongDangKyRepo(_context);

        public ITrangThaiHocVienRepo TrangThaiHocVienRepo => new TrangThaiHocVienRepo(_context);
    }
}
