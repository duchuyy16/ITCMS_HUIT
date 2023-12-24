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

        private HocVien _idhocVienNavigation = null!;
        public virtual HocVien IdhocVienNavigation
        {
            get => _idhocVienNavigation ??= new HocVien();
            set => _idhocVienNavigation = value ?? throw new ArgumentNullException(nameof(value));
        }
        private LopHoc _idlopHocNavigation = null!;
        public virtual LopHoc IdlopHocNavigation
        {
            get => _idlopHocNavigation ??= new LopHoc();
            set => _idlopHocNavigation = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
