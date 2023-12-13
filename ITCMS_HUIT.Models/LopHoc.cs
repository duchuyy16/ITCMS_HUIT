using System;
using System.Collections.Generic;

namespace ITCMS_HUIT.Models
{
    public partial class LopHoc
    {
        public LopHoc()
        {
            ThongTinHocViens = new HashSet<ThongTinHocVien>();
        }

        public int IdlopHoc { get; set; }
        public string TenLopHoc { get; set; } = null!;
        public string ThoiGian { get; set; } = null!;
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string DiaDiem { get; set; } = null!;
        public int IdkhoaHoc { get; set; }
        public string? IdgiaoVien { get; set; }
        public string? PhongHoc { get; set; }

        public virtual GiaoVien? IdgiaoVienNavigation { get; set; }
        public virtual KhoaHoc IdkhoaHocNavigation { get; set; } = null!;
        public virtual ICollection<ThongTinHocVien> ThongTinHocViens { get; set; }
    }
}
