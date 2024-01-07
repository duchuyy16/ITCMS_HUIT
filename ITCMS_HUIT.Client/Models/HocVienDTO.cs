using System.ComponentModel.DataAnnotations;

namespace ITCMS_HUIT.Client.Models
{
    public class HocVienDTO
    {
        public int IdhocVien { get; set; }
        public string TenHocVien { get; set; } = null!;

        [Required(ErrorMessage = "Ngày sinh là trường bắt buộc.")]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2007", ErrorMessage = "Ngày sinh phải nhỏ hơn hoặc bằng năm 2006.")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Email là trường bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; } = null!;

        [RegularExpression("^0[0-9]{9}$", ErrorMessage = "Số điện thoại không đúng định dạng.")]
        public string? Sdt { get; set; }
        public string DiaChi { get; set; } = null!;
        public DateTime? NgayDangKy { get; set; }
        public int IddoiTuong { get; set; }
        public int IdtrangThai { get; set; }

        public DoiTuongDangKyDTO? IddoiTuongNavigation { get; set; } = null!;
        public TrangThaiHocVienDTO? IdtrangThaiNavigation { get; set; } = null!;
    }

    public class HocVienModel
    {
        public int IdhocVien { get; set; }
        public string TenHocVien { get; set; } = null!;
        [Required(ErrorMessage = "Ngày sinh là trường bắt buộc.")]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2007", ErrorMessage = "Ngày sinh phải nhỏ hơn hoặc bằng năm 2006.")]
        public DateTime NgaySinh { get; set; }
        [Required(ErrorMessage = "Email là trường bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; } = null!;
        [RegularExpression("^0[0-9]{9}$", ErrorMessage = "Số điện thoại không đúng định dạng.")]
        public string? Sdt { get; set; }
        public string DiaChi { get; set; } = null!;
        public DateTime? NgayDangKy { get; set; }
        public int IddoiTuong { get; set; }
        public int IdtrangThai { get; set; }
    }
}
