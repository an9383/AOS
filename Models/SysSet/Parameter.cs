using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysSet
{
    public class Parameter
    {

        /// <summary> 
        /// 1. 화면 검색 Parameter CLass 
        /// </summary> 
        public class SrchParam
        {
            /// <summary> 
            /// SP 검색구분 
            /// </summary> 
            public string s_parameter_code { get; set; }
            public string s_parameter_remark { get; set; }

            // default 검색 생성자 
            public SrchParam()
            {
                this.s_parameter_code = "";
                this.s_parameter_remark = "";
            }
        }

        // SP 구분자
        public string Gubun { get; set; }


        ///검색 조건들
        // 파라미터, 설명 구분
        //public string s_parameter_code { get; set; }
        // 시작일자
        //public string s_parameter_remark { get; set; }       

        /// SP 결과값들
        public string parameter_code { get; set; }
        public string parameter_value { get; set; }
        public string parameter_remark { get; set; }


        public Parameter()
        {

        }
    }
}
