using HACCP.Attribute;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.RowMatln;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.RowMatln
{
    public class ReceiveMaterialService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_ReceiveMaterial1";

        internal DataTable Select(ReceiveMaterial model)
        {
            string sGubun = "S1";
            string[] param = new string[10];

            param[0] = "@start_date:" + model.start_date;
            param[1] = "@end_date:" + model.end_date;
            param[2] = "@item_cd:" + model.s_item_nm;
            param[3] = "@lot_cust:" + model.lot_cust;
            param[4] = "@s_receipt_no:" + model.s_receipt_no;
            param[5] = "@Material_Or_PackMaterial:" + model.Material_Or_PackMaterial;
            param[6] = "@ReceiptType:" + model.ReceiptType;
            param[7] = "@item_gb:" + model.item_gb;
            param[8] = "@search_date:" + model.search_date;
            param[9] = "@option2:" + model.option2;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable ItemCDPopup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "item_cd, item_nm, trade_nm, trade_cd, item_gb, ";
            strsql += "item_type3_nm, item_type3 ";
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

        internal DataTable ManufacturePopup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "vender_cd, vender_nm, license, ";
            strsql += "owner_nm, phone, zipcode, ";
            strsql += "address, bigo ";
            strsql += "FROM  v_vender_material ";
            strsql += "WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%') ";
            strsql += "AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%' ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable SupplyPopup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "vender_cd, vender_nm, license, ";
            strsql += "owner_nm, phone, zipcode, ";
            strsql += "address, bigo ";
            strsql += "FROM  v_vender_material ";
            strsql += "WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%') ";
            strsql += "AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%' ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable ManufacturePPopup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "vender_cd, vender_nm, license, ";
            strsql += "owner_nm, phone, zipcode, address, bigo ";
            strsql += "FROM  v_vender_pack ";
            strsql += "WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%') ";
            strsql += "AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%' ";
            strsql += "ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable SupplyPPopup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "vender_cd, vender_nm, license, ";
            strsql += "owner_nm, phone, zipcode, address, bigo ";
            strsql += "FROM  v_vender_pack ";
            strsql += "WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%') ";
            strsql += "AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%' ";
            strsql += "ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable Select_List(ReceiveMaterial model)
        {
            string sGubun = "S3"; // S3 + SWR 프로시져 병합
            string[] param = new string[2];

            param[0] = "@receipt_no:" + model.receipt_no; // 입고번호
            param[1] = "@receipt_id:" + model.receipt_id; // 입고순번

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Select_S2(ReceiveMaterial model)
        {
            string sGubun = "S2";
            string[] param = new string[3];

            param[0] = "@receipt_no:" + model.receipt_no; // 입고번호
            param[1] = "@receipt_id:" + model.receipt_id; // 입고순번
            param[2] = "@Material_Or_PackMaterial:" + model.Material_Or_PackMaterial; // 입고순번

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Select_SW(ReceiveMaterial model)
        {
            string sGubun = "SW"; // S3 + SWR 프로시져 병합
            string[] param = new string[1];

            param[0] = "@receipt_no:" + model.receipt_no; // 입고번호

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable S4(ReceiveMaterial model)
        {
            string sGubun = "S4";
            string[] param = new string[1];

            param[0] = "@receipt_time_date:" + DateTime.Today.ToShortDateString();

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable ReceiptCheckView(ReceiptCheckView model)
        {
            sSpName = "SP_ReceiptCheckView";
            string sGubun = "S";
            string[] param = new string[5];

            param[0] = "@start_date:" + DateTime.Today.AddDays(-7).ToShortDateString(); // (검색) 시작일
            param[1] = "@end_date:" + DateTime.Today.ToShortDateString(); // (검색) 종료일
            param[2] = "@search:" + "";
            param[3] = "@option:" + model.option; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            param[4] = "@option2:" + model.option2; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            sSpName = "SP_ReceiveMaterial1";

            return dt;
        }

        internal DataTable ReceiptCheckView_Search(ReceiptCheckView model)
        {
            sSpName = "SP_ReceiptCheckView";
            string sGubun = "S";
            string[] param = new string[5];

            param[0] = "@start_date:" + model.start_date; // (검색) 시작일
            param[1] = "@end_date:" + model.end_date; // (검색) 종료일
            param[2] = "@search:" + "";
            param[3] = "@option:" + model.option; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            param[4] = "@option2:" + model.option2; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            sSpName = "SP_ReceiveMaterial1";

            return dt;
        }

        internal DataTable receiptStatus_C1(ReceiveMaterial model)
        {
            string sGubun = "C1";
            string[] param = new string[2];

            param[0] = "@receipt_no:" + model.receipt_no; // 입고번호
            param[1] = "@receipt_id:" + model.receipt_id; // 입고순번

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable receiptStatus_C3(ReceiveMaterial model)
        {
            string sGubun = "C3";
            string[] param = new string[3];

            param[0] = "@receipt_no:" + model.receipt_no; // 입고번호
            param[1] = "@receipt_id:" + model.receipt_id; // 입고순번
            param[2] = "@item_cd:" + model.item_cd; // 품목코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal string receiptStatus_ES(ReceiveMaterial model)
        {
            string gubun = "ES";

            string res = "";

            string[] param = new string[6];

            param[0] = "@receipt_no:" + model.receipt_no; // 입고번호
            //param[1] = "@receipt_id:" + "1"; // 입고순번
            param[1] = "@receipt_id:" + model.receipt_id; // 입고순번
            param[2] = "@request_no:" + model.request_no; // 생성번호
            //param[3] = "@request_date:" + model.request_date; // 시험의뢰 요청날짜
            //param[3] = "@request_time1:" + string.Format("{0} {1}", model.request_date, DateTime.Now.ToString("HH:mm:ss.fff"));
            param[3] = "@emp_cd:" + HttpContext.Current.Session["loginCD"];
            param[4] = "@validation_type:" + "2";
            //param[5] = "@testrequest_no:" + model.request_no;
            param[5] = "@Material_Or_PackMaterial:" + model.Material_Or_PackMaterial;

            res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }

        internal string receiptStatus_DE(ReceiveMaterial model)
        {
            string gubun = "DE";

            string res = "";

            string[] param = new string[3];

            param[0] = "@receipt_no:" + model.receipt_no; // 입고번호
            //param[1] = "@receipt_id:" + "1"; // 입고순번
            param[1] = "@receipt_id:" + model.receipt_id; // 입고순번
            param[2] = "@receipt_qc_no1:" + model.receipt_qc_no1; // 의뢰번호

            res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }

        internal string receiptStatus_C2(ReceiveMaterial model)
        {
            string gubun = "C2";

            string res = "";

            string[] param = new string[2];

            param[0] = "@receipt_no:" + model.receipt_no; // 입고번호
            //param[1] = "@receipt_id:" + "1"; // 입고순번
            param[1] = "@receipt_id:" + model.receipt_id; // 입고순번

            res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }
        
        internal string ReceiveMaterial_ST(ReceiveMaterial model)
        {
            string sGubun = "ST";
            string res = "";
            string[] param = new string[1];

            param[0] = "@item_cd:" + model.item_cd;

            res = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);

            return res;
        }

        internal DataTable receiptStatus_SA(ReceiveMaterial model)
        {
            string sGubun = "SA_AOS";
            string[] param = new string[1];

            param[0] = "@item_cd:" + model.item_cd;            

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable receiptStatus_SM(string item_cd, string cust_cd)
        {
            string sGubun = "SM";
            string[] param = new string[2];

            param[0] = "@item_cd:" + item_cd;
            param[1] = "@cust_cd:" + cust_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }
        
        internal string CRUD(ReceiveMaterial model)
        {
            string sGubun = "";
            string message = "";

            if (model.gubun == "I1" || model.gubun == "U1")
            {
                model.receipt_time_hh = 0;
                model.receipt_time_mm = 0;
                model.receipt_rate = 0;
                model.sampled_qty = 0;
                // GMS 솔루션에 하드코딩 되있음.....
                model.imported_faulty_goods_change = "I";
                model.item_exam_yn = "Y";

                sGubun = model.gubun;
                string[] param = GetParam(model);

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }
            else if (model.gubun == "Product_I1")
            {
                sGubun = model.gubun;
                string[] param = new string[8];

                param[0] = "@item_cd:" + model.item_cd;
                param[1] = "@lot_no:" + model.receipt_lot_no;                       //제조번호
                param[2] = "@receipt_lot_date:" + model.receipt_lot_date;           //제조일자
                param[3] = "@receipt_time_date:" + model.receipt_time_date;         //입고일자
                param[4] = "@receipt_qty:" + model.receipt_qty;                     //포장량
                param[5] = "@end_date:" + model.valid_period;                       //유효기간
                param[6] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];                
                param[7] = "@item_stock_id:" + model.item_stock_id;                 //identity값 생성

                DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

                message = "" + dt.Rows[0].ItemArray[0];
            }
            else if (model.gubun == "I2")
            {
                sGubun = model.gubun;
                string[] param = new string[12];

                param[0] = "@receipt_no:" + model.receipt_no;
                param[1] = "@receipt_id:" + model.receipt_id;
                param[2] = "@receipt_pack_seq:" + model.receipt_pack_seq;
                param[3] = "@receipt_pack_unit:" + model.receipt_pack_unit;
                param[4] = "@receipt_pack_qty:" + model.receipt_pack_qty;
                param[5] = "@receipt_pack_remark:" + model.receipt_pack_remark;
                param[6] = "@receipt_pack_remain_qty:" + model.receipt_pack_remain_qty;
                param[7] = "@old_receipt_pack_remain_qty:" + model.old_receipt_pack_remain_qty;
                param[8] = "@new_receipt_pack_remain_qty:" + model.new_receipt_pack_remain_qty;
                param[9] = "@cost_cd:" + model.cost_cd;
                param[10] = "@Material_Or_PackMaterial:" + model.Material_Or_PackMaterial;
                param[11] = "@item_gb:" + model.item_gb;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }
            else if (model.gubun == "Product_I2")
            {
                sGubun = model.gubun;
                string[] param = new string[5];

                param[0] = "@item_stock_id:" + model.item_stock_id;                         //생성된 identity값
                param[1] = "@prod_qty:" + model.receipt_pack_qty;                           //포장량
                param[2] = "@stock_qty:" + "0";                                             //재고량
                param[3] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
                param[4] = "@box_barcode_no:" + model.receipt_pack_barcode;                 //팔레트생성번호

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }
            else if (model.gubun == "Product_Export")
            {
                sGubun = model.gubun;
                string[] param = new string[6];

                param[0] = "@receipt_no:" + model.receipt_no;                               //입고번호
                param[1] = "@receipt_id:" + model.receipt_id;
                param[2] = "@receipt_pack_seq:" + model.receipt_pack_seq;                   //팩 seq 번호
                param[3] = "@out_qty:" + model.receipt_pack_qty;                            //포장량
                param[4] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
                param[5] = "@out_type:" + "6";                                              //출고구분 / 재고조정 : 6

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }            
            else if (model.gubun == "AllPackDelete")
            {
                sGubun = model.gubun;
                string[] param = new string[2];

                param[0] = "@receipt_no:" + model.receipt_no;
                param[1] = "@receipt_id:" + model.receipt_id;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }
            else if (model.gubun == "D1")
            {
                sGubun = model.gubun;
                string[] param = new string[4];

                param[0] = "@receipt_no:" + model.receipt_no;
                param[1] = "@receipt_id:" + model.receipt_id;
                param[2] = "@receipt_check_no:" + model.receipt_check_no;
                param[3] = "@receipt_check_id:" + model.receipt_check_id;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }

            return message;
        }


        private string[] GetParam(ReceiveMaterial model)
        {
            string[] param = new string[55];

            if (model.gubun == "I1") model.receipt_id = "1"; 

            param[0] = "@receipt_no:" + model.receipt_no;
            param[1] = "@receipt_id:" + model.receipt_id;
            param[2] = "@cust_cd:" + model.cust_cd;
            param[3] = "@receipt_material_maker_cd:" + model.receipt_material_maker_cd;
            param[4] = "@receipt_lot_cust_nm:" + model.receipt_lot_cust_nm;
            param[5] = "@receipt_lot_no:" + model.receipt_lot_no;
            param[6] = "@receipt_lot_date:" + model.receipt_lot_date;
            param[7] = "@receipt_lot_valid_date:" + model.receipt_lot_valid_date;
            param[8] = "@receipt_pack_qty:" + model.receipt_pack_qty;
            param[9] = "@receipt_time_date:" + model.receipt_time_date;
            param[10] = "@receipt_time_hh:" + model.receipt_time_hh;
            param[11] = "@receipt_time_mm:" + model.receipt_time_mm;
            param[12] = "@receipt_rate:" + model.receipt_rate;
            param[13] = "@warehouse:" + model.warehouse;
            param[14] = "@warehouse_location:" + model.warehouse_location;
            param[15] = "@remark:" + model.remark;
            param[16] = "@sampler_cd:" + model.sampler_cd;
            param[17] = "@sampling_date:" + model.sampling_date;
            param[18] = "@sampled_qty:" + model.sampled_qty;
            param[19] = "@receipt_qc_no3:" + model.receipt_qc_no3;
            param[20] = "@item_cd:" + model.item_cd;
            param[21] = "@receipt_qty:" + model.receipt_qty;
            param[22] = "@test_rdate:" + model.test_rdate;
            param[23] = "@valid_period_t:" + model.valid_period;
            param[24] = "@receipt_status:" + model.receipt_status;
            param[25] = "@receipt_qc_no1:" + model.receipt_qc_no1;
            param[26] = "@receipt_qty_unit:" + model.item_unit;
            param[27] = "@receipt_qc_pollination:" + model.receipt_qc_pollination;
            param[28] = "@receipt_qc_rate:" + model.receipt_qc_rate;
            // 원료 입고 프로그램을 실행하면 재고상태는 무조건 양품 상태로 저장되도록 한다.            
            param[29] = "@imported_faulty_goods_change:" + model.imported_faulty_goods_change;            
            //param[30] = "@purchase_no:" + model. ;
            //param[31] = "@purchase_id:" + model. ;
            param[30] = "@purchase_no:" + model.purchase_no;
            param[31] = "@purchase_id:" + model.purchase_id;
            param[32] = "@retest_yn:" + model.retest_yn;
            param[33] = "@item_exam_yn:" + model.item_exam_yn;
            param[34] = "@cc_no:" + model.cc_no;
            param[35] = "@cc_seq:" + model.cc_seq;
            param[36] = "@lc_no:" + model.lc_no;
            param[37] = "@lc_seq:" + model.lc_seq;
            param[38] = "@interface_field1:" + model.interface_field1;
            param[39] = "@interface_field2:" + model.interface_field2;
            param[40] = "@interface_field3:" + model.interface_field3;
            param[41] = "@interface_field4:" + model.interface_field4;
            param[42] = "@interface_field5:" + model.interface_field5;
            param[43] = "@receipt_type:" + model.receipt_type;
            param[44] = "@make_item_cd:" + model.make_item_cd;
            param[45] = "@cost_cd:" + model.cost_cd;
            param[46] = "@container_shape:" + model.container_shape;
            
            param[47] = "@purchase_date:" + model.purchase_date;
            param[48] = "@receipt_ca_rate:" + model.receipt_ca_rate;

            //20140617임소희추가
            param[49] = "@receipt_check_no:" + model.receipt_check_no;
            param[50] = "@receipt_check_id:" + model.receipt_check_id;
            //param[50] = "@receipt_check_id:" + "1";

            //20140702임소희추가
            param[51] = "@obtain_status:" + model.obtain_status;
            param[52] = "@receipt_pack_in_qty:" + model.receipt_pack_in_qty;

            //20150112임소희추가 : 코스맥스 - 생산입고 구분 추가
            param[53] = "@product_receipt:" + model.product_receipt_YN;

            //20210304 전상배추가 : AOS -  품목 구분 추가
            param[54] = "@item_gb:" + model.item_gb;

            return param;
        }

        //팩 재고량 수정 Service
        internal string ReceiveMaterial_batch([JsonBinder] List<ReceiveMaterial> receiveMaterial, string gubun)
        {
            string sGubun = "";
            string message = "";

            for (int i = 0; i < receiveMaterial.Count; i++)
            {
                if (receiveMaterial[i].gubun.Equals("U2"))
                {
                    sGubun = "U2";
                    string[] pParam = new string[11];

                    pParam[0] = "@receipt_no:" + receiveMaterial[i].receipt_no;
                    pParam[1] = "@receipt_id:" + receiveMaterial[i].receipt_id;
                    pParam[2] = "@receipt_pack_seq:" + receiveMaterial[i].receipt_pack_seq;
                    pParam[3] = "@receipt_pack_unit:" + receiveMaterial[i].receipt_pack_unit;
                    pParam[4] = "@receipt_pack_qty:" + receiveMaterial[i].receipt_pack_qty;
                    pParam[5] = "@receipt_pack_remark:" + receiveMaterial[i].receipt_pack_remark;
                    pParam[6] = "@receipt_pack_remain_qty:" + receiveMaterial[i].receipt_pack_remain_qty;
                    pParam[7] = "@old_receipt_pack_remain_qty:" + receiveMaterial[i].old_receipt_pack_remain_qty;
                    pParam[8] = "@new_receipt_pack_remain_qty:" + receiveMaterial[i].new_receipt_pack_remain_qty;
                    pParam[9] = "@cost_cd:" + receiveMaterial[i].cost_cd;
                    pParam[10] = "@Material_Or_PackMaterial:" + receiveMaterial[i].Material_Or_PackMaterial;

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
            }

            return message;
        }

        internal DataTable ReceiveMaterial_GetItemUnit(string item_cd)
        {
            string pParam = "@item_cd:" + item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "item_unit", pParam);

            return dt;

        }

    }
}