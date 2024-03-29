﻿using AutoMapper;
using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class KhoaHocService
    {
        private readonly IMapper _mapper;
        private readonly IKhoaHocRepo _khoaHoc;
        private readonly IHostingEnvironment _webHostEnvironment;
        public KhoaHocService(IRepo repo, IMapper mapper, IHostingEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _khoaHoc = repo.KhoaHocRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public bool IsExist(int id)
        {
            return _khoaHoc.IsExist(id);
        }

        public bool Update([FromForm] KhoaHocDTO model, IFormFile? imageFile)
        {
            var khoaHoc = new KhoaHoc
            {
                IdkhoaHoc = model.IdkhoaHoc,
                IdchuongTrinh = model.IdchuongTrinh,
                TenKhoaHoc = model.TenKhoaHoc,
                SoGio = model.SoGio,
                SoTuan = model.SoTuan,
                HocPhi = model.HocPhi,
                Mota = model.Mota,
                HinhAnh=model.HinhAnh,
            };

            if(imageFile != null) 
                UploadFiles(imageFile, khoaHoc);

            var result = _khoaHoc.Update(khoaHoc);
            
            return result;
        }

        [NonAction]
        public void UploadFiles(IFormFile file, KhoaHoc model)
        {
            if (file != null)
            {
                string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "UploadFiles");
                Directory.CreateDirectory(directoryPath);
                string filePath = Path.Combine(directoryPath, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                model.HinhAnh = file.FileName;
            }    
        }

        public bool Delete(KhoaHocDTO model)
        {
            var khoaHoc=_khoaHoc.GetById(model.IdkhoaHoc);
            
            var result=_khoaHoc.Delete(khoaHoc);

            return result;
        }

        public KhoaHocDTO Add([FromForm]KhoaHocDTO model,IFormFile? imageFile)
        {
            var khoaHoc = new KhoaHoc
            {
                IdchuongTrinh = model.IdchuongTrinh,
                TenKhoaHoc = model.TenKhoaHoc,
                SoGio = model.SoGio,
                SoTuan = model.SoTuan,
                HocPhi = model.HocPhi,
                Mota = model.Mota,
                HinhAnh = model.HinhAnh,
            };

            if (imageFile != null)
            {
                UploadFiles(imageFile, khoaHoc);
            }

            var result = _khoaHoc.Add(khoaHoc);

            var khoaHocDTO = _mapper.Map<KhoaHocDTO>(result);

            return khoaHocDTO;
        }

        public List<KhoaHocDTO> GetAll()
        {
            var dsKhoaHoc = _khoaHoc.GetAll();

            //var dsKhoaHocDTO = _mapper.Map<List<KhoaHocDTO>>(dsKhoaHoc);

            var dsKhoaHocDTO = dsKhoaHoc.Select(s => new KhoaHocDTO
            {
                IdkhoaHoc = s.IdkhoaHoc,
                IdchuongTrinh = s.IdchuongTrinh,
                TenKhoaHoc = s.TenKhoaHoc,
                SoGio = s.SoGio,
                SoTuan = s.SoTuan,
                HocPhi = s.HocPhi,
                Mota = s.Mota,
                HinhAnh = s.HinhAnh,
                IdchuongTrinhNavigation = s.IdchuongTrinhNavigation.Adapt<ChuongTrinhDaoTaoDTO>(),
                LopHocs=s.LopHocs.Adapt<List<LopHocModel>>()
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
                HinhAnh = s.HinhAnh,
                IdchuongTrinhNavigation = s.IdchuongTrinhNavigation.Adapt<ChuongTrinhDaoTaoDTO>(),
                LopHocs = s.LopHocs.Adapt<List<LopHocModel>>()
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
                HinhAnh = khoaHoc.HinhAnh,
                IdchuongTrinhNavigation = khoaHoc.IdchuongTrinhNavigation.Adapt<ChuongTrinhDaoTaoDTO>(),
                LopHocs = khoaHoc.LopHocs.Adapt<List<LopHocModel>>(),

            };

            return khoaHocDTO;
        }

        public List<KhoaHocDTO> GetByIdCTDT(int id)
        {
            var idCTDT = _khoaHoc.GetByIdCTDT(id);

            var dsKhoaHocDTO = idCTDT.Select(s => new KhoaHocDTO
            {
                IdkhoaHoc = s.IdkhoaHoc,
                IdchuongTrinh = s.IdchuongTrinh,
                TenKhoaHoc = s.TenKhoaHoc,
                SoGio = s.SoGio,
                SoTuan = s.SoTuan,
                HocPhi = s.HocPhi,
                Mota = s.Mota,
                HinhAnh = s.HinhAnh,
                IdchuongTrinhNavigation = s.IdchuongTrinhNavigation.Adapt<ChuongTrinhDaoTaoDTO>(),
                LopHocs = s.LopHocs.Adapt<List<LopHocModel>>(),

            }).ToList();

            return dsKhoaHocDTO;
        }
    }
}
