using System;
using System.Collections.Generic;

namespace ITCMS_HUIT.Models
{
    public partial class TrangThaiHocVien
    {
        public TrangThaiHocVien()
        {
            HocViens = new HashSet<HocVien>();
        }

        public int IdtrangThai { get; set; }
        public string TenTrangThai { get; set; } = null!;
        public string MoTa { get; set; } = null!;

        public virtual ICollection<HocVien> HocViens { get; set; }
    }
}
