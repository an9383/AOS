using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.ProductionManage
{
    public class TestProgressStatus
    {
        public string date_option { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string test_type { get; set; }
        public string test_status { get; set; }
        public string search_gubun { get; set; }
        public string search_number { get; set; }
        public string item_form_cd { get; set; }

        public TestProgressStatus()
        {
               
        }
    }
}
