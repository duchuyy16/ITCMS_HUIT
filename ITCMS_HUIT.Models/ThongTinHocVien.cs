using System;
using System.Collections.Generic;

namespace ITCMS_HUIT.Models
{
    public partial class ThongTinHocVien
    {
        public int IdhocVien { get; set; }
        public int IdlopHoc { get; set; }
        public decimal? Diem { get; set; }
        public DateTime? NgayThongBao { get; set; }
        public bool? TrangThaiThongBao { get; set; }
        public int? SoLanVangMat { get; set; }
        public string? LyDoThongBao { get; set; }
        public decimal? HocPhi { get; set; }
        public DateTime? NgayGioGiaoDich { get; set; }
        public bool? TrangThaiThanhToan { get; set; }

        public virtual HocVien? IdhocVienNavigation { get; set; } = null!;
        public virtual LopHoc? IdlopHocNavigation { get; set; } = null!;
    }
}
