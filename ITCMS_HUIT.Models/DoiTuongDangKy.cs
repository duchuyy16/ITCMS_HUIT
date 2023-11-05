using System;
using System.Collections.Generic;

namespace ITCMS_HUIT.Models
{
    public partial class DoiTuongDangKy
    {
        public DoiTuongDangKy()
        {
            HocViens = new HashSet<HocVien>();
        }

        public int IddoiTuong { get; set; }
        public string DoiTuongDangKy1 { get; set; } = null!;

        public virtual ICollection<HocVien> HocViens { get; set; }
    }
}
