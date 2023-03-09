using HACCP.Libs.Database;
using HACCP.Models.LIMSMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.LIMSMaster
{
    public class WorkroomManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_WorkroomManage";

        internal DataTable WorkroomManageSelect(WorkroomManage workroomManage)
        {
            string[] pParam = new string[1];
            pParam[0] = "@workroom_S:" + workroomManage.workroom_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "W", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable WorkroomManageSelectLocation(WorkroomManage workroomManage)
        {
            string[] pParam = new string[1];
            pParam[0] = "@workroom_cd:" + workroomManage.workroom_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "R", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string WorkroomManageLocationTRX(WorkroomManage location)
        {
            string[] pParam = new string[7];
            pParam[0] = "@workroom_cd:" + location.workroom_cd;
            pParam[1] = "@workroom_location:" + location.workroom_location;
            pParam[2] = "@location_nm:" + location.location_nm;
            pParam[3] = "@loading_limit:" + location.loading_limit;
            pParam[4] = "@loading_limit_unit:" + location.loading_limit_unit;
            pParam[5] = "@loading_priority:" + location.loading_priority;
            pParam[6] = "@abc_type:" + location.abc_type;

            string res = _bllSpExecute.SpExecuteString(sSpName, location.gubun, pParam);

            return res;
        }
    }
}