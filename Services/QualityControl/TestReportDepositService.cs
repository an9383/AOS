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
    public class TestReportDepositService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_TestReportDeposit";
        //private string sign_set_cd = "1007";

        #region 조회영역
        internal DataTable Select(TestReportDeposit.SrchParam srch)
        {
            string[] pParam = GetParam(srch);

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, srch.Gubun, pParam);
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
        internal DataTable DownloadFile(string file_id)
        {            
            string sGubun = "FS";

            string[] pParam = new string[1];
            pParam[0] = "@doc_file_id:" + file_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }
        //internal bool DataCheckYN(TestRecognitionE dto)
        //{
        //    bool check = false;
        //    string gubun = "C2";
        //    string[] pParam = new string[1];

        //    pParam[0] = "@testcontrol_id:" + dto.testcontrol_id;
        //    string message = _bllSpExecute.SpExecuteString(this.sSpName, gubun, pParam);

        //    check = ("Y".Equals(message)) ? true : false;
        //    return check;
        //}

        #endregion
        #region sp param 설정
        private string[] GetParam(TestReportDeposit.SrchParam srch)
        {
            string[] pParam = null;
                       
            if ("S".Equals(srch.Gubun))
            {
                pParam = new string[1];
                pParam[0] = "@test_no:" + srch.Test_no;                
            }
            //else if ("S1".Equals(srch.Gubun))
            //{
            //    pParam = new string[1];
            //    pParam[0] = "@testcontrol_id:" + dto.testcontrol_id;
            //}
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