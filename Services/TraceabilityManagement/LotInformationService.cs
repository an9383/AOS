using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.TraceabilityManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.TraceabilityManagement
{
    public class LotInformationService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable LotInformationSearch(string s_date, string e_date)
        {
            string[] param = new string[2];
            param[0] = "@date_S:" + s_date;
            param[1] = "@date_E:" + e_date;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_LotInformation", "S_All", param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable LotInformationDetailSearch(string gms_pdtlot_seq)
        {
            string[] param = new string[1];
            param[0] = "@gms_pdtlot_seq:" + gms_pdtlot_seq;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_LotInformation", "S_Material", param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal void LotInformationSave(List<LotInformation> lotInformation)
        {
            for(var i =0; i < lotInformation.Count; i++)
            {
                string[] param = new string[7];
                param[0] = "@gms_pdtlot_seq:" + lotInformation[i].gms_seq;
                param[1] = "@remark:" + lotInformation[i].remark;
                param[2] = "@gmo_yn:" + lotInformation[i].gmo_yn;
                param[3] = "@temp_1:" + lotInformation[i].temp_1;
                param[4] = "@temp_2:" + lotInformation[i].temp_2;
                param[5] = "@temp_3:" + lotInformation[i].temp_3;
                param[6] = "@update_user_cd:" + Public_Function.User_cd;

                _bllSpExecute.SpExecuteString("SP_LotInformation", "U_Remark", param);
            }
        }


        internal void LotInformationMaterialSave(List<MaterialInformation> materialData)
        {
            for (var i = 0; i < materialData.Count; i++)
            {
                string[] param = new string[7];
                param[0] = "@gms_pdtlot_seq:" + materialData[i].gms_seq;
                param[1] = "@remark:" + materialData[i].remark;
                param[2] = "@gmo_yn:" + materialData[i].gmo_yn;
                param[3] = "@temp_1:" + materialData[i].temp_1;
                param[4] = "@temp_2:" + materialData[i].temp_2;
                param[5] = "@temp_3:" + materialData[i].temp_3;
                param[6] = "@update_user_cd:" + Public_Function.User_cd;

                _bllSpExecute.SpExecuteString("SP_LotInformation", "U_Remark_Material", param);
            }

        }

        internal string LotInformationDelete(string gms_pdtlot_seq)
        {
            string[] param = new string[1];
            param[0] = "@gms_seq:" + gms_pdtlot_seq;

            string result = _bllSpExecute.SpExecuteString("SP_LotInformation", "delete", param);

            return result;
        }

        internal void LoadAll(string s_date, string e_date)
        {
            TimeSpan dateDiff = DateTime.Parse(e_date) - DateTime.Parse(s_date);
            int day = dateDiff.Days;

            for (int i = 0; i < day; i++)
            {
                string[] param = new string[2];
                param[0] = "@date_S:" + (DateTime.Parse(s_date).AddDays(i).ToString("yyyy-MM-dd"));
                param[1] = "@insert_user_cd:" + Public_Function.User_cd;

                _bllSpExecute.SpExecuteString("SP_LotInformation", "S_LoadAll", param);
            }
        }

        internal void LoadPart(string date_S, string gms_item_stock_id, string gms_pdtlot_seq)
        {

            string[] param = new string[4];
            param[0] = "@date_S:" + date_S;
            param[1] = "@gms_item_stock_id:" + gms_item_stock_id;
            param[2] = "@insert_user_cd:" + Public_Function.User_cd;
            param[3] = "@gms_pdtlot_seq:" + gms_pdtlot_seq;

            _bllSpExecute.SpExecuteString("SP_LotInformation", "S_LoadPart", param);

        }


        internal string LotInformationMaterialDelete(string gms_pdtlot_seq, string gms_material_seq)
        {
            string[] param = new string[2];
            param[0] = "@gms_pdtlot_seq:" + gms_pdtlot_seq;
            param[1] = "@gms_material_seq:" + gms_material_seq;

           string result =  _bllSpExecute.SpExecuteString("SP_LotInformation", "material_delete", param);
           return result;
        }

        internal string LotDataCheck(string gms_pdtlot_seq)
        {
           // bool check = true;
            string check = "Y";

            string[] param = new string[1];
            param[0] = "@gms_pdtlot_seq:" + gms_pdtlot_seq;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_LotInformation", "DataCheck", param);

            //PDTLOT 공백 확인  
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (ds.Tables[0].Rows[i][j].ToString() == "")
                        {
                            //  check = false;
                            check = "N";
                            break;
                        }
                    }

                }
            }
            else
            {
               // check = false;
                check = "N";
                return check;
            }

            if ("Y" == check)
            {
                //PDTLOT_ORM_INFO 공백 확인
                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        for (int j = 0; j < ds.Tables[1].Columns.Count; j++)
                        {
                            if (ds.Tables[1].Rows[i][j].ToString() == "")
                            {
                                //check = false;
                                check = "N";
                                break;
                            }
                        }
                    }
                }
                else //저장된 내용이 없으면
                {
                    //check = false;
                    check = "N";
                    return check;
                }
            }

            return check;

        }


        internal void LotPreparePart(string gms_pdtlot_seq, string date_S, string emp_cd)
        {
            string[] param = new string[5];
            param[0] = "@gms_pdtlot_seq:" + gms_pdtlot_seq;
            param[1] = "@date_S:" + date_S;
            param[2] = "@update_user_cd:" + Public_Function.User_cd;

            param[3] = "@emp_cd:" + emp_cd;
            param[4] = "@validation_type:" + "2";

            _bllSpExecute.SpExecuteString("SP_LotInformation", "Prepare_Part", param);
        }

        internal void LotPrepareCancel(string gms_pdtlot_seq)
        {
            string[] param = new string[2];
            param[0] = "@gms_pdtlot_seq:" + gms_pdtlot_seq;
            param[1] = "@update_user_cd:" + Public_Function.User_cd;

            _bllSpExecute.SpExecuteString("SP_LotInformation", "Prepare_Cancel", param);
        }
    }
}