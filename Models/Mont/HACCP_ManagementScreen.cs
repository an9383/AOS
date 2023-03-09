using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Mont
{
    public class HACCP_ManagementScreen
    {
        public string gubun { get; set; }
        public string Auth { get; set; }
        public string SiteCode { get; set; }       //사이트코드
        public string HaccpCode { get; set; }       //문서코드
        public string doc_name { get; set; }        //문서명
        public string ChgSerNo { get; set; }        //revision
        public string EmpCode { get; set; }         //담당자코드
        public string BDate { get; set; }           //작성일자
        public string EDate { get; set; }           //종료일자
        public string use_yn { get; set; }          //사용여부
        public string FinalConfirm { get; set; }    //최종승인


        public string CodeCode { get; set; }           //항목코드
        public string CodeName { get; set; }           //항목명
        public string CodeUse { get; set; }            //사용여부
        public string ParentCode { get; set; }         //상위코드
        public string CodeDescr { get; set; }          //비고
        public string MakeDate { get; set; }           //작성일자
        
        public string CCP_gubun { get; set; }          //CCP 공정구분 코드
        public string process_cd { get; set; }          //대표공정코드
        public string CCP_sdate { get; set; }           //CCP시작일자
        public string CCP_edate { get; set; }           //CCP종료일자
        public string CCP_item_cd { get; set; }         //CCP품목코드
        public string order_no { get; set; }            //대표공정코드
        public string item_cd { get; set; }             //품목코드
        public string lot_no { get; set; }              //lot번호
        public string ccp_cd { get; set; }              //CCP코드
        public string index_key { get; set; }           //문서별 전자서명 구분값
        public string sign_set_cd { get; set; }           //서명코드

    }
}