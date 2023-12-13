using AutoMapper;
using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Implement;
using ITCMS_HUIT.Repository.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LopHocService
    {
        private readonly IMapper _mapper;
        public readonly ILopHocRepo _lopHoc;
        public LopHocService(IRepo lophoc, IMapper mapper)
        {
            _mapper = mapper;
            _lopHoc = lophoc.LopHocRepo;
        }

        public int Count()
        {
            return _lopHoc.Count();
        }

        public bool IsExist(int id)
        {
            return _lopHoc.IsExist(id);
        }

        public List<LopHocDTO> GetAll()
        {
            var dsLopHoc = _lopHoc.GetAll();
            var dsLopHocDTO = dsLopHoc.Select(s => new LopHocDTO
            {
                IdlopHoc = s.IdlopHoc,
                TenLopHoc = s.TenLopHoc,
                ThoiGian = s.ThoiGian,
                NgayBatDau = s.NgayBatDau,
                NgayKetThuc = s.NgayKetThuc,
                DiaDiem = s.DiaDiem,
                PhongHoc = s.PhongHoc,
                IdgiaoVien = s.IdgiaoVien,
                IdkhoaHoc = s.IdkhoaHoc,
                IdgiaoVienNavigation = s.IdgiaoVienNavigation.Adapt<GiaoVienDTO>(),
                IdkhoaHocNavigation=s.IdkhoaHocNavigation.Adapt<KhoaHocModel>(),
                
            }).ToList();

            return dsLopHocDTO;
        }

        public LopHocDTO GetById(int id)
        {
            var lopHoc= _lopHoc.GetById(id);
            LopHocDTO lopHocDTO = new LopHocDTO
            {
                IdlopHoc = lopHoc.IdlopHoc,
                TenLopHoc = lopHoc.TenLopHoc,
                ThoiGian = lopHoc.ThoiGian,
                NgayBatDau = lopHoc.NgayBatDau,
                NgayKetThuc = lopHoc.NgayKetThuc,
                DiaDiem = lopHoc.DiaDiem,
                PhongHoc = lopHoc.PhongHoc,
                IdgiaoVien=lopHoc.IdgiaoVien,
                IdkhoaHoc=lopHoc.IdkhoaHoc,
                IdkhoaHocNavigation = lopHoc.IdkhoaHocNavigation.Adapt<KhoaHocModel>(),
                IdgiaoVienNavigation = lopHoc.IdgiaoVienNavigation.Adapt<GiaoVienDTO>(),
            };
            return lopHocDTO;
        }

        public List<LopHocDTO> Search(string keyword)
        {
            var lophoc = _lopHoc.Search(keyword);

            var dsLopHocDTO = lophoc.Select(s => new LopHocDTO
            {
                IdlopHoc = s.IdlopHoc,
                TenLopHoc = s.TenLopHoc,
                ThoiGian = s.ThoiGian,
                NgayBatDau = s.NgayBatDau,
                NgayKetThuc = s.NgayKetThuc,
                DiaDiem = s.DiaDiem,
                PhongHoc = s.PhongHoc,
                IdgiaoVien = s.IdgiaoVien,
                IdkhoaHoc = s.IdkhoaHoc,
                IdkhoaHocNavigation = s.IdkhoaHocNavigation.Adapt<KhoaHocModel>(),
                IdgiaoVienNavigation = s.IdgiaoVienNavigation.Adapt<GiaoVienDTO>(),
            }).ToList();

            return dsLopHocDTO;
        }

        public bool Delete(LopHocDTO model)
        {
            var lopHoc = _lopHoc.GetById(model.IdlopHoc);

            var result= _lopHoc.Delete(lopHoc);

            return result;
        }

        public bool Update(LopHocDTO model)
        {
            var lopHoc = new LopHoc
            {
                IdlopHoc = model.IdlopHoc,
                TenLopHoc = model.TenLopHoc,
                ThoiGian = model.ThoiGian,
                NgayBatDau = model.NgayBatDau,
                NgayKetThuc = model.NgayKetThuc,
                DiaDiem = model.DiaDiem,
                PhongHoc = model.PhongHoc,
                IdgiaoVien = model.IdgiaoVien,
                IdkhoaHoc=model.IdkhoaHoc,
            };

            var result = _lopHoc.Update(lopHoc);

            return result;
        }

        public LopHocDTO Add(LopHocDTO model)
        {
            var lopHoc = new LopHoc
            {
                TenLopHoc = model.TenLopHoc,
                ThoiGian = model.ThoiGian,
                NgayBatDau = model.NgayBatDau,
                NgayKetThuc = model.NgayKetThuc,
                DiaDiem = model.DiaDiem,
                PhongHoc=model.PhongHoc,
                IdgiaoVien = model.IdgiaoVien,
                IdkhoaHoc = model.IdkhoaHoc,
            };

            var result = _lopHoc.Add(lopHoc);

            var lopHocDTO=_mapper.Map<LopHocDTO>(result);

            return lopHocDTO;
        }
    }

}
