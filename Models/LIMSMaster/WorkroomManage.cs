using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.LIMSMaster
{
    public class WorkroomManage
    {
        public string gubun { get; set; }
        public string workroom_cd { get; set; }
        public string workroom_location { get; set; }
        public string location_nm { get; set; }
        public string loading_limit { get; set; }
        public string loading_limit_unit { get; set; }
        public string loading_priority { get; set; }
        public string abc_type { get; set; }
    }
}