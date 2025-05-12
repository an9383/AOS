using DevExpress.CodeParser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.Common
{
    public class SelectBoxGubun
    {

        public string keyfield { get; set; }
        public string displayfield { get; set; }
        public string ord { get; set; }

        public SelectBoxGubun()
        {

        }

        public SelectBoxGubun(DataRow row, bool hasord)
        {
            if (hasord)
            {
                keyfield = row["keyfield"].ToString();
                displayfield = row["displayfield"].ToString();
                ord = row["ord"].ToString();
            }

            if (!hasord)
            {
                keyfield = row["keyfield"].ToString();
                displayfield = row["displayfield"].ToString();
            }
            
        }

    }
}
