using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Base;
using ITCMS_HUIT.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.Repository.Implement
{
    public class KhoaHocRepo : BaseRepository<KhoaHoc>, IKhoaHocRepo
    {
        public KhoaHocRepo(ITCMS_HUITContext context) : base(context)
        {
        }

        public List<KhoaHoc> GetAll()
        {
            return _context.KhoaHocs.Include(c => c.IdchuongTrinhNavigation).Include(l=>l.LopHocs).ToList();
        }


        public KhoaHoc GetById(int id)
        {
            return _context.KhoaHocs.Include(c => c.IdchuongTrinhNavigation).Include(i => i.LopHocs).FirstOrDefault(f=>f.IdkhoaHoc==id)!;
        }

        public List<KhoaHoc> Search(string keyword)
        {
            return _context.KhoaHocs.Include(c => c.IdchuongTrinhNavigation).Include(i => i.LopHocs).Where(w=>w.TenKhoaHoc!.Contains(keyword)).ToList();
        }

        public List<KhoaHoc> GetByIdCTDT(int chuongTrinhID)
        {
            return _context.KhoaHocs.Include(c => c.IdchuongTrinhNavigation).Include(i => i.LopHocs).Where(w => w.IdchuongTrinh == chuongTrinhID).ToList();
        }

        public bool IsExist(int id)
        {
            return _context.KhoaHocs.Any(g => g.IdkhoaHoc == id);
        }
    }
}
