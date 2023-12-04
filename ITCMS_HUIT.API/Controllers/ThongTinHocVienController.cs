﻿using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongTinHocVienController : ControllerBase
    {
        private readonly ThongTinHocVienService _thongTin;
        public ThongTinHocVienController(ThongTinHocVienService thongTin)
        {
            _thongTin = thongTin;
        }

        [HttpPost("kiem-tra-ton-tai/{idHocVien}&{idLopHoc}")]
        public IActionResult IsExist(int idHocVien, int idLopHoc)
        {
            try
            {
                bool isExist = _thongTin.IsExist(idHocVien, idLopHoc);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = "Success",
                    Message = isExist ? "Thông tin học viên tồn tại" : "Thông tin học viên không tồn tại",
                    Data = isExist
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("danh-sach-thong-tin-hoc-vien")]
        public IActionResult GetAll()
        {
            try
            {
                List<ThongTinHocVienDTO> thongTinList = _thongTin.GetAll();

                var apiResponse = new ApiResponse<List<ThongTinHocVienDTO>>
                {
                    Status = "Success",
                    Message = "Danh sách thông tin học viên",
                    Data = thongTinList
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("thong-tin-hoc-vien/{idHocVien}&{idLopHoc}")]
        public IActionResult GetById(int idHocVien, int idLopHoc)
        {
            try
            {
                ThongTinHocVienDTO thongTin = _thongTin.GetById(idHocVien, idLopHoc);

                var apiResponse = new ApiResponse<ThongTinHocVienDTO>
                {
                    Status = "Success",
                    Message = "Thông tin học viên",
                    Data = thongTin
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }


        [HttpPost("tim-kiem-thong-tin-hoc-vien/{idHocVien}")]
        public IActionResult Search(int idHocVien)
        {
            try
            {
                List<ThongTinHocVienDTO> searchResults = _thongTin.Search(idHocVien);

                var apiResponse = new ApiResponse<List<ThongTinHocVienDTO>>
                {
                    Status = "Success",
                    Message = "Kết quả tìm kiếm thông tin học viên",
                    Data = searchResults
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("xoa-thong-tin-hoc-vien")]
        public IActionResult Delete(ThongTinHocVienDTO model)
        {
            try
            {
                bool deletionResult = _thongTin.Delete(model);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = deletionResult ? "Success" : "Error",
                    Message = deletionResult ? "Xóa thông tin học viên thành công" : "Không thể xóa thông tin học viên",
                    Data = deletionResult
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("cap-nhat-thong-tin-hoc-vien")]
        public IActionResult Update(ThongTinHocVienDTO model)
        {
            try
            {
                ThongTinHocVienDTO updatedThongTin = _thongTin.Update(model);

                var apiResponse = new ApiResponse<ThongTinHocVienDTO>
                {
                    Status = "Success",
                    Message = "Cập nhật thông tin học viên thành công",
                    Data = updatedThongTin
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("them-thong-tin-hoc-vien")]
        public IActionResult Add(ThongTinHocVienDTO model)
        {
            try
            {
                ThongTinHocVienDTO addedThongTin = _thongTin.Add(model);

                var apiResponse = new ApiResponse<ThongTinHocVienDTO>
                {
                    Status = "Success",
                    Message = "Thêm thông tin học viên thành công",
                    Data = addedThongTin
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
