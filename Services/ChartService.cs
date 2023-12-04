using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ChartService
    {
        public readonly IChuongTrinhDaoTaoRepo _chuongTrinhDaoTao;
        private readonly IKhoaHocRepo _khoaHoc;
        private readonly IDoiTuongDangKyRepo _doiTuong;
        private readonly IHocVienRepo _hocVien;
        public ChartService(IRepo repo)
        {
            _hocVien = repo.HocVienRepo;
            _doiTuong = repo.DoiTuongDangKyRepo;
            _chuongTrinhDaoTao = repo.ChuongTrinhDaoTaoRepo;
            _khoaHoc = repo.KhoaHocRepo;
        }

        public List<KhoaHocTheoChuongTrinhDTO> GetCourseCountsByProgram()
        {
            var chuongTrinhList = _chuongTrinhDaoTao.GetAll();
            var khoaHocList = _khoaHoc.GetAll();
            List<KhoaHocTheoChuongTrinhDTO> statistics = new List<KhoaHocTheoChuongTrinhDTO>();
            foreach (var chuongTrinh in chuongTrinhList)
            {
                var IdChuongTrinh = chuongTrinh.IdchuongTrinh;
                var soLuongKhoaHoc = khoaHocList.Where(x => new List<int> { x.IdchuongTrinh }.Contains(IdChuongTrinh)).Count();
                statistics.Add(new KhoaHocTheoChuongTrinhDTO
                {
                    IdchuongTrinh = chuongTrinh.IdchuongTrinh,
                    SoLuongKhoaHoc = soLuongKhoaHoc,
                    TenChuongTrinh = chuongTrinh.TenChuongTrinh!,             
                });
            }
            return statistics;
        }

        public List<ThongKeDoiTuongDangKyDTO> ThongKeDoiTuongDangKy()
        {
            var doiTuongList = _doiTuong.GetAll();
            var hocVienList= _hocVien.GetAll();
            var statistics = new List<ThongKeDoiTuongDangKyDTO>();

            foreach (var doiTuong in doiTuongList)
            {
                var IdDoiTuong = doiTuong.IddoiTuong;
                var soLuong = hocVienList.Where(x => new List<int> { x.IddoiTuong }.Contains(IdDoiTuong)).Count();
                statistics.Add(new ThongKeDoiTuongDangKyDTO
                {
                    IddoiTuong = doiTuong.IddoiTuong,
                    SoLuong = soLuong,
                    DoiTuongDangKy = doiTuong.DoiTuongDangKy1,
                });
            }
            return statistics;
        }

    }
}
