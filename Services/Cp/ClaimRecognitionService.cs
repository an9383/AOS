using HACCP.Libs.Database;
using HACCP.Models.Cp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace HACCP.Services.Cp
{
    public class ClaimRecognitionService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        //처리 결과 승인 리스트 조회
        public List<ClaimRecognition> GridSelect(ClaimRecognition.SrchParam srch)
        {
            string sSpName = "SP_ClaimRecognition_Y";

            string[] pParam = new String[6];
            pParam[0] = "@select_S:" + srch.select_S;
            pParam[1] = "@Sdate_S:" + srch.Sdate_S;
            pParam[2] = "@Edate_S:" + srch.Edate_S;
            pParam[3] = "@select_gubun_S:" + srch.select_gubun_S;
            pParam[4] = "@searchtext_S:" + srch.searchtext_S;
            pParam[5] = "@claim_status_S:" + srch.claim_status_S;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S", pParam);

            List<ClaimRecognition> claimRecognitionList = new List<ClaimRecognition>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                ClaimRecognition claimRecognition = new ClaimRecognition(row);
                claimRecognitionList.Add(claimRecognition);
            }

            return claimRecognitionList;
        }

        //처리 결과 승인 세부사항 조회
        public DataTable GridSelect1(string claim_id)
        {
            string sSpName = "SP_ClaimRecognition_Y";
            string[] pParam = new string[1];
            pParam[0] = "@claim_id:" + claim_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S1", pParam);
            return dt;
        }

        #region 전자서명

        //삭제시 서명자조회검사
        public string GetRepresentativeAuthority(string sign_history_id)
        {
            string sSpName = "SP_ClaimRecognition_Y";

            string[] pParam = new string[1];
            pParam[0] = "@sign_history_id:" + sign_history_id;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, "EZ", pParam);

            return message;
        }

        //전자서명부분 삭제
        public string DeleteSign(string claim_id, string delgubun)
        {
            string sSpName = "SP_ClaimRecognition_Y";

            string[] pParam = new string[2];
            pParam[0] = "@claim_id:" + claim_id;
            pParam[1] = "@delgubun:" + delgubun;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, "DE", pParam);

            return message;
        }

        //대리자 권한조회
        public string GetRepresentativeYN(string emp_cd, string sign_set_dt_seq)
        {
            string sSpName = "SP_ClaimRecognition_Y";

            string[] pParam = new string[2];
            pParam[0] = "@emp_cd:" + emp_cd;
            pParam[1] = "@sign_set_dt_seq:" + sign_set_dt_seq;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "ER", pParam);

            string result = "";

            for (int i = 0; i < dt.Rows[0].ItemArray.Length; i++)
            {
                if (emp_cd.Equals(dt.Rows[0].ItemArray[i]))
                {
                    result = dt.Rows[0].ItemArray[i].ToString();
                }
            }


            return result;
        }

        //전자서명 자료 저장
        public string InsSignInfo(string sign_emp_cd, string sign_type, string representative_yn, string sign_set_dt_seq, string claim_id, string remark)
        {
            string sSpName = "SP_ClaimRecognition_Y";

            string[] pParam = new string[6];
            pParam[0] = "@sign_emp_cd:" + sign_emp_cd;
            pParam[1] = "@sign_type:" + sign_type;
            pParam[2] = "@representative_yn:" + representative_yn;
            pParam[3] = "@sign_set_dt_seq:" + sign_set_dt_seq;
            pParam[4] = "@claim_id:" + claim_id;
            pParam[5] = "@remark:" + remark;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, "Z", pParam);

            return message;
        }

        //전자서명 lookup 데이터
        public DataTable LookupSetting(string claim_id)
        {
            string sSpName = "SP_ClaimRecognition_Y";
            string[] pParam = new string[1];
            pParam[0] = "@claim_id:" + claim_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "ES", pParam);
            return dt;
        }

        //전자서명 조회
        public DataTable ElecSearchData(string sign_history_id)
        {
            string sSpName = "SP_ClaimRecognition_Y";
            string[] pParam = new string[1];
            pParam[0] = "@sign_history_id:" + sign_history_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "ED", pParam);
            DataTable dt_1 = new DataTable();

            if (dt.Rows.Count > 0)
            {
                dt_1.Columns.Add("sign_emp_nm", typeof(String));
                dt_1.Columns.Add("sign_representative_yn", typeof(String));
                dt_1.Columns.Add("sign_time", typeof(String));
                dt_1.Columns.Add("emp_sign", typeof(String));

                foreach (DataRow row in dt.AsEnumerable())
                {
                    DataRow _row = dt_1.NewRow();
                    _row["sign_emp_nm"] = row["sign_emp_nm"].ToString();
                    _row["sign_representative_yn"] = row["sign_representative_yn"].ToString();
                    _row["sign_time"] = row["sign_time"].ToString();
                    _row["emp_sign"] = row["emp_sign"].ToString();

                    if (row["emp_sign"] != System.DBNull.Value)
                    {
                        string str = Convert.ToBase64String((byte[])row["emp_sign"]);
                        string url = "data:Image/png;base64," + str;
                        _row["emp_sign"] = url;
                    }
                    dt_1.Rows.Add(_row);
                }

            }
            return dt_1;
        }

        public DataTable ClaimRecognitionESInfo(string claim_id, string sign_set_cd)
        {
            string sSpName = "SP_ClaimRecognition_Y";
            string sGubun = "ES";
            string[] pParam = new string[2];
            pParam[0] = "@claim_id:" + claim_id;
            pParam[1] = "@sign_set_cd:" + sign_set_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable table = new DataTable();

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

        #endregion


        //리포트
        public void PreviewOrPrint(string claim_id, bool check)
        {
            string sSpName = "SP_ClaimRecognition_Y";

            string[] pParam = new string[1];
            pParam[0] = "@claim_id:" + claim_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "RP", pParam);
            dt.TableName = "ClaimReportDB";

        }

        //파일 조회
        internal DataTable GetFile(string doc_file_id)
        {

            string sSpName = "SP_ClaimRecognition_Y";
            string sGubun = "FS";
            string pParam = "@doc_file_id:" + doc_file_id;
     
            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);
            return dt;

            //string message = "";
            //if (dt.Rows.Count > 0)
            //{
            //    string fileName = "";

            //    //파일의 결재라인 체크 함수 사용여부를 불러온다,
            //    string DocumentFileStoredFolder = "";   //문서저장 폴더
            //    DocumentFileStoredFolder = _bllSpExecute.SpExecuteString("SP_ProgramInit", "S2", "@code:Doc_record_folder_value"); ;

            //    //파일관리 폴더가 존재하지 않는경우에는 생성한다.
            //    if (Directory.Exists(DocumentFileStoredFolder) == false)
            //    {
            //        Directory.CreateDirectory(DocumentFileStoredFolder);
            //    }

            //    //이미지 정보를 폴더에 파일로 만든다.
            //    fileName = dt.Rows[0]["doc_name"].ToString() + dt.Rows[0]["file_extension_name"].ToString();
            //    //파일이 존재하면 삭제하도록 한다.
            //    if (File.Exists(@DocumentFileStoredFolder + "\\" + fileName))
            //    {
            //        File.Delete(@DocumentFileStoredFolder + "\\" + fileName);
            //    }

            //    byte[] bs;
            //    System.IO.FileStream fs = new FileStream(@DocumentFileStoredFolder + "\\" + fileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            //    bs = (byte[])dt.Rows[0]["file_image"];
            //    System.IO.BinaryWriter bw;
            //    bw = new System.IO.BinaryWriter(fs);
            //    bw.Write(bs);
            //    bw.Close();
            //    fs.Close();

            //    message = "success";

            //    Process.Start(@DocumentFileStoredFolder + "\\" + fileName);
            //}
            //else
            //{
            //    message = "fail";
            //}

            //return message;

        }


        public string GetProgramSet()
        {
            string sSpName = "SP_ProgramInit";

            string[] pParam = new string[1];
            pParam[0] = "@code:" + "IsLoginUserCheckAtElectronicSign";

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "S2", pParam);
           
            return result;
        }
    }
}