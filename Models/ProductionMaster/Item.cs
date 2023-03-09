using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.ProductionMaster
{
    public class Item
    {
        public string item_cd { get; set; }
        public string item_nm { get; set; }
        public string item_gb_nm { get; set; }

        public Item()
        {

        }

        public Item(DataRow row ,string type)
        {
            if(type == "gb")
            {
                item_cd = row["item_cd"].ToString();
                item_nm = row["item_nm"].ToString();
                item_gb_nm = row["item_gb_nm"].ToString();
            }
            else
            {
                item_cd = row["item_cd"].ToString();
                item_nm = row["item_nm"].ToString();
            }

        }
    }
}