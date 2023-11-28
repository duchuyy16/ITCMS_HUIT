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
        public List<TrangThaiHocVienDTO> GetAll()
        {
            return _trangThai.GetAll();

        }
    }
}
