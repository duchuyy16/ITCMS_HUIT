using ITCMS_HUIT.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiaoVienController : ControllerBase
    {
        private readonly GiaoVienService _giaoVien;
        public GiaoVienController(GiaoVienService giaoVien)
        {
            _giaoVien = giaoVien;
        }

        [HttpPost("cap-nhat-giao-vien")]
        public IActionResult Update([FromForm] GiaoVienDTO model, IFormFile? imageFile)
        {
            try
            {
                bool updateResult = _giaoVien.Update(model, imageFile!);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = "Thành công",
                    Message = updateResult ? "Cập nhật giáo viên thành công" : "Không thể cập nhật giáo viên",
                    Data = updateResult
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<bool> { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("danh-sach-giao-vien")]
        public IActionResult GetAll()
        {
            try
            {
                List<GiaoVienDTO> giaoVienList = _giaoVien.GetAll();

                var apiResponse = new ApiResponse<List<GiaoVienDTO>>
                {
                    Status = "Thành công",
                    Message = "Danh sách giáo viên",
                    Data = giaoVienList
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<GiaoVienDTO>> { Status = "Lỗi", Message = ex.Message });
            }
        }

		[HttpPost("tim-kiem-giao-vien/{tenGiaoVien}")]
		public IActionResult Search(string tenGiaoVien)
		{
			try
			{
				List<GiaoVienDTO> giaoVienList = _giaoVien.Search(tenGiaoVien);

				var apiResponse = new ApiResponse<List<GiaoVienDTO>>
				{
					Status = "Thành công",
					Message = "Danh sách giáo viên",
					Data = giaoVienList
				};

				return Ok(apiResponse);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<GiaoVienDTO>> { Status = "Lỗi", Message = ex.Message });
			}
		}

		[HttpPost("giao-vien/{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                GiaoVienDTO giaoVien = _giaoVien.GetById(id);

                var apiResponse = new ApiResponse<GiaoVienDTO>
                {
                    Status = "Thành công",
                    Message = "Thông tin giáo viên",
                    Data = giaoVien
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<GiaoVienDTO> { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("kiem-tra-ton-tai/{id}")]
        public IActionResult IsExist(string id)
        {
            try
            {
                bool isExist = _giaoVien.IsExist(id);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = "Thành công",
                    Message = isExist ? "Giáo viên tồn tại" : "Giáo viên không tồn tại",
                    Data = isExist
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<bool> { Status = "Lỗi", Message = ex.Message });
            }
        }


        [HttpPost("xoa-giao-vien")]
        public IActionResult Delete(GiaoVienDTO model)
        {
            try
            {
                bool deletionResult = _giaoVien.Delete(model);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = "Thành công",
                    Message = deletionResult ? "Xóa giáo viên thành công" : "Không thể xóa giáo viên",
                    Data = deletionResult
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<bool> { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost("them-giao-vien")]
        public IActionResult Add(GiaoVienDTO model)
        {
            try
            {
                GiaoVienDTO addedGiaoVien = _giaoVien.Add(model);

                var apiResponse = new ApiResponse<GiaoVienDTO>
                {
                    Status = "Thành công",
                    Message = "Thêm giáo viên thành công",
                    Data = addedGiaoVien
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<GiaoVienDTO> { Status = "Lỗi", Message = ex.Message });
            }
        }

    }
}
