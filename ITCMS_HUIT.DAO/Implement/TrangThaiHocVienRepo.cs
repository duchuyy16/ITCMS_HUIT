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
    public class TrangThaiHocVienRepo : BaseRepository<TrangThaiHocVien>, ITrangThaiHocVienRepo
    {
        public TrangThaiHocVienRepo(ITCMS_HUITContext context) : base(context)
        {
        }

        public List<TrangThaiHocVien> GetAll()
        {
            return _context.TrangThaiHocViens.ToList();
        }
    }
}
