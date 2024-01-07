using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocVienController : ControllerBase
    {
        private readonly HocVienService _hocVien;
        public HocVienController(HocVienService hocVien)
        {
            _hocVien = hocVien;
        }

        [HttpPost("kiem-tra-ton-tai/{id}")]
        public IActionResult IsExist(int id)
        {
            try
            {
                bool isExist = _hocVien.IsExist(id);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = "Thành công",
                    Message = isExist ? "Học viên tồn tại" : "Học viên không tồn tại",
                    Data = isExist
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<bool> { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("danh-sach-hoc-vien")]
        public IActionResult GetAll()
        {
            try
            {
                List<HocVienDTO> hocVienList = _hocVien.GetAll();

                var apiResponse = new ApiResponse<List<HocVienDTO>>
                {
                    Status = "Thành công",
                    Message = "Danh sách học viên",
                    Data = hocVienList
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<HocVienDTO>> { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("hoc-vien/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                HocVienDTO hocVien = _hocVien.GetById(id);

                var apiResponse = new ApiResponse<HocVienDTO>
                {
                    Status = "Thành công",
                    Message = "Thông tin học viên",
                    Data = hocVien
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<HocVienDTO> { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("tim-kiem-hoc-vien/{keyword}")]
        public IActionResult Search(string keyword)
        {
            try
            {
                List<HocVienDTO> searchResults = _hocVien.Search(keyword);

                var apiResponse = new ApiResponse<List<HocVienDTO>>
                {
                    Status = "Thành công",
                    Message = "Kết quả tìm kiếm học viên",
                    Data = searchResults
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<HocVienDTO>> { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("xoa-hoc-vien")]
        public IActionResult Delete(HocVienDTO model)
        {
            try
            {
                bool deletionResult = _hocVien.Delete(model);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = deletionResult ? "Thành công" : "Error",
                    Message = deletionResult ? "Xóa học viên thành công" : "Không thể xóa học viên",
                    Data = deletionResult
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<bool> { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("cap-nhat-hoc-vien")]
        public IActionResult Update(HocVienDTO model)
        {
            try
            {
                bool updateResult = _hocVien.Update(model);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = updateResult ? "Thành công" : "Lỗi",
                    Message = updateResult ? "Cập nhật học viên thành công" : "Không thể cập nhật học viên",
                    Data = updateResult
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<bool> { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("them-hoc-vien/{idLopHoc}")]
        public IActionResult Add(HocVienDTO model, int idLopHoc)
        {
            try
            {
                HocVienDTO addedHocVien = _hocVien.Add(model, idLopHoc);

                var apiResponse = new ApiResponse<HocVienDTO>
                {
                    Status = "Thành công",
                    Message = "Thêm học viên thành công",
                    Data = addedHocVien
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<HocVienDTO> { Status = "Lỗi", Message = ex.Message });
            }
        }

    }
}
