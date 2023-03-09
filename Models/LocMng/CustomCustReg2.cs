using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.LocMng
{
    public class CustomCustReg2
    {
        //SP구분자
        public string Gubun { get; set; }

        //검색조건
        public string s_cust_cd { get; set; }

        //SP 결과값들
        public string plant_cd { get; set; }
        public string cust_cd { get; set; }
        public string custIn_cd { get; set; }
        public string custIn_remark { get; set; }
    
        public CustomCustReg2()
        {

        }

        public CustomCustReg2(DataRow row)
        {
            this.s_cust_cd = row["s_cust_cd"].ToString();
            this.plant_cd = row["plant_cd"].ToString();
            this.cust_cd = row["cust_cd"].ToString();
            this.custIn_cd = row["custIn_cd"].ToString();
            this.custIn_remark = row["custIn_remark"].ToString();
        }
    }
}
