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
    public class TestProgressStatusService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "sp_TestProgressStatus";        
        //private string sign_set_cd = "1007";

        #region 조회영역
        internal DataTable Select(TestProgressStatus.SrchParam srchParam)
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
        private string[] GetParam(TestProgressStatus.SrchParam srch, TestProgressStatus dto)
        {
            string[] pParam = null;
                       
            if ("S".Equals(srch.Gubun))
            {
                pParam = new string[8];

                pParam[0] = "@date_option:" + srch.rg_ReqorRec;
                pParam[1] = "@start_date:" + srch.de_SDate;
                pParam[2] = "@end_date:" + srch.de_EDate;
                pParam[3] = "@test_type:" + srch.le_test_type;
                pParam[4] = "@test_status:" + srch.le_test_status;
                pParam[5] = "@search_gubun:" + srch.ce_gubun_number;
                pParam[6] = "@search_number:" + srch.te_number;
                pParam[7] = "@item_form_cd:" + srch.le_item_form_cd;
            }            
            return pParam;
        }

        private string[] TrxGetParam(TestProgressStatus dto)
        {
            string[] pParam = new string[0];
            return pParam;
        }
        #endregion
    }
}