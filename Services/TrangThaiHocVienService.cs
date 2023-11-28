using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TrangThaiHocVienService
    {
        private readonly ITrangThaiHocVienRepo _trangThai;
        public TrangThaiHocVienService(IRepo _repo)
        {
            _trangThai = _repo.TrangThaiHocVienRepo;
        }

        public List<TrangThaiHocVienDTO> GetAll()
        {
            var dsTrangThaiHocVien = _trangThai.GetAll();

            var dsTrangThaiHocVienDTO = dsTrangThaiHocVien.Select(x => new TrangThaiHocVienDTO
            {
                IdtrangThai=x.IdtrangThai,
                TenTrangThai=x.TenTrangThai,
                MoTa=x.MoTa,
            }).ToList();

            return dsTrangThaiHocVienDTO;
        }
    }
}
