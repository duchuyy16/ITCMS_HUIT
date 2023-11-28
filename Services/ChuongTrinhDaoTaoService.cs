using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ChuongTrinhDaoTaoService
    {
        public readonly IChuongTrinhDaoTaoRepo _chuongTrinhDaoTao;
        public ChuongTrinhDaoTaoService(IRepo ctdt)
        {
            _chuongTrinhDaoTao = ctdt.ChuongTrinhDaoTaoRepo;
        }

        public int Count()
        {
            return _chuongTrinhDaoTao.Count();
        }

        public List<ChuongTrinhDaoTaoDTO> GetAll()
        {
            var dsChuongTrinhDT= _chuongTrinhDaoTao.GetAll();

            var dsChuongTrinhDaoTaoDTO = dsChuongTrinhDT.Select(s => new ChuongTrinhDaoTaoDTO
            {
                IdchuongTrinh = s.IdchuongTrinh,
                TenChuongTrinh = s.TenChuongTrinh!,
                KhoaHocs=s.KhoaHocs.Adapt<List<KhoaHocModel>>()
            }).ToList();

            return dsChuongTrinhDaoTaoDTO;
        }
    }
}
