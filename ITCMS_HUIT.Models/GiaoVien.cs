using System;
using System.Collections.Generic;

namespace ITCMS_HUIT.Models
{
    public partial class GiaoVien
    {
        public GiaoVien()
        {
            LopHocs = new HashSet<LopHoc>();
        }

        public string TenGiaoVien { get; set; } = null!;
        public string TrinhDo { get; set; } = null!;
        public string ChungChi { get; set; } = null!;
        public string HoSoCaNhan { get; set; } = null!;
        public string IdgiaoVien { get; set; } = null!;
        public string? HinhAnh { get; set; }

        public virtual ICollection<LopHoc> LopHocs { get; set; }
    }
}
