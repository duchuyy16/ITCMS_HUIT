namespace ITCMS_HUIT.Client.Common
{
    public class ConstantValues
    {
        public class Account
        {
            public const string DanhSachNguoiDung = "api/Account/danh-sach-nguoi-dung";
            public const string CapNhat = "api/Account/cap-nhat-quyen";
        }

        public class Authenticate
        {
            public const string Login = "api/Authenticate/dangnhap";
            public const string Register = "api/Authenticate/dang-ky-giao-vien";
            public const string RegisterAdmin = "api/Authenticate/dangky-quantri";
            public const string Logout = "api/Authenticate/dangxuat";
            public const string ChangePassword = "api/Authenticate/thay-doi-mat-khau";
        }

        public class Chart
        {
            public const string GetCourseCountsByProgram = "api/Chart/so-luong-khoa-hoc-theo-chuong-trinh";
            public const string ThongKeDoiTuongDangKy = "api/Chart/thong-ke-doi-tuong-dang-ky";
        }

        public class CoSoDuLieu
        {
            public const string Backup = "api/CoSoDuLieu/backup";
            public const string Restore = "api/CoSoDuLieu/restore/{0}";
            public const string BackupFiles = "api/CoSoDuLieu/backup-files";
        }

        public class ChuongTrinhDaoTao
        {
            public const string DanhSachChuongTrinhDaoTao = "api/ChuongTrinhDaoTao/danh-sach-chuong-trinh-dao-tao";
        }

        public class Count
        {
            public const string Dashboard = "api/Count/dashboard";
        }

        public class DoiTuongDangKy
        {
            public const string DanhSachDoiTuongDangKy = "api/DoiTuongDangKy/danh-sach-doi-tuong-dang-ky";
        }

        public class GiaoVien
        {
            public const string DanhSach = "api/GiaoVien/danh-sach-giao-vien";
            public const string KiemTraTonTai = "api/GiaoVien/kiem-tra-ton-tai/{0}";
            public const string ChiTietGiaoVien = "api/GiaoVien/giao-vien/{0}";
            //public const string TimKiem = "api/GiaoVien/timkiemhociven/{keyword}";
            public const string Xoa = "api/GiaoVien/xoa-giao-vien";
            public const string CapNhat = "api/GiaoVien/cap-nhat-giao-vien";
            public const string Them = "api/GiaoVien/them-giao-vien";
        }

        public class HocVien
        {
            public const string DanhSach = "api/HocVien/danh-sach-hoc-vien";
            public const string KiemTraTonTai = "api/HocVien/kiem-tra-ton-tai/{0}";
            public const string ChiTietHocVien = "api/HocVien/hoc-vien/{0}";
            public const string TimKiem = "api/HocVien/tim-kiem-hoc-vien/{0}";
            public const string Xoa = "api/HocVien/xoa-hoc-vien";
            public const string CapNhat = "api/HocVien/cap-nhat-hoc-vien";
            public const string Them = "api/HocVien/them-hoc-vien";
        }

        public class KhoaHoc
        {
            public const string DanhSach = "api/KhoaHoc/danh-sach-khoa-hoc";
            public const string KiemTraTonTai = "api/KhoaHoc/kiem-tra-ton-tai/{0}";
            public const string ChiTietKhoaHoc = "api/KhoaHoc/chi-tiet-khoa-hoc/{0}";
            public const string DanhSachKhoaHocTheoChuongTrinh = "api/KhoaHoc/danh-sach-khoa-hoc-theo-chuong-trinh/{0}";
            public const string TimKiem = "api/KhoaHoc/tim-kiem-khoa-hoc/{0}";
            public const string Xoa = "api/KhoaHoc/xoa-khoa-hoc";
            public const string CapNhat = "api/KhoaHoc/cap-nhat-khoa-hoc";
            public const string Them = "api/KhoaHoc/them-khoa-hoc";
        }

        public class LopHoc
        {
            public const string DanhSach = "api/LopHoc/danh-sach-lop-hoc";
            public const string KiemTraTonTai = "api/LopHoc/kiem-tra-ton-tai/{0}";
            public const string ChiTietLopHoc = "api/LopHoc/lop-hoc/{0}";
            public const string TimKiem = "api/LopHoc/tim-kiem-lop-hoc/{0}";
            public const string Xoa = "api/LopHoc/xoa-lop-hoc";
            public const string CapNhat = "api/LopHoc/cap-nhat-lop-hoc";
            public const string Them = "api/LopHoc/them-lop-hoc";
        }

        public class ThongTinHocVien
        {
            public const string DanhSach = "api/ThongTinHocVien/danh-sach-thong-tin-hoc-vien";
            public const string KiemTraTonTai = "api/ThongTinHocVien/kiem-tra-ton-tai/{0}&{1}";
            public const string ThongTinChiTiet = "api/ThongTinHocVien/thong-tin-hoc-vien/{0}&{1}";
            public const string TimKiem = "api/ThongTinHocVien/tim-kiem-thong-tin-hoc-vien/{0}";
            public const string Xoa = "api/ThongTinHocVien/xoa-thong-tin-hoc-vien";
            public const string CapNhat = "api/ThongTinHocVien/cap-nhat-thong-tin-hoc-vien";
            public const string Them = "api/ThongTinHocVien/them-thong-tin-hoc-vien";
        }

        public class TrangThaiHocVien
        {
            public const string DanhSachTrangThaiHocVien = "api/TrangThaiHocVien/danh-sach-trang-thai-hoc-vien";
        }
    }
}
