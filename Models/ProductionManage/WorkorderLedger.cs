using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.ProductionManage
{
    public class WorkorderLedger
    {

        public string SubLot_ck { get; set; }
        public string item_cd { get; set; }
        public string item_nm { get; set; }
        public string lot_no { get; set; }
        public string order_qty { get; set; }
        public string order_bigo { get; set; }
        public string order_no { get; set; }
        public string order_id { get; set; }
        public string order_date { get; set; }
        public string making_dept_cd { get; set; }
        public string making_dept_cd2 { get; set; }
        public string batch_size { get; set; }
        public string PB_date { get; set; }
        public string lot_date { get; set; }
        public string valid_date { get; set; }
        public string revision_no { get; set; }
        public string order_request_no { get; set; }
        public string workorder_seq { get; set; }
        public string gubun { get; set; }


        public WorkorderLedger()
        {

        }

    }
}
