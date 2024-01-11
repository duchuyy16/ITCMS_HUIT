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
    public class HocVienService
    {
        private readonly IMailService _mail;
        private readonly IMapper _mapper;
        private readonly IHocVienRepo _hocVien;
        private readonly ILopHocRepo _lopHoc;
        private readonly IThongTinHocVienRepo _thongTin;
        public HocVienService(IRepo repo, IMapper mapper, IMailService mail)
        {
            _mail = mail;
            _mapper = mapper;
            _hocVien = repo.HocVienRepo;
            _lopHoc = repo.LopHocRepo;
            _thongTin = repo.ThongTinHocVienRepo;
        }

        public int Count()
        {
            return _hocVien.Count();
        }

        public bool IsExist(int id)
        {
            return _hocVien.IsExist(id);
        }

        public bool IsEmailExist(string email)
        {
            return _hocVien.IsEmailExist(email);
        }

        public bool Update(HocVienDTO model)
        {
            var hocVien = new HocVien
            {
                IdhocVien = model.IdhocVien,
                TenHocVien = model.TenHocVien,
                NgaySinh = model.NgaySinh,
                Email = model.Email,
                Sdt = model.Sdt!,
                DiaChi = model.DiaChi,
                NgayDangKy = model.NgayDangKy,
                IddoiTuong=model.IddoiTuong,
                IdtrangThai=model.IdtrangThai,
            };

            var result = _hocVien.Update(hocVien);

            return result;
        }

        public bool Delete(HocVienDTO model)
        {
            var hocVien = _hocVien.GetById(model.IdhocVien);

            var thongTinHocVien = _thongTin.GetByIdHocVien(model.IdhocVien);

            var deleteTTHV = _thongTin.Delete(thongTinHocVien);

            var result = _hocVien.Delete(hocVien);

            return result;
        }

        private bool IsEmailExistInClass(string email, int idLopHoc)
        {
            return _thongTin.GetAll().Any(tt => tt.IdhocVienNavigation.Email.Equals(email) && tt.IdlopHoc == idLopHoc);
        }


        public HocVienDTO Add(HocVienDTO model, int idLopHoc)
        {
            var hocVien = new HocVien
            {
                TenHocVien = model.TenHocVien,
                NgaySinh = model.NgaySinh,
                Email = model.Email,
                Sdt = model.Sdt!,
                DiaChi = model.DiaChi,
                NgayDangKy = DateTime.Now,
                IddoiTuong = model.IddoiTuong,
                IdtrangThai = 2,
            };

            // Kiểm tra Email tồn tại và thuộc lớp học
            if (IsEmailExistInClass(model.Email, idLopHoc))            
                return null!;

            //var dsTTHocVien = _thongTin.GetAll();
            //if (IsEmailExist(model.Email))
            //{
            //    foreach (var item in dsTTHocVien)
            //    {
            //        if (item.IdhocVienNavigation.Email.Equals(model.Email))
            //            if(item.IdlopHoc==idLopHoc)
            //            return null!;
            //    }    
            //}

            var result = _hocVien.Add(hocVien);          

            var addTTHV = _thongTin.Add(new ThongTinHocVien { IdhocVien = result!.IdhocVien, IdlopHoc = idLopHoc });        

            var hocVienDTO = _mapper.Map<HocVienDTO>(result);

            _mail.NotifyStudentRegistration(hocVienDTO.TenHocVien, hocVienDTO.Email);

            return hocVienDTO;
        }

        public List<HocVienDTO> GetAll()
        {
            var dsHocVien = _hocVien.GetAll();

            var dsHocVienDTO = dsHocVien.Select(s => new HocVienDTO
            {
                IdhocVien = s.IdhocVien,
                TenHocVien = s.TenHocVien,
                NgaySinh = s.NgaySinh,
                Email = s.Email,
                Sdt = s.Sdt,
                DiaChi = s.DiaChi,
                NgayDangKy = s.NgayDangKy,
                IddoiTuong = s.IddoiTuong,
                IdtrangThai = s.IdtrangThai,
                IddoiTuongNavigation = s.IddoiTuongNavigation.Adapt<DoiTuongDangKyDTO>(),
                IdtrangThaiNavigation = s.IdtrangThaiNavigation.Adapt<TrangThaiHocVienDTO>(),
            }).ToList();


            return dsHocVienDTO;
        }

        public List<HocVienDTO> Search(string keyword)
        {
            var dsHocVien = _hocVien.Search(keyword);

            var dsHocVienDTO = dsHocVien.Select(s => new HocVienDTO
            {
                IdhocVien = s.IdhocVien,
                TenHocVien = s.TenHocVien,
                NgaySinh = s.NgaySinh,
                Email = s.Email,
                Sdt = s.Sdt,
                DiaChi = s.DiaChi,
                NgayDangKy = s.NgayDangKy,
                IddoiTuong = s.IddoiTuong,
                IdtrangThai = s.IdtrangThai,
                IddoiTuongNavigation = s.IddoiTuongNavigation.Adapt<DoiTuongDangKyDTO>(),
                IdtrangThaiNavigation = s.IdtrangThaiNavigation.Adapt<TrangThaiHocVienDTO>(),

            }).ToList();

            return dsHocVienDTO;
        }

        public HocVienDTO GetById(int id)
        {
            var hocVien = _hocVien.GetById(id);

            var hocVienDTO = new HocVienDTO
            {
                IdhocVien = hocVien.IdhocVien,
                TenHocVien = hocVien.TenHocVien,
                NgaySinh = hocVien.NgaySinh,
                Email = hocVien.Email,
                Sdt = hocVien.Sdt,
                DiaChi = hocVien.DiaChi,
                NgayDangKy = hocVien.NgayDangKy,
                IddoiTuong = hocVien.IddoiTuong,
                IdtrangThai = hocVien.IdtrangThai,
                IddoiTuongNavigation = hocVien.IddoiTuongNavigation.Adapt<DoiTuongDangKyDTO>(),
                IdtrangThaiNavigation = hocVien.IdtrangThaiNavigation.Adapt<TrangThaiHocVienDTO>(),
            };

            return hocVienDTO;
        }
    }
}
