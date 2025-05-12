using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.RowMatWh
{
    public class MaterialMonthlyStock
    {
        public string Gubun { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string item_gb { get; set; }
        public string item_cd_S { get; set; }
        public string search_date_S { get; set; }
        public string use_ck_S { get; set; }
        public string obtain_status { get; set; }
    }
}
