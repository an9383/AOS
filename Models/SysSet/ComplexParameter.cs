using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysSet
{
    public class ComplexParameter
    {

        /// <summary> 
        /// 1. 화면 검색 Parameter CLass 
        /// </summary> 
        public class SrchParam
        {
            /// <summary> 
            /// SP 검색구분 
            /// </summary> 
            public string sComplex_cd { get; set; }

            // default 검색 생성자 
            public SrchParam()
            {
                this.sComplex_cd = "";
            }
        }

        // SP 구분자
        public string Gubun { get; set; }


        ///검색 조건들
        // 파라미터, 설명 구분
        public string complex_cd { get; set; }
        public int complex_id { get; set; }

        public string code1 { get; set; }
        public string code2 { get; set; }
        public string code3 { get; set; }
        public string code4 { get; set; }
        public string code5 { get; set; }
        public string code6 { get; set; }
        public string code7 { get; set; }
        public string code8 { get; set; }
        public string code9 { get; set; }
        public string code10 { get; set; }
        public string code11 { get; set; }
        public string code12 { get; set; }


        public ComplexParameter()
        {

        }
    }
}
