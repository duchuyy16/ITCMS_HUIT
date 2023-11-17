namespace ITCMS_HUIT.Client.Common
{
    public class ConstantValues
    {
        public class Authenticate
        {
            public const string Login = "api/Authenticate/dangnhap";
            public const string Register = "api/Authenticate/dangky-giaovien";
            public const string RegisterAdmin = "api/Authenticate/dangky-quantri";
            public const string Logout = "api/Authenticate/dangxuat";
            public const string ChangePassword = "api/Authenticate/thay-doi-mat-khau";
        }

        public class ChuongTrinhDaoTao
        {
            public const string DanhSachChuongTrinhDaoTao = "api/ChuongTrinhDaoTao/danh-sach-chuong-trinh-dao-tao";
        }

        public class Count
        {
            public const string Dashboard = "api/Count/dashboard";
        }

        public class ThongTinHocVien
        {
            public const string DanhSach = "api/ThongTinHocVien/danh-sach-thong-tin-hoc-vien";
            public const string KiemTraTonTai = "api/ThongTinHocVien/kiem-tra-ton-tai/{idHocVien}&&{idLopHoc}";
            public const string ThongTinChiTiet = "api/ThongTinHocVien/thong-tin-hoc-vien/{idHocVien}&{idLopHoc}";
            public const string TimKiem = "api/ThongTinHocVien/tim-kiem-thong-tin-hoc-vien/{idHocVien}";
            public const string Xoa = "api/ThongTinHocVien/xoa-thong-tin-hoc-vien";
            public const string CapNhat = "api/ThongTinHocVien/cap-nhat-thong-tin-hoc-vien";
            public const string Them = "api/ThongTinHocVien/them-thong-tin-hoc-vien";
        }

        public class HocVien
        {
            public const string DanhSach = "api/HocVien/danh-sach-hoc-vien";
            public const string KiemTraTonTai = "api/HocVien/kiem-tra-ton-tai/{id}";
            public const string ChiTietHocVien = "api/HocVien/hocvien/{id}";
            public const string TimKiem = "api/HocVien/tim-kiem-hoc-vien/{keyword}";
            public const string Xoa = "api/HocVien/xoa-hoc-vien";
            public const string CapNhat = "api/HocVien/cap-nhat-hoc-vien";
            public const string Them = "api/HocVien/them-hoc-vien";
        }

        public class LopHoc
        {
            public const string DanhSach = "api/LopHoc/danh-sach-lop-hoc";
            public const string KiemTraTonTai = "api/LopHoc/kiem-tra-ton-tai/{id}";
            public const string ChiTietLopHoc = "api/LopHoc/lophoc/{id}";
            public const string TimKiem = "api/LopHoc/tim-kiem-lop-hoc/{keyword}";
            public const string Xoa = "api/LopHoc/xoa-lop-hoc";
            public const string CapNhat = "api/LopHoc/cap-nhat-lop-hoc";
            public const string Them = "api/LopHoc/them-lop-hoc";
        }

        public class GiaoVien
        {
            public const string DanhSach = "api/GiaoVien/danh-sach-giao-vien";
            public const string KiemTraTonTai = "api/GiaoVien/kiem-tra-ton-tai/{id}";
            public const string ChiTietGiaoVien = "api/GiaoVien/giaovien/{id}";
            //public const string TimKiem = "api/GiaoVien/timkiemhociven/{keyword}";
            public const string Xoa = "api/GiaoVien/xoa-giao-vien";
            public const string CapNhat = "api/GiaoVien/cap-nhat-giao-vien";
            public const string Them = "api/GiaoVien/them-giao-vien";
        }

        public class KhoaHoc
        {
            public const string DanhSach = "api/KhoaHoc/danh-sach-khoa-hoc";
            public const string KiemTraTonTai = "api/KhoaHoc/kiem-tra-ton-tai/{id}";
            public const string ChiTietKhoaHoc = "api/KhoaHoc/chi-tiet-khoa-hoc/{id}";
            public const string DanhSachKhoaHocTheoChuongTrinh = "api/KhoaHoc/danh-sach-khoa-hoc-theo-chuong-trinh/{id}";
            public const string TimKiem = "api/KhoaHoc/tim-kiem-khoa-hoc/{keyword}";
            public const string Xoa = "api/KhoaHoc/xoa-khoa-hoc";
            public const string CapNhat = "api/KhoaHoc/cap-nhat-khoa-hoc";
            public const string Them = "api/KhoaHoc/them-khoa-hoc";
        }
    }
}
