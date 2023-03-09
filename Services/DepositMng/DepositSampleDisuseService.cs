using HACCP.Libs.Database;
using HACCP.Models.DepositMng;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.DepositMng
{
    public class DepositSampleDisuseService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_DepositSampleDisuse";

        internal DataTable DepositSampleDisuseSelect(DepositSampleUse.SrchParam param)
        {
            string[] pParam = new string[7];
            pParam[0] = "@selecttype:" + param.selecttype;
            pParam[1] = "@sdate:" + param.sdate;
            pParam[2] = "@edate:" + param.edate;
            pParam[3] = "@test_type:" + param.test_type;
            pParam[4] = "@item_cd:" + param.item_cd;
            pParam[5] = "@selecttype2:" + param.selecttype2;
            pParam[6] = "@write_gb:" + param.write_gb;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string DepositSampleDisuseTRX(string[] depositsample_id, string write_gb, string gubun)
        {
            string[] pParam = new string[4];

            int cnt = 0;
            string res = "";

            for (int i = 0; i < depositsample_id.Length; i++)
            {
                pParam[0] = "@depositsample_id:" + depositsample_id[i];
                pParam[1] = "@write_gb:" + write_gb;
                pParam[2] = "@disuse_date:" + DateTime.Now.ToShortDateString();
                pParam[3] = "@disuse_emp_cd:" + HttpContext.Current.Session["LoginCD"];

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

                try
                {
                    var tmpCnt = int.Parse(res);
                    cnt += tmpCnt;
                }
                catch (Exception)
                {
                    return "작업중 오류가 발생하였습니다.";
                }
            }

            return cnt + "건 " + (gubun.Equals("PU") ? "폐기" : "폐기 취소") + "되었습니다.";
        }
    }
}