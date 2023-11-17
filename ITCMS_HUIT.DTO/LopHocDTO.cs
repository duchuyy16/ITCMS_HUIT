namespace ITCMS_HUIT.DTO
{
    public class LopHocDTO
    {
        public int IdlopHoc { get; set; }
        public string TenLopHoc { get; set; } = null!;
        public string ThoiGian { get; set; } = null!;
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string DiaDiem { get; set; } = null!;
        public int IdkhoaHoc { get; set; }
        public string? IdgiaoVien { get; set; }

        public GiaoVienDTO? IdgiaoVienNavigation { get; set; } = null!;
        public KhoaHocModel? IdkhoaHocNavigation { get; set; } = null!;
    }

    public class LopHocModel
    {
        public int? IdlopHoc { get; set; }
        public string? TenLopHoc { get; set; } = null!;
        public string? ThoiGian { get; set; } = null!;
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? DiaDiem { get; set; } = null!;
        public int? IdkhoaHoc { get; set; }
        public string? IdgiaoVien { get; set; }
    }
}