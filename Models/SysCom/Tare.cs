using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.SysCom
{
    public class Tare
    {
        public string gubun { get; set; }
        public string taremaster_type { get; set; }
        public string taremaster_material { get; set; }
        public string taremaster_class { get; set; }
        public string taremaster_volume { get; set; }
        public string taremaster_dimension { get; set; }
        public string taredetail_cd { get; set; }
        public string taredetail_mass { get; set; }
        public string taredetail_use_date { get; set; }
        public string taredetail_gb { get; set; }
        public string taredetail_disuse_date { get; set; }
        public string taredetail_use_ck { get; set; }

    }
}