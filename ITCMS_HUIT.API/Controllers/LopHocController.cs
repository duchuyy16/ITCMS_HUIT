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
        public bool IsExist(int id)
        {
            return _lopHoc.IsExist(id);
        }

        [HttpPost("danh-sach-lop-hoc")]
        public List<LopHocDTO> GetAll()
        {
            return _lopHoc.GetAll();
        }

        [HttpPost("lop-hoc/{id}")]
        public LopHocDTO GetById(int id)
        {
            return _lopHoc.GetById(id);
        }

        [HttpPost("tim-kiem-lop-hoc/{keyword}")]
        public List<LopHocDTO> Search(string keyword)
        {
            return _lopHoc.Search(keyword);
        }

        [HttpPost("xoa-lop-hoc")]
        public bool Delete(LopHocDTO model)
        {
            return _lopHoc.Delete(model);
        }

        [HttpPost("cap-nhat-lop-hoc")]
        public bool Update(LopHocDTO model)
        {
            return _lopHoc.Update(model);
        }

        [HttpPost("them-lop-hoc")]
        public LopHocDTO Add(LopHocDTO model)
        {
            return _lopHoc.Add(model);
        }
    }
}
