using System.ComponentModel.DataAnnotations;

namespace ITCMS_HUIT.Client.Models
{
    public class KhoaHocDTO
    {
        public int IdkhoaHoc { get; set; }
        public int IdchuongTrinh { get; set; }
        public string? TenKhoaHoc { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Số giờ không thể nhỏ hơn 0")]
        public int SoGio { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Số tuần không thể nhỏ hơn 0")]
        public int SoTuan { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Học phí không thể nhỏ hơn 0")]
        public decimal HocPhi { get; set; }
        public string? Mota { get; set; }
        public string? HinhAnh { get; set; }

        public ChuongTrinhDaoTaoDTO? IdchuongTrinhNavigation { get; set; } = null!;
        public List<LopHocModel>? LopHocs { get; set; }
    }

    public class KhoaHocModel
    {
        public int IdkhoaHoc { get; set; }
        public int IdchuongTrinh { get; set; }
        public string? TenKhoaHoc { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Số giờ không thể nhỏ hơn 0")]
        public int SoGio { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Số tuần không thể nhỏ hơn 0")]
        public int SoTuan { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Học phí không thể nhỏ hơn 0")]
        public decimal HocPhi { get; set; }
        public string? Mota { get; set; }
        public string? HinhAnh { get; set; }
    }
}
