using AutoMapper;
using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using MiniExcelLibs;
using Services.MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace Services
{
    public class ThongTinHocVienService
    {
        private readonly IMailService _mail;
        private readonly IMapper _mapper;
        private readonly IThongTinHocVienRepo _thongTin;
        private readonly IDoiTuongDangKyRepo _doiTuongDangKy;
        private readonly ITrangThaiHocVienRepo _trangThai;
        private readonly IKhoaHocRepo _khoaHoc;
        private readonly IGiaoVienRepo _giaoVien;

        public ThongTinHocVienService(IRepo thongTin, IMapper mapper, IMailService mail)
        {
            _mail = mail;
            _mapper = mapper;
            _thongTin = thongTin.ThongTinHocVienRepo;
            _doiTuongDangKy = thongTin.DoiTuongDangKyRepo;
            _trangThai = thongTin.TrangThaiHocVienRepo;
            _khoaHoc = thongTin.KhoaHocRepo;
            _giaoVien = thongTin.GiaoVienRepo;

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
                IdhocVien = s.IdhocVien,
                IdlopHoc = s.IdlopHoc,
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

        public List<ThongTinHocVienDTO> Search(string tenHocVien)
        {
            var dsThongTinHocVien = _thongTin.Search(tenHocVien);

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

        //public MemoryStream Export()
        //{
        //    var dsThongTin = GetAll();
        //    var dsDoiTuong= _doiTuongDangKy.GetAll();
        //    var dsTrangThai= _trangThai.GetAll();
        //    var dsKhoaHoc= _khoaHoc.GetAll();
        //    var dsGiaoVien=_giaoVien.GetAll();

        //    var dsThongTinHocVienDTO = dsThongTin.Select(s => new 
        //    {
        //        MaHocVien = s.IdhocVien,
        //        TenHocVien =s.IdhocVienNavigation.TenHocVien,
        //        NgaySinh = s.IdhocVienNavigation.NgaySinh,
        //        Email = s.IdhocVienNavigation.Email,
        //        SDT = s.IdhocVienNavigation.Sdt,
        //        DiaChi = s.IdhocVienNavigation.DiaChi,
        //        NgayDangKy = s.IdhocVienNavigation.NgayDangKy,
        //        DoiTuong = dsDoiTuong
        //            .Where(x => x.IddoiTuong == s.IdhocVienNavigation.IddoiTuong)
        //            .Select(x => x.DoiTuongDangKy1)
        //            .FirstOrDefault(),
        //        TrangThai = dsTrangThai
        //            .Where(x => x.IdtrangThai == s.IdhocVienNavigation.IdtrangThai)
        //            .Select(x => x.TenTrangThai)
        //            .FirstOrDefault(),
        //        LopHoc=s.IdlopHocNavigation.IdlopHoc,
        //        TenLopHoc = s.IdlopHocNavigation.TenLopHoc,
        //        ThoiGian = s.IdlopHocNavigation.ThoiGian,
        //        NgayBatDau = s.IdlopHocNavigation.NgayBatDau,
        //        NgayKetThuc = s.IdlopHocNavigation.NgayKetThuc,
        //        DiaDiem = s.IdlopHocNavigation.DiaDiem,
        //        PhongHoc=s.IdlopHocNavigation.PhongHoc,
        //        KhoaHoc = dsKhoaHoc
        //            .Where(x => x.IdkhoaHoc == s.IdlopHocNavigation.IdkhoaHoc)
        //            .Select(x => x.TenKhoaHoc)
        //            .FirstOrDefault(),
        //        GiaoVien = dsGiaoVien
        //            .Where(x => x.IdgiaoVien == s.IdlopHocNavigation.IdgiaoVien)
        //            .Select(x => x.TenGiaoVien)
        //            .FirstOrDefault(),
        //        Diem = s.Diem,
        //        NgayThongBao = s.NgayThongBao,
        //        TrangThaiThongBao = s.TrangThaiThongBao!,
        //        SoLanVangMat = s.SoLanVangMat,
        //        LyDoThongBao = s.LyDoThongBao,
        //        HocPhi = s.HocPhi,
        //        NgayGioGiaoDich = s.NgayGioGiaoDich!,
        //        TrangThaiThanhToan = s.TrangThaiThanhToan,


        //    }).ToList();

        //    var memoryStream = new MemoryStream();
        //    memoryStream.SaveAs(dsThongTinHocVienDTO);
        //    memoryStream.Seek(0, SeekOrigin.Begin);

        //    return memoryStream;
        //}

        public MemoryStream Export()
        {
            var dsThongTin = GetAll();
            var dsDoiTuong = _doiTuongDangKy.GetAll();
            var dsTrangThai = _trangThai.GetAll();
            var dsKhoaHoc = _khoaHoc.GetAll();
            var dsGiaoVien = _giaoVien.GetAll();

            var dsThongTinHocVienDTO = dsThongTin.Select(s => new
            {
                MaHocVien = s.IdhocVien,
                TenHocVien = s.IdhocVienNavigation!.TenHocVien,
                LopHoc = s.IdlopHocNavigation!.IdlopHoc,
                TenLopHoc = s.IdlopHocNavigation.TenLopHoc,
                Diem = s.Diem,
                NgayThongBao = s.NgayThongBao,
                TrangThaiThongBao = s.TrangThaiThongBao!,
                SoLanVangMat = s.SoLanVangMat,
                LyDoThongBao = s.LyDoThongBao,
                HocPhi = s.HocPhi,
                NgayGioGiaoDich = s.NgayGioGiaoDich!,
                TrangThaiThanhToan = s.TrangThaiThanhToan,


            }).ToList();

            var memoryStream = new MemoryStream();
            memoryStream.SaveAs(dsThongTinHocVienDTO);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        public bool Import(string? fileName)
        {
            //string FilePath = Path.Combine(fileName);
            //if (!File.Exists(FilePath)) 
            //    return false;

            var rows = MiniExcel.Query(fileName).ToList();

            foreach (var row in rows)
            {
                var thongTinHocVien = new ThongTinHocVien();

                if (int.TryParse(row[0].ToString(), out int idHocVien))
                {
                    thongTinHocVien.IdhocVien = idHocVien;
                }
                else
                {
                    thongTinHocVien.IdhocVien = 0;
                }

                thongTinHocVien.IdhocVienNavigation.TenHocVien = row[1].ToString();

                // Parsing IdlopHoc
                if (int.TryParse(row[2].ToString(), out int idLopHoc))
                {
                    thongTinHocVien.IdlopHoc = idLopHoc;
                }
                else
                {
                    thongTinHocVien.IdlopHoc = 0;
                }

                thongTinHocVien.IdlopHocNavigation.TenLopHoc = row[3].ToString(); // Correct index

                // Parsing Diem
                if (decimal.TryParse(row[4].ToString(), out decimal diem))
                {
                    thongTinHocVien.Diem = diem;
                }
                else
                {
                    thongTinHocVien.Diem = 0.0m;
                }

                // Parsing NgayThongBao
                if (DateTime.TryParse(row[5].ToString(), out DateTime ngayThongBao))
                {
                    thongTinHocVien.NgayThongBao = ngayThongBao;
                }
                else
                {
                    thongTinHocVien.NgayThongBao = default(DateTime);
                }

                // Parsing TrangThaiThongBao
                if (bool.TryParse(row[6].ToString(), out bool trangThaiThongBao))
                {
                    thongTinHocVien.TrangThaiThongBao = trangThaiThongBao;
                }
                else
                {
                    thongTinHocVien.TrangThaiThongBao = false;
                }

                // Parsing SoLanVangMat
                if (int.TryParse(row[7].ToString(), out int soLanVangMat))
                {
                    thongTinHocVien.SoLanVangMat = soLanVangMat;
                }
                else
                {
                    thongTinHocVien.SoLanVangMat = 0;
                }

                thongTinHocVien.LyDoThongBao = row[8].ToString();

                // Parsing HocPhi
                if (decimal.TryParse(row[9].ToString(), out decimal hocPhi))
                {
                    thongTinHocVien.HocPhi = hocPhi;
                }
                else
                {
                    thongTinHocVien.HocPhi = 0.0m;
                }

                // Parsing NgayGioGiaoDich
                if (DateTime.TryParse(row[10].ToString(), out DateTime ngayGioGiaoDich))
                {
                    thongTinHocVien.NgayGioGiaoDich = ngayGioGiaoDich;
                }
                else
                {
                    thongTinHocVien.NgayGioGiaoDich = default(DateTime);
                }

                // Parsing TrangThaiThanhToan
                if (bool.TryParse(row[11].ToString(), out bool trangThaiThanhToan))
                {
                    thongTinHocVien.TrangThaiThanhToan = trangThaiThanhToan;
                }
                else
                {
                    thongTinHocVien.TrangThaiThanhToan = false;
                }

                var update = _thongTin.Update(thongTinHocVien);
            }
            return true;
        }
    }
}
