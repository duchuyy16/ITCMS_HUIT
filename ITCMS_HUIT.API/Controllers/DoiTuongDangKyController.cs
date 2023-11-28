using ITCMS_HUIT.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoiTuongDangKyController : ControllerBase
    {
        private readonly DoiTuongDangKyService _doiTuong;
        public DoiTuongDangKyController(DoiTuongDangKyService doiTuong)
        {
            _doiTuong = doiTuong;
        }

        [HttpPost("danh-sach-doi-tuong-dang-ky")]
        public List<DoiTuongDangKyDTO> GetAll()
        {
            return _doiTuong.GetAll();

        }
    }
}
