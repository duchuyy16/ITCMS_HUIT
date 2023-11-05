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
    public class LopHocRepo : BaseRepository<LopHoc>, ILopHocRepo
    {
        public LopHocRepo(ITCMS_HUITContext context) : base(context)
        {
        }

        public List<LopHoc> GetAll()
        {
            return _context.LopHocs.Include(i=>i.IdgiaoVienNavigation).Include(k=>k.IdkhoaHocNavigation).Include(t=>t.ThongTinHocViens).ToList();
        }

        public List<LopHoc> GetAllByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public LopHoc GetById(int id)
        {
            return _context.LopHocs.Include(i => i.IdgiaoVienNavigation).Include(k => k.IdkhoaHocNavigation).Include(t => t.ThongTinHocViens).SingleOrDefault(l => l.IdlopHoc == id)!;
        }

        public LopHoc GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public List<LopHoc> Search(string keyword)
        {
            return _context.LopHocs.Include(i => i.IdgiaoVienNavigation).Include(k => k.IdkhoaHocNavigation).Include(t => t.ThongTinHocViens).Where(s=>s.TenLopHoc == keyword).ToList();
        }
    }
}
