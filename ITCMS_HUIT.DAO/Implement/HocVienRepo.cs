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
    public class HocVienRepo : BaseRepository<HocVien> , IHocVienRepo
    {
        public HocVienRepo(ITCMS_HUITContext context) : base(context)
        {
        }

        public int Count()
        {
            return _context.HocViens.Count();
        }

        public List<HocVien> GetAll()
        {
            return _context.HocViens.Include(i=>i.IddoiTuongNavigation)
                .Include(t => t.IdtrangThaiNavigation)
                .ToList();
        }

        public HocVien GetById(int id)
        {
            return _context.HocViens.Include(i => i.IddoiTuongNavigation)
                .Include(t => t.IdtrangThaiNavigation)
                .FirstOrDefault(w => w.IdhocVien==id)!;
        }

        public bool IsEmailExist(string email)
        {
            return _context.HocViens.Any(g => g.Email == email);
        }

        public bool IsExist(int id)
        {
            return _context.HocViens.Any(g => g.IdhocVien == id);
        }

        public List<HocVien> Search(string keyword)
        {
            return _context.HocViens.Include(i => i.IddoiTuongNavigation)
                .Include(t => t.IdtrangThaiNavigation)
                .Where(w => w.TenHocVien.Contains(keyword))
                .ToList();
        }
    }
}
