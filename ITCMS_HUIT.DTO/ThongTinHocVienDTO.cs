using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.DTO
{
    public class ThongTinHocVienDTO
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

        public HocVienModel? IdhocVienNavigation { get; set; } = null!;
        public LopHocModel? IdlopHocNavigation { get; set; } = null!;
    }

    public class ThongTinHocVienModel
    {
        public int IdhocVien { get; set; }
        public int IdlopHoc { get; set; }
        public decimal? Diem { get; set; }
        public DateTime? NgayThongBao { get; set; }
        public bool TrangThaiThongBao { get; set; }
        public int? SoLanVangMat { get; set; }
        public string? LyDoThongBao { get; set; }
        public decimal? HocPhi { get; set; }
        public DateTime? NgayGioGiaoDich { get; set; }
        public bool? TrangThaiThanhToan { get; set; }
    }
}
