using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.PrdOut;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdOut
{
    public class DespatchManageOrder2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable DespatchManageOrder2_S1(DespatchManageOrder2 dModel)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "S1";
            string[] pParam = new string[7];

            pParam[0] = "@s_cust_cd:" + dModel.s_cust_cd;
            pParam[1] = "@s_sdate:" + dModel.s_sdate;
            pParam[2] = "@s_edate:" + dModel.s_edate;
            pParam[3] = "@s_issue_status:" + dModel.s_issue_status;
            pParam[4] = "@search_ck:" + dModel.search_ck;
            pParam[5] = "@s_sign_status:" + dModel.s_sign_status;
            pParam[6] = "@item_cd:" + dModel.item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable DespatchManageOrder2_S2(DespatchManageOrder2 dModel)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "S2";
            string[] pParam = new string[2];

            pParam[0] = "@despatch_no:" + dModel.despatch_no;
            pParam[1] = "@item_cd:" + dModel.item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable DespatchManageOrder2_S3(DespatchManageOrder2 dModel)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "S3";
            string[] pParam = new string[3];

            pParam[0] = "@despatch_no:" + dModel.despatch_no;
            pParam[1] = "@item_cd:" + dModel.item_cd;
            pParam[2] = "@lot_no:" + dModel.lot_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }
        
        internal DataTable DespatchManageOrder2_GetLotNoSum(DespatchManageOrder2 dModel,string check)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "GetLotNoSum";
            string[] pParam = new string[3];

            pParam[0] = "@item_cd:" + dModel.item_cd;
            pParam[1] = "@check:" + check;
            pParam[2] = "@despatch_no:" + dModel.despatch_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        //거래처팝업
        internal DataTable DO2_s_cust_Popup()
        {
            string strsql = "SELECT vender_cd, vender_nm, license, owner_nm, phone, zipcode, address";
            strsql += " FROM vender";
            strsql += " WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%')";
            strsql += " AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        //품목팝업
        internal DataTable DO2_s_item_Popup()
        {
            string strsql = "SELECT item_cd, item_nm";
            strsql += " FROM v_packingitem";
            strsql += " WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%')";
            strsql += " AND item_cd  LIKE '%%' AND item_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        //출고전표상세 - 제품코드 팝업
        internal DataTable DO2_item_cd_Popup()
        {
            string strsql = "SELECT item_cd, item_nm, vender_item_cd, item_s_nm";
            strsql += " FROM v_packingitem";
            strsql += " WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%')";
            strsql += " AND item_cd  LIKE '%%' AND item_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        //출고전표등록 - 거래처 팝업
        internal DataTable DO2_i_cust_Popup()
        {
            string strsql = "SELECT vender_cd, vender_nm, address";
            strsql += " FROM  v_vender_item";
            strsql += " WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%')";
            strsql += " AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        //출고전표등록 - 간납처 팝업
        internal DataTable DO2_i_custIn_Popup()
        {
            string strsql = "SELECT cust_indirect_cd, vender_nm, address, cust_cd";
            strsql += " FROM  v_vender_custreg";
            strsql += " WHERE (cust_indirect_cd  LIKE '%%' OR vender_nm  LIKE '%%')";
            strsql += " AND cust_cd  LIKE '%%' AND cust_cd  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }       

        //출고번호 자동생성
        internal DataTable DespatchManageOrder2_S_NO(DespatchManageOrder2 dModel)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "S_NO";
            string[] pParam = new string[1];

            pParam[0] = "@issue_date:" + dModel.ISSUE_DATE;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal string DespatchManageOrder2_TRX(DespatchManageOrder2 dModel, string gubun)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "";
            string message = "";

            if (gubun == "I" || gubun == "U")
            {
                sGubun = (gubun == "I") ? "I1" : "U1";

                string[] pParam = new string[14];

                pParam[0] = "@despatch_no:" + dModel.DESPATCH_ORDER_NO;
                pParam[1] = "@issue_date:" + dModel.ISSUE_DATE;
                pParam[2] = "@request_no:" + dModel.request_no;
                pParam[3] = "@issue_status:" + dModel.issue_ck_cd;
                pParam[4] = "@cust_cd:" + dModel.CUST_CD;
                pParam[5] = "@pass_cust_cd:" + dModel.PASS_CUST_CD;
                pParam[6] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
                pParam[7] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
                pParam[8] = "@log_user_id:" + HttpContext.Current.Session["loginCD"];
                pParam[9] = "@issue_trans_gb:" + dModel.ISSUE_TRANS_GB;
                pParam[10] = "@issue_trans_cust_cd:" + dModel.ISSUE_TRANS_CUST_CD;
                pParam[11] = "@TaxBill_Yn:" + dModel.TaxBillYn;
                pParam[12] = "@release_type:" + dModel.release_type;
                pParam[13] = "@sign_yn:" + "Y";

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            else if (gubun == "D")
            {
                sGubun = "D1";
                string[] pParam = new string[3];

                pParam[0] = "@despatch_no:" + dModel.DESPATCH_ORDER_NO;
                pParam[1] = "@log_user_id:" + HttpContext.Current.Session["loginCD"];
                pParam[2] = "@item_cd:" + dModel.item_cd;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            else if (gubun == "U4")
            {
                sGubun = "U4";
                string[] pParam = new string[4];

                pParam[0] = "@despatch_no:" + dModel.DESPATCH_ORDER_NO;
                pParam[1] = "@lot_no:" + dModel.lot_no;
                pParam[2] = "@item_issue_id:" + dModel.item_issue_id;
                pParam[3] = "@Issue_Price:" + dModel.Issue_Price2;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }

            return message;
        }

        internal string DespatchManageOrder2_batch(List<DespatchManageOrder2> despatchManageOrder2, string gubun, string issue_detail_date, string despatch_no, string item_issue_id, string Issue_Price)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "";
            string message = "";

            for (int i = 0; i < despatchManageOrder2.Count; i++)
            {
                if (despatchManageOrder2[i].gubun.Equals("I2"))
                {
                    sGubun = "I2";
                    string[] pParam = new string[15];

                    pParam[0] = "@issue_detail_date:" + issue_detail_date;
                    pParam[1] = "@item_cd:" + despatchManageOrder2[i].item_cd;
                    //pParam[2] = "@issue_qty:" + despatchManageOrder2[i].ISSUE_QTY;
                    pParam[2] = "@issue_qty:" + Convert.ToInt32(despatchManageOrder2[i].ISSUE_QTY);
                    pParam[3] = "@lot_no:" + despatchManageOrder2[i].lot_no;
                    pParam[4] = "@lot_date:" + despatchManageOrder2[i].lot_date;
                    pParam[5] = "@end_date:" + despatchManageOrder2[i].end_date;
                    pParam[6] = "@despatch_no:" + despatch_no;
                    pParam[7] = "@item_issue_id:" + item_issue_id;
                    pParam[8] = "@box_barcode_no:" + despatchManageOrder2[i].box_barcode_no;
                    pParam[9] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
                    pParam[10] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
                    pParam[11] = "@log_user_id:" + HttpContext.Current.Session["loginCD"];
                    pParam[12] = "@box_qty:" + despatchManageOrder2[i].box_qty;

                    if (despatchManageOrder2[i].keeping_status == "Y" || despatchManageOrder2[i].keeping_status.Equals("true"))
                    {
                        pParam[13] = "@keeping_status:" + "Y";
                    }
                    else
                    {
                        pParam[13] = "@keeping_status:" + "N";
                    }

                    pParam[14] = "@Issue_Price:" + despatchManageOrder2[i].Issue_Price; /*Issue_Price;*/

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (despatchManageOrder2[i].gubun.Equals("U3"))
                {
                    sGubun = "U3";
                    string[] pParam = new string[10];

                    pParam[0] = "@despatch_no:" + despatch_no;
                    pParam[1] = "@item_cd:" + despatchManageOrder2[i].item_cd;
                    pParam[2] = "@issue_qty:" + despatchManageOrder2[i].ISSUE_QTY;
                    pParam[3] = "@lot_no:" + despatchManageOrder2[i].lot_no;
                    pParam[4] = "@box_barcode_no:" + despatchManageOrder2[i].box_barcode_no;
                    pParam[5] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
                    pParam[6] = "@log_user_id:" + HttpContext.Current.Session["loginCD"];
                    pParam[7] = "@box_qty:" + despatchManageOrder2[i].box_qty;
                    if (despatchManageOrder2[i].keeping_status == "Y" || despatchManageOrder2[i].keeping_status.Equals("true"))
                    {
                        pParam[8] = "@keeping_status:" + "Y";
                    }
                    else
                    {
                        pParam[8] = "@keeping_status:" + "N";
                    }
                    pParam[9] = "@Issue_Price:" + Issue_Price;


                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (despatchManageOrder2[i].gubun.Equals("D3"))
                {
                    sGubun = "D3";
                    string[] pParam = new string[4];

                    pParam[0] = "@despatch_no:" + despatch_no;
                    pParam[1] = "@item_cd:" + despatchManageOrder2[i].item_cd;
                    pParam[2] = "@lot_no:" + despatchManageOrder2[i].lot_no;
                    pParam[3] = "@box_barcode_no:" + despatchManageOrder2[i].box_barcode_no;

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }

            }

            return message;

        }

    }
}