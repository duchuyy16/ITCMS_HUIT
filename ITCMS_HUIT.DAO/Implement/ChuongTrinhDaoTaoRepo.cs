using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Base;
using ITCMS_HUIT.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.Repository.Implement
{
    public class ChuongTrinhDaoTaoRepo : BaseRepository<ChuongTrinhDaoTao>, IChuongTrinhDaoTaoRepo
    {
        public ChuongTrinhDaoTaoRepo(ITCMS_HUITContext context) : base(context)
        {
        }

        public int Count()
        {
            return _context.ChuongTrinhDaoTaos.Count();
        }

        public List<ChuongTrinhDaoTao> GetAll()
        {
            return _context.ChuongTrinhDaoTaos.ToList();
        }
    }
}
