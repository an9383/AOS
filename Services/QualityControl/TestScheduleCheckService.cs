using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.QualityControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.QualityControl
{
    public class TestScheduleCheckService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_TestScheduleCheck";        
        //private string sign_set_cd = "1007";

        #region 조회영역
        internal DataTable Select(TestScheduleCheck.SrchParam srchParam)
        {

            string[] pParam = GetParam(srchParam, null);
           
            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, srchParam.Gubun, pParam);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        #endregion

        #region Trx영역
        // 필요시 주석해지하여 사용
        //internal string TestRecognitionETRX(TestRecognitionE dto)
        //{
        //    string[] pParam = null;

        //    switch (dto.gubun)
        //    {
        //        case "I":
        //        case "U":
        //            pParam = GetParam(dto);
        //            break;

        //        case "D":

        //            pParam = new string[3];

        //            pParam[0] = "@testcontrol_id:" + dto.testcontrol_id;
        //            pParam[1] = "@program_cd:" + program_cd;
        //            pParam[2] = "@test_type:" + dto.test_type;

        //            break;
        //    }

        //    string res = _bllSpExecute.SpExecuteString(sSpName, dto.gubun, pParam);

        //    return res;
        //}

        #endregion

        #region 프로그램별 영역
       
        #endregion
        #region sp param 설정
        private string[] GetParam(TestScheduleCheck.SrchParam srch, TestScheduleCheck dto)
        {
            string[] pParam = null;
                       
            if ("S".Equals(srch.Gubun))
            {
                pParam = new string[5];

                pParam[0] = "@start_date:" + srch.de_SDate;
                pParam[1] = "@end_date:" + srch.de_EDate;
                pParam[2] = "@test_type:" + srch.le_s_test_type;
                pParam[3] = "@test_status:" + srch.le_s_test_status;
                pParam[4] = "@emp_cd:" + srch.le_s_emp_cd;      
            }            
            return pParam;
        }

        private string[] TrxGetParam(TestRecognitionE dto)
        {
            string[] pParam = new string[0];
            return pParam;
        }
        #endregion
    }
}