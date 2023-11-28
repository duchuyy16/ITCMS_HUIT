using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.DTO
{
    public class ChuongTrinhDaoTaoDTO
    {
        public int IdchuongTrinh { get; set; }
        public string TenChuongTrinh { get; set; } = null!;
        public List<KhoaHocModel>? KhoaHocs { get; set;}
    }

    
}
