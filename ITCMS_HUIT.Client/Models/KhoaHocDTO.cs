namespace ITCMS_HUIT.Client.Models
{
    public class KhoaHocDTO
    {
        public int IdkhoaHoc { get; set; }
        public int IdchuongTrinh { get; set; }
        public string? TenKhoaHoc { get; set; }
        public int SoGio { get; set; }
        public int SoTuan { get; set; }
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
        public int SoGio { get; set; }
        public int SoTuan { get; set; }
        public decimal HocPhi { get; set; }
        public string? Mota { get; set; }
        public string? HinhAnh { get; set; }
    }
}
