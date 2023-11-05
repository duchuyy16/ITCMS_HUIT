using System;
using System.Collections.Generic;

namespace ITCMS_HUIT.Models
{
    public partial class HocVien
    {
        public HocVien()
        {
            ThongTinHocViens = new HashSet<ThongTinHocVien>();
        }

        public int IdhocVien { get; set; }
        public string TenHocVien { get; set; } = null!;
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; } = null!;
        public int Sdt { get; set; }
        public string DiaChi { get; set; } = null!;
        public DateTime? NgayDangKy { get; set; }
        public int IddoiTuong { get; set; }
        public int IdtrangThai { get; set; }

        public virtual DoiTuongDangKy IddoiTuongNavigation { get; set; } = null!;
        public virtual TrangThaiHocVien IdtrangThaiNavigation { get; set; } = null!;
        public virtual ICollection<ThongTinHocVien> ThongTinHocViens { get; set; }
    }
}
