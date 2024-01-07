using System.ComponentModel.DataAnnotations;

namespace ITCMS_HUIT.Client.Models
{
    public class ThongTinHocVienDTO
    {
        public int IdhocVien { get; set; }
        public int IdlopHoc { get; set; }
        [Range(0, 10.0, ErrorMessage = "Giá trị của Diem phải nằm trong khoảng từ 0 đến 10.")]
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
		[Range(0, 10.0, ErrorMessage = "Giá trị của Diem phải nằm trong khoảng từ 0 đến 10.")]
		public decimal? Diem { get; set; }
        public DateTime? NgayThongBao { get; set; }
        public bool TrangThaiThongBao { get; set; }
        public int? SoLanVangMat { get; set; }
        public string? LyDoThongBao { get; set; }
        public decimal? HocPhi { get; set; }
        public DateTime NgayGioGiaoDich { get; set; }
        public bool? TrangThaiThanhToan { get; set; }
    }
}
