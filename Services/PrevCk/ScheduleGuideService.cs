using DevExtreme.AspNet.Mvc.Builders;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models;
using HACCP.Models.PrevCk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrevCk
{
    public class ScheduleGuideService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        public DataTable GetRepositoryItem(string gubun)
        {
            string sSpName = "SP_ScheduleGuide_Y";
            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        public DataTable GridSelect(string obj_type, string work_type, string schedule_type, string responsibility_worker, string obj_cd)
        {

            DataTable programSet = GetProgramSet();

            string commonCd = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString();//Option1

            string sSpName = "SP_ScheduleGuide_Y";
            string sGubun = "S1";
            string[] pParam = new string[6];

            pParam[0] = "@obj_type:" + obj_type;
            pParam[1] = "@work_type:" + work_type;
            pParam[2] = "@schedule_type:" + schedule_type;
            pParam[3] = "@s_responsibility_worker:" + responsibility_worker;
            pParam[4] = "@s_obj_cd:" + obj_cd;
            pParam[5] = "@common_cd:" + commonCd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        public DataTable GridSelect2(string schedule_master_id)
        {
            string sSpName = "SP_ScheduleGuide_Y";
            string sGubun = "S3";
            string[] pParam = new string[1];

            pParam[0] = "@schedule_master_id:" + schedule_master_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        public string GridDelete(string schedule_master_id, string work_id)
        {
            string sSpName = "SP_ScheduleGuide_Y";
            string sGubun = "D";

            string[] pParam = new string[2];
            pParam[0] = "@schedule_master_id:" + schedule_master_id;
            pParam[1] = "@work_id:" + work_id;

            string result = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return result;
        }


        public string Copy(string schedule_master_id_copy, string schedule_master_id)
        {
            string sSpName = "SP_ScheduleGuide_Y";
            string sGubun = "COPY";

            string[] pParam = new string[2];
            pParam[0] = "@schedule_master_id_copy:" + schedule_master_id_copy;
            pParam[1] = "@schedule_master_id:" + schedule_master_id;

            string result = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return result;
        }

        public string ScheduleGuide_TRX(List<ScheduleGuide> paramData)
        {
            string sSpName = "SP_ScheduleGuide_Y";
            string res = "";

            foreach (ScheduleGuide tmp in paramData)
            {
                if(tmp.gubun =="I" || tmp.gubun == "U")
                {
                    string[] pParam = new string[16];
                    pParam[0] = "@schedule_master_id:" + tmp.schedule_master_id;
                    pParam[1] = "@obj_type:" + tmp.obj_type;
                    pParam[2] = "@work_type:" + tmp.work_type;
                    pParam[3] = "@schedule_type:" + tmp.schedule_type;
                    pParam[4] = "@obj_cd:" + tmp.obj_cd;
                    pParam[5] = "@insert_emp:" + HttpContext.Current.Session["loginCD"].ToString();
                    pParam[6] = "@work_id:" + tmp.work_id;

                    pParam[7] = "@work_seq:" + tmp.work_seq;
                    pParam[8] = "@work_method:" + tmp.work_method;
                    pParam[9] = "@schedule_guide_data_type:" + tmp.schedule_guide_data_type;
                    pParam[10] = "@schedule_guide_min_manage:" + tmp.schedule_guide_min_manage;
                    pParam[11] = "@schedule_guide_max_manage:" + tmp.schedule_guide_max_manage;
                    pParam[12] = "@schedule_guide_min_permit:" + tmp.schedule_guide_min_permit;
                    pParam[13] = "@schedule_guide_max_permit:" + tmp.schedule_guide_max_permit;
                    pParam[14] = "@equip_cd:" + tmp.equip_cd;
                    pParam[15] = "@schedule_guide_sub_item:" + tmp.schedule_guide_sub_item;

                    res = _bllSpExecute.SpExecuteString(sSpName, tmp.gubun, pParam);
                }

                if (tmp.gubun == "D")
                {
                    string[] pParam = new string[2];
                    pParam[0] = "@schedule_master_id:" + tmp.schedule_master_id;
                    pParam[1] = "@work_id:" + tmp.work_id;

                    res = _bllSpExecute.SpExecuteString(sSpName, tmp.gubun, pParam);
                }
            }

            return res;
        }

        public string SaveNodes(ScheduleGuide scheduleGuide)
        {
            string sSpName = "SP_ScheduleGuide_Y";
            string sGubun = scheduleGuide.gubun;

            string[] pParam = new string[16];
            pParam[0] = "@schedule_master_id:" + scheduleGuide.schedule_master_id;
            pParam[1] = "@obj_type:" + scheduleGuide.obj_type;
            pParam[2] = "@work_type:" + scheduleGuide.work_type;
            pParam[3] = "@schedule_type:" + scheduleGuide.schedule_type;
            pParam[4] = "@obj_cd:" + scheduleGuide.obj_cd;
            pParam[5] = "@insert_emp:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[6] = "@work_id:" + scheduleGuide.work_id;

            pParam[7] = "@work_seq:" + scheduleGuide.work_seq;
            pParam[8] = "@work_method:" + scheduleGuide.work_method;
            pParam[9] = "@schedule_guide_data_type:" + scheduleGuide.schedule_guide_data_type;
            pParam[10] = "@schedule_guide_min_manage:" + scheduleGuide.schedule_guide_min_manage;
            pParam[11] = "@schedule_guide_max_manage:" + scheduleGuide.schedule_guide_max_manage;
            pParam[12] = "@schedule_guide_min_permit:" + scheduleGuide.schedule_guide_min_permit;
            pParam[13] = "@schedule_guide_max_permit:" + scheduleGuide.schedule_guide_max_permit;
            pParam[14] = "@equip_cd:" + scheduleGuide.equip_cd;
            pParam[15] = "@schedule_guide_sub_item:" + scheduleGuide.schedule_guide_sub_item;

            string result = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return result;
        }

        public DataTable reportPrintOrPreview(string schedule_master_id)
        {
            DataTable programSet = GetProgramSet();

            string commonCd = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString();//Option1

            string sSpName = "SP_ScheduleGuide_Y";
            string sGubun = "SR";
            string[] pParam = new string[2];

            pParam[0] = "@schedule_master_id:" + schedule_master_id;
            pParam[1] = "@common_cd:" + commonCd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            DataTable dt_1 = _bllSpExecute.SpExecuteTable(sSpName, "SelectFile", "@schedule_master_id:" + schedule_master_id);

            //리포트 설정

            return dt;
        }

        #region 프로그램 설정값
        public DataTable GetProgramSet()
        {
            string sSpName = "SP_ProgramInit";
            string[] pParam = new string[1];
            pParam[0] = "@code:" + "ScheduleGuide";
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