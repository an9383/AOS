using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysOp
{
    public class Group_manage
    {
        // SP 구분자
        public string Gubun { get; set; }


        ///검색 조건들
        // 파라미터, 설명 구분
        public string group_text { get; set; }
        // 시작일자
        public string use_yn_S { get; set; }


        /// SP 결과값들
        [Required]
        public string emp_group_cd { get; set; }
        [Required]
        public string emp_group_nm { get; set; }
        public string remark { get; set; }
        public string use_yn { get; set; }
        public string use_yn_nm { get; set; }


        public Group_manage()
        {

        }

        public Group_manage(DataRow row)
        {
            this.emp_group_cd = row["emp_group_cd"].ToString();
            this.emp_group_nm = row["emp_group_nm"].ToString();
            this.remark = row["remark"].ToString();
            this.use_yn = row["use_yn"].ToString();
            this.use_yn_nm = row["use_yn_nm"].ToString();

        }
    }
}
