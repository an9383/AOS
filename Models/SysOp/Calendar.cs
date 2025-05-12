using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysOp
{
    public class Calendar
    {
        public string anniversary_nm { get; set; }
        public string calendar_cd { get; set; }
        public DateTime calendar_date { get; set; }
        public string calendar_day { get; set; }
        public string calendar_seq { get; set; }
        public string calendar_type { get; set; }
        public string attend_time { get; set; }
        public string leave_time { get; set; }
        public string working_minute { get; set; }
        public string rest_minute { get; set; }
        public string thismonth_yn { get; set; }

    }
}
