using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models
{
    public class Menu
    {

        //public string Text { get; set; }
        //public string url { get; set; }
        //public bool Expanded { get; set; }
        //public IEnumerable<Menu> Items { get; set; }

        public string Parent_cd { get; set; }

        public string ID { get; set; }

        public string Child_cd { get; set; }

        public string Child_nm { get; set; }

        public int Module_seq { get; set; }

        public int form_seq { get; set; }

        public int ImageIndex { get; set; }

        public int ord1 { get; set; }

        public int ord2 { get; set; }

        public bool Expanded { get; set; }

        public string programRoot { get; set; }

        public string contUrl { get; set; }

        public string Child_url { get; set; }

        public string Interface { get; set; }

        public IEnumerable<Menu> Items { get; set; }

        public Menu(DataRow row)
        {
            Parent_cd = row["Parent_cd"].ToString();
            ID = row["ID"].ToString();
            Child_cd = row["Child_cd"].ToString();
            Child_nm = row["Child_nm"].ToString();
            Module_seq = Int32.Parse(row["Module_seq"].ToString());
            form_seq = Int32.Parse(row["form_seq"].ToString());
            ImageIndex = Int32.Parse(row["ImageIndex"].ToString());
            ord1 = Int32.Parse(row["ord1"].ToString());
            ord2 = Int32.Parse(row["ord2"].ToString());
            Child_url = (row["Child_url"] == null) ? "" : row["Child_url"].ToString();
            Interface = row["Interface"].ToString();
        }

        public Menu()
        {
        }
    }
}
