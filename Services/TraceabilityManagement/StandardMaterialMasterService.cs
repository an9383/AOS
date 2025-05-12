using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.TraceabilityManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HACCP.Services.TraceabilityManagement
{
    public class StandardMaterialMasterService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable StandardMaterialMasterSearch(string standard_cd, string use_ck)
        {
            string[] param = new string[2];
            param[0] = "@standard_cd_S:" + standard_cd;
            param[1] = "@use_ck_S:" + use_ck;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_StandardMaterialMaster", "S", param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }
        
        internal DataTable StandardMaterialMasterSearchItem(string standard_cd)
        {
            string[] param = new string[1];
            param[0] = "@standard_cd:" + standard_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_StandardMaterialMaster", "S2", param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string StandardMaterialMasterSave(string standard_cd, string use_ck)
        {
            string[] param = new string[2];
            param[0] = "@standard_cd:" + standard_cd;
            param[1] = "@use_ck:" + use_ck;

            string result = _bllSpExecute.SpExecuteString("SP_StandardMaterialMaster", "U", param);

            return result;
        }

        internal string InsertExcelDataTemp(List<StandardMaterialMaster> standardData)
        {
            string result = "";

            DbAgent myDbAgent = new DbAgent("", "", System.Data.CommandType.Text, Public_Function.selectedDB);
            // SqlConnection connection = new SqlConnection(context.GetConnStr());
            //SqlCommand myCommand;

            if (myDbAgent.MyConnection.State == ConnectionState.Closed) myDbAgent.MyConnection.Open();

            if (myDbAgent.MyConnection.State != null && myDbAgent.MyConnection.State.ToString() == "Open")
            {
                //mfds_item_standard_temp 데이터 삭제
                _bllSpExecute.SpExecuteString("SP_StandardMaterialMaster", "C");

                try
                {
                    DataTable dt = new DataTable();

                    dt.Columns.Add("std_material_cd", typeof(String)); //표준원료코드
                    dt.Columns.Add("std_material_nm", typeof(String)); //표준원료명
                    dt.Columns.Add("ingr_eng_name", typeof(String));   //원료영문명

                    for (var i = 0; i < standardData.Count; i++)
                    {
                        var row = dt.NewRow();
                        row["std_material_cd"] = standardData[i].std_material_cd;
                        row["std_material_nm"] = standardData[i].std_material_nm;
                        row["ingr_eng_name"] = standardData[i].ingr_eng_name;
                        dt.Rows.Add(row);
                    }


                    using (SqlBulkCopy bulk = new SqlBulkCopy(myDbAgent.ConnectionString))
                    {
                        bulk.DestinationTableName = "mfds_item_standard_temp";
                        bulk.WriteToServer(dt);
                        dt.Clear();
                        bulk.Close();
                    }

                    //mfds_item_standard_temp에 insert한 데이터 를 mfds_item_standard에 insert시키고 mfds_item_standard_temp 데이터 삭제
                    _bllSpExecute.SpExecuteString("SP_StandardMaterialMaster", "I");
                    result = "원료코드를 업로드하였습니다";

                }
                catch (Exception ex)
                {
                    result = "업로드 중 오류가 발생하였습니다\n 에러메세지:" + ex.Message;
                    myDbAgent.MyConnection.Close();
                }
                finally
                {
                    myDbAgent.MyConnection.Close();
                    myDbAgent.MyConnection.Dispose();
                }
            }
            else
            {
                result = "원료코드 업로드에 실패하였습니다";
            }

            return result;

        }
    }
}