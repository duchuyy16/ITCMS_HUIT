using ITCMS_HUIT.DTO;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = UserRoles.Teacher + "," + UserRoles.Admin)]
        [HttpPost("dashboard")]
        public IActionResult Count()
        {
            try
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

                var apiResponse = new ApiResponse<Count>
                {
                    Status = "Thành công",
                    Message = "Dữ liệu đếm thành công",
                    Data = result
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = ex.Message });
            }
        }       
    }
}
