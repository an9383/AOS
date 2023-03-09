using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Change;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace HACCP.Services.Change
{
    public class ChangeControlRequestService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectChangeRequest(ChangeRequest.SrchParam param)
        {
            string sSpName = "SP_ChangeRequest";

            string[] pParam = new string[6];
            pParam[0] = "@de_Sdate:" + param.sdate;
            pParam[1] = "@de_Edate:" + param.edate;
            pParam[2] = "@changecontrol_cd:" + param.changecontrol_cd;
            pParam[3] = "@changecontrol_status:" + param.changecontrol_status;
            pParam[4] = "@request_emp_cd:" + param.request_emp_cd;
            pParam[5] = "@request_app_emp_cd:" + param.request_app_emp_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataSet ChangeControlRequestSelectDetail(string changecontrol_no, string sign_set_cd)
        {
            string sSpName = "SP_ChangeRequest";

            string[] pParam = new string[1];
            pParam[0] = "@changecontrol_no:" + changecontrol_no;

            DataSet tempDs = new DataSet();

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S4", pParam);
            if (ds != null && ds.Tables[0].Rows.Count >= 0)
            {
                DataTable tempDt = ds.Tables[0].Copy();
                tempDt.TableName = "tempDocDt";
                tempDs.Tables.Add(tempDt);
            }

            pParam = new string[2];
            pParam[0] = "@changecontrol_no:" + changecontrol_no;
            pParam[1] = "@sign_set_cd:" + sign_set_cd;

            ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);
            if (ds != null && ds.Tables[0].Rows.Count >= 0)
            {
                DataTable dt = new DataTable();

                if (ds != null)
                {
                    dt = ds.Tables[0];
                }

                DataTable tempDt = new DataTable();
                tempDt.TableName = "tempSignDt";
                tempDt.Columns.Add("sign_set_dt_id", typeof(String));
                tempDt.Columns.Add("displayfield", typeof(String));
                tempDt.Columns.Add("sign_yn", typeof(String));
                tempDt.Columns.Add("sign_time", typeof(String));
                tempDt.Columns.Add("sign_emp_cd", typeof(String));
                tempDt.Columns.Add("responsible_emp_cd", typeof(String));
                tempDt.Columns.Add("sign_image", typeof(String));
                tempDt.Columns.Add("responsible_emp_nm", typeof(String));
                tempDt.Columns.Add("sign_emp_nm", typeof(String));
                tempDt.Columns.Add("prev_sign_yn", typeof(String));
                tempDt.Columns.Add("next_sign_yn", typeof(String));
                tempDt.Columns.Add("sign_set_dt_seq", typeof(String));

                foreach (DataRow row in dt.AsEnumerable())
                {
                    DataRow _row = tempDt.NewRow();

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

                    tempDt.Rows.Add(_row);
                }

                tempDs.Tables.Add(tempDt);
            }

            return tempDs;
        }

        internal DataTable AttachmentFileDownload(string file_id)
        {
            string sSpName = "SP_ChangeRequest";
            string sGubun = "S5";
            string pParam = "@doc_file_id:" + file_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal ChangeRequest SetChangeControlNo(ChangeRequest changeRequest)
        {
            string sSpName = "SP_ChangeRequest";

            string[] pParam = new string[1];
            pParam[0] = "@request_date:" + changeRequest.request_date;

            string changecontrol_no = _bllSpExecute.SpExecuteString(sSpName, "S1", pParam);

            changeRequest.changecontrol_no = changecontrol_no;

            return changeRequest;
        }

        internal string ChangeControlRequestTRX(ChangeRequest changeRequest)
        {
            string sSpName = "SP_ChangeRequest";
            string res = "";

            if (changeRequest.gubun.Equals("I") || changeRequest.gubun.Equals("U"))
            {
                string[] pParam = new string[12];
                pParam[0] = "@changecontrol_no:" + changeRequest.changecontrol_no;
                pParam[1] = "@request_date:" + changeRequest.request_date;
                pParam[2] = "@request_emp_cd:" + changeRequest.request_emp_cd;
                pParam[3] = "@request_contents:" + changeRequest.request_contents;
                pParam[4] = "@change_evidence:" + changeRequest.change_evidence;
                pParam[5] = "@temp_change_yn:" + changeRequest.temp_change_yn;
                pParam[6] = "@temp_change_limit:" + changeRequest.temp_change_limit;
                pParam[7] = "@changecontrol_cd:" + changeRequest.changecontrol_cd;
                pParam[8] = "@change_title:" + changeRequest.change_title;
                pParam[9] = "@change_date:" + changeRequest.change_date;
                pParam[10] = "@request_gubun:" + changeRequest.request_gubun;
                pParam[11] = "@request_special:" + changeRequest.request_special;

                res = _bllSpExecute.SpExecuteString(sSpName, changeRequest.gubun, pParam);
            }else if (changeRequest.gubun.Equals("D"))
            {
                string[] pParam = new string[1];
                pParam[0] = "@changecontrol_no:" + changeRequest.changecontrol_no;

                res = _bllSpExecute.SpExecuteString(sSpName, changeRequest.gubun, pParam);
            }

            return res;
        }

        internal void uploadFile(byte[] myBytes, string fileName, string contentType, string changecontrol_no)
        {
            var doc_type = "";

            if (contentType.Equals("application/haansofthwp"))
            {
                doc_type = ".hwp";
            }
            else if (contentType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
            {
                doc_type = ".docx";
            }
            else if (contentType.Equals("application/msword"))
            {
                doc_type = ".doc";
            }

            string sSpName = "SP_ChangeRequest";

            string[] pParam = new string[4];
            pParam[0] = "@doc_name:" + fileName;
            pParam[1] = "@doc_type:" + doc_type;
            pParam[2] = "@changecontrol_no:" + changecontrol_no;
            pParam[3] = "@request_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();

            string message = _bllSpExecute.SpExecuteString(sSpName, "IF", "@file_image", myBytes, pParam);

        }

        internal string ChangeControlRequestFileOpen(string file_id)
        {
            string sSpName = "SP_ChangeRequest";
            string sGubun = "S5";
            string pParam = "@doc_file_id:" + file_id;

            string message = "";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            if (dt.Rows.Count > 0)
            {
                string fileName = "";

                //파일의 결재라인 체크 함수 사용여부를 불러온다,
                string DocumentFileStoredFolder = "";   //문서저장 폴더
                DocumentFileStoredFolder = _bllSpExecute.SpExecuteString("SP_ProgramInit", "S2", "@code:Doc_record_folder_value"); ;

                //파일관리 폴더가 존재하지 않는경우에는 생성한다.
                if (Directory.Exists(DocumentFileStoredFolder) == false)
                {
                    Directory.CreateDirectory(DocumentFileStoredFolder);
                }

                //이미지 정보를 폴더에 파일로 만든다.
                fileName = dt.Rows[0]["doc_name"].ToString();
                //파일이 존재하면 삭제하도록 한다.
                if (File.Exists(@DocumentFileStoredFolder + "\\" + fileName))
                {
                    File.Delete(@DocumentFileStoredFolder + "\\" + fileName);
                }

                byte[] bs;
                System.IO.FileStream fs = new FileStream(@DocumentFileStoredFolder + "\\" + fileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
                bs = (byte[])dt.Rows[0]["file_image"];
                System.IO.BinaryWriter bw;
                bw = new System.IO.BinaryWriter(fs);
                bw.Write(bs);
                bw.Close();
                fs.Close();

                message = "success";

                Process.Start(@DocumentFileStoredFolder + "\\" + fileName);
            }
            else
            {
                message = "fail";
            }

            return message;
        }

        internal string ChangeControlRequestSignTRX(ChangeRequest changeRequest)
        {
            string res = "";

            if (changeRequest.gubun.Equals("U"))
            {
                string sSpName = "SP_ChangeRequest";

                string[] pParam = new string[4];
                pParam[0] = "@sign_type:" + changeRequest.sign_type;
                pParam[1] = "@request_app_emp_cd:" + changeRequest.request_app_emp_cd;
                pParam[2] = "@request_app_emp_type:" + changeRequest.request_app_emp_type;
                pParam[3] = "@changecontrol_no:" + changeRequest.changecontrol_no;

                res = _bllSpExecute.SpExecuteString(sSpName, "ES", pParam);

            }
            else if (changeRequest.gubun.Equals("D"))
            {
                string sSpName = "SP_ChangeRequest";

                string[] pParam = new string[2];
                pParam[0] = "@sign_type:" + changeRequest.sign_type;
                pParam[1] = "@changecontrol_no:" + changeRequest.changecontrol_no;

                res = _bllSpExecute.SpExecuteString(sSpName, "ESD", pParam);
            }

            return res;
        }

        internal string ChangeControlRequestSignReciept(string changecontrol_no)
        {
            string sSpName = "SP_ChangeRequest";

            string[] pParam = new string[1];
            pParam[0] = "@changecontrol_no:" + changecontrol_no;

            string res = _bllSpExecute.SpExecuteString(sSpName, "NC", pParam);

            return res;
        }

        internal string ChangeControlDeleteDoc(string changecontrol_no, string changecontrol_attatch_doc_id)
        {
            string sSpName = "SP_ChangeRequest";

            string[] pParam = new string[2];
            pParam[0] = "@changecontrol_no:" + changecontrol_no;
            pParam[1] = "@changecontrol_attatch_doc_id:" + changecontrol_attatch_doc_id;

            string res = _bllSpExecute.SpExecuteString(sSpName, "DD", pParam);

            return res;
        }

    }
}