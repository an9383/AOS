using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Mont;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Mont
{
    public class HACCP_ManagementScreenService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable getHaccpHeaderDoc(string use_yn, string FinalConfirm)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getHaccpHeaderDoc";
            string[] pParam = new string[2];

            pParam[0] = "@use_yn:" + "Y";
            pParam[1] = "@FinalConfirm:" + "Y";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getFileList(string use_yn, string FinalConfirm)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getFileList";
            string[] pParam = new string[2];

            pParam[0] = "@use_yn:" + use_yn;
            pParam[1] = "@FinalConfirm:" + FinalConfirm;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getFileListDetail(string sdate, string edate, string doc_cd)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getFileListDetail";
            string[] pParam = new string[3];

            pParam[0] = "@SDate:" + sdate;
            pParam[1] = "@EDate:" + edate;
            pParam[2] = "@doc_no:" + doc_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }
        


        internal DataTable getHaccpBaseItem(HACCP_ManagementScreen hACCP_ManagementScreen)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getHaccpBaseItem";
            string[] pParam = new string[2];
            
            pParam[0] = "@HaccpCode:" + hACCP_ManagementScreen.HaccpCode;
            pParam[1] = "@ChgSerNo:" + hACCP_ManagementScreen.ChgSerNo;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getReportBaseItem(HACCP_ManagementScreen hACCP_ManagementScreen)
        {
            string sSpName = "SP_GET_REPORT_DATA";
            string gubun = "getHaccpBaseItem";
            string[] pParam = new string[2];

            pParam[0] = "@HaccpCode:" + hACCP_ManagementScreen.HaccpCode;
            pParam[1] = "@ChgSerNo:" + hACCP_ManagementScreen.ChgSerNo;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }


        internal DataTable getCCP_Type(HACCP_ManagementScreen hACCP_ManagementScreen)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getCCP_Type";
            string[] pParam = new string[1];

            pParam[0] = "@ccp_cd:" + hACCP_ManagementScreen.ccp_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);
            return dt;
        }

        internal DataTable getCCP_Order(HACCP_ManagementScreen hACCP_ManagementScreen)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getCCP_Order";
            string[] pParam = new string[4];

            pParam[0] = "@process_cd:" + hACCP_ManagementScreen.process_cd;
            pParam[1] = "@CCP_sdate:" + hACCP_ManagementScreen.CCP_sdate;
            pParam[2] = "@CCP_edate:" + hACCP_ManagementScreen.CCP_edate;
            pParam[3] = "@CCP_item_cd:" + hACCP_ManagementScreen.CCP_item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;

        }

        internal List<string> getCCP_preview(HACCP_ManagementScreen hACCP_ManagementScreen, string CCP_gubun)
        {
            string sSpName = "SP_CCP_Monitoring";
            string gubun = CCP_gubun;
            string[] pParam = new string[7];

            pParam[0] = "@order_no:" + hACCP_ManagementScreen.order_no;
            pParam[1] = "@process_cd:" + hACCP_ManagementScreen.process_cd;
            pParam[2] = "@item_cd:" + hACCP_ManagementScreen.item_cd;
            pParam[3] = "@lot_no:" + hACCP_ManagementScreen.lot_no;
            pParam[4] = "@HaccpCode:" + hACCP_ManagementScreen.HaccpCode;
            pParam[5] = "@index_key:" + hACCP_ManagementScreen.index_key;
            pParam[6] = "@sign_set_cd:" + hACCP_ManagementScreen.sign_set_cd;
            

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            if (ds == null)
            {
                return null;
            }

            List<string> jsonList = new List<string>();

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                string jsonStr = Public_Function.DataTableToJSON(ds.Tables[i]);

                if (jsonStr.Length > 0)
                {
                    //DataTable dt = ds.Tables[i];

                    //if (dt.Columns.IndexOf("signer_sign") > 0) //서명이 있는 일탈 테이블
                    //{
                    //    dt.Columns.Add("_signer_sign", typeof(String));
                    //    dt.Columns.Add("_confirmer_sign", typeof(String));

                    //    foreach (DataRow row in dt.AsEnumerable())
                    //    {
                    //        if (row["signer_sign"] != System.DBNull.Value)
                    //        {
                    //            string str = Convert.ToBase64String((byte[])row["signer_sign"]);
                    //            string url = "data:Image/png;base64," + str;

                    //            row["_signer_sign"] = url;
                    //        }
                    //        else if (row["signer"] != null && row["signer"].ToString() != "")
                    //        {
                    //            string url = "/Content/image/defaultSign.png";
                    //            row["_signer_sign"] = url;
                    //        }
                    //        else
                    //        {
                    //            string url = "/Content/image/empty_logo.jpg";
                    //            row["_signer_sign"] = url;
                    //        }

                    //        if (row["confirmer_sign"] != System.DBNull.Value)
                    //        {
                    //            string str = Convert.ToBase64String((byte[])row["confirmer_sign"]);
                    //            string url = "data:Image/png;base64," + str;

                    //            row["_confirmer_sign"] = url;
                    //        }
                    //        else if (row["confirmer"] != null && row["confirmer"].ToString() != "")
                    //        {
                    //            string url = "/Content/image/defaultSign.png";
                    //            row["_confirmer_sign"] = url;
                    //        }
                    //        else
                    //        {
                    //            string url = "/Content/image/empty_logo.jpg";
                    //            row["_confirmer_sign"] = url;
                    //        }
                    //    }
                    //}

                    //일탈이 너무 많으면 서명정보 때문에 데이터 자체가 안나와 화면에서는 보이지 않도록 수정 (레포트에서만 나옴)
                    DataTable dt = ds.Tables[i];

                    if (dt.Columns.IndexOf("signer_sign") > 0) //서명이 있는 일탈 테이블
                    {
                        foreach (DataRow row in dt.AsEnumerable())
                        {
                            row["signer_sign"] = null;
                            row["confirmer_sign"] = null;
                        }
                    }
                    jsonStr = Public_Function.DataTableToJSON(dt);
                    jsonList.Add(jsonStr);
                }
                else if (jsonStr.Length <= 0)
                {
                    jsonList.Add("{ \"data\" : \"empty\" }");
                }
                
            }

            return jsonList;
        }

        internal DataTable CCP_SignerSearch(string indexKey, string sign_set_cd)
        {            
            string sSpName = "SP_ImHaccpCodeMaster";
            string pGubun = "CCP_SignerSearch";
            string[] pParam = new string[2];

            pParam[0] = "@index_key:" + indexKey;
            pParam[1] = "@sign_set_cd:" + sign_set_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);
            
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables[0].Rows.Count >= 0)
            {
                dt = ds.Tables[0];
            }

            DataTable table = new DataTable();

            table.TableName = "tempSignDt";
            table.Columns.Add("sign_set_dt_id", typeof(String));
            table.Columns.Add("displayfield", typeof(String));
            table.Columns.Add("sign_yn", typeof(String));
            table.Columns.Add("sign_time", typeof(String));
            table.Columns.Add("sign_emp_cd", typeof(String));
            table.Columns.Add("responsible_emp_cd", typeof(String));
            table.Columns.Add("sign_image", typeof(String));
            table.Columns.Add("responsible_emp_nm", typeof(String));
            table.Columns.Add("sign_emp_nm", typeof(String));
            table.Columns.Add("prev_sign_yn", typeof(String));
            table.Columns.Add("next_sign_yn", typeof(String));
            table.Columns.Add("sign_set_dt_seq", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                _row["sign_set_dt_id"] = row["sign_set_dt_id"].ToString();
                _row["displayfield"] = row["displayfield"].ToString();
                _row["sign_yn"] = row["sign_yn"].ToString();
                _row["sign_time"] = row["sign_time"].ToString();
                _row["sign_emp_cd"] = row["sign_emp_cd"].ToString();
                _row["responsible_emp_cd"] = row["responsible_emp_cd"].ToString();

                if (row["sign_image"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["sign_image"]);
                    string url = "data:Image/png;base64," + str;
                    _row["sign_image"] = url;
                }
                else
                {
                    _row["sign_image"] = "/Content/image/defaultSign.png";
                }

                _row["responsible_emp_nm"] = row["responsible_emp_nm"].ToString();
                _row["sign_emp_nm"] = row["sign_emp_nm"].ToString();
                _row["prev_sign_yn"] = row["prev_sign_yn"].ToString();
                _row["next_sign_yn"] = row["next_sign_yn"].ToString();
                _row["sign_set_dt_seq"] = row["sign_set_dt_seq"].ToString();

                table.Rows.Add(_row);
            }



            return table;
        }

        internal bool CCP_format_SignSave(string sign_emp_cd, string sign_type, string representative_yn, string index_key, string sign_set_cd, string sign_set_dt_seq)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string pGubun = "CCP_format_SignSave";

            bool check = false;

            string[] pParam = new string[6];

            pParam[0] = "@sign_emp_cd:" + sign_emp_cd;
            pParam[1] = "@sign_type:" + sign_type;
            pParam[2] = "@representative_yn:" + representative_yn;
            pParam[3] = "@index_key:" + index_key;
            pParam[4] = "@sign_set_cd:" + sign_set_cd;
            pParam[5] = "@sign_set_dt_seq:" + sign_set_dt_seq;
            
            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            if (message.Length > 0)
            {
                check = true;
            }

            return check;
        }
        
        internal bool CCP_format_SignCancel(string index_key, string sign_set_dt_seq, string sign_set_cd)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string pGubun = "CCP_format_SignCancel";

            if (string.IsNullOrEmpty(sign_set_dt_seq))
            {
                pGubun = "SignCancelAll";
                sign_set_dt_seq = "";   //null일 경우 빈스트링으로 치환
            }

            bool check = false;

            string[] pParam = new string[3];

            pParam[0] = "@index_key:" + index_key;
            pParam[1] = "@sign_set_dt_seq:" + sign_set_dt_seq;
            pParam[2] = "@sign_set_cd:" + sign_set_cd;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            if (message.Length > 0)
            {
                check = true;
            }

            return check;
        }
    }
}