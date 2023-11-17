namespace ITCMS_HUIT.Client.Models
{
    public class HocVienDTO
    {
        public int IdhocVien { get; set; }
        public string TenHocVien { get; set; } = null!;
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; } = null!;
        public int Sdt { get; set; }
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
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; } = null!;
        public int Sdt { get; set; }
        public string DiaChi { get; set; } = null!;
        public DateTime? NgayDangKy { get; set; }
        public int IddoiTuong { get; set; }
        public int IdtrangThai { get; set; }

    }
}
