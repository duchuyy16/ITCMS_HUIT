using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class KhoaHocService
    {
        public readonly IKhoaHocRepo _khoaHoc;
        public KhoaHocService(IRepo khoaHoc)
        {
            _khoaHoc = khoaHoc.KhoaHocRepo;
        }
      
        public bool Update(KhoaHocDTO model)
        {
            var khoaHoc = new KhoaHoc
            {
                IdkhoaHoc = model.IdkhoaHoc,
                IdchuongTrinh = model.IdchuongTrinh,
                TenKhoaHoc = model.TenKhoaHoc,
                SoGio = model.SoGio,
                SoTuan = model.SoTuan,
                HocPhi = model.HocPhi,
                Mota = model.Mota
            };

            var result = _khoaHoc.Update(khoaHoc);
            
            return result;
        }

        public List<KhoaHocDTO> GetAll()
        {
            var dsKhoaHoc = _khoaHoc.GetAll();

            var dsKhoaHocDTO = dsKhoaHoc.Select(s => new KhoaHocDTO
            {
                IdkhoaHoc = s.IdkhoaHoc,
                IdchuongTrinh = s.IdchuongTrinh,
                TenKhoaHoc = s.TenKhoaHoc,
                SoGio = s.SoGio,
                SoTuan = s.SoTuan,
                HocPhi = s.HocPhi,
                Mota = s.Mota,
                IdchuongTrinhNavigation = s.IdchuongTrinhNavigation.Adapt<ChuongTrinhDaoTaoDTO>(),
                
                LopHocs = s.LopHocs.Select(l => new LopHocDTO
                {
                    IdlopHoc = l.IdlopHoc,
                    TenLopHoc = l.TenLopHoc,
                    ThoiGian = l.ThoiGian,
                    NgayBatDau = l.NgayBatDau,
                    NgayKetThuc = l.NgayKetThuc,
                    DiaDiem = l.DiaDiem,
                    IdkhoaHocNavigation = l.IdkhoaHocNavigation.Adapt<KhoaHocDTO>(),
                    IdgiaoVienNavigation = l.IdgiaoVienNavigation.Adapt<GiaoVienDTO>(),
                    ThongTinHocViens = l.ThongTinHocViens.Select(t => new ThongTinHocVienDTO
                    {
                        IdhocVien = t.IdhocVien,
                    }).ToList()
                }).ToList()

            }).ToList();

            return dsKhoaHocDTO;
        }

        public List<KhoaHocDTO> Search(string keyword)
        {
            var dsKhoaHoc = _khoaHoc.Search(keyword);

            var dsKhoaHocDTO = dsKhoaHoc.Select(s => new KhoaHocDTO
            {
                IdkhoaHoc = s.IdkhoaHoc,
                IdchuongTrinh = s.IdchuongTrinh,
                TenKhoaHoc = s.TenKhoaHoc,
                SoGio = s.SoGio,
                SoTuan = s.SoTuan,
                HocPhi = s.HocPhi,
                Mota = s.Mota,
                IdchuongTrinhNavigation = s.IdchuongTrinhNavigation.Adapt<ChuongTrinhDaoTaoDTO>(),

                LopHocs = s.LopHocs.Select(l => new LopHocDTO
                {
                    IdlopHoc = l.IdlopHoc,
                    TenLopHoc = l.TenLopHoc,
                    ThoiGian = l.ThoiGian,
                    NgayBatDau = l.NgayBatDau,
                    NgayKetThuc = l.NgayKetThuc,
                    DiaDiem = l.DiaDiem,
                    IdkhoaHocNavigation = l.IdkhoaHocNavigation.Adapt<KhoaHocDTO>(),
                    IdgiaoVienNavigation = l.IdgiaoVienNavigation.Adapt<GiaoVienDTO>(),
                    ThongTinHocViens = l.ThongTinHocViens.Select(t => new ThongTinHocVienDTO
                    {
                        IdhocVien = t.IdhocVien,
                    }).ToList()
                }).ToList()

            }).ToList();

            return dsKhoaHocDTO;
        }

        public KhoaHocDTO GetById(int id)
        {
            var khoaHoc = _khoaHoc.GetById(id);

            var khoaHocDTO = new KhoaHocDTO
            {
                IdkhoaHoc = khoaHoc.IdkhoaHoc,
                IdchuongTrinh = khoaHoc.IdchuongTrinh,
                TenKhoaHoc = khoaHoc.TenKhoaHoc,
                SoGio = khoaHoc.SoGio,
                SoTuan = khoaHoc.SoTuan,
                HocPhi = khoaHoc.HocPhi,
                Mota = khoaHoc.Mota,
                IdchuongTrinhNavigation = khoaHoc.IdchuongTrinhNavigation.Adapt<ChuongTrinhDaoTaoDTO>(),

                LopHocs = khoaHoc.LopHocs.Select(l => new LopHocDTO
                {
                    IdlopHoc = l.IdlopHoc,
                    TenLopHoc = l.TenLopHoc,
                    ThoiGian = l.ThoiGian,
                    NgayBatDau = l.NgayBatDau,
                    NgayKetThuc = l.NgayKetThuc,
                    DiaDiem = l.DiaDiem,
                    IdkhoaHocNavigation = l.IdkhoaHocNavigation.Adapt<KhoaHocDTO>(),
                    IdgiaoVienNavigation = l.IdgiaoVienNavigation.Adapt<GiaoVienDTO>(),
                    ThongTinHocViens = l.ThongTinHocViens.Select(t => new ThongTinHocVienDTO
                    {
                        IdhocVien = t.IdhocVien,
                    }).ToList()
                }).ToList()

            };

            return khoaHocDTO;
        }
    }
}
