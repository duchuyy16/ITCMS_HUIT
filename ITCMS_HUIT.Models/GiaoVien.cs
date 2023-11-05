﻿using System;
using System.Collections.Generic;

namespace ITCMS_HUIT.Models
{
    public partial class GiaoVien
    {
        public GiaoVien()
        {
            LopHocs = new HashSet<LopHoc>();
        }

        public int IdgiaoVien { get; set; }
        public string TenGiaoVien { get; set; } = null!;
        public string TrinhDo { get; set; } = null!;
        public string ChungChi { get; set; } = null!;
        public string HoSoCaNhan { get; set; } = null!;

        public virtual ICollection<LopHoc> LopHocs { get; set; }
    }
}
