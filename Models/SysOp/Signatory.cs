using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysOp
{
    public class Signatory
    {
        public string sign_set_cd { get; set; }
        public string sign_set_dt_id { get; set; }
        public string sign_set_dt_seq { get; set; }
        public string sign_set_dt_nm { get; set; }
        public string emp_cd { get; set; }
        public string necessary_yn { get; set; }
        public string representative_yn { get; set; }

        public Signatory()
        {

        }

    }
}
