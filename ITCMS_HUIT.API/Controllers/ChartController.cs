using ITCMS_HUIT.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        public readonly ChartService _chart;
        public ChartController(ChartService chart)
        {
            _chart = chart;
        }

        [HttpPost("so-luong-khoa-hoc-theo-chuong-trinh")]
        public IActionResult GetCourseCountsByProgram()
        {
            try
            {
                List<KhoaHocTheoChuongTrinhDTO> courseCounts = _chart.GetCourseCountsByProgram();

                var apiResponse = new ApiResponse<List<KhoaHocTheoChuongTrinhDTO>>
                {
                    Status = "Thành công",
                    Message = "Backup thành công.",
                    Data = courseCounts
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("thong-ke-doi-tuong-dang-ky")]
        public IActionResult ThongKeDoiTuongDangKy()
        {
            try
            {
                List<ThongKeDoiTuongDangKyDTO> doiTuongStats = _chart.ThongKeDoiTuongDangKy();

                var apiResponse = new ApiResponse<List<ThongKeDoiTuongDangKyDTO>>
                {
                    Status = "Success",
                    Message = "Backup thành công.",
                    Data = doiTuongStats
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
