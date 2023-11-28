using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DoiTuongDangKyService
    {
        private readonly IDoiTuongDangKyRepo _doiTuongDangKy;
        public DoiTuongDangKyService(IRepo _repo)
        {
            _doiTuongDangKy = _repo.DoiTuongDangKyRepo;
        }

        public List<DoiTuongDangKyDTO> GetAll()
        {
            var dsDoiTuongDangKy = _doiTuongDangKy.GetAll();

            var dsDoiTuongDangKyDTO = dsDoiTuongDangKy.Select(x => new DoiTuongDangKyDTO
            {
                IddoiTuong = x.IddoiTuong,
                DoiTuongDangKy1 = x.DoiTuongDangKy1,
            }).ToList();

            return dsDoiTuongDangKyDTO;
        }
    }
}
