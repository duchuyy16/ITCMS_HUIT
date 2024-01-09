using AutoMapper;
using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Services
{
    public class GiaoVienService
    {
        private readonly IMapper _mapper;
        private readonly IGiaoVienRepo _giaoVien;
        private readonly IHostingEnvironment _webHostEnvironment;
        public GiaoVienService(IRepo giaoVien, IMapper mapper, IHostingEnvironment webHostEnvironment) 
        {
            _webHostEnvironment= webHostEnvironment;
            _mapper = mapper;
            _giaoVien = giaoVien.GiaoVienRepo;
        }

        public bool Update([FromForm] GiaoVienDTO model, IFormFile? imageFile)
        {
            var giaoVien = new GiaoVien
            {
                IdgiaoVien = model.IdgiaoVien!,
                TenGiaoVien = model.TenGiaoVien!,
                TrinhDo = model.TrinhDo!,
                ChungChi = model.ChungChi!,
                HoSoCaNhan = model.HoSoCaNhan!,
                HinhAnh = model.HinhAnh!,
            };

            if (imageFile != null) 
                UploadFiles(imageFile, giaoVien);

            var result = _giaoVien.Update(giaoVien);

            return result;
        }

        [NonAction]
        public void UploadFiles(IFormFile file, GiaoVien model)
        {
            if(file!=null)
            {
                string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "images");
                Directory.CreateDirectory(directoryPath);
                string filePath = Path.Combine(directoryPath, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                model.HinhAnh = file.FileName;
            }    
        }

        public int Count()
        {  
            return _giaoVien.Count(); 
        }

        public bool IsExist(string id)
        {
            return _giaoVien.IsExist(id);
        }

		public List<GiaoVienDTO> Search(string tenGiaoVien)
		{
			var dsGiaoVien = _giaoVien.Search(tenGiaoVien);

			var dsGiaoVienDTO = dsGiaoVien.Select(x => new GiaoVienDTO
			{
				IdgiaoVien = x.IdgiaoVien,
				TenGiaoVien = x.TenGiaoVien,
				TrinhDo = x.TrinhDo,
				ChungChi = x.ChungChi,
				HoSoCaNhan = x.HoSoCaNhan,
				HinhAnh = x.HinhAnh!,
			}).ToList();

			return dsGiaoVienDTO;
		}

		public List<GiaoVienDTO> GetAll() 
        {
            var dsGiaoVien = _giaoVien.GetAll();

            var dsGiaoVienDTO = dsGiaoVien.Select(x => new GiaoVienDTO
            {
                IdgiaoVien = x.IdgiaoVien,
                TenGiaoVien = x.TenGiaoVien,
                TrinhDo = x.TrinhDo,
                ChungChi = x.ChungChi,
                HoSoCaNhan = x.HoSoCaNhan,      
                HinhAnh=x.HinhAnh!,
            }).ToList();

            return dsGiaoVienDTO;
        }

        public GiaoVienDTO GetById(string id)
        {
            var giaoVien= _giaoVien.GetById(id);

            GiaoVienDTO giaoVienDTO = new GiaoVienDTO
            {
                IdgiaoVien = giaoVien.IdgiaoVien,
                TenGiaoVien = giaoVien.TenGiaoVien,
                TrinhDo = giaoVien.TrinhDo,
                ChungChi = giaoVien.ChungChi,
                HoSoCaNhan = giaoVien.HoSoCaNhan,
                HinhAnh = giaoVien.HinhAnh!,
            };

            return giaoVienDTO;
        }

        public bool Delete(GiaoVienDTO model)
        {
            var giaoVien = _giaoVien.GetById(model.IdgiaoVien!);

            var result = _giaoVien.Delete(giaoVien);

            return result;
        }

        public GiaoVienDTO Add(GiaoVienDTO model)
        {
            var giaoVien = new GiaoVien
            {
                TenGiaoVien = model.TenGiaoVien!,
                TrinhDo = model.TrinhDo!,
                ChungChi = model.ChungChi!,
                HoSoCaNhan = model.HoSoCaNhan!,
                IdgiaoVien=model.IdgiaoVien!,
                HinhAnh = model.HinhAnh!,
            };

            var result = _giaoVien.Add(giaoVien);

            var giaoVienDTO = _mapper.Map<GiaoVienDTO>(result);

            return giaoVienDTO;
        }
    }
}
