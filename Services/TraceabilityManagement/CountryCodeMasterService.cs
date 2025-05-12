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

    public class CountryCodeMasterService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable CountryCodeMasterSearch(string country_cd, string use_ck)
        {
            string[] param = new string[2];
            param[0] = "@country_cd_S:" + country_cd;
            param[1] = "@use_ck_S:" + use_ck;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_CountryCodeMaster", "S", param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable CountryCodeMasterSearchItem(string country_cd)
        {
            string[] param = new string[1];
            param[0] = "@country_cd:" + country_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_CountryCodeMaster", "S2", param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string CountryCodeMasterSave(string country_cd, string use_ck)
        {
            string[] param = new string[2];
            param[0] = "@country_cd:" + country_cd;
            param[1] = "@use_ck:" + use_ck;

            string result = _bllSpExecute.SpExecuteString("SP_CountryCodeMaster", "U", param);

            return result;
        }

        internal string InsertExcelDataTemp(List<CountryMaster> countryData)
        {
            string result = "";

            DbAgent myDbAgent = new DbAgent("", "", System.Data.CommandType.Text, Public_Function.selectedDB);
            // SqlConnection connection = new SqlConnection(context.GetConnStr());
            //SqlCommand myCommand;

            if (myDbAgent.MyConnection.State == ConnectionState.Closed) myDbAgent.MyConnection.Open();

            if (myDbAgent.MyConnection.State != null && myDbAgent.MyConnection.State.ToString() == "Open")
            {
                //mfds_item_standard_temp 데이터 삭제
                _bllSpExecute.SpExecuteString("SP_CountryCodeMaster", "C");

                try
                {
                    DataTable dt = new DataTable();

                    dt.Columns.Add("country_cd", typeof(String)); //국가코드
                    dt.Columns.Add("country_nm", typeof(String)); //국가명

                    for (var i = 0; i < countryData.Count; i++)
                    {
                        var row = dt.NewRow();
                        row["country_cd"] = countryData[i].country_cd;
                        row["country_nm"] = countryData[i].country_nm;
                        dt.Rows.Add(row);
                    }


                    using (SqlBulkCopy bulk = new SqlBulkCopy(myDbAgent.ConnectionString))
                    {
                        bulk.DestinationTableName = "item_standard_country_temp";
                        bulk.WriteToServer(dt);
                        dt.Clear();
                        bulk.Close();
                    }

                    //mfds_item_standard_temp에 insert한 데이터 를 mfds_item_standard에 insert시키고 mfds_item_standard_temp 데이터 삭제
                    _bllSpExecute.SpExecuteString("SP_CountryCodeMaster", "I");
                    result = "국가코드를 업로드하였습니다";

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
                result = "국가코드 업로드에 실패하였습니다";
            }

            return result;

        }
    }
}