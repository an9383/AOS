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
    public class ItemStandardService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectItemStandard(string item_cd, string item_license_current_ck, string item_gb)
        {
            string sSpName = "SP_Item_Standard2";
            string sGubun = "S";
            string[] pParam = new string[5];

            pParam[0] = "@s_item_form_cd:" + "%";
            pParam[1] = "@s_item_cd:" + item_cd;
            pParam[2] = "@s_item_license_current_ck:" + item_license_current_ck;
            pParam[3] = "@s_item_type1:" + "%";
            pParam[4] = "@item_gb:" + item_gb;

            DataTable dt = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam).Tables[0];

            return dt;
        }

        internal DataTable SelectItemDetail(string item_cd)
        {
            string sSpName = "SP_Item_Standard2";
            string sGubun = "SR";
            string[] pParam = new string[1];

            pParam[0] = "@item_cd:" + item_cd;

            DataTable dt = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam).Tables[0];

            return dt;
        }

        internal DataTable SelectItemLicenseNo(string item_cd)
        {
            string sSpName = "SP_Item_Standard2";
            string sGubun = "SR2";
            string[] pParam = new string[1];

            pParam[0] = "@item_cd:" + item_cd;

            DataTable dt = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam).Tables[0];

            return dt;
        }

        internal string itemCdDuplicateCheck(string item_cd)
        {
            string sSpName = "SP_Item_Standard2";
            string sGubun = "SC";
            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + item_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal DataTable SelectWarehousePopupData()
        {
            string strsql = "SELECT workroom_cd, workroom_nm";
            strsql += " FROM workroom";
            strsql += " WHERE (workroom_cd  LIKE '%%' OR workroom_nm  LIKE '%%')";
            strsql += " AND workroom_type  LIKE '%%' AND workroom_type  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal string ItemStandardCRUD(ItemStandard itemStandard)
        {
            string sSpName = "SP_Item_Standard2";
            string sGubun = itemStandard.gubun;

            string message = "";

            if (sGubun == "DR")
            {
                string[] pParam = new string[3];
                pParam[0] = "@item_cd:" + itemStandard.item_cd;
                pParam[1] = "@item_license_no:" + itemStandard.item_license_no;
                pParam[2] = "@at_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            else if (sGubun == "D")
            {
                string[] pParam = new string[3];
                pParam[0] = "@item_cd:" + itemStandard.item_cd;
                pParam[1] = "@item_license_no:" + itemStandard.item_license_no;
                pParam[2] = "@at_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();

                _bllSpExecute.SpExecuteString(sSpName, "DR", pParam);

                pParam = new string[3];
                pParam = new string[3];
                pParam[0] = "@item_cd:" + itemStandard.item_cd;
                pParam[1] = "@at_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
                pParam[2] = "@item_gb:" + itemStandard.item_gb;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            else
            {
                string[] pParam = GetParam(itemStandard);

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }

            if (!(message==""))
            {
                switch (sGubun)
                {
                    case "I":
                        message = "입력되었습니다.";
                        break;
                    case "U":
                        message = "수정되었습니다.";
                        break;
                    case "DR":
                        message = "삭제되었습니다.";
                        break;
                }
            }

            if (sGubun=="I")
            {
                createItemImageRow(itemStandard.item_cd, "","","");
            }

            return message;
        }

        private void createItemImageRow(string item_cd, string upper_image, string middle_image, string lower_image)
        {
            string sSpName = "SP_Item_Standard2";
            string sGubun = "I_I";
            string[] pParam = new string[4];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@upper_image:" + upper_image;
            pParam[2] = "@middle_image:" + middle_image;
            pParam[3] = "@lower_image:" + lower_image;

            _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
        }

        private string[] GetParam(ItemStandard itemStandard)
        {
            string[] param = new string[80];

            param[0] = "@abbreviation_cd:" + itemStandard.abbreviation_cd;
            param[1] = "@abc_gubun:" + itemStandard.abc_gubun;
            param[2] = "@at_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            param[3] = "@break_type:" + itemStandard.break_type;
            param[4] = "@concentrate_yn:" + itemStandard.concentrate_yn;
            param[5] = "@container_shape:" + itemStandard.container_shape;
            param[6] = "@in_out_ck:" + itemStandard.in_out_ck;
            param[7] = "@item_cd:" + itemStandard.item_cd;
            param[8] = "@item_cd_n:" + itemStandard.item_cd_n;
            param[9] = "@item_class:" + itemStandard.item_class;
            param[10] = "@item_content:" + itemStandard.item_content;
            param[11] = "@item_content_unit:" + itemStandard.item_content_unit;
            param[12] = "@item_content2:" + itemStandard.item_content2;
            param[13] = "@item_content3:" + itemStandard.item_content3;
            param[14] = "@item_enm:" + itemStandard.item_enm;
            param[15] = "@item_exchangerate:" + itemStandard.item_exchangerate;
            param[16] = "@item_form_cd:" + itemStandard.item_form_cd;
            param[17] = "@item_gb:" + itemStandard.item_gb;
            param[18] = "@item_input_content:" + itemStandard.item_input_content;
            param[19] = "@item_jnm:" + itemStandard.item_jnm;
            param[20] = "@item_keep_condition:" + itemStandard.item_keep_condition;
            param[21] = "@item_keep_temperature:" + itemStandard.item_keep_temperature;
            param[22] = "@item_keeping_term:" + itemStandard.item_keeping_term;
            param[23] = "@item_keeping_unit:" + itemStandard.item_keeping_unit;
            param[24] = "@item_license_caution:" + itemStandard.item_license_caution;
            param[25] = "@item_license_current_ck:" + itemStandard.prod_end;
            param[26] = "@item_license_dosage:" + itemStandard.item_license_dosage;
            param[27] = "@item_license_effect:" + itemStandard.item_license_effect;
            param[28] = "@item_license_no:" + itemStandard.item_license_no;
            param[29] = "@item_license_start_date:" + itemStandard.item_license_start_date;
            param[30] = "@item_license_storage:" + itemStandard.item_license_storage;
            param[31] = "@item_license_unit:" + itemStandard.item_license_unit;
            param[32] = "@item_lot_real_size:" + itemStandard.item_lot_real_size;
            param[33] = "@item_lot_real_size2:" + itemStandard.item_lot_real_size2;
            param[34] = "@item_lot_size:" + itemStandard.item_lot_size;
            param[35] = "@item_lot_size_unit:" + itemStandard.item_lot_size_unit;
            param[36] = "@item_lot_size2:" + itemStandard.item_lot_size2;
            param[37] = "@item_make_cd:" + itemStandard.item_make_cd;
            param[38] = "@item_nm:" + itemStandard.item_nm;
            param[39] = "@item_pack_barcode:" + itemStandard.item_pack_barcode;
            param[40] = "@item_pack_size:" + itemStandard.item_pack_size;
            param[41] = "@item_packing_type:" + itemStandard.item_packing_type;
            param[42] = "@item_packunit:" + itemStandard.item_packunit;
            param[43] = "@item_packunit2:" + itemStandard.item_packunit2;
            param[44] = "@item_permission_date:" + itemStandard.item_permission_date;
            param[45] = "@item_permission_no:" + itemStandard.item_permission_no;
            param[46] = "@item_proc_no:" + itemStandard.item_proc_no;
            param[47] = "@item_s_nm:" + itemStandard.item_s_nm;
            param[48] = "@item_safety_day:" + itemStandard.item_safety_day;
            param[49] = "@item_surface:" + itemStandard.item_surface;
            param[50] = "@item_type1:" + itemStandard.item_type1;
            param[51] = "@item_type2:" + itemStandard.item_type2;
            param[52] = "@item_type3:" + itemStandard.item_type3;
            param[53] = "@item_unit:" + itemStandard.item_unit;
            param[54] = "@item_validity_period:" + itemStandard.item_validity_period;
            param[55] = "@item_validity_period_unit:" + itemStandard.item_validity_period_unit;
            param[56] = "@item_write_date:" + itemStandard.item_write_date;
            param[57] = "@keeping_warehouse:" + itemStandard.keeping_warehouse;
            param[58] = "@lot_size_display_type:" + itemStandard.lot_size_display_type;
            param[59] = "@Multi_Yn:" + itemStandard.Multi_Yn;
            param[60] = "@outer_process:" + itemStandard.outer_process;
            param[61] = "@packing_major_ck:" + itemStandard.packing_major_ck;
            param[62] = "@plant_cd:" + itemStandard.plant_cd;
            param[63] = "@prod_end:" + itemStandard.prod_end;
            param[64] = "@revision_remark:" + itemStandard.revision_remark;
            param[65] = "@safe_stock:" + itemStandard.safe_stock;
            param[66] = "@sampling_calc:" + itemStandard.sampling_calc;
            param[67] = "@second_pack_qt:" + itemStandard.second_pack_qt;
            param[68] = "@second_pack_type:" + itemStandard.second_pack_type;
            param[69] = "@set_Yn:" + itemStandard.set_Yn;
            param[70] = "@traceability_ck:" + itemStandard.traceability_ck;
            param[71] = "@traceability_reg_num:" + itemStandard.traceability_reg_num;
            param[72] = "@trade_cd:" + itemStandard.trade_cd;
            param[73] = "@trade_cd2:" + itemStandard.trade_cd2;
            param[74] = "@vender_item_cd:" + itemStandard.vender_item_cd;
            param[75] = "@weighing_time:" + itemStandard.weighing_time;
            param[76] = "@item_error_range:" + itemStandard.item_error_range;
            if (itemStandard.taxfree_yn == null || itemStandard.taxfree_yn == "") itemStandard.taxfree_yn = "N";
            param[77] = "@taxfree_yn:" + itemStandard.taxfree_yn;  //면세대상 여부
            param[78] = "@item_permission_type:" + itemStandard.item_permission_type;
            param[79] = "@certification_type:" + itemStandard.certification_type;

            return param;
        }

        internal string GetLicenseNo(string item_cd)
        {
            string sSpName = "SP_Item_Standard2";
            string sGubun = "GetLicenseNo";

            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + item_cd;

            string licenseNo = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return licenseNo;
        }

        internal DataTable selectItemImage(string item_cd)
        {
            string sSpName = "SP_Item_Standard2";
            string sGubun = "SData";
            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable table = new DataTable();
            table.Columns.Add("upper_image", typeof(String));
            table.Columns.Add("middle_image", typeof(String));
            table.Columns.Add("lower_image", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                if (row["upper_image"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["upper_image"]);
                    string url = "data:Image/png;base64," + str;
                    _row["upper_image"] = url;
                }
                else
                {
                    _row["upper_image"] = "/Content/image/defaultSign.png";
                }

                if (row["middle_image"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["middle_image"]);
                    string url = "data:Image/png;base64," + str;
                    _row["middle_image"] = url;
                }
                else
                {
                    _row["middle_image"] = "/Content/image/defaultSign.png";
                }

                if (row["lower_image"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["lower_image"]);
                    string url = "data:Image/png;base64," + str;
                    _row["lower_image"] = url;
                }
                else
                {
                    _row["lower_image"] = "/Content/image/defaultSign.png";
                }

                table.Rows.Add(_row);
            }

            return table;
        }

        internal void uploadImage(string item_cd, string fGubun, string fileInputName, byte[] myBytes)
        {
            string sSpName = "SP_Item_Standard2";
            string sGubun = "I_U";
            string[] pParam = new string[2];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@fgubun:" + fGubun;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, ("@"+ fileInputName), myBytes, pParam);

            return;
        }

        internal string deleteImage(string item_cd, string fGubun)
        {
            string sSpName = "SP_Item_Standard2";
            string sGubun = "I_D";

            string[] pParam = new string[2];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@fgubun:" + fGubun;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal DataTable SelectMAkeItemPopupData()
        {
            string strsql = "SELECT item_cd, item_nm";
            strsql += " FROM v_item_makingstandard";
            strsql += " WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%')";
            strsql += " AND item_cd  LIKE '%%' AND item_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }
    }
}