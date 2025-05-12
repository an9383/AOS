using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.LIMSMaster
{
    public class TestManageStepSetting
    {
        /// <summary>
        /// sp 구분자
        /// </summary>
        public string gubun { get; set; }

        /// <summary>
        /// 시험종류
        /// </summary>
        public string test_type { get; set; }

        /// <summary>
        /// 시험관리, 샘플관리
        /// </summary>
        public string process_kind { get; set; }

        /// <summary>
        /// 단계(상태)
        /// </summary>
        public string curr_status { get; set; }

        /// <summary>
        /// 순서
        /// </summary>
        public string test_process_set_seq { get; set; }

        /// <summary>
        /// 프로그램 코드
        /// </summary>
        public string program_cd { get; set; }

        /// <summary>
        /// 다음단계
        /// </summary>
        public string next_status { get; set; }

        /// <summary>
        /// 서명코드
        /// </summary>
        public string sign_set_cd { get; set; }

        /// <summary>
        /// 출력 횟수
        /// </summary>
        public string default_rpt_cnt { get; set; }
        
    }
}