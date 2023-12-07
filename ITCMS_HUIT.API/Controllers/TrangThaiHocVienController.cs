using ITCMS_HUIT.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrangThaiHocVienController : ControllerBase
    {
        private readonly TrangThaiHocVienService _trangThai;
        public TrangThaiHocVienController(TrangThaiHocVienService trangThai)
        {
            _trangThai = trangThai;
        }

        [HttpPost("danh-sach-trang-thai-hoc-vien")]
        public IActionResult GetAll()
        {
            try
            {
                List<TrangThaiHocVienDTO> trangThaiList = _trangThai.GetAll();

                var apiResponse = new ApiResponse<List<TrangThaiHocVienDTO>>
                {
                    Status = "Thành công",
                    Message = "Danh sách trạng thái học viên",
                    Data = trangThaiList
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
