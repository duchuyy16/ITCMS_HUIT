using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.DTO
{
    public class GiaoVienDTO
    {
        public int IdgiaoVien { get; set; }
        public string TenGiaoVien { get; set; } = null!;
        public string TrinhDo { get; set; } = null!;
        public string ChungChi { get; set; } = null!;
        public string HoSoCaNhan { get; set; } = null!;

        public List<LopHocDTO>? LopHocs { get; set; }
    }
}
