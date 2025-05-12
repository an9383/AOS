using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Mont;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Mont
{
    public class ReportDrawingService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable getReportMainItem(ReportDataFormat rModel)
        {
            string sSpName = "SP_GET_REPORT_DATA";
            string gubun = "S";
            string[] pParam = new string[4];


            pParam[0] = "@V_TYPE:" + rModel.doc_cd;
            pParam[1] = "@WORKROOM_AREA:" + rModel.workroom_area;
            pParam[2] = "@SDATE:" + rModel.sdate;
            pParam[3] = "@EDATE:" + rModel.edate;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }


        internal List<string> getReportItem(ReportDataFormat rModel)
        {
            string sSpName = "SP_GET_REPORT_DATA";
            string gubun = rModel.gubun;
            string[] pParam = new string[6];
            pParam[0] = "@V_TYPE:" + rModel.doc_seq;
            pParam[1] = "@WORKROOM_AREA:" + rModel.workroom_area;
            pParam[2] = "@SDATE:" + rModel.sdate;
            pParam[3] = "@EDATE:" + rModel.edate;
            pParam[4] = "@DOC_CD:" + rModel.doc_cd;
            pParam[5] = "@index_key:" + rModel.index_key;

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
                    jsonList.Add(Public_Function.DataTableToJSON(ds.Tables[i]));
                    //jsonList.Add(ds.Tables[i].ToString());
                }
                else if (jsonStr.Length <= 0)
                {
                    jsonList.Add("{ \"data\" : \"empty\" }");
                }
            }

            return jsonList;
        }


        internal DataTable getWorkroomList(string workroom_name, string workroom_area)
        {
            string sSpName = "SP_GET_REPORT_DATA";
            string gubun = "S_Workroom";
            string[] pParam = new string[2];

            pParam[0] = "@workroom_nm:" + workroom_name;
            pParam[1] = "@WORKROOM_AREA:" + workroom_area;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal DataTable getEmployeeList()
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "S2";

            //파라메터 사용안함ㄴ
            string[] pParam = new string[1];
            pParam[0] = "@sdate:";


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal string setReportItem(List<ReportDataFormat> mModel, string gubun, string V_TYPE)
        {
            string sSpName = "SP_GET_REPORT_DATA";
            string sGubun = gubun;
            string message = "";

            for (int i = 0; i < mModel.Count; i++)
            {
                string[] pParam = getParam(gubun, V_TYPE, mModel[i]);

                if (pParam != null)
                {
                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

                }
            }
            return message;
        }


        internal string[] getParam(string gubun, string V_TYPE, ReportDataFormat mModel)
        {
            if (gubun == "U_Deviation")
            {
                string[] pParam = new string[9];
                pParam[0] = "@V_TYPE:" + V_TYPE;
                pParam[1] = "@deviation_no:" + mModel.deviation_no;
                pParam[2] = "@index_key:" + mModel.index_key;
                pParam[3] = "@deviation_date:" + mModel.dev_date;
                pParam[4] = "@detect_method:" + mModel.dev_result;
                pParam[5] = "@deviation_receive_comment:" + mModel.dev_contents;
                pParam[6] = "@deviation_end_date:" + mModel.dev_repair_date;
                pParam[7] = "@deviation_place:" + mModel.workroom_cd;
                pParam[8] = "@asign_emp_code:" + mModel.asign_emp_code;

                return pParam;
            }
            else
            {
                if (V_TYPE == "30001")
                {

                    string[] pParam = new string[25];
                    pParam[0] = "@V_TYPE:" + V_TYPE;
                    pParam[1] = "@index_key:" + mModel.index_key;
                    pParam[2] = "@workroom_cd:" + mModel.workroom_cd;
                    pParam[3] = "@result_temp_mon:" + mModel.result_temp_mon;
                    pParam[4] = "@result_temp_tue:" + mModel.result_temp_tue;
                    pParam[5] = "@result_temp_wed:" + mModel.result_temp_wed;
                    pParam[6] = "@result_temp_thu:" + mModel.result_temp_thu;
                    pParam[7] = "@result_temp_fri:" + mModel.result_temp_fri;
                    pParam[8] = "@result_temp_sat:" + mModel.result_temp_sat;
                    pParam[9] = "@result_hum_mon:" + mModel.result_hum_mon;
                    pParam[10] = "@result_hum_tue:" + mModel.result_hum_tue;
                    pParam[11] = "@result_hum_wed:" + mModel.result_hum_wed;
                    pParam[12] = "@result_hum_thu:" + mModel.result_hum_thu;
                    pParam[13] = "@result_hum_fri:" + mModel.result_hum_fri;
                    pParam[14] = "@result_hum_sat:" + mModel.result_hum_sat;
                    pParam[15] = "@result_press_mon:" + mModel.result_press_mon;
                    pParam[16] = "@result_press_tue:" + mModel.result_press_tue;
                    pParam[17] = "@result_press_wed:" + mModel.result_press_wed;
                    pParam[18] = "@result_press_thu:" + mModel.result_press_thu;
                    pParam[19] = "@result_press_fri:" + mModel.result_press_fri;
                    pParam[20] = "@result_press_sat:" + mModel.result_press_sat;
                    pParam[21] = "@asign_emp_code:" + mModel.asign_emp_code;
                    pParam[22] = "@sdate:" + mModel.sdate;
                    pParam[23] = "@edate:" + mModel.edate;
                    pParam[24] = "@stime:" + mModel.stime;

                    return pParam;

                }
                else if (V_TYPE == "30002")
                {

                    string[] pParam = new string[25];
                    pParam[0] = "@V_TYPE:" + V_TYPE;
                    pParam[1] = "@index_key:" + mModel.index_key;
                    pParam[2] = "@workroom_cd:" + mModel.workroom_cd;
                    pParam[3] = "@result_temp_mon:" + mModel.result_temp_mon;
                    pParam[4] = "@result_temp_tue:" + mModel.result_temp_tue;
                    pParam[5] = "@result_temp_wed:" + mModel.result_temp_wed;
                    pParam[6] = "@result_temp_thu:" + mModel.result_temp_thu;
                    pParam[7] = "@result_temp_fri:" + mModel.result_temp_fri;
                    pParam[8] = "@result_temp_sat:" + mModel.result_temp_sat;
                    pParam[9] = "@result_hum_mon:" + mModel.result_hum_mon;
                    pParam[10] = "@result_hum_tue:" + mModel.result_hum_tue;
                    pParam[11] = "@result_hum_wed:" + mModel.result_hum_wed;
                    pParam[12] = "@result_hum_thu:" + mModel.result_hum_thu;
                    pParam[13] = "@result_hum_fri:" + mModel.result_hum_fri;
                    pParam[14] = "@result_hum_sat:" + mModel.result_hum_sat;
                    pParam[15] = "@result_press_mon:" + mModel.result_press_mon;
                    pParam[16] = "@result_press_tue:" + mModel.result_press_tue;
                    pParam[17] = "@result_press_wed:" + mModel.result_press_wed;
                    pParam[18] = "@result_press_thu:" + mModel.result_press_thu;
                    pParam[19] = "@result_press_fri:" + mModel.result_press_fri;
                    pParam[20] = "@result_press_sat:" + mModel.result_press_sat;
                    pParam[21] = "@asign_emp_code:" + mModel.asign_emp_code;
                    pParam[22] = "@sdate:" + mModel.sdate;
                    pParam[23] = "@edate:" + mModel.edate;
                    pParam[24] = "@stime:" + mModel.stime;

                    return pParam;

                }
                else if (V_TYPE == "30005")
                {
                    string[] pParam = new string[12];
                    pParam[0] = "@V_TYPE:" + V_TYPE;
                    pParam[1] = "@index_key:" + mModel.index_key;
                    pParam[2] = "@result_code:" + mModel.CodeCode;
                    pParam[3] = "@result_1:" + mModel.result_1;
                    pParam[4] = "@result_2:" + mModel.result_2;
                    pParam[5] = "@result_3:" + mModel.result_3;
                    pParam[6] = "@result_4:" + mModel.result_4;
                    pParam[7] = "@result_5:" + mModel.result_5;
                    pParam[8] = "@result_6:" + mModel.result_6;
                    pParam[9] = "@asign_emp_code:" + mModel.asign_emp_code;
                    pParam[10] = "@sdate:" + mModel.sdate;
                    pParam[11] = "@edate:" + mModel.edate;

                    return pParam;
                }
                else if (V_TYPE == "30006")
                {
                    string[] pParam = new string[12];
                    pParam[0] = "@V_TYPE:" + V_TYPE;
                    pParam[1] = "@index_key:" + mModel.index_key;
                    pParam[2] = "@result_code:" + mModel.CodeCode;
                    pParam[3] = "@result_1:" + mModel.result_1;
                    pParam[4] = "@result_2:" + mModel.result_2;
                    pParam[5] = "@result_3:" + mModel.result_3;
                    pParam[6] = "@result_4:" + mModel.result_4;
                    pParam[7] = "@result_5:" + mModel.result_5;
                    pParam[8] = "@result_6:" + mModel.result_6;
                    pParam[9] = "@asign_emp_code:" + mModel.asign_emp_code;
                    pParam[10] = "@sdate:" + mModel.sdate;
                    pParam[11] = "@edate:" + mModel.edate;

                    return pParam;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}