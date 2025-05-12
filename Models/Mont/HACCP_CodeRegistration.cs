using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Mont
{
    public class HACCP_CodeRegistration
    {
        public string gubun { get; set; }
        public string SiteCode { get; set; }       //사이트코드
        public string HaccpCode { get; set; }       //문서코드
        public string doc_name { get; set; }        //문서명
        public string ChgSerNo { get; set; }        //revision
        public string EmpCode { get; set; }         //담당자코드
        public string BDate { get; set; }           //작성일자
        public string EDate { get; set; }           //종료일자
        public string use_yn { get; set; }          //사용여부
        public string FinalConfirm { get; set; }    //최종승인
        public string CCP_yn { get; set; }    //CCP사용여부


        public string CodeCode { get; set; }           //항목코드
        public string CodeName { get; set; }           //항목명
        public string CodeUse { get; set; }           //사용여부
        public string ParentCode { get; set; }           //상위코드
        public string CodeDescr { get; set; }           //비고


    }
}