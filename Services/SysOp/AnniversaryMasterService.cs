using HACCP.Libs.Database;
using HACCP.Models.SysOp;
using System;
using System.Data;

namespace HACCP.Services.SysOp
{
    public class AnniversaryMasterService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable Select(string pYear)
        {
            string sSpName = "SP_AnniversaryMaster";
            string sGubun = "S";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun);
            //DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string anniversaryCRUD(Anniversary anniversary, string pGubun)
        {
            string sSpName = "SP_AnniversaryMaster";
            string gubun = pGubun;

            string[] param = new string[1];

            param = getParam(anniversary);

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }

        private string[] getParam(Anniversary anniversary)
        {
            string[] param = new string[9];

            param[0] = "@id:" + (anniversary.id == null ? "0" : anniversary.id);
            param[1] = "@lunar_yn:" + anniversary.lunar_yn;
            param[2] = "@anniversary_date:" + anniversary.anniversary_date;
            param[3] = "@anniversary_nm:" + anniversary.anniversary_nm;
            param[4] = "@calendar_type:" + anniversary.calendar_type;
            param[5] = "@attend_time:" + anniversary.attend_time;
            param[6] = "@leave_time:" + anniversary.leave_time;
            param[7] = "@working_minute:" + anniversary.working_minute;
            param[8] = "@rest_minute:" + anniversary.rest_minute;

            return param;
        }
    }
}