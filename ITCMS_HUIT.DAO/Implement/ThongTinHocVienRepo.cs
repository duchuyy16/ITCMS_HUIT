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
    public class ThongTinHocVienRepo : BaseRepository<ThongTinHocVien>, IThongTinHocVienRepo
    {
        public ThongTinHocVienRepo(ITCMS_HUITContext context) : base(context)
        {
        }

        public List<ThongTinHocVien> GetAll()
        {
            return _context.ThongTinHocViens.Include(i=>i.IdhocVienNavigation).Include(l=>l.IdlopHocNavigation).ToList();
        }

        public ThongTinHocVien GetById(int idHocVien, int idLopHoc)
        {
            return _context.ThongTinHocViens
                .Include(i => i.IdhocVienNavigation)
                .Include(l => l.IdlopHocNavigation)
                .FirstOrDefault(w => w.IdhocVien == idHocVien && w.IdlopHoc == idLopHoc)!;
        }

        public bool IsExist(int idHocVien, int idLopHoc)
        {
            return _context.ThongTinHocViens.Any(w => w.IdhocVien == idHocVien && w.IdlopHoc == idLopHoc);
        }

        public List<ThongTinHocVien> Search(int idHocVien)
        {
            return _context.ThongTinHocViens.Include(i => i.IdhocVienNavigation).Include(l => l.IdlopHocNavigation).Where(w => w.IdhocVien==idHocVien).ToList();
        }
    }
}
