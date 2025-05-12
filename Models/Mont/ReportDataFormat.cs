using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Mont
{
    public class ReportDataFormat
    {
        public string gubun { get; set; }
        public string v_type { get; set; }
        public string workroom_cd { get; set; }
        public string workroom_area { get; set; }
        public string doc_cd { get; set; }          //문서코드
        public string doc_seq { get; set; }          //레포트번호
        public string doc_name { get; set; }        //문서명
        public string revision_no { get; set; }     //개정번호
        public string sdate { get; set; }         //시작일
        public string edate {get; set; }           //종료일
        public string stime { get; set; }           //점검시간
        public string index_key { get; set; }           //레포트번호+시작일+종료일


        public string CodeCode { get; set; }
        public string CodeName { get; set; }
        public string result_code { get; set; }
        public string result_1 { get; set; }
        public string result_2 { get; set; }
        public string result_3 { get; set; }
        public string result_4 { get; set; }
        public string result_5 { get; set; }
        public string result_6 { get; set; }

        public string result_temp_mon { get; set; }
        public string result_temp_tue { get; set; }
        public string result_temp_wed { get; set; }
        public string result_temp_thu { get; set; }
        public string result_temp_fri { get; set; }
        public string result_temp_sat { get; set; }
        public string result_hum_mon { get; set; }
        public string result_hum_tue { get; set; }
        public string result_hum_wed { get; set; }
        public string result_hum_thu { get; set; }
        public string result_hum_fri { get; set; }
        public string result_hum_sat { get; set; }
        public string result_press_mon { get; set; }
        public string result_press_tue { get; set; }
        public string result_press_wed { get; set; }
        public string result_press_thu { get; set; }
        public string result_press_fri { get; set; }
        public string result_press_sat { get; set; }

        public string asign_emp_code { get; set; }

        public string deviation_no { get; set; }
        public string dev_date { get; set; }
        public string dev_contents { get; set; }
        public string dev_result { get; set; }
        public string dev_repair_date { get; set; }
        public string dev_location { get; set; }


    }
}