using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopHocController : ControllerBase
    {
        private readonly LopHocService _lopHoc;
        public LopHocController(LopHocService lophoc)
        {
            _lopHoc = lophoc;
        }

        [HttpPost("kiem-tra-ton-tai/{id}")]
        public IActionResult IsExist(int id)
        {
            try
            {
                bool isExist = _lopHoc.IsExist(id);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = "Thành công",
                    Message = isExist ? "Lớp học tồn tại" : "Lớp học không tồn tại",
                    Data = isExist
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("danh-sach-lop-hoc")]
        public IActionResult GetAll()
        {
            try
            {
                List<LopHocDTO> lopHocList = _lopHoc.GetAll();

                var apiResponse = new ApiResponse<List<LopHocDTO>>
                {
                    Status = "Thành công",
                    Message = "Danh sách lớp học",
                    Data = lopHocList
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("lop-hoc/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                LopHocDTO lopHoc = _lopHoc.GetById(id);

                var apiResponse = new ApiResponse<LopHocDTO>
                {
                    Status = "Thành công",
                    Message = "Thông tin lớp học",
                    Data = lopHoc
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("tim-kiem-lop-hoc/{keyword}")]
        public IActionResult Search(string keyword)
        {
            try
            {
                List<LopHocDTO> searchResults = _lopHoc.Search(keyword);

                var apiResponse = new ApiResponse<List<LopHocDTO>>
                {
                    Status = "Thành công",
                    Message = "Kết quả tìm kiếm lớp học",
                    Data = searchResults
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("xoa-lop-hoc")]
        public IActionResult Delete(LopHocDTO model)
        {
            try
            {
                bool deletionResult = _lopHoc.Delete(model);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = deletionResult ? "Thành Công" : "Lỗi",
                    Message = deletionResult ? "Xóa lớp học thành công" : "Không thể xóa lớp học",
                    Data = deletionResult
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("cap-nhat-lop-hoc")]
        public IActionResult Update(LopHocDTO model)
        {
            try
            {
                bool updateResult = _lopHoc.Update(model);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = updateResult ? "Thành công" : "Lỗi",
                    Message = updateResult ? "Cập nhật lớp học thành công" : "Không thể cập nhật lớp học",
                    Data = updateResult
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("them-lop-hoc")]
        public IActionResult Add(LopHocDTO model)
        {
            try
            {
                LopHocDTO addedLopHoc = _lopHoc.Add(model);

                var apiResponse = new ApiResponse<LopHocDTO>
                {
                    Status = "Thành công",
                    Message = "Thêm lớp học thành công",
                    Data = addedLopHoc
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
