using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.DTO
{
    public class KhoaHocTheoChuongTrinhDTO
    {
        public int IdchuongTrinh { get; set; }
        public int SoLuongKhoaHoc { get; set; }
        public string TenChuongTrinh { get; set; } = null!;      
    }
}
