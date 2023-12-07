using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaHocController : ControllerBase
    {
        private readonly KhoaHocService _khoaHoc;
        public KhoaHocController(KhoaHocService khoaHoc)
        {
            _khoaHoc = khoaHoc;
        }

        [HttpPost("kiem-tra-ton-tai/{id}")]
        public IActionResult IsExist(int id)
        {
            try
            {
                bool isExist = _khoaHoc.IsExist(id);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = "Thành công",
                    Message = isExist ? "Khóa học tồn tại" : "Khóa học không tồn tại",
                    Data = isExist
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("danh-sach-khoa-hoc")]
        public IActionResult GetAll()
        {
            try
            {
                List<KhoaHocDTO> khoaHocList = _khoaHoc.GetAll();

                var apiResponse = new ApiResponse<List<KhoaHocDTO>>
                {
                    Status = "Success",
                    Message = "Danh sách khóa học",
                    Data = khoaHocList
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("tim-kiem-khoa-hoc/{keyword}")]
        public IActionResult Search(string keyword)
        {
            try
            {
                List<KhoaHocDTO> searchResult = _khoaHoc.Search(keyword);

                var apiResponse = new ApiResponse<List<KhoaHocDTO>>
                {
                    Status = "Thành công",
                    Message = "Kết quả tìm kiếm khóa học",
                    Data = searchResult
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("danh-sach-khoa-hoc-theo-chuong-trinh/{id}")]
        public IActionResult GetByIdCTDT(int id)
        {
            try
            {
                List<KhoaHocDTO> khoaHocList = _khoaHoc.GetByIdCTDT(id);

                var apiResponse = new ApiResponse<List<KhoaHocDTO>>
                {
                    Status = "Thành công",
                    Message = "Danh sách khóa học theo chương trình đào tạo",
                    Data = khoaHocList
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("chi-tiet-khoa-hoc/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                KhoaHocDTO khoaHoc = _khoaHoc.GetById(id);

                var apiResponse = new ApiResponse<KhoaHocDTO>
                {
                    Status = "Thành công",
                    Message = "Thông tin chi tiết khóa học",
                    Data = khoaHoc
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }


        [HttpPost("xoa-khoa-hoc")]
        public IActionResult Delete(KhoaHocDTO model)
        {
            try
            {
                bool deletionResult = _khoaHoc.Delete(model);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = "Thành công",
                    Message = deletionResult ? "Xóa khóa học thành công" : "Không thể xóa khóa học",
                    Data = deletionResult
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("cap-nhat-khoa-hoc")]
        public IActionResult Update(KhoaHocDTO model)
        {
            try
            {
                bool updateResult = _khoaHoc.Update(model);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = "Thành công",
                    Message = updateResult ? "Cập nhật khóa học thành công" : "Không thể cập nhật khóa học",
                    Data = updateResult
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("them-khoa-hoc")]
        public IActionResult Add(KhoaHocDTO model)
        {
            try
            {
                KhoaHocDTO addedKhoaHoc = _khoaHoc.Add(model);

                var apiResponse = new ApiResponse<KhoaHocDTO>
                {
                    Status = "Thành công",
                    Message = "Thêm khóa học thành công",
                    Data = addedKhoaHoc
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
