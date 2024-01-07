using AutoMapper;
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
    public class GiaoVienService
    {
        private readonly IMapper _mapper;
        private readonly IGiaoVienRepo _giaoVien;
        public GiaoVienService(IRepo giaoVien, IMapper mapper) 
        {
            _mapper = mapper;
            _giaoVien = giaoVien.GiaoVienRepo;
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

        public bool Update(GiaoVienDTO model)
        {
            var giaoVien = new GiaoVien
            {
                IdgiaoVien = model.IdgiaoVien!,
                TenGiaoVien = model.TenGiaoVien!,
                TrinhDo = model.TrinhDo!,
                ChungChi = model.ChungChi!,
                HoSoCaNhan = model.HoSoCaNhan!,
                HinhAnh= model.HinhAnh!,
            };

            var result = _giaoVien.Update(giaoVien);

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
