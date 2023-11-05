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

        [HttpPost("DanhSachChuongTrinhDaoTao")]
        public List<ChuongTrinhDaoTaoDTO> GetAll()
        {
            return _chuongTrinhDaoTao.GetAll();
          
        }
    }
}
