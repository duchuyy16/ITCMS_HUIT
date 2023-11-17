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

        [HttpPost("danh-sach-giao-vien")]
        public List<GiaoVienDTO> GetAll()
        {
            return _giaoVien.GetAll();
        }

        [HttpPost("giao-vien/{id}")]
        public GiaoVienDTO GetById (string id)
        {
            return _giaoVien.GetById(id);
        }

        [HttpPost("kiem-tra-ton-tai/{id}")]
        public bool IsExist(string id)
        {
            return _giaoVien.IsExist(id);
        }

        [HttpPost("xoa-giao-vien")]
        public bool Delete(GiaoVienDTO model)
        {
            return _giaoVien.Delete(model);
        }

        [HttpPost("cap-nhat-giao-vien")]
        public bool Update(GiaoVienDTO model)
        {
            return _giaoVien.Update(model);
        }

        [HttpPost("them-giao-vien")]
        public GiaoVienDTO Add(GiaoVienDTO model)
        {
            return _giaoVien.Add(model);
        }
    }
}
