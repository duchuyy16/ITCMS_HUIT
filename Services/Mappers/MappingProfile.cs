using AutoMapper;
using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<LopHocDTO, LopHoc>();
            CreateMap<LopHoc, LopHocDTO>();

            CreateMap<ChuongTrinhDaoTaoDTO,ChuongTrinhDaoTao>();
            CreateMap<ChuongTrinhDaoTao,ChuongTrinhDaoTaoDTO>();

            CreateMap<KhoaHocDTO, KhoaHoc>();
            CreateMap<KhoaHoc, KhoaHocDTO>();

            CreateMap<GiaoVienDTO, GiaoVien>();
            CreateMap<GiaoVien, GiaoVienDTO>();

            CreateMap<ThongTinHocVienDTO, ThongTinHocVien>();
            CreateMap<ThongTinHocVien, ThongTinHocVienDTO>();

            CreateMap<HocVienDTO, HocVien>();
            CreateMap<HocVien, HocVienDTO>();
        }
    }
}
