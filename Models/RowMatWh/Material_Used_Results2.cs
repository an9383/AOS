using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.RowMatWh
{
    public class Material_Used_Results2
    {
        public string Gubun { get; set; }
        public string S_ORDER_START_DATE { get; set; } //지시일자 시작
        public string S_ORDER_END_DATE { get; set; }    //지시일자 끝
        public string S_ITEM_CD { get; set; }   
        public string S_LOT_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string PROCESS_CD { get; set; }
        public string INPUT_ORDER_ID { get; set; }
    }
}
