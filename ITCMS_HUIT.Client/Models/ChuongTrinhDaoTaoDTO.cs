namespace ITCMS_HUIT.Client.Models
{
    public class ChuongTrinhDaoTaoDTO
    {
        public int IdchuongTrinh { get; set; }
        public string TenChuongTrinh { get; set; } = null!;
        public List<KhoaHocModel>? KhoaHocs { get; set;}
    }
}
