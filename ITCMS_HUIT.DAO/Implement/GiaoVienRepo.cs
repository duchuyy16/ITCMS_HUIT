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
    public class GiaoVienRepo : BaseRepository<GiaoVien>, IGiaoVienRepo
    {
        public GiaoVienRepo(ITCMS_HUITContext context) : base(context)
        {
        }

        //public GiaoVien GetById(string id)
        //{
        //    return _context.GiaoViens.Where(g => g.IdgiaoVien == id).ToList().Select(s => 
        //    {
        //        s.LopHocs = _context.LopHocs.Where(w => w.IdgiaoVien == id).ToList();
        //        return s!;
        //    }).SingleOrDefault()!;
        //}

        public GiaoVien GetById(string id)
        {
            return _context.GiaoViens.FirstOrDefault(g => g.IdgiaoVien == id)!;
        }

        public List<GiaoVien> GetAll()
        {
            return _context.GiaoViens.ToList();
        }

        public bool IsExist(string id)
        {
            return _context.GiaoViens.Any(g=>g.IdgiaoVien == id);
        }

        public int Count()
        {
            return _context.GiaoViens.Count();
        }

		public List<GiaoVien> Search(string tenGiaoVien)
		{
			return _context.GiaoViens.Where(x=>x.TenGiaoVien.Contains(tenGiaoVien)).ToList();
		}
	}
}
