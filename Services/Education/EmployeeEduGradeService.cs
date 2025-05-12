using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Education;

namespace HACCP.Services.Education
{
    public class EmployeeEduGradeService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable EmployeeEduGrade_Search(EmployeeEduGrade iModel)
        {
            string sSpName = "SP_EmployeeEduGrade_AOS";
            string gubun = "S1";

            string[] pParam = new string[3];
            //조회시 사용
            pParam[0] = "@title:" + iModel.title;
            pParam[1] = "@sdate:" + iModel.sdate;
            pParam[2] = "@edate:" + iModel.edate;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal DataTable EmployeeEduGrade_SearchEmp(string edu_no)
        {
            string sSpName = "SP_EmployeeEduGrade_AOS";
            string gubun = "S2";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@edu_no:" + edu_no;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal DataTable EmployeeEduGrade_SearchFile(string edu_no)
        {
            string sSpName = "SP_EmployeeEduGrade_AOS";
            string gubun = "S_File";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@edu_no:" + edu_no;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }


        internal string EmployeeEduGrade_Update(EmployeeEduGrade iModel)
        {
            string sSpName = "SP_EmployeeEduGrade_AOS";
            string gubun = "U";
            string message = "";

            string[] pParam = new string[5];
            //조회시 사용
            pParam[0] = "@edu_no:" + iModel.edu_no;
            pParam[1] = "@emp_cd:" + iModel.emp_cd;
            pParam[2] = "@attendance_yn:" + iModel.attendance_yn;
            pParam[3] = "@complete_yn:" + iModel.complete_yn;
            pParam[4] = "@evaluation:" + iModel.evaluation;

            message = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            //정상 처리 되었습니다.
            return message;
        }

        internal string EmployeeEduGrade_FileOpen(string file_id)
        {
            string sSpName = "SP_EmployeeEduGrade_AOS";
            string sGubun = "ReportOpen";
            string pParam = "@file_id:" + file_id;

            string message = "";


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);


            if (dt.Rows.Count > 0)
            {
                string fileName = "";

                //파일의 결재라인 체크 함수 사용여부를 불러온다,
                string DocumentFileStoredFolder = "";   //문서저장 폴더
                DocumentFileStoredFolder = _bllSpExecute.SpExecuteString("SP_ProgramInit", "S2", "@code:Doc_record_folder_value");

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

        internal string EmployeeEduGradeEduFinished(string edu_no)
        {
            string sSpName = "SP_EmployeeEduGrade_AOS";
            string gubun = "EduFinished";
            string message = "";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@edu_no:" + edu_no;

            message = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return message;
        }
        

        #region 전자서명


        internal bool SaveSign(string index_key, string moduleType, string signCode, string sign_set_dt_seq, string emp_cd, string validation_type, string representative_yn, string docNo, string revisionNo)
        {
            string sSpName = "SP_EmployeeEduGrade_AOS";
            string pGubun = "SignSave";

            bool check = false;

            string[] pParam = new string[9];
            pParam[0] = "@index_key:" + index_key;
            pParam[1] = "@module_type:" + moduleType;
            pParam[2] = "@sign_set_cd:" + signCode;
            pParam[3] = "@sign_set_dt_seq:" + sign_set_dt_seq;
            pParam[4] = "@sign_emp_cd:" + emp_cd;
            pParam[5] = "@sign_type:" + validation_type;
            pParam[6] = "@representative_yn:" + representative_yn;
            pParam[7] = "@doc_no:" + docNo;
            pParam[8] = "@revision_no:" + revisionNo;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            if (message.Length > 0)
            {
                check = true;
            }

            return check;
        }

        internal bool CancelSign(string index_key, string moduleType, string SignSeq, string docNo, string revisionNo)
        {
            string sSpName = "SP_EmployeeEduGrade_AOS";
            string pGubun = "SignCancel";

            if (string.IsNullOrEmpty(SignSeq))
            {
                pGubun = "SignCancelAll";
                SignSeq = "";   //null일 경우 빈스트링으로 치환
            }

            bool check = false;

            string[] pParam = new string[5];
            pParam[0] = "@index_key:" + index_key;
            pParam[1] = "@module_type:" + moduleType;
            pParam[2] = "@sign_set_dt_seq:" + SignSeq;
            pParam[3] = "@doc_no:" + docNo;
            pParam[4] = "@revision_no:" + revisionNo;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            if (message.Length > 0)
            {
                check = true;
            }

            return check;
        }

        internal DataTable SettingSign(string index_key, string moduleType, string signCode)
        {
            string sSpName = "SP_ElectronicSignature";
            string pGubun = "Setting";
            string[] pParam = new string[3];

            pParam[0] = "@index_key:" + index_key;
            pParam[1] = "@module_type:" + moduleType;
            pParam[2] = "@sign_set_cd:" + signCode;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal bool GetModifiableAuthority(string index_key, string moduleType, string emp_cd)
        {
            string sSpName = "SP_ElectronicSignature";
            string pGubun = "ModifiableSignerSearch";

            bool check = false;
            string[] param = new string[3];
            param[0] = "@index_key:" + index_key;
            param[1] = "@module_type:" + moduleType;
            param[2] = "@sign_emp_cd:" + emp_cd;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, param);

            if (message == "Y")
                check = true;

            return check;

        }


        internal bool GetRepresentativeAuthority(string emp_cd, string sign_set_dt_seq, string signCode)
        {
            string sSpName = "SP_ElectronicSignature";
            string pGubun = "RepresentativeSearch";

            bool check = false;


            string[] pParam = new string[3];
            pParam[0] = "@sign_emp_cd:" + emp_cd;
            pParam[1] = "@sign_set_dt_seq:" + sign_set_dt_seq;
            pParam[2] = "@sign_set_cd:" + signCode;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            if (message == "Y")
                check = true;

            return check;
        }

        internal DataTable SignerSearch(string index_key, string moduleType, string signCode, string signSeq)
        {
            string sSpName = "SP_EmployeeEduGrade_AOS";
            string pGubun = "SignerSearch";
            string[] pParam = new string[4];

            pParam[0] = "@index_key:" + index_key;
            pParam[1] = "@module_type:" + moduleType;
            pParam[2] = "@sign_set_cd:" + signCode;
            pParam[3] = "@sign_set_dt_seq:" + signSeq;

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

        internal DataTable GetProgramSet()
        {
            string sSpName = "SP_ProgramInit";
            string[] pParam = new string[1];
            //pParam[0] = "@code:" + Public_Function.program_id;
            pParam[0] = "@code:" + "EmployeeEduGrade";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }
        #endregion
    }
}