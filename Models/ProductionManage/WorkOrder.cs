using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.ProductionManage
{
    public class WorkOrder
    {
        public string order_status { get; set; }
        public string order_date { get; set; }
        public string planned_date { get; set; }
        public string start_date { get; set; }
        public string order_qty { get; set; }
        public string order_bigo { get; set; }
        public string cost_cd { get; set; }
        public string valid_date { get; set; }
        public string order_no { get; set; }

        public WorkOrder()
        {

        }
    }
}
