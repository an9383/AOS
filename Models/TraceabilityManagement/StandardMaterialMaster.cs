using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Models.TraceabilityManagement
{
    public class StandardMaterialMaster
    {
        public string std_material_cd { get; set; }

        [AllowHtml]
        public string std_material_nm { get; set; }

        [AllowHtml]
        public string ingr_eng_name { get; set; }


        public StandardMaterialMaster()
        {

        }
    }

}

