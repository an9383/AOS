using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysCom
{
    public class CommonCode
    {
        [Required]
        public string common_cd { get; set; }
        public string common_part_cd { get; set; }
        public string common_nm { get; set; }
        public string common_part_nm { get; set; }
        public string common_sys_ck { get; set; }
        public string common_module { get; set; }
        public string common_etc { get; set; }
        public string common_remark { get; set; }
        public string use_yn { get; set; }
        public string dis_seq { get; set; }
        public string interface_cd { get; set; }
        public string group_nm { get; set; }

        public CommonCode()
        {

        }
    }
}
