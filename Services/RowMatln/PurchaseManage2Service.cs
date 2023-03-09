using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.RowMatln;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HACCP.Services.RowMatln
{
    public class PurchaseManage2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_PurchaseManage2";

        internal DataTable Select(PurchaseManage2 model)
        {
            string sGubun = "S1";
            string[] param = new string[10];

            param[0] = "@item_cd:" + model.manufacture_item_cd_S;
            param[1] = "@search_date:" + model.search_data; // (검색) 기간 상태값 (발주일자 or 입고예정일)
            param[2] = "@search_status:" + model.purchase_state; //(검색) 상태
            param[3] = "@start_date:" + model.start_date; // (검색) 시작일
            param[4] = "@end_date:" + model.end_date; // (검색) 종료일
            param[5] = "@option:" + model.option; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            param[6] = "@option2:" + model.option2; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            param[7] = "@order_request_no_S:" + model.order_request_no_S; // (검색) 생산의뢰번호
            param[8] = "@vender_cd_S:" + model.vender_cd_S; // (검색) 거래처 코드
            param[9] = "@manufacture_item_cd_S:" + ""; // (검색) 제조품목 코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable ManufacturePopup(string pSpName)
        {
            string strsql = "SELECT";
            strsql += " vender_cd, vender_nm, license,";
            strsql += " owner_nm, phone, zipcode,";
            strsql += " address, bigo ";
            strsql += "FROM  v_vender_manufacture WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%') ";
            strsql += "AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%' ";
            strsql += "ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable SupplyPopup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "vender_cd, vender_nm, license, ";
            strsql += "owner_nm, phone, zipcode, ";
            strsql += "address, bigo ";
           // strsql += "FROM  v_vender_material ";
            strsql += "FROM  vender ";
            strsql += "WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%') ";            
            strsql += "AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%' ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable SupplyPPopup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "vender_cd, vender_nm, license, ";
            strsql += "owner_nm, phone, zipcode, address, bigo ";
            //strsql += "FROM  v_vender_pack ";
            strsql += "FROM  vender ";
            strsql += "WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%') ";            
            strsql += "AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%' ";
            strsql += "ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable EmpPopup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "emp_cd, emp_nm, ";
            strsql += "dept_cd, dept_nm ";
            strsql += "FROM V_EMPLOYEE WHERE(emp_cd LIKE '%%' OR emp_nm  LIKE '%%') ";
            strsql += "AND emp_cd LIKE '%%' AND emp_nm  LIKE '%%' ";
            strsql += "ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable ItemCDPopup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "item_cd, item_nm, item_gb ";
            strsql += "FROM  v_item_material3 ";
            strsql += "WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%') ";
            strsql += "AND item_cd  LIKE '%%' AND item_nm  LIKE '%%' ";
            strsql += "ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable ItemPCDPopup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "item_cd, item_nm ";
            strsql += "FROM  v_item_material4 ";
            strsql += "WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%') ";
            strsql += "AND item_cd  LIKE '%%' AND item_nm  LIKE '%%' ";
            strsql += "ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable Select_List(PurchaseManage2 model)
        {
            string sGubun = "S2";
            string[] param = new string[1];

            param[0] = "@purchase_no:" + model.purchase_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable S_NO(PurchaseManage2 model)
        {
            string sGubun = "S_NO";
            string[] param = new string[1];

            param[0] = "@purchase_date:" + DateTime.Today.ToShortDateString();

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal string CRUD(PurchaseManage2 model, string gubun)
        {
            string sGubun = "";
            string message = "";

            if (gubun == "I" || gubun == "U")
            {   

                // "추가" I1 , 수정 "U1"
                sGubun = (gubun == "I") ? "I1" : "U1";

                string[] param = new string[11];

                param[0] = "@purchase_no:" + model.purchase_no;
                param[1] = "@purchase_date:" + model.purchase_date;
                param[2] = "@manufacture_cd:" + model.manufacture_cd;
                param[3] = "@supply_cd:" + model.supply_cd;
                param[4] = "@remark:" + model.remark;
                param[5] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
                param[6] = "@log_user_id:" + HttpContext.Current.Session["loginCD"];
                param[7] = "@purchase_kind:" + model.purchase_kind;
                param[8] = "@emp_cd:" + model.emp_cd;
                param[9] = "@emp_nm:" + model.emp_nm;
                param[10] = "@item_gb:" + model.item_gb;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
                
            }
            else if (gubun == "I_V")
            {
                sGubun = "U_CI";
                string[] param = new string[4];

                param[0] = "@commercial_personnel:" + model.commercial_personnel;
                param[1] = "@phone:" + model.phone;
                param[2] = "@supply_fax:" + model.supply_fax;
                param[3] = "@vender_cd_S:" + model.supply_cd;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }
            else if (gubun == "D")
            {
                sGubun = "D1";

                string[] param = new string[2];

                param[0] = "@purchase_no:" + model.purchase_no;
                param[1] = "@purchase_seq:" + model.purchase_seq;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }
            else if (gubun == "D3")
            {
                sGubun = "D3";

                string[] param = new string[1];

                param[0] = "@purchase_no:" + model.purchase_no;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }

            return message;
        }

        internal string PurchaseManage2_batch(List<PurchaseManage2> purchaseManage2, string gubun, string purchase_no)
        {
            string sSpName = "SP_PurchaseManage2";
            string sGubun = "";
            string message = "";

            for (int i = 0; i < purchaseManage2.Count; i++)
            {
                if (purchaseManage2[i].gubun.Equals("I2"))
                {
                    sGubun = "I2";
                    string[] pParam = new string[14];

                    pParam[0] = "@purchase_no:" + purchase_no;
                    pParam[1] = "@purchase_seq:" + purchaseManage2[i].purchase_seq;
                    pParam[2] = "@purchase_item_cd:" + purchaseManage2[i].purchase_item_cd;
                    pParam[3] = "@purchase_qty:" + purchaseManage2[i].purchase_qty;
                    pParam[4] = "@purchase_unit:" + purchaseManage2[i].purchase_unit;
                    pParam[5] = "@purchase_require_date:" + DateTime.Parse(purchaseManage2[i].purchase_require_date).ToShortDateString();
                    pParam[6] = "@purchase_price:" + purchaseManage2[i].purchase_price;
                    pParam[7] = "@purchase_amt:" + purchaseManage2[i].purchase_amt;
                    pParam[8] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
                    pParam[9] = "@log_user_id:" + HttpContext.Current.Session["loginCD"];
                    pParam[10] = "@order_request_no:" + purchaseManage2[i].order_request_no;
                    pParam[11] = "@purchase_status:" + purchaseManage2[i].purchase_status;
                    pParam[12] = "@obtain_status:" + purchaseManage2[i].obtain_status;
                    pParam[13] = "@manufacture_item_cd:" + purchaseManage2[i].manufacture_item_cd;

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (purchaseManage2[i].gubun.Equals("U2"))
                {
                    sGubun = "U2";
                    string[] pParam = new string[14];

                    pParam[0] = "@purchase_no:" + purchaseManage2[i].purchase_no;
                    pParam[1] = "@purchase_seq:" + purchaseManage2[i].purchase_seq;
                    pParam[2] = "@purchase_item_cd:" + purchaseManage2[i].purchase_item_cd;
                    pParam[3] = "@purchase_qty:" + purchaseManage2[i].purchase_qty;
                    pParam[4] = "@purchase_unit:" + purchaseManage2[i].purchase_unit;
                    pParam[5] = "@purchase_require_date:" + DateTime.Parse(purchaseManage2[i].purchase_require_date).ToShortDateString();
                    pParam[6] = "@purchase_price:" + purchaseManage2[i].purchase_price;
                    pParam[7] = "@purchase_amt:" + purchaseManage2[i].purchase_amt;
                    pParam[8] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
                    pParam[9] = "@log_user_id:" + HttpContext.Current.Session["loginCD"];
                    pParam[10] = "@order_request_no:" + purchaseManage2[i].order_request_no;
                    pParam[11] = "@purchase_status:" + purchaseManage2[i].purchase_status;
                    pParam[12] = "@obtain_status:" + purchaseManage2[i].obtain_status;
                    pParam[13] = "@manufacture_item_cd:" + purchaseManage2[i].manufacture_item_cd;


                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (purchaseManage2[i].gubun.Equals("D2"))
                {
                    sGubun = "D2";

                    string[] pParam = new string[2];

                    pParam[0] = "@purchase_no:" + purchaseManage2[i].purchase_no;
                    pParam[1] = "@purchase_seq:" + purchaseManage2[i].purchase_seq;

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }

            }

            return message;

        }

        internal DataTable S_empInfo(PurchaseManage2 model)
        {
            string sGubun = "S_empInfo";
            string[] param = new string[1];

            param[0] = "@supply_cd:" + model.supply_cd;            

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable S_PRICE(PurchaseManage2 model)
        {
            string sGubun = "S_PRICE";
            string[] param = new string[1];

            param[0] = "@item_cd:" + model.purchase_item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }
    }
}
