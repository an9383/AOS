using DevExpress.CodeParser;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.SysItem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HACCP.Services.SysItem
{
    public class PackMaterialMaster2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectPackingMaterial(string item_cd, string prod_end)
        {
            string sSpName = "SP_PackMaterialMaster2";
            string sGubun = "S";
            string[] pParam = new string[4];

            pParam[0] = "@s_item_cd:" + item_cd;
            pParam[1] = "@s_item_type1:" + "%";
            pParam[2] = "@s_item_type2:" + "%";
            pParam[3] = "@s_prod_end:" + prod_end;

            DataTable dt = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam).Tables[0];

            return dt;
        }

        internal DataTable Select_Supllier(string vender_gb)
        {
            string sSpName = "SP_Vender";
            string sGubun = "S";
            string[] pParam = new string[3];

            pParam[0] = "@vender_cd_S:";
            pParam[1] = "@s_use_yn:" + "Y";
            pParam[2] = "@s_vender_gb:" + vender_gb;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }
        

        internal string itemCdDuplicateCheck(string item_cd)
        {
            string sSpName = "SP_PackMaterialMaster2";
            string sGubun = "SC";
            string[] pParam = new string[1];
            pParam[0] = "@item_cd_ss:" + item_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal DataTable SelectPackingMaterialPopupData()
        {
            string strsql = "SELECT item_cd, item_nm";
            strsql += " FROM v_item_material_m2";
            strsql += " WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%')";
            strsql += " AND item_cd  LIKE '%%' AND item_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable SelectPackingMaterialBrandData()
        {
            string strsql = "SELECT trade_cd, trade_nm";
            strsql += " FROM v_trade";
            strsql += " WHERE (trade_cd  LIKE '%%' OR trade_nm  LIKE '%%')";
            strsql += " AND trade_cd  LIKE '%%' AND trade_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal string PackMaterialCRUD(PackMaterial packMaterial, string pGubun)
        {
            string sSpName = "SP_PackMaterialMaster2";
            string sGubun = pGubun;

            string message = "";

            if (pGubun.Equals("D"))
            {
                string[] pParam = new string[1];
                pParam[0] = "@item_cd:" + packMaterial.item_cd;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            else
            {
                string[] pParam = GetParam(packMaterial);

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }

            if (!message.Equals(""))
            {
                switch(pGubun)
                {
                    //입력시에는 item_cd 받아와야함.(제조처 그리드 데이터 저장시 item_cd가 안들어오므로)
                    //case "I":
                    //    message = "입력되었습니다.";
                    //    break;
                    case "U":
                        message = "수정되었습니다.";
                        break;
                    case "D":
                        message = "삭제되었습니다.";
                        break;
                }
            }

            return message;
        }

        private string[] GetParam(PackMaterial packMaterial)
        {
            string[] param = new string[59];

            param[0] = "@item_cd:" + packMaterial.item_cd;
            param[1] = "@item_nm:" + packMaterial.item_nm;
            param[2] = "@abbreviation_cd:" + packMaterial.abbreviation_cd;
            param[3] = "@item_s_nm:" + packMaterial.item_s_nm;
            param[4] = "@item_enm:" + packMaterial.item_enm;
            param[5] = "@item_jnm:" + packMaterial.item_jnm;
            param[6] = "@prod_end:" + packMaterial.prod_end;
            param[7] = "@break_type:" + packMaterial.break_type;
            param[8] = "@print_item:" + packMaterial.print_item;
            param[9] = "@outer_process:" + packMaterial.outer_process;
            param[10] = "@in_out_ck:" + packMaterial.in_out_ck;
            param[11] = "@abc_gubun:" + packMaterial.abc_gubun;
            param[12] = "@plant_cd:" + packMaterial.plant_cd;
            param[13] = "@item_type1:" + packMaterial.item_type1;
            param[14] = "@item_type2:" + packMaterial.item_type2;
            param[15] = "@item_unit:" + packMaterial.item_unit;
            if (packMaterial.item_exam_yn != "Y") packMaterial.item_exam_yn = "N";
            param[16] = "@item_exam_yn:" + packMaterial.item_exam_yn;
            param[17] = "@sampling_calc:" + packMaterial.sampling_calc;
            param[18] = "@item_validity_period:" + packMaterial.item_validity_period;
            param[19] = "@item_validity_period_unit:" + packMaterial.item_validity_period_unit;
            param[20] = "@item_keep_condition:" + packMaterial.item_keep_condition;
            param[21] = "@item_keep_temperature:" + packMaterial.item_keep_temperature;
            param[22] = "@keeping_warehouse:" + packMaterial.keeping_warehouse;
            param[23] = "@hs_no:" + packMaterial.hs_no;
            param[24] = "@item_pack_barcode:" + packMaterial.item_pack_barcode;
            param[25] = "@sampling_report_type:" + packMaterial.sampling_report_type;
            param[26] = "@test_level:" + packMaterial.test_level;
            param[27] = "@material_out_type:" + packMaterial.material_out_type;
            param[28] = "@item_keeping_term:" + packMaterial.item_keeping_term;
            param[29] = "@item_keeping_unit:" + packMaterial.item_keeping_unit;
            param[30] = "@container_shape:" + packMaterial.container_shape;
            param[31] = "@item_safety_day:" + packMaterial.item_safety_day;
            param[32] = "@monthly_qt:" + packMaterial.monthly_qt;
            param[33] = "@basic_purchase_qty:" + packMaterial.basic_purchase_qty;
            param[34] = "@basic_purchase_unit:" + packMaterial.basic_purchase_unit;
            param[35] = "@delivery_period:" + packMaterial.delivery_period;
            param[36] = "@test_period:" + packMaterial.test_period;
            param[37] = "@material_maker:" + packMaterial.material_maker;
            param[38] = "@item_case_cd:" + packMaterial.item_case_cd;
            param[39] = "@sampling_qt:" + packMaterial.sampling_qt;
            param[40] = "@sampling_unit:" + packMaterial.sampling_unit;
            param[41] = "@sampling_emp_cd:" + packMaterial.sampling_emp_cd;
            param[42] = "@ordering_type:" + packMaterial.ordering_type;
            param[43] = "@materialmaker_ck:" + packMaterial.materialmaker_ck;
            param[44] = "@at_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            param[45] = "@scale_factor:" + packMaterial.scale_factor;
            param[46] = "@item_sampling:" + packMaterial.item_sampling;
            param[47] = "@item_pack_size:" + packMaterial.item_pack_size;
            param[48] = "@item_cd_n:" + packMaterial.item_cd_n;
            param[49] = "@item_form_cd:" + packMaterial.item_form_cd;
            param[50] = "@trade_cd:" + packMaterial.trade_cd;
            param[51] = "@trade_cd2:" + packMaterial.trade_cd2;
            param[52] = "@management_type:" + packMaterial.management_type;
            param[53] = "@item_type3:" + packMaterial.item_type3;
            param[54] = "@unit_price:" + packMaterial.unit_price; 
            param[55] = "@manufacture_qty_yn:" + packMaterial.manufacture_qty_yn;
            param[56] = "@item_spec_type:" + packMaterial.item_spec_type;
            param[57] = "@item_spec_qty:" + packMaterial.item_spec_qty;
            if (packMaterial.taxfree_yn == null || packMaterial.taxfree_yn == "") packMaterial.taxfree_yn = "N";
            param[58] = "@taxfree_yn:" + packMaterial.taxfree_yn;  //면세대상 여부

            return param;
        }

        internal DataTable PackMaterialMaster2SelectMaker(string packMaterial_cd)
        {
            string sSpName = "SP_PackMaterialMaster2";
            string sGubun = "S2";
            string[] pParam = new string[1];
            pParam[0] = "@packMaterial_cd:" + packMaterial_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string PackMaterialMakerCRUD(List<PackMaterial> packMaterials)
        {
            string sSpName = "SP_PackMaterialMaster2";
            string[] pParam;
            string res = "";

            foreach (PackMaterial tmp in packMaterials)
            {
                switch (tmp.pGubun)
                {
                    case "I2":
                        pParam = new string[2];
                        pParam[0] = "@packMaterial_maker_cd:" + tmp.material_maker_cd;
                        pParam[1] = "@packMaterial_cd:" + tmp.item_cd;

                        res = _bllSpExecute.SpExecuteString(sSpName, tmp.pGubun, pParam);

                        break;

                    case "U2":
                        pParam = new string[3];
                        pParam[0] = "@packMaterial_maker_cd:" + tmp.material_maker_cd;
                        pParam[1] = "@u_packMaterial_maker_cd:" + tmp.u_material_maker_cd;
                        pParam[2] = "@packMaterial_cd:" + tmp.item_cd;

                        res = _bllSpExecute.SpExecuteString(sSpName, tmp.pGubun, pParam);

                        break;

                    case "D2":
                        pParam = new string[2];
                        pParam[0] = "@packMaterial_maker_cd:" + tmp.material_maker_cd;
                        pParam[1] = "@packMaterial_cd:" + tmp.item_cd;

                        res = _bllSpExecute.SpExecuteString(sSpName, tmp.pGubun, pParam);

                        break;
                }
            }

            return res;
        }
    }
}
