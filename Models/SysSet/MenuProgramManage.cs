using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysSet
{
    public class MenuProgramManage
    {
        public string gubun { get; set; }


        ///검색 조건들
        public string module_gb { get; set; }

        /// SP 결과값들
        public string form_cd { get; set; }
        public string form_seq { get; set; }
        public string display_nm { get; set; }

        // module_cd, ParentID
        public string module_cd { get; set; }
        public string module_nm { get; set; }
        public string module_seq { get; set; }

        public string ParentID { get; set; }
        public string ID { get; set; }


        public MenuProgramManage()
        {

        }
    }
}
