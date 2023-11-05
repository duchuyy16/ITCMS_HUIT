using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Implement;
using ITCMS_HUIT.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LopHocService
    {
        public readonly ILopHocRepo _lophoc;
        public LopHocService(IRepo lophoc)
        {
            _lophoc = lophoc.LopHocRepo;
        }

        public List<LopHocDTO> GetAll()
        {
            var dsLopHoc = _lophoc.GetAll();
            var dsLopHocDTO = dsLopHoc.Select(s => new LopHocDTO
            {
                IdlopHoc = s.IdlopHoc,
                TenLopHoc = s.TenLopHoc,
                ThoiGian = s.ThoiGian,
                NgayBatDau = s.NgayBatDau,
                NgayKetThuc = s.NgayKetThuc,
                DiaDiem = s.DiaDiem,
                IdkhoaHocNavigation = s.IdkhoaHocNavigation.Adapt<KhoaHocDTO>(),
                IdgiaoVienNavigation = s.IdgiaoVienNavigation.Adapt<GiaoVienDTO>(),
                ThongTinHocViens = s.ThongTinHocViens.Select(t => new ThongTinHocVienDTO
                {
                    IdhocVien = t.IdhocVien,
                }).ToList()
            }).ToList();

            return dsLopHocDTO;
        }

        public LopHocDTO GetById(int id)
        {
            var lopHoc=_lophoc.GetById(id);
            LopHocDTO lopHocDTO = new LopHocDTO
            {
                IdlopHoc = lopHoc.IdlopHoc,
                TenLopHoc = lopHoc.TenLopHoc,
                ThoiGian = lopHoc.ThoiGian,
                NgayBatDau = lopHoc.NgayBatDau,
                NgayKetThuc = lopHoc.NgayKetThuc,
                DiaDiem = lopHoc.DiaDiem,
                IdkhoaHocNavigation = lopHoc.IdkhoaHocNavigation.Adapt<KhoaHocDTO>(),
                IdgiaoVienNavigation = lopHoc.IdgiaoVienNavigation.Adapt<GiaoVienDTO>(),
                ThongTinHocViens = lopHoc.ThongTinHocViens.Select(t => new ThongTinHocVienDTO
                {
                    IdhocVien = t.IdhocVien,
                }).ToList()
            };
            return lopHocDTO;
        }

        public List<LopHocDTO> Search(string keyword)
        {
            var lophoc = _lophoc.Search(keyword);

            var dsLopHocDTO = lophoc.Select(s => new LopHocDTO
            {
                IdlopHoc = s.IdlopHoc,
                TenLopHoc = s.TenLopHoc,
                ThoiGian = s.ThoiGian,
                NgayBatDau = s.NgayBatDau,
                NgayKetThuc = s.NgayKetThuc,
                DiaDiem = s.DiaDiem,
                IdkhoaHocNavigation = s.IdkhoaHocNavigation.Adapt<KhoaHocDTO>(),
                IdgiaoVienNavigation = s.IdgiaoVienNavigation.Adapt<GiaoVienDTO>(),
                ThongTinHocViens = s.ThongTinHocViens.Select(t => new ThongTinHocVienDTO
                {
                    IdhocVien = t.IdhocVien,
                }).ToList()
            }).ToList();

            return dsLopHocDTO;
        }


    }
}
