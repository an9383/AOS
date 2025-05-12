using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.RowMatWh
{
    public class MaterialInManage
    {
        public string gubun { get; set; }
        public string receipt_no { get; set; }
        public string receipt_id { get; set; }
        public string receipt_pack_seq { get; set; }
        public string history_id { get; set; }
        public string s_gubun { get; set; }
        
        public string in_type { get; set; }
        public string in_qty { get; set; }
        public string in_date { get; set; }
        public string in_remark { get; set; }
        public string order_no { get; set; }
        public string input_order_id { get; set; }
        public string process_cd { get; set; }
        public string weighing_seq { get; set; }
        public string receive_flag { get; set; }
        public string receive_time { get; set; }
        public string end_flag { get; set; }
        public string end_time { get; set; }


        public string start_date { get; set; }
        public string end_date { get; set; }
        public string item { get; set; }
        public string update_user_cd { get; set; }
        public string insert_user_cd { get; set; }
        public string item_cd { get; set; }
        public string log_user_id { get; set; }

        public MaterialInManage()
        {

        }
    }
}
