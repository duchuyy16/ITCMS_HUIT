using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Base;
using ITCMS_HUIT.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public ThongTinHocVien GetByIdHocVien(int idHocVien)
        {
            return _context.ThongTinHocViens
                .Include(i => i.IdhocVienNavigation)
                .FirstOrDefault(w => w.IdhocVien == idHocVien)!;
        }

        public bool IsExist(int idHocVien, int idLopHoc)
        {
            return _context.ThongTinHocViens.Any(w => w.IdhocVien == idHocVien && w.IdlopHoc == idLopHoc);
        }

        public List<ThongTinHocVien> Search(string tenHocVien)
        {
            //return _context.ThongTinHocViens
            //   .Include(i => i.IdhocVienNavigation)
            //   .Include(l => l.IdlopHocNavigation)
            //   .Where(w => w.IdhocVienNavigation.Email == email.Trim())
            //   .ToList();

            return _context.ThongTinHocViens
                        .Include(i => i.IdhocVienNavigation)
                        .Include(l => l.IdlopHocNavigation)
                        .Where(w => w.IdhocVienNavigation.TenHocVien.Contains(tenHocVien))
                        .ToList();
        }
	}
}
