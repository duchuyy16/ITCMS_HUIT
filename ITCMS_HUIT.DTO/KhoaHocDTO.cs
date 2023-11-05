﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.DTO
{
    public class KhoaHocDTO
    {
        public int IdkhoaHoc { get; set; }
        public int IdchuongTrinh { get; set; }
        public string? TenKhoaHoc { get; set; }
        public int SoGio { get; set; }
        public int SoTuan { get; set; }
        public decimal HocPhi { get; set; }
        public string? Mota { get; set; }

        public ChuongTrinhDaoTaoDTO IdchuongTrinhNavigation { get; set; } = null!;
        public List<LopHocDTO>? LopHocs { get; set; }
    }
}
