using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.ProductionManage
{
    public class Item
    {
        public string item_cd { get; set; }
        public string item_nm { get; set; }

        public Item()
        {

        }

        public Item(DataRow row)
        {
            item_cd = row["item_cd"].ToString();
            item_nm = row["item_nm"].ToString();
        }
    }
}