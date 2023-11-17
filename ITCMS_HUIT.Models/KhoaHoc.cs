using System;
using System.Collections.Generic;

namespace ITCMS_HUIT.Models
{
    public partial class KhoaHoc
    {
        public KhoaHoc()
        {
            LopHocs = new HashSet<LopHoc>();
        }

        public int IdkhoaHoc { get; set; }
        public int IdchuongTrinh { get; set; }
        public string? TenKhoaHoc { get; set; }
        public int SoGio { get; set; }
        public int SoTuan { get; set; }
        public decimal HocPhi { get; set; }
        public string? Mota { get; set; }
        public string? HinhAnh { get; set; }

        public virtual ChuongTrinhDaoTao IdchuongTrinhNavigation { get; set; } = null!;
        public virtual ICollection<LopHoc> LopHocs { get; set; }
    }
}
