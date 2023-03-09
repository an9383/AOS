using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysSet
{
    public class Upgrade_v2
    {
        // SP 구분자
        public string Gubun { get; set; }

        ///검색 조건들
        // 파라미터, 설명 구분


        // 본문 내용들
        public string file_id { get; set; }
        public string file_use_yn { get; set; }     // 파일사용여부
        public string file_name { get; set; }       // 파일명
        public string file_version { get; set; }    // 파일버전
        public string file_size { get; set; }       // 파일크기
        public string file_modify_date { get; set; }    // 파일수정일자
        public string file_upload_date { get; set; }    // 업로드일자
        public string remark { get; set; }    // 비고
        public string emp_nm { get; set; }    // 사원


        public Upgrade_v2()
        {

        }

        public Upgrade_v2(DataRow row)
        {
            this.file_id = row["file_id"].ToString();
            this.file_use_yn = row["file_use_yn"].ToString();
            this.file_name = row["file_name"].ToString();
            this.file_version = row["file_version"].ToString();
            this.file_size = row["file_size"].ToString();
            this.file_modify_date = row["file_modify_date"].ToString();
            this.file_upload_date = row["file_upload_date"].ToString();
            this.remark = row["remark"].ToString();
            this.emp_nm = row["emp_nm"].ToString();
        }
    }
}
