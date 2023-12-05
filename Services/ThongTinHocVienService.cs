using AutoMapper;
using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Interfaces;
using Mapster;
using Services.MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ThongTinHocVienService
    {
        private readonly IMailService _mail;
        private readonly IMapper _mapper;
        private readonly IThongTinHocVienRepo _thongTin;
        public ThongTinHocVienService(IRepo thongTin, IMapper mapper, IMailService mail)
        {
            _mail = mail;
            _mapper = mapper;
            _thongTin = thongTin.ThongTinHocVienRepo;
        }

        public bool IsExist(int idHocVien, int idLopHoc)
        {
            return _thongTin.IsExist(idHocVien, idLopHoc);
        }

        public List<ThongTinHocVienDTO> GetAll()
        {
            var dsThongTinHocVien = _thongTin.GetAll();

            var dsThongTinHocVienDTO = dsThongTinHocVien.Select(s => new ThongTinHocVienDTO
            {
                IdhocVien=s.IdhocVien,
                IdlopHoc=s.IdlopHoc,
                Diem = s.Diem,
                NgayThongBao = s.NgayThongBao,
                TrangThaiThongBao = s.TrangThaiThongBao!,
                SoLanVangMat = s.SoLanVangMat,
                LyDoThongBao = s.LyDoThongBao,
                HocPhi = s.HocPhi,
                NgayGioGiaoDich = s.NgayGioGiaoDich!,
                TrangThaiThanhToan = s.TrangThaiThanhToan,

                IdhocVienNavigation = s.IdhocVienNavigation.Adapt<HocVienModel>(),
                IdlopHocNavigation = s.IdlopHocNavigation.Adapt<LopHocModel>(),
            }).ToList();

            return dsThongTinHocVienDTO;
        }

        public List<ThongTinHocVienDTO> Search(int idHocVien)
        {
            var dsThongTinHocVien = _thongTin.Search(idHocVien);

            var dsThongTinHocVienDTO = dsThongTinHocVien.Select(s => new ThongTinHocVienDTO
            {
                IdhocVien = s.IdhocVien,
                IdlopHoc = s.IdlopHoc,
                Diem = s.Diem,
                NgayThongBao = s.NgayThongBao,
                TrangThaiThongBao = s.TrangThaiThongBao,
                SoLanVangMat = s.SoLanVangMat,
                LyDoThongBao = s.LyDoThongBao,
                HocPhi = s.HocPhi,
                NgayGioGiaoDich = s.NgayGioGiaoDich,
                TrangThaiThanhToan = s.TrangThaiThanhToan,

                IdhocVienNavigation = s.IdhocVienNavigation.Adapt<HocVienModel>(),
                IdlopHocNavigation = s.IdlopHocNavigation.Adapt<LopHocModel>(),
            }).ToList();

            return dsThongTinHocVienDTO;
        }

        public ThongTinHocVienDTO GetById(int idHocVien, int idLopHoc)
        {
            var thongTinHocVien = _thongTin.GetById(idHocVien, idLopHoc);

            var thongTinHocVienDTO = new ThongTinHocVienDTO
            {
                IdhocVien = thongTinHocVien.IdhocVien,
                IdlopHoc = thongTinHocVien.IdlopHoc,
                Diem = thongTinHocVien.Diem,
                NgayThongBao = thongTinHocVien.NgayThongBao,
                TrangThaiThongBao = thongTinHocVien.TrangThaiThongBao,
                SoLanVangMat = thongTinHocVien.SoLanVangMat,
                LyDoThongBao = thongTinHocVien.LyDoThongBao,
                HocPhi = thongTinHocVien.HocPhi,
                NgayGioGiaoDich = thongTinHocVien.NgayGioGiaoDich,
                TrangThaiThanhToan = thongTinHocVien.TrangThaiThanhToan,

                IdhocVienNavigation = thongTinHocVien.IdhocVienNavigation.Adapt<HocVienModel>(),
                IdlopHocNavigation = thongTinHocVien.IdlopHocNavigation.Adapt<LopHocModel>(),
            };

            return thongTinHocVienDTO;
        }

        public bool Delete(ThongTinHocVienDTO model)
        {
            var thongTinHocVien = _thongTin.GetById(model.IdhocVien, model.IdlopHoc);

            var result = _thongTin.Delete(thongTinHocVien);

            return result;
        }

        public ThongTinHocVienDTO Update(ThongTinHocVienDTO model)
        {
            var thongTinHocVien = new ThongTinHocVien
            {
                IdlopHoc = model.IdlopHoc,
                IdhocVien = model.IdhocVien,
                Diem = model.Diem,
                NgayThongBao = model.NgayThongBao,
                TrangThaiThongBao = model.TrangThaiThongBao,
                SoLanVangMat = model.SoLanVangMat,
                LyDoThongBao = model.LyDoThongBao,
                HocPhi = model.HocPhi,
                NgayGioGiaoDich = model.NgayGioGiaoDich,
                TrangThaiThanhToan = model.TrangThaiThanhToan,

            };

            var result = _thongTin.UpdateTTHV(thongTinHocVien);

            if (result!.SoLanVangMat > 3 && model.SoLanVangMat != result.SoLanVangMat)
            {
                _mail?.SendMail(model.IdhocVienNavigation!.TenHocVien, model.IdhocVienNavigation.Email, result.SoLanVangMat);
                result.TrangThaiThongBao = true;
            }

            if (result.HocPhi != model.HocPhi)
            {
                result.TrangThaiThanhToan = false;
            }

            var thongTinHocVienDTO = _mapper?.Map<ThongTinHocVienDTO>(result);

            return thongTinHocVienDTO!;
        }

        public ThongTinHocVienDTO Add(ThongTinHocVienDTO model)
        {
            var thongTinHocVien = new ThongTinHocVien
            {
                IdhocVien = model.IdhocVien,
                IdlopHoc = model.IdlopHoc,
                Diem = model.Diem,
                NgayThongBao = model.NgayThongBao,
                TrangThaiThongBao = model.TrangThaiThongBao,
                SoLanVangMat = model.SoLanVangMat,
                LyDoThongBao = model.LyDoThongBao,
                HocPhi = model.HocPhi,
                NgayGioGiaoDich = model.NgayGioGiaoDich,
                TrangThaiThanhToan = model.TrangThaiThanhToan,
            };

            var result = _thongTin.Add(thongTinHocVien);

            var thongTinHocVienDTO = _mapper.Map<ThongTinHocVienDTO>(result);

            return thongTinHocVienDTO;
        }
    }
}
