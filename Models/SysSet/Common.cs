using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysSet
{
    public class Common
    {
        // SP 구분자
        public string Gubun { get; set; }

        ///검색 조건들
        public string sCommon_cd { get; set; }
        public string sCommon_part_nm { get; set; }

        // 본문 내용들
        public string common_cd { get; set; }
        public string common_part_cd { get; set; }
        public string common_nm { get; set; }
        public string common_part_nm { get; set; }
        public string common_sys_ck { get; set; }
        public string common_module { get; set; }
        public string common_etc { get; set; }
        public string common_remark { get; set; }
        public string use_yn { get; set; }
        public string insert_emp { get; set; }
        public string dis_seq { get; set; }
        public string interface_cd { get; set; }
        public string group_nm { get; set; }


        public Common()
        {

        }

        public Common(DataRow row)
        {
            this.common_cd = row["common_cd"].ToString();
            this.common_part_cd = row["common_part_cd"].ToString();
            this.common_nm = row["common_nm"].ToString();
            this.common_part_nm = row["common_part_nm"].ToString();
            this.common_sys_ck = row["common_sys_ck"].ToString();
            this.common_module = row["common_module"].ToString();
            this.common_etc = row["common_etc"].ToString();
            this.common_remark = row["common_remark"].ToString();
            this.use_yn = row["use_yn"].ToString();
            this.insert_emp = row["insert_emp"].ToString();
            this.dis_seq = row["dis_seq"].ToString();
            this.interface_cd = row["interface_cd"].ToString();
            this.group_nm = row["group_nm"].ToString();
        }
    }
}
