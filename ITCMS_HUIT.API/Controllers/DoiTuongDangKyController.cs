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
        public IActionResult GetAll()
        {
            try
            {
                List<DoiTuongDangKyDTO> doiTuongList = _doiTuong.GetAll();

                var apiResponse = new ApiResponse<List<DoiTuongDangKyDTO>>
                {
                    Status = "Success",
                    Message = "Danh sách đối tượng đăng ký",
                    Data = doiTuongList
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }

    }
}
