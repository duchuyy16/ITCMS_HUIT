using System;
using System.Collections.Generic;

namespace ITCMS_HUIT.Models
{
    public partial class ChuongTrinhDaoTao
    {
        public ChuongTrinhDaoTao()
        {
            KhoaHocs = new HashSet<KhoaHoc>();
        }

        public int IdchuongTrinh { get; set; }
        public string TenChuongTrinh { get; set; } = null!;

        public virtual ICollection<KhoaHoc> KhoaHocs { get; set; }
    }
}
