using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaHocController : ControllerBase
    {
        private readonly KhoaHocService _khoaHoc;
        public KhoaHocController(KhoaHocService khoaHoc)
        {
            _khoaHoc = khoaHoc;
        }

        [HttpPost("kiem-tra-ton-tai/{id}")]
        public bool IsExist(int id)
        {
            return _khoaHoc.IsExist(id);
        }

        [HttpPost("danh-sach-khoa-hoc")]
        public List<KhoaHocDTO> GetAll()
        {
            return _khoaHoc.GetAll();
        }

        [HttpPost("tim-kiem-khoa-hoc/{keyword}")]
        public List<KhoaHocDTO> Search(string keyword)
        {
            return _khoaHoc.Search(keyword);
        }

        [HttpPost("danh-sach-khoa-hoc-theo-chuong-trinh/{id}")]
        public List<KhoaHocDTO> GetByIdCTDT(int id)
        {
            return _khoaHoc.GetByIdCTDT(id);
        }

        [HttpPost("chi-tiet-khoa-hoc/{id}")]
        public KhoaHocDTO GetById(int id)
        {
            return _khoaHoc.GetById(id);
        }

        [HttpPost("xoa-khoa-hoc")]
        public bool Delete(KhoaHocDTO model)
        {
            return _khoaHoc.Delete(model);
        }

        [HttpPost("cap-nhat-khoa-hoc")]
        public bool Update(KhoaHocDTO model)
        {
            return _khoaHoc.Update(model);
        }

        [HttpPost("them-khoa-hoc")]
        public KhoaHocDTO Add(KhoaHocDTO model)
        {
            return _khoaHoc.Add(model);
        }
    }
}
