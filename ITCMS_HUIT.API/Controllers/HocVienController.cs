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
        public bool IsExist(int id)
        {
            return _hocVien.IsExist(id);
        }

        [HttpPost("danh-sach-hoc-vien")]
        public List<HocVienDTO> GetAll()
        {
            return _hocVien.GetAll();
        }

        [HttpPost("hoc-vien/{id}")]
        public HocVienDTO GetById(int id)
        {
            return _hocVien.GetById(id);
        }

        [HttpPost("tim-kiem-hoc-vien/{keyword}")]
        public List<HocVienDTO> Search(string keyword)
        {
            return _hocVien.Search(keyword);
        }

        [HttpPost("xoa-hoc-vien")]
        public bool Delete(HocVienDTO model)
        {
            return _hocVien.Delete(model);
        }

        [HttpPost("cap-nhat-hoc-vien")]
        public bool Update(HocVienDTO model)
        {
            return _hocVien.Update(model);
        }

        [HttpPost("them-hoc-vien")]
        public HocVienDTO Add(HocVienDTO model)
        {
            return _hocVien.Add(model);
        }
    }
}
