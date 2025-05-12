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
    public class DespatchInformationService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable LotInformationSearch(string s_date, string e_date)
        {
            string[] param = new string[2];
            param[0] = "@date_S:" + s_date;
            param[1] = "@date_E:" + e_date;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_DespatchInformation", "S_All", param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal void LotInformationSave(List<DespatchInformation> despatchData)
        {
            for (var i = 0; i < despatchData.Count; i++)
            {
                string[] param = new string[6];
                param[0] = "@gms_seq:" + despatchData[i].gms_seq;
                param[1] = "@remark:" + despatchData[i].remark;
                param[2] = "@temp_1:" + despatchData[i].temp_1;
                param[3] = "@temp_2:" + despatchData[i].temp_2;
                param[4] = "@temp_3:" + despatchData[i].temp_3;
                param[5] = "@update_user_cd:" + Public_Function.User_cd;

                _bllSpExecute.SpExecuteString("SP_DespatchInformation", "U_Remark", param);
            }
        }

        internal void DepatchLoadAll(string s_date, string e_date)
        {
            TimeSpan dateDiff = DateTime.Parse(e_date) - DateTime.Parse(s_date);
            int day = dateDiff.Days;

            for (int i = 0; i < day; i++)
            {
                string[] param = new string[2];
                param[0] = "@date_S:" + (DateTime.Parse(s_date).AddDays(i).ToString("yyyy-MM-dd"));
                param[1] = "@insert_user_cd:" + Public_Function.User_cd;

                _bllSpExecute.SpExecuteString("SP_DespatchInformation", "S_LoadAll", param);
            }
        }


        internal void DepatchLoadPart(string date_S, string gms_despatch_order_no, string gms_item_issue_id)
        {

            string[] param = new string[4];
            param[0] = "@date_S:" + date_S;
            param[1] = "@gms_despatch_order_no:" + gms_despatch_order_no;
            param[2] = "@gms_item_issue_id:" + gms_item_issue_id;
            param[3] = "@gms_pdtlot_seq:" + Public_Function.User_cd;

            _bllSpExecute.SpExecuteString("SP_DespatchInformation", "S_LoadPart", param);

        }

        internal string DepatchDataCheck(string gms_seq)
        {
            // bool check = true;
            string check = "Y";

            string[] param = new string[1];
            param[0] = "@gms_seq:" + gms_seq;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_DespatchInformation", "DataCheck", param);

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

            return check;

        }

        internal void DespatchPreparePart(string gms_seq, string tgow_dt, string emp_cd)
        {
            string[] param = new string[5];
            param[0] = "@gms_seq:" + gms_seq;
            param[1] = "@date_S:" + tgow_dt;
            param[2] = "@update_user_cd:" + Public_Function.User_cd;

            param[3] = "@emp_cd:" + emp_cd;
            param[4] = "@validation_type:" + "2";

            _bllSpExecute.SpExecuteString("SP_DespatchInformation", "Prepare_Part", param);
        }

        internal void DespatchPrepareCancel(string gms_seq)
        {
            string[] param = new string[2];
            param[0] = "@gms_seq:" + gms_seq;
            param[1] = "@update_user_cd:" + Public_Function.User_cd;

            _bllSpExecute.SpExecuteString("SP_DespatchInformation", "Prepare_Cancel", param);
        }

    }
}