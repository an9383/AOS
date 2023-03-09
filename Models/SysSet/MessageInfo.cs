using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysSet
{
    public class MessageInfo
    {
        // SP 구분자
        public string Gubun { get; set; }

        /// <summary> 
        /// 1. 화면 검색 Parameter CLass 
        /// </summary> 
        public class SrchParam
        {
            /// <summary> 
            /// SP 검색구분 
            /// </summary> 
            public string sMsg_name { get; set; }

            // default 검색 생성자 
            public SrchParam()
            {
                this.sMsg_name = "";
            }
        }

        ///검색 조건들
        //public string sMsg_name { get; set; }

        // 본문 내용들
        public string MSG_CODE { get; set; }
        public string MSG_TITLE { get; set; }

        public string MSG_NAME { get; set; }
        public string MSG_BIGO { get; set; }

        public MessageInfo()
        {

        }
    }
}
