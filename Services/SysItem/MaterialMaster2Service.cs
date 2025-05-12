using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.SysItem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.SysItem
{
    public class MaterialMaster2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// 원료들의 리스트를 불러어는 메소드
        /// </summary>
        /// <param name="pItemCd"></param>
        /// <param name="pItemType1"></param>
        /// <param name="pItemType2"></param>
        /// <param name="pProdEnd"></param>
        /// <returns></returns>
        internal DataTable Select(string pItemCd, string pItemType1, string pItemType2, string pProdEnd)
        {
            string sSpName = "SP_MaterialMaster2";
            string sGubun = "S";
            string[] pParam = new string[4];

            pParam[0] = "@s_item_cd:" + pItemCd;
            pParam[1] = "@s_item_type1:" + pItemType1;
            pParam[2] = "@s_item_type2:" + pItemType2;
            pParam[3] = "@s_prod_end:" + pProdEnd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }
        
        internal DataTable SelectSuppliers(string vender_gb)
        {
            string sSpName = "SP_Vender";
            string gubun = "S";

            string[] param = new string[3];

            param[0] = "@vender_cd_S:";
            param[1] = "@s_use_yn:" + "Y";
            param[2] = "@s_vender_gb:" + vender_gb;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        /// <summary>
        /// 원료 코드 중복체크 메소드
        /// </summary>
        /// <param name="pItemCd"></param>
        /// <returns></returns>
        internal string CdDuplicateCheck(string pItemCd)
        {
            string sSpName = "SP_MaterialMaster2";
            string sGubun = "SC";
            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + pItemCd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        /// <summary>
        /// 원료 DB에 삽입
        /// </summary>
        /// <param name="pMaterial"></param>
        /// <returns></returns>
        internal string CRUD(Material pMaterial)
        {
            string sSpName = "SP_MaterialMaster2";
            string sGubun = pMaterial.pGubun;
            string[] pParam = GetParam(pMaterial);

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            //return (message.Equals("1") ? true : false);
            return message;

        }

        private string[] GetParam(Material pMaterial)
        {
            string[] param = new string[62];

            //기본정보
            param[0] = "@item_cd:" + pMaterial.item_cd;
            param[1] = "@item_nm:" + pMaterial.item_nm;
            param[2] = "@abbreviation_cd:" + pMaterial.abbreviation_cd;
            param[3] = "@item_s_nm:" + pMaterial.item_s_nm;
            param[4] = "@item_enm:" + pMaterial.item_enm;
            param[5] = "@item_jnm:" + pMaterial.item_jnm;
            param[6] = "@prod_end:" + pMaterial.prod_end;
            param[7] = "@break_type:" + pMaterial.break_type;
            param[61] = "@item_nm_registry:" + pMaterial.item_nm_registry;

            //분류정보
            param[8] = "@outer_process:" + (pMaterial.outer_process == null ? "" : pMaterial.outer_process);
            param[9] = "@in_out_ck:" + (pMaterial.in_out_ck == null ? "" : pMaterial.in_out_ck);
            param[10] = "@abc_gubun:" + pMaterial.abc_gubun;
            param[11] = "@plant_cd:" + (pMaterial.plant_cd == null ? "" : pMaterial.plant_cd);
            param[12] = "@item_type1:" + pMaterial.form_item_type1;
            param[13] = "@item_type2:" + pMaterial.form_item_type2;

            //주요정보
            param[14] = "@item_unit:" + pMaterial.item_unit_cd;
            param[15] = "@item_exam_yn:" + pMaterial.item_exam_yn;
            param[16] = "@sampling_calc:" + pMaterial.sampling_calc;
            param[17] = "@item_validity_period:" + pMaterial.item_validity_period;
            param[18] = "@item_validity_period_unit:" + pMaterial.item_validity_period_unit;
            param[19] = "@item_keep_condition:" + (pMaterial.item_keep_condition == null ? "" : pMaterial.item_keep_condition);
            param[20] = "@item_keep_temperature:" + (pMaterial.item_keep_temperature == null ? "" : pMaterial.item_keep_temperature);
            param[21] = "@keeping_warehouse:" + pMaterial.keeping_warehouse;
            param[22] = "@hs_no:" + pMaterial.hs_no;
            param[23] = "@asepsis_item_ck:" + pMaterial.asepsis_item_ck;

            //2012.05.04 by 황은일 추가(주요정보) 
            param[24] = "@item_type3:" + pMaterial.item_type3;                 //원료구분
            param[25] = "@item_keeping_term:" + pMaterial.kepping_term;   //보관기간(1)
            param[26] = "@item_keeping_unit:" + pMaterial.kepping_term_unit;   //보관기간단위
            param[27] = "@container_shape1_1:" + pMaterial.container_shape;       //포장재질1_1
            param[28] = "@item_sampling:" + pMaterial.pack_sampling_ck;           //통별 샘플링 대상

            //생산계획정보
            param[29] = "@item_safety_day:" + (pMaterial.item_safety_day == null ? "0" : pMaterial.item_safety_day.Replace(",", ""));
            param[30] = "@monthly_qt:" + (pMaterial.monthly_qt == null ? "0" : pMaterial.monthly_qt.Replace(",", ""));
            param[31] = "@basic_purchase_qty:" + (pMaterial.basic_purchase_qty == null ? "0" : pMaterial.basic_purchase_qty.Replace(",", ""));
            param[32] = "@basic_purchase_unit:" + (pMaterial.basic_purchase_unit == null ? "" : pMaterial.basic_purchase_unit);
            param[33] = "@delivery_period:" + (pMaterial.delivery_period == null ? "0" : pMaterial.delivery_period.Replace(",", ""));
            param[34] = "@test_period:" + (pMaterial.test_period == null ? "0" : pMaterial.test_period.Replace(",", ""));
            param[35] = "@material_maker:" + pMaterial.material_maker;
            param[36] = "@item_case_cd:" + pMaterial.item_case_cd;
            param[37] = "@sampling_qt:" + (pMaterial.sampling_qt == null ? "0" : pMaterial.sampling_qt.Replace(",", ""));
            param[38] = "@sampling_unit:" + pMaterial.sampling_unit;
            param[39] = "@sampling_emp_cd:" + pMaterial.sampling_emp_cd;
            param[40] = "@ordering_type:" + pMaterial.ordering_type;
            param[41] = "@materialmaker_ck:" + pMaterial.materialmaker_ck;

            //Audit Trail
            param[42] = "@at_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();//AUDIT TRAIL을 위한 프로그램사용자코드[2010.12.20 류현민]

            //포장재질 추가 2012.05.25 신현강
            param[43] = "@container_shape1_2:" + pMaterial.container_shape1_2;       //포장재질
            param[44] = "@container_shape1_3:" + pMaterial.container_shape1_3;       //포장재질
            param[45] = "@container_shape2_1:" + pMaterial.container_shape2_1;       //포장재질
            param[46] = "@container_shape2_2:" + pMaterial.container_shape2_2;       //포장재질
            param[47] = "@container_shape2_3:" + pMaterial.container_shape2_3;       //포장재질
            param[48] = "@container_shape3_1:" + pMaterial.container_shape3_1;       //포장재질
            param[49] = "@container_shape3_2:" + pMaterial.container_shape3_2;       //포장재질
            param[50] = "@container_shape3_3:" + pMaterial.container_shape3_3;       //포장재질

            //환산계수 
            param[51] = "@scale_factor:" + pMaterial.scale_factor;       //2012.06.04 황안례 추가
            param[52] = "@trade_cd:" + (pMaterial.trade_cd == null ? "" : pMaterial.trade_cd);

            param[53] = "@hb_ck:" + pMaterial.hb_ck; // 허벌라이프 전용 원료 체크하기 위해서 추가 20140713 김소형

            param[54] = "@standard_cd:" + pMaterial.standard_cd;    //표준코드

            param[55] = "@country_cd:" + pMaterial.country_cd;    //국가코드

            param[56] = "@gmo_material_yn:" + pMaterial.gmo_material_yn;
            param[57] = "@retest_yn:" + pMaterial.retest_yn;  //재시험대상여부

            // 추가 정보
            param[58] = "@test_standard:" + (String.IsNullOrEmpty(pMaterial.test_standard) ? "" : pMaterial.test_standard.Trim());
            param[59] = "@unit_price:" + pMaterial.unit_price;  //구매단가
            if (pMaterial.manufacture_qty_yn == null || pMaterial.manufacture_qty_yn =="") pMaterial.manufacture_qty_yn = "N";
            param[60] = "@manufacture_qty_yn:" + pMaterial.manufacture_qty_yn;  //허가량합계 제외
            if (pMaterial.taxfree_yn == null || pMaterial.taxfree_yn == "") pMaterial.taxfree_yn = "N";
            param[61] = "@taxfree_yn:" + pMaterial.taxfree_yn;  //면세대상 여부

            return param;
        }

        internal DataTable SelectMaterialImage(string item_cd)
        {
            string sSpName = "SP_Item_Standard2";

            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "sData", pParam);

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

        internal string MaterialMaster2DeleteImage(string item_cd, int gubun)
        {
            string sSpName = "SP_Item_Standard2";
            string sGubun = "I_D";
            string[] pParam = new string[2];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@fgubun:" + gubun;

            string res = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return res;
        }

        internal string MaterialMakerCRUD(List<Material> materials)
        {
            string sSpName = "SP_MaterialMaster2";
            string[] pParam;
            string res = "";

            foreach (Material tmp in materials)
            {
                switch (tmp.pGubun)
                {
                    case "I2":
                        pParam = new string[2];
                        pParam[0] = "@material_maker_cd:" + tmp.material_maker_cd;
                        pParam[1] = "@material_cd:" + tmp.item_cd;

                        res = _bllSpExecute.SpExecuteString(sSpName, tmp.pGubun, pParam);

                        break;

                    case "U2":
                        pParam = new string[4];
                        pParam[0] = "@material_maker_cd:" + tmp.material_maker_cd;
                        pParam[1] = "@u_material_maker_cd:" + tmp.u_material_maker_cd;
                        pParam[2] = "@material_cd:" + tmp.item_cd;
                       // pParam[3] = "@idx:" + tmp.idx;

                        res = _bllSpExecute.SpExecuteString(sSpName, tmp.pGubun, pParam);

                        break;

                    case "D2":
                        pParam = new string[3];
                        pParam[0] = "@material_maker_cd:" + tmp.material_maker_cd;
                        pParam[1] = "@material_cd:" + tmp.item_cd;
                       // pParam[2] = "@idx:" + tmp.idx;

                        res = _bllSpExecute.SpExecuteString(sSpName, tmp.pGubun, pParam);

                        break;
                }
            }

            return res;
        }

        internal void uploadImage(byte[] myBytes, string item_cd, int gubun, string fileName, int imageCnt)
        {
            if (imageCnt <= 0)
            {
                createMaterialImageRow(item_cd);
            }

            string sSpName = "SP_Item_Standard2";
            string sGubun = "I_U";
            string[] pParam = new string[2];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@fgubun:" + gubun;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, "@"+ fileName, myBytes, pParam);

            return;
        }

        private void createMaterialImageRow(string item_cd)
        {
            string sSpName = "SP_Item_Standard2";
            string sGubun = "I_I";
            string[] pParam = new string[4];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@upper_image:" + "";
            pParam[2] = "@middle_image:" + "";
            pParam[3] = "@lower_image:" + "";

            _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
        }

        internal DataTable MaterialMaster2SelectMaker(string material_cd)
        {
            string sSpName = "SP_MaterialMaster2";
            string sGubun = "S2";
            string[] pParam = new string[1];
            pParam[0] = "@material_cd:" + material_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }


        internal DataTable MaterialMaster2SelectContent(string material_cd)
        {
            string sSpName = "SP_MaterialMaster2";
            string sGubun = "S3";
            string[] pParam = new string[1];
            pParam[0] = "@material_cd:" + material_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string DeleteMaterial(string item_cd)
        {
            string sSpName = "SP_MaterialMaster2";
            string sGubun = "D";
            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + item_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return (String.IsNullOrEmpty(message) ? "실패했습니다." : message);
        }


        internal DataTable GetStandardCd(string pStr)
        {
            string sSpName = "CODEHELP";
            string strsql = "";

            switch (pStr)
            {
                case "standard":
                    strsql = "exec CODEHELP @strsql = N'SELECT item_cd, item_nm FROM  v_material_standard_cd_Y WHERE (item_cd  LIKE ''%%'' OR item_nm  LIKE ''%%'') AND item_cd  LIKE ''%%'' AND item_nm  LIKE ''%%'' ORDER BY 1 '";

                    break;

                case "country":
                    //strsql = "exec CODEHELP @strsql=N'SELECT three_digit_cd,two_digit_cd,kr_nm,en_nm FROM country_code'";
                    strsql = "exec CODEHELP @strsql=N'SELECT country_cd, country_nm FROM item_standard_country'";           //식품의약품안전처 제공 국가코드
                    break;

                case "vendor":
                    strsql = "exec CODEHELP @strsql=N'SELECT vender_cd, vender_nm FROM v_vender_buy WHERE (vender_cd  LIKE ''%%'' OR vender_nm  LIKE ''%%'') AND vender_cd  LIKE ''%%'' AND vender_nm  LIKE ''%%'' ORDER BY 1 '";

                    break;
            }

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, strsql);

            return dt;
        }

        internal string MaterialContentCRUD(List<Material> materials)
        {
            string sSpName = "SP_MaterialMaster2";
            string[] pParam;
            string res = "";

            foreach (Material tmp in materials)
            {
                switch (tmp.pGubun)
                {
                    case "I3":
                        pParam = new string[3];
                        pParam[0] = "@material_content_nm:" + tmp.material_content_nm;
                        pParam[1] = "@material_content_percent:" + tmp.material_content_percent;
                        pParam[2] = "@material_cd:" + tmp.item_cd;

                        res = _bllSpExecute.SpExecuteString(sSpName, tmp.pGubun, pParam);

                        break;

                    case "U3":
                        pParam = new string[4];
                        pParam[0] = "@material_content_nm:" + tmp.material_content_nm;
                        pParam[1] = "@material_content_percent:" + tmp.material_content_percent;
                        pParam[2] = "@material_cd:" + tmp.item_cd;
                       // pParam[3] = "@idx:" + tmp.idx;

                        res = _bllSpExecute.SpExecuteString(sSpName, tmp.pGubun, pParam);

                        break;

                    case "D3":
                        pParam = new string[3];
                        pParam[0] = "@material_content_nm:" + tmp.material_content_nm;
                        pParam[1] = "@material_cd:" + tmp.item_cd;
                       // pParam[2] = "@idx:" + tmp.idx;

                        res = _bllSpExecute.SpExecuteString(sSpName, tmp.pGubun, pParam);

                        break;
                }
            }

            return res;
        }


        
    }
}