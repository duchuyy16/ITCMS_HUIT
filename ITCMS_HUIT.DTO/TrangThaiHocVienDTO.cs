using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.DTO
{
    public class TrangThaiHocVienDTO
    {
        public int IdtrangThai { get; set; }
        public string TenTrangThai { get; set; } = null!;
        public string MoTa { get; set; } = null!;

        public List<HocVienDTO>? HocViens { get; set; }
    }
}
