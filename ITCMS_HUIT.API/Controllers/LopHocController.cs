using ITCMS_HUIT.DTO;
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
        private readonly LopHocService _lophoc;
        public LopHocController(LopHocService lophoc)
        {
            _lophoc = lophoc;
        }

        [HttpPost("DanhSachLopHoc")]
        public List<LopHocDTO> GetAll()
        {
            return _lophoc.GetAll();
        }

        [HttpPost("DanhSachLopHoc")]
        public LopHocDTO GetById(int id)
        {
            return _lophoc.GetById(id);
        }

        [HttpPost("DanhSachLopHoc")]
        public List<LopHocDTO> Search(string keyword)
        {
            return _lophoc.Search(keyword);
        }
    }
}
