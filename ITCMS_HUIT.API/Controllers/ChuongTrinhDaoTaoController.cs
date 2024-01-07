using ITCMS_HUIT.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuongTrinhDaoTaoController : ControllerBase
    {
        private readonly ChuongTrinhDaoTaoService _chuongTrinhDaoTao;
        public ChuongTrinhDaoTaoController(ChuongTrinhDaoTaoService chuongTrinhDaoTao)
        {
            _chuongTrinhDaoTao = chuongTrinhDaoTao;
        }

        [HttpPost("danh-sach-chuong-trinh-dao-tao")]
        public IActionResult GetAll()
        {
            try
            {
                List<ChuongTrinhDaoTaoDTO> chuongTrinhList = _chuongTrinhDaoTao.GetAll();

                var apiResponse = new ApiResponse<List<ChuongTrinhDaoTaoDTO>>
                {
                    Status = "Thành công",
                    Message = "Danh sách chương trình đào tạo",
                    Data = chuongTrinhList
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<ChuongTrinhDaoTaoDTO>> { Status = "Lỗi", Message = ex.Message });
            }
        }

    }
}
