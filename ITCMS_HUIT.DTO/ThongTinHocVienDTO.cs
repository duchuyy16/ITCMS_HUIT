using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.DTO
{
    public class ThongTinHocVienDTO
    {
        public int IdhocVien { get; set; }
        public int IdlopHoc { get; set; }
        [Range(0, 10.0, ErrorMessage = "Giá trị của Diem phải nằm trong khoảng từ 0 đến 10.")]
        public decimal? Diem { get; set; }
        public DateTime? NgayThongBao { get; set; }
        public bool? TrangThaiThongBao { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Số lần vắng mặt không thể nhỏ hơn 0")]
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
        [Range(0, int.MaxValue, ErrorMessage = "Số lần vắng mặt không thể nhỏ hơn 0")]
        public int? SoLanVangMat { get; set; }
        public string? LyDoThongBao { get; set; }
        public decimal? HocPhi { get; set; }
        public DateTime? NgayGioGiaoDich { get; set; }
        public bool? TrangThaiThanhToan { get; set; }
    }
}
