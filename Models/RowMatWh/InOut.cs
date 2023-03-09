using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.RowMatWh
{
    public class InOut
    {
        public string Gubun { get; set; }
        public string s_item { get; set; }
        public string s_test_no { get; set; }
        public string test_no { get; set; }
        public string s_gubun { get; set; }
        public string item_cd { get; set; }
        public string use_ck { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }

        /// <summary>
        /// 입고번호
        /// </summary>
        public string receipt_no { get; set; }
    }
}
