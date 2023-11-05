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
    }
}
