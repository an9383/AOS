using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.RowMatWh
{
    public class MaterialReserveQtyList
    {
        //SP구분자
        public string Gubun { get; set; }
        public string item_cd { get; set; }
        public string s_item_cd { get; set; }
        public string option { get; set; }
    }

}
