using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.QualityControl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.QualityControl
{
    public class QualityStatisticalAnalysisService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_QualityStatisticalAnalysis";        
        //private string sign_set_cd = "1007";

        #region 조회영역
        /// <summary>
        /// 조회(gubun="S1")
        /// </summary>
        /// <param name="srchParam"></param>
        /// <returns></returns>
        internal DataTable Select(QualityStatisticalAnalysis.SrchParam srchParam)
        {
            //srchParam.Gubun = "S1";
            string[] pParam = GetParam(srchParam);           
            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, srchParam.Gubun, pParam);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 시험항목 조회(gubun="S2")
        /// </summary>
        /// <param name="srchParam"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        internal DataTable SelectTestItem(QualityStatisticalAnalysis.SrchParam srchParam, QualityStatisticalAnalysis dto)
        {
            //srchParam.Gubun = "S2";
            string[] pParam = GetParam(srchParam, dto);
            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, srchParam.Gubun, pParam);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary> 
        /// 시험데이터 조회 : chart데이터와 함께 리턴하므로,collection으로 리턴함 (gubun="S3")
        /// </summary>
        /// <param name="srchParam"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        internal DataTableCollection SelectTestData(QualityStatisticalAnalysis.SrchParam srchParam, QualityStatisticalAnalysis.TestItem dto)
        {
            //srchParam.Gubun = "S3";
            string[] pParam = GetParam(srchParam, dto);
            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, srchParam.Gubun, pParam);
            //DataTable dt = new DataTable();
            DataTableCollection dtc = null;
            if (ds != null)
            {
                dtc = ds.Tables;
            }
            return dtc;
        }

        /// <summary>
        /// 시험데이터 조회 (gubun="S4")
        /// </summary>
        /// <param name="srchParam"></param>
        /// <param name="dto"></param>
        /// <param name="itemParam">item항목 map</param>
        /// <returns></returns>
        internal DataTable SelectTestItemDetail(QualityStatisticalAnalysis.SrchParam srchParam, QualityStatisticalAnalysis dto, string itemParam)
        {
            Dictionary<string, string> mapParam = JsonConvert.DeserializeObject<Dictionary<string, string>>(itemParam);

            //srchParam.Gubun = "S4";
            string[] pParam = GetParam(srchParam, dto, mapParam);
            
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
        private string[] GetParam(QualityStatisticalAnalysis.SrchParam srch, Object gridGb=null, IDictionary<string, string> mapParam = null)
        {
            string[] pParam = null;

            // dto 설정
            if ("S1".Equals(srch.Gubun))
            {
                //QualityStatisticalAnalysis dto = gridGb is QualityStatisticalAnalysis ? (QualityStatisticalAnalysis)gridGb : null;
                if(gridGb == null)
                {
                    pParam = new string[6];
                    pParam[0] = "@test_type:" + srch.le_s_test_type;
                    pParam[1] = "@item_cd:" + srch.be_s_item_cd;
                    pParam[2] = "@start_date:" + srch.de_start_date;
                    pParam[3] = "@end_date:" + srch.de_end_date;
                    pParam[4] = "@vender:" + srch.be_receipt_material_maker_cd;
                    pParam[5] = "@vender_name:" + srch.te_receipt_lot_cust_nm;
                }

            } 
            else if ("S2".Equals(srch.Gubun))
            {
                //QualityStatisticalAnalysis dto = gridGb is QualityStatisticalAnalysis ? (QualityStatisticalAnalysis)gridGb : null;
                if(gridGb is QualityStatisticalAnalysis dto)                
                {
                    pParam = new string[5];
                    pParam[0] = "@test_type:" + dto.test_type;
                    pParam[1] = "@item_cd:" + dto.item_cd;
                    pParam[2] = "@process_cd:" + dto.process_cd;
                    pParam[3] = "@start_date:" + srch.de_start_date;
                    pParam[4] = "@end_date:" + srch.de_end_date;                    
                }
            }
            else if ("S3".Equals(srch.Gubun))
            {             
                if(gridGb is QualityStatisticalAnalysis.TestItem dto )                
                {
                    pParam = new string[7];
                    pParam[0] = "@test_type:"+ dto.test_type;
                    pParam[1] = "@item_cd:" + dto.item_cd;
                    pParam[2] = "@process_cd:"+dto.process_cd;
                    pParam[3] = "@testitem_cd:"+dto.testitem_cd;
                    pParam[4] = "@start_date:"+srch.de_start_date;
                    pParam[5] = "@end_date:"+srch.de_end_date;
                    pParam[6] = "@all_item:N";

                }
            }
            else if ("S4".Equals(srch.Gubun))
            {
                //QualityStatisticalAnalysis.TestItemDetail dto = gridGb is QualityStatisticalAnalysis.TestItemDetail ? (QualityStatisticalAnalysis.TestItemDetail)gridGb : null;
                pParam = new string[15];
                
                if (gridGb is QualityStatisticalAnalysis dto)
                {
                    pParam[0] = "@test_type:" + dto.test_type ;
                    pParam[1] = "@item_cd:" + dto.item_cd;
                    pParam[2] = "@process_cd:" + dto.process_cd ;
                    pParam[3] = "@start_date:" + srch.de_start_date;
                    pParam[4] = "@end_date:" + srch.de_end_date;
                }
                // 항목10개조회 param
                int i = 5;
                if (mapParam is IDictionary<string, string>)
                {
                    foreach (var item in mapParam) { pParam[i++] = "@" + item.Key + ":" + item.Value; }
                }
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