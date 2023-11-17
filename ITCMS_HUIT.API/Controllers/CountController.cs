using ITCMS_HUIT.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using Services;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountController : ControllerBase
    {
        private readonly LopHocService _lopHoc;
        private readonly HocVienService _hocVien;
        private readonly ChuongTrinhDaoTaoService _ctdt;
        private readonly GiaoVienService _giaoVien;

        public CountController(LopHocService lopHoc, HocVienService hocVien, ChuongTrinhDaoTaoService ctdt, GiaoVienService giaoVien)
        {
            _lopHoc = lopHoc;
            _hocVien = hocVien;
            _ctdt = ctdt;
            _giaoVien = giaoVien;
        }

        [HttpPost("dashboard")]
        public IActionResult Count()
        {
            int demSoLopHoc = _lopHoc.Count();
            int demSoHocVien = _hocVien.Count();
            int demSoChuongTrinhDaoTao = _ctdt.Count();
            int demSoGiaoVien = _giaoVien.Count();

            var result = new Count
            {
                LopHoc = demSoLopHoc,
                GiaoVien = demSoGiaoVien,
                ChuongTrinhDaoTao = demSoChuongTrinhDaoTao,
                HocVien = demSoHocVien
            };

            return Ok(result);
        }
    }
}
