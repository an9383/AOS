using HACCP.Libs;
using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HACCP.Models.Doc;
using System.IO;
using System.Diagnostics;
using System.Web;

namespace HACCP.Services.Doc
{
    public class GmpDocManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        private string sSpName = "SP_Gmp_doc_manage";

        internal DataTable Search(string doc_nm_S, string search_gubun_S, string search_select, string s_gubun_S)
        {
            string pGubun = "S";
            string[] pParam = new string[5];

            pParam[0] = "@doc_nm_S:" + doc_nm_S;
            pParam[1] = "@search_gubun_S:" + search_gubun_S;
            pParam[2] = "@search_select:" + search_select;//탭 구분
            pParam[3] = "@login_nm_S:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[4] = "@s_gubun_S:" + s_gubun_S;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, pGubun, pParam);

            return dt;
        }

        internal DataTable GetLookupData(string doc_no)
        {
            string pGubun = "S1";
            string[] pParam = new string[1];

            pParam[0] = "@doc_no:" + doc_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, pGubun, pParam);

            return dt;
        }

        internal DataTable GetRevisionData(string doc_no, string revision_no)
        {
            string pGubun = "S2";
            string[] pParam = new string[2];

            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@revision_no:" + revision_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, pGubun, pParam);

            return dt;
        }

        internal DataTable GetOverPersonInChargeEmpCd(string sign_set_cd)
        {
            string pGubun = "GetOverPersonInChargeEmpCd";
            string[] pParam = new string[1];

            pParam[0] = "@sign_set_cd:" + sign_set_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, pGubun, pParam);

            return dt;
        }

        internal DataTable GridSelect1(string doc_no, string revision_no)
        {
            string pGubun = "S3";
            string[] pParam = new string[2];

            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@revision_no:" + revision_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, pGubun, pParam);

            return dt;
        }

        internal DataTable GridSelect2(string doc_no, string revision_no, string dist_id)
        {
            string pGubun = "S4";
            string[] pParam = new string[3];

            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@revision_no:" + revision_no;
            pParam[2] = "@dist_id:" + dist_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, pGubun, pParam);

            return dt;
        }

        internal bool GridDelete(string doc_no, string revision_no, string dist_id)
        {
            string pGubun = "D";

            bool check = false;

            string[] pParam = new string[3];
            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@revision_no:" + revision_no;
            pParam[2] = "@dist_id:" + dist_id;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            if (message.Length > 0)
            {
                check = true;
            }

            return check;
        }
        internal bool GridDelete_1(string doc_no, string revision_no, string dist_id, string disuse_id)
        {
            string pGubun = "D1";

            bool check = false;

            string[] pParam = new string[4];
            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@revision_no:" + revision_no;
            pParam[2] = "@dist_id:" + dist_id;
            pParam[3] = "@disuse_id:" + disuse_id;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            if (message.Length > 0)
            {
                check = true;
            }

            return check;
        }


        internal string GmpDocManage_D1_TRX(List<DocDistribute> paramData)
        {
            string res = "";

            foreach (DocDistribute tmp in paramData)
            {
                string[] pParam = new string[10];
                pParam[0] = "@doc_no:" + tmp.doc_no;
                pParam[1] = "@revision_no:" + tmp.revision_no;
                pParam[2] = "@dist_id:" + tmp.dist_id;
                pParam[3] = "@dist_status:" + tmp.dist_status;
                pParam[4] = "@dist_type:" + tmp.dist_type;
                pParam[5] = "@dist_dept_cd:" + tmp.dist_dept_cd;
                pParam[6] = "@dist_date:" + tmp.dist_date;
                pParam[7] = "@dist_qty:" + tmp.dist_qty;
                pParam[8] = "@over_emp_cd:" + tmp.over_emp_cd;
                pParam[9] = "@accept_emp_cd:" + tmp.accept_emp_cd;

                if (tmp.gubun.Equals("I"))
                {                   
                    res = _bllSpExecute.SpExecuteString(sSpName, "I", pParam);
                }
                else if (tmp.gubun.Equals("U"))
                {
                    res = _bllSpExecute.SpExecuteString(sSpName, "U", pParam);
                }
            }

            return res;
        }

        internal string InsertSave(DocDistribute doc)
        {
            string pGubun = "I";
      
            string[] pParam = new string[10];
            pParam[0] = "@doc_no:" + doc.doc_no;
            pParam[1] = "@revision_no:" + doc.revision_no;
            pParam[2] = "@dist_id:" + doc.dist_id;
            pParam[3] = "@dist_status:" + doc.dist_status;
            pParam[4] = "@dist_type:" + doc.dist_type;
            pParam[5] = "@dist_dept_cd:" + doc.dist_dept_cd;
            pParam[6] = "@dist_date:" + doc.dist_date;
            pParam[7] = "@dist_qty:" + doc.dist_qty;
            pParam[8] = "@over_emp_cd:" + doc.over_emp_cd;
            pParam[9] = "@accept_emp_cd:" + doc.accept_emp_cd;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return message;
        }

        internal string UpdateSave(DocDistribute doc)
        {
            string pGubun = "U";

            string[] pParam = new string[10];
            pParam[0] = "@doc_no:" + doc.doc_no;
            pParam[1] = "@revision_no:" + doc.revision_no;
            pParam[2] = "@dist_id:" + doc.dist_id;
            pParam[3] = "@dist_status:" + doc.dist_status;
            pParam[4] = "@dist_type:" + doc.dist_type;
            pParam[5] = "@dist_dept_cd:" + doc.dist_dept_cd;
            pParam[6] = "@dist_date:" + doc.dist_date;
            pParam[7] = "@dist_qty:" + doc.dist_qty;
            pParam[8] = "@over_emp_cd:" + doc.over_emp_cd;
            pParam[9] = "@accept_emp_cd:" + doc.accept_emp_cd;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return message;
        }

        internal string GmpDocManage_D2_TRX(List<DocDisuse> paramData)
        {
            string res = "";

            foreach (DocDisuse tmp in paramData)
            {
                string[] pParam = new string[8];
                pParam[0] = "@doc_no:" + tmp.doc_no;
                pParam[1] = "@revision_no:" + tmp.revision_no;
                pParam[2] = "@dist_id:" + tmp.dist_id;
                pParam[3] = "@disuse_id:" + tmp.disuse_id;
                pParam[4] = "@disuse_date:" + tmp.disuse_date;
                pParam[5] = "@disuse_qty:" + tmp.disuse_qty;
                pParam[6] = "@disuse_emp_cd:" + tmp.disuse_emp_cd;
                pParam[7] = "@disuse_reason:" + tmp.disuse_reason;

                if (tmp.gubun.Equals("I"))
                {
                    res = _bllSpExecute.SpExecuteString(sSpName, "I1", pParam);
                }
                else if (tmp.gubun.Equals("U"))
                {
                    res = _bllSpExecute.SpExecuteString(sSpName, "U1", pParam);
                }
            }

            return res;
        }

        internal string InsertSave_1(DocDisuse doc)
        {
            string pGubun = "I1";

            string[] pParam = new string[8];
            pParam[0] = "@doc_no:" + doc.doc_no;
            pParam[1] = "@revision_no:" + doc.revision_no;
            pParam[2] = "@dist_id:" + doc.dist_id;
            pParam[3] = "@disuse_id:" + doc.disuse_id;
            pParam[4] = "@disuse_date:" + doc.disuse_date;
            pParam[5] = "@disuse_qty:" + doc.disuse_qty;
            pParam[6] = "@disuse_emp_cd:" + doc.disuse_emp_cd;
            pParam[7] = "@disuse_reason:" + doc.disuse_reason;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return message;
        }

        internal string UpdateSave_1(DocDisuse doc)
        {
            string pGubun = "U1";

            string[] pParam = new string[8];
            pParam[0] = "@doc_no:" + doc.doc_no;
            pParam[1] = "@revision_no:" + doc.revision_no;
            pParam[2] = "@dist_id:" + doc.dist_id;
            pParam[3] = "@disuse_id:" + doc.disuse_id;
            pParam[4] = "@disuse_date:" + doc.disuse_date;
            pParam[5] = "@disuse_qty:" + doc.disuse_qty;
            pParam[6] = "@disuse_emp_cd:" + doc.disuse_emp_cd;
            pParam[7] = "@disuse_reason:" + doc.disuse_reason;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return message;
        }

        internal string ChkVoidDate(string void_date)
        {

            string pGubun = "CHK_V";
            string[] pParam = new string[1];

            void_date = (void_date == "") ? "2100-01-01" : void_date;
            pParam[0] = "@void_date:" + void_date;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return result;
        }

        internal DataTable GetFile(string doc_file_id)
        {
            string pGubun = "FS";
            string[] pParam = new string[1];

            pParam[0] = "@doc_file_id:" + doc_file_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, pGubun, pParam);

            return dt;

            //string sSpName = "SP_Gmp_doc_manage";
            //string sGubun = "FS";
            //string pParam = "@doc_file_id:" + doc_file_id;

            //string message = "";

            //DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

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

        #region 전자서명
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

        internal bool GetModifiableAuthority(string indexKey, string moduleType, string emp_cd)
        {
            string sSpName = "SP_ElectronicSignature";
            string pGubun = "ModifiableSignerSearch";

            bool check = false;

            string[] param = new string[3];
            param[0] = "@index_key:" + indexKey;
            param[1] = "@module_type:" + moduleType;
            param[2] = "@sign_emp_cd:" + emp_cd;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, param);

            if (message == "Y")
                check = true;

            return check;

        }

        internal void DeleteDistribution(string indexKey)
        {
            string pGubun = "Delete_Distribution";

            string[] param = new string[1];
            param[0] = "@dist_id:" + indexKey;

            _bllSpExecute.SpExecuteString(sSpName, pGubun, param);

        }

        internal bool SaveSign(string indexKey, string moduleType, string signCode, string sign_set_dt_seq, string emp_cd, string validation_type, string representative_yn, string docNo, string revisionNo)
        {
            string pGubun = "SignSave";

            bool check = false;

            string[] pParam = new string[9];
            pParam[0] = "@index_key:" + indexKey;
            pParam[1] = "@module_type:" + moduleType;
            pParam[2] = "@sign_set_cd:" + signCode;
            pParam[3] = "@sign_set_dt_seq:" + sign_set_dt_seq;
            pParam[4] = "@sign_emp_cd:" + emp_cd;
            pParam[5] = "@sign_type:" + validation_type;
            pParam[6] = "@representative_yn:" + representative_yn;
            pParam[7] = "@doc_no:" + docNo;
            pParam[8] = "revision_no:" + revisionNo;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            if (message.Length > 0)
            {
                check = true;
            }

            return check;
        }

        internal bool CancelSign(string indexKey, string moduleType, string SignSeq)
        {
            string sSpName = "SP_ElectronicSignature";
            string pGubun = "SignCancel";

            if (string.IsNullOrEmpty(SignSeq))
            {
                pGubun = "SignCancelAll";
                SignSeq = "";   //null일 경우 빈스트링으로 치환
            }

            bool check = false;

            string[] pParam = new string[3];
            pParam[0] = "@index_key:" + indexKey;
            pParam[1] = "@module_type:" + moduleType;
            pParam[2] = "@sign_set_dt_seq:" + SignSeq;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            if (message.Length > 0)
            {
                check = true;
            }

            return check;
        }
        #endregion

        internal DataTable getEmpData()
        {
            string strsql = "SELECT emp_cd, emp_nm, dept_cd, dept_nm";
            strsql += " FROM V_EMPLOYEE";
            strsql += " WHERE (emp_cd  LIKE '%%' OR emp_nm  LIKE '%%')";
            strsql += " AND emp_cd  LIKE '%%' AND emp_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        #region 프로그램 설정값
        internal DataTable GetProgramSet()
        {
            string sSpName = "SP_ProgramInit";
            string[] pParam = new string[1];
            //pParam[0] = "@code:" + Public_Function.program_id;
            pParam[0] = "@code:" + "Gmp_doc_manage";

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
