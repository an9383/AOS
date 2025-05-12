using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Libs.File;
using HACCP.Models.Mont;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Mont
{
    public class EquipmentCollectDataService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();


        internal DataTable selectEquipment(string equip_use_gb, string plant_cd)
        {
            string sSpName = "SP_EQUIPMENT";

            string gubun = "SC";
            string[] param = new string[2];

            param[0] = "@s_equip_use_gb:" + equip_use_gb;
            param[1] = "@PLANT_CD:" + plant_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, param);

            return dt;
        }


        internal String EquipDataCountCheck(string equip_cd, string equip_type, string s_date, string e_date, string s_time, string e_time)
        {
            string sSpName = "SP_GET_EQUIPMENT_DATA";

            string gubun = "CC";
            string[] param = new string[4];

            param[0] = "@EQUIP_CD:" + equip_cd;
            param[1] = "@EQUIP_TYPE:" + equip_type;
            param[2] = "@SDATE:" + s_date + " " + s_time + ":00";
            param[3] = "@EDATE:" + e_date + " " + e_time + ":59";

            String count = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return count;
        }


        internal DataTable EquipDataSearch(string equip_cd, string equip_type, string s_date, string e_date, string s_time, string e_time)
        {
            string sSpName = "SP_GET_EQUIPMENT_DATA";

            string gubun = "S";
            string[] param = new string[4];

            param[0] = "@EQUIP_CD:" + equip_cd;
            param[1] = "@EQUIP_TYPE:" + equip_type;
            param[2] = "@SDATE:" + s_date + " " + s_time + ":00";
            param[3] = "@EDATE:" + e_date + " " + e_time + ":59";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, param);


            return dt;

            //DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, param);
            //DataSet ds = new DataSet();

            //if (dt.Rows.Count > 0)
            //{
            //    DataTable dataTable = new DataTable();

            //    foreach (DataColumn column in dt.Columns)
            //    {
            //        dataTable.Columns.Add(column.ColumnName);
            //    }

            //    int rowLimit = 3500; // 반환 받은 row가 rowLimit보다 많을 경우 DataSet으로 변환
            //    int count = (int)(dt.Rows.Count / rowLimit) + 1;

            //    if (dt.Rows.Count > rowLimit)
            //    {
            //        for (int i = 0; i < count; i++)
            //        {
            //            for (int j = ((i) * rowLimit); j < ((i + 1) * rowLimit); j++)
            //            {
            //                if (j >= dt.Rows.Count)
            //                {
            //                    break;
            //                }

            //                DataRow dr = dt.NewRow();
            //                dr = dt.Rows[j];

            //                dataTable.Rows.Add(dr.ItemArray);
            //            }

            //            DataTable dtCopy = dataTable.Copy();
            //            dataTable.Clear();

            //            ds.Tables.Add(dtCopy);
            //        }
            //    }
            //    else
            //    {
            //        DataTable dtCopy = dt.Copy();

            //        ds.Tables.Add(dtCopy);
            //    }

            //}

            //return ds;
        }


        internal DataTable EquipKeyDataSearch(string equip_type_nm, string s_date, string e_date, string s_time, string e_time)
        {
            string sSpName = "SP_GET_EQUIPMENT_DATA";

            string gubun = "S2";
            string[] param = new string[3];

            param[0] = "@EQUIP_TYPE_NM:" + equip_type_nm;
            param[1] = "@SDATE:" + s_date + " " + s_time + ":00";
            param[2] = "@EDATE:" + e_date + " " + e_time + ":59";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, param);


            return dt;
        }

        internal string EquipDataCRUD(EquipmentCollectData edata)
        {
            string sSpName = "SP_GET_EQUIPMENT_DATA";

            string gubun = edata.gubun;

            string[] pParam = GetParam(edata);

            string msg = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);


            return msg;

        }

        internal DataTable EquipSelectATData(EquipmentCollectData edata)
        {
            string sSpName = "SP_GET_EQUIPMENT_DATA";

            string gubun = "S_AT";

            string[] pParam = GetParam(edata);

            //DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);
            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
                //dt.Columns.Remove("sign_image");
                dt.Columns.Add("_sign_image", typeof(String));
                //dt.Columns.Remove("confirmer_sign_image");
                dt.Columns.Add("_confirmer_sign_image", typeof(String));

            }


            foreach (DataRow row in dt.AsEnumerable())
            {
                if (row["sign_image"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["sign_image"]);
                    string url = "data:Image/png;base64," + str;

                    row["_sign_image"] = url;
                }
                else if (row["signer"] != null && row["signer"].ToString() != "")
                {
                    string url = "/Content/image/defaultSign.png";
                    row["_sign_image"] = url;
                }
                else
                {
                    string url = "/Content/image/empty_logo.jpg";
                    row["_sign_image"] = url;
                }

                if (row["confirmer_sign_image"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["confirmer_sign_image"]);
                    string url = "data:Image/png;base64," + str;

                    row["_confirmer_sign_image"] = url;
                }
                else if(row["confirmer"] != null && row["confirmer"].ToString() != "")
                {
                    string url = "/Content/image/defaultSign.png";
                    row["_confirmer_sign_image"] = url;
                }
                else
                {
                    string url = "/Content/image/empty_logo.jpg";
                    row["_confirmer_sign_image"] = url;
                }
            }

            return dt;

        }


        private string[] GetParam(EquipmentCollectData edata)
        {
            string[] param = new string[52];

            param[0]  = "@equip_cd:" + edata.equip_cd;
            param[1]  = "@workroom_cd:" + edata.workroom_cd;
            param[2]  = "@equip_type:" + edata.equip_type;
                      
            param[3]  = "@CODE:" + edata.CODE;
            param[4]  = "@IDX:" + edata.IDX;
            param[5]  = "@CNT_OK:" + edata.CNT_OK;
            param[6]  = "@CNT_NG:" + edata.CNT_NG;
            param[7]  = "@CNT_OK_APD:" + edata.CNT_OK_APD;
            param[8]  = "@CNT_NG_APD:" + edata.CNT_NG_APD;
            param[9]  = "@START_DT:" + edata.START_DT;
            param[10] = "@STOP_DT:" + edata.STOP_DT;
            param[11] = "@I_DATE:" + edata.I_DATE;
            param[12] = "@O_DATE:" + edata.O_DATE;
            param[13] = "@SENSITIVITY:" + edata.SENSITIVITY;
            param[14] = "@PROPERTY:" + edata.PROPERTY;
            param[15] = "@RESULT:" + edata.RESULT;
            param[16] = "@CONFIRMED:" + edata.CONFIRMED;

            param[17] = "@DATA1:" + edata.DATA1;
            param[18] = "@DATA2:" + edata.DATA2;
            param[19] = "@DATA3:" + edata.DATA3;
            param[20] = "@DATA4:" + edata.DATA4;
            param[21] = "@DATA5:" + edata.DATA5;
            param[22] = "@CONFIRM_TIME:" + edata.confirm_time;

            param[23] = "@WORK_GU:" + edata.WORK_GU;
            param[24] = "@WORK_RST:" + edata.WORK_RST;
            param[25] = "@FILE_IDX:" + edata.FILE_IDX;
            param[26] = "@CONFIRMED_NM:" + edata.CONFIRMED_NM;
            param[27] = "@WORK_ORDER_NO:" + edata.WORK_ORDER_NO;

            param[28] = "@SPEED:" + edata.SPEED;
            param[29] = "@ALARM_CODE:" + edata.ALARM_CODE;

            param[30] = "@CNT_H:" + edata.CNT_H;
            param[31] = "@CNT_N:" + edata.CNT_N;
            param[32] = "@CNT_L:" + edata.CNT_L;
            param[33] = "@CNT_TOT:" + edata.CNT_TOT;

            param[34] = "@TEMPERATURE:" + edata.TEMPERATURE;
            param[35] = "@HUMIDITY:" + edata.HUMIDITY;
            param[36] = "@PRESSURE:" + edata.PRESSURE;

            param[37] = "@WEIGHT:" + edata.WEIGHT;

            param[38] = "@LITER:" + edata.LITER;
            param[39] = "@LITER_APD:" + edata.LITER_APD;
            param[40] = "@LITER_SPEED:" + edata.LITER_SPEED;

            param[41] = "@TEMP_HOT:" + edata.TEMP_HOT;
            param[42] = "@TEMP_COLD:" + edata.TEMP_COLD;
            param[43] = "@TEMP_DIVISION:" + edata.TEMP_DIVISION;

            param[44] = "@STD_TEMP_HOT:" + edata.STD_TEMP_HOT;
            param[45] = "@STD_TEMP_COLD:" + edata.STD_TEMP_COLD;
            param[46] = "@STD_TEMP_DIVISION:" + edata.STD_TEMP_DIVISION;

            param[47] = "@TEMP:" + edata.TEMP;
            param[48] = "@TEMP2:" + edata.TEMP2;
            param[49] = "@DATA_TYPE:" + edata.DATA_TYPE;

            param[50] = "@audit_remark:" + edata.audit_remark;
            param[51] = "@OWD_audittrail_id:" + edata.OWD_audittrail_id;
            

            return param;
        }
    }
}