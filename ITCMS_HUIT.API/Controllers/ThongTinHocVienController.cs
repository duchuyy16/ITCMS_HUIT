using ITCMS_HUIT.DTO;
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

        [HttpPost("kiem-tra-ton-tai/{idHocVien}&&{idLopHoc}")]
        public bool IsExist(int idHocVien, int idLopHoc)
        {
            return _thongTin.IsExist(idHocVien, idLopHoc);
        }

        [HttpPost("danh-sach-thong-tin-hoc-vien")]
        public List<ThongTinHocVienDTO> GetAll()
        {
            return _thongTin.GetAll();
        }

        [HttpPost("thong-tin-hoc-vien/{idHocVien}&{idLopHoc}")]
        public ThongTinHocVienDTO GetById(int idHocVien,int idLopHoc)
        {
            return _thongTin.GetById(idHocVien, idLopHoc);
        }

        [HttpPost("tim-kiem-thong-tin-hoc-vien/{idHocVien}")]
        public List<ThongTinHocVienDTO> Search(int idHocVien)
        {
            return _thongTin.Search(idHocVien);
        }

        [HttpPost("xoa-thong-tin-hoc-vien")]
        public bool Delete(ThongTinHocVienDTO model)
        {
            return _thongTin.Delete(model);
        }

        [HttpPost("cap-nhat-thong-tin-hoc-vien")]
        public ThongTinHocVienDTO Update(ThongTinHocVienDTO model)
        {
            return _thongTin.Update(model);
        }

        [HttpPost("them-thong-tin-hoc-vien")]
        public ThongTinHocVienDTO Add(ThongTinHocVienDTO model)
        {
            return _thongTin.Add(model);
        }
    }
}
