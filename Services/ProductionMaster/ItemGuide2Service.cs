using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.ProductionMaster;
using HACCP.Services.Comm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.ProductionMaster
{
    public class ItemGuide2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable selectRevisionData(bool latestRevisionCk, bool revisionSignEndCk, bool revisionCurrentCk)
        {
            string strsql = "";

            strsql += "SELECT distinct a.item_cd as item_cd";
            strsql += ", b.item_nm as item_nm";
            strsql += ", isnull(a.revision_num, '') as revision_num";
            strsql += ", isnull(a.revision_no, '') as revision_no";
            strsql += ", isnull(a.revision_remark, '') as revision_remark";
            strsql += ", a.batch_size as batchsize";
            strsql += ", isnull(a.batch_size_unit, '') as batchsize_unit_cd";
            strsql += ", (case when isnull(a.revision_sign_end_ck, 'N') = 'Y' then 'Y' else 'N' end) as accept_yn";
            strsql += " FROM item_process_revision a";
            strsql += " inner join v_item_standard b on b.item_cd = a.item_cd and b.sys_plant_cd = a.sys_plant_cd";

            if (latestRevisionCk)
            {
                strsql += " inner join (";
                strsql += " select item_cd as item_cd, max(revision_no) as revision_no ";
                strsql += " from item_process_revision ";
                strsql += " where revision_sign_end_ck = ";

                if (revisionSignEndCk)
                    strsql += "'Y'";
                else
                    strsql += "'N'";

                strsql += " and mp_ck = 'P'";
                strsql += " group by item_cd) c on c.revision_no = a.revision_no and c.item_cd = a.item_cd";
            }

            if (revisionSignEndCk)
            {
                strsql += " WHERE a.revision_sign_end_ck = ";
                strsql += "'Y'";
                strsql += " and a.sys_plant_cd = ";
                strsql += ("'" + Public_Function.selectedPLANT + "'");
            }
            else
            {
                strsql += " WHERE a.revision_sign_end_ck = ";
                strsql += "'N'";
                strsql += " and a.sys_plant_cd = ";
                strsql += ("'" + Public_Function.selectedPLANT + "'");
            }

            strsql += " and a.mp_ck = 'P'";

            if (revisionCurrentCk)
            {
                strsql += " and a.revision_current_ck = 'Y'";
            }

            strsql += " ORDER BY item_cd, revision_no";

            DataTable dt = _bllSpExecute.SpExecuteTable("CodeHelp", strsql);

            return dt;
        }

        internal string ItemGuide2RevisionTRX(RevisionData revisionData)
        {
            string sSpName = "SP_StandardItemGuide2";

            string res = "";

            if (revisionData.gubun.Equals("C"))
            {
                string[] pParam = new string[6];
                pParam[0] = "@item_cd:" + revisionData.item_cd;
                pParam[1] = "@revision_num:" + revisionData.revision_num;
                pParam[2] = "@revision_special_mention:" + revisionData.revision_special_mention;
                pParam[3] = "@revision_remark:" + revisionData.revision_remark;
                pParam[4] = "@revision_use_date:" + revisionData.revision_use_date;
                pParam[5] = "@revision_no:" + "";


                string count = _bllSpExecute.SpExecuteString(sSpName, "S11", pParam);

                if (Int32.Parse(count) > 0)
                {
                    return "1";
                }

                pParam[5] = "@revision_no:" + "NEW";

                string revision_no = "";

                try
                {
                    revision_no = _bllSpExecute.SpExecuteString(sSpName, revisionData.gubun, pParam);
                }
                catch
                {
                    return "2";
                }

                pParam[5] = "@revision_no:" + revision_no;

                DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "U2", pParam);

                res = "0";

            } else if (revisionData.gubun.Equals("U0"))
            {
                string[] pParam = new string[15];
                pParam[0] = "@item_cd:" + revisionData.item_cd;
                pParam[1] = "@revision_no:" + revisionData.revision_no;
                pParam[2] = "@batch_size:" + "1";
                pParam[3] = "@revision_num:" + revisionData.revision_num;
                pParam[4] = "@revision_use_date:" + revisionData.revision_use_date;
                pParam[5] = "@revision_remark:" + revisionData.revision_remark;
                pParam[6] = "@mbr_type:" + revisionData.mbr_type;
                pParam[7] = "@export_words:" + revisionData.export_words;
                pParam[8] = "@batch_weight:" + revisionData.batch_weight;
                pParam[9] = "@batch_size_sub:" + revisionData.batch_size_sub;
                pParam[10] = "@permit_standard_qty:" + revisionData.permit_standard_qty;
                pParam[11] = "@permit_standard_qty_unit:" + revisionData.permit_standard_qty_unit;
                pParam[12] = "@revision_special_mention:" + revisionData.revision_special_mention;
                pParam[13] = "@t_making_dept_cd:" + revisionData.t_making_dept_cd;
                pParam[14] = "@t_making_dept_cd2:" + revisionData.t_making_dept_cd2;

                res = _bllSpExecute.SpExecuteString(sSpName, revisionData.gubun, pParam);

            } else if (revisionData.gubun.Equals("Delete_All"))
            {
                sSpName = "SP_PackingBomFull2";

                string[] pParam = new string[3];
                pParam[0] = "@item_cd:" + revisionData.item_cd;
                pParam[1] = "@item_bom_no:" + revisionData.revision_no;
                pParam[2] = "@mp_ck:" + "P";

                res = _bllSpExecute.SpExecuteString(sSpName, revisionData.gubun, pParam);
            }

            return res;
        }

        internal string ItemGuide2CopyProcess(RevisionData revisionData)
        {
            string sSpName = "SP_StandardItemGuide2";

            string[] pParam = new string[12];

            pParam[0] = "@item_cd:" + revisionData.item_cd;
            pParam[1] = "@t_item_cd:" + revisionData.t_item_cd;
            pParam[2] = "@t_making_dept_cd:" + "1";
            pParam[3] = "@t_making_dept_cd2:" + revisionData.t_making_dept_cd2;
            pParam[4] = "@new_revision_no:" + revisionData.new_revision_no;
            pParam[5] = "@revision_no:" + revisionData.revision_no;
            pParam[6] = "@mbr_type:" + revisionData.mbr_type;
            pParam[7] = "@export_words:" + revisionData.export_words;
            pParam[8] = "@batch_weight:" + revisionData.batch_weight;
            pParam[9] = "@batch_size_sub:" + revisionData.batch_size_sub;
            pParam[10] = "@permit_standard_qty:" + revisionData.permit_standard_qty;
            pParam[11] = "@permit_standard_qty_unit:" + revisionData.permit_standard_qty_unit;

            string res = _bllSpExecute.SpExecuteString(sSpName, revisionData.gubun, pParam);

            return res;
        }

        internal string ItemGuide2ProcessTRX(List<RevisionData> processsData)
        {
            string sSpName = "SP_StandardItemGuide2";
            string res = "";

            foreach (RevisionData revisionData in processsData)
            {
                if (revisionData.gubun.Equals("MC1") || revisionData.gubun.Equals("MC4"))
                {
                    string[] pParam = new string[5];

                    pParam[0] = "@item_cd:" + revisionData.item_cd;
                    pParam[1] = "@revision_no:" + revisionData.revision_no;
                    pParam[2] = "@batch_size:" + "1";
                    pParam[3] = "@startingkey:" + revisionData.startingkey;
                    pParam[4] = "@process_cd:" + revisionData.process_cd;

                    res = _bllSpExecute.SpExecuteString(sSpName, revisionData.gubun, pParam);
                }
                else if (revisionData.gubun.Equals("D"))
                {
                    sSpName = "SP_Item_Process";

                    string[] pParam = new string[5];

                    pParam[0] = "@mp_ck:" + "P";
                    pParam[1] = "@item_cd:" + revisionData.item_cd;
                    pParam[3] = "@revision_no:" + revisionData.revision_no;
                    pParam[2] = "@batch_size:" + "1";
                    pParam[4] = "@process_cd:" + revisionData.process_cd;

                    res = _bllSpExecute.SpExecuteString(sSpName, revisionData.gubun, pParam);
                }
                else if (revisionData.gubun.Equals("U"))
                {
                    sSpName = "SP_Item_Process";

                    string[] param = new string[27];

                    param[0] = "@mp_ck:" + "P";
                    param[1] = "@item_cd:" + revisionData.item_cd;
                    param[2] = "@revision_no:" + revisionData.revision_no;
                    param[3] = "@batch_size:" + revisionData.batch_size;
                    param[4] = "@process_cd:" + revisionData.process_cd;
                    param[5] = "@item_proc_seq:" + revisionData.item_proc_seq;
                    param[6] = "@item_proc_qc_ck:" + revisionData.item_proc_qc_ck;
                    param[7] = "@test_type:" + revisionData.test_type;
                    param[8] = "@test_standby_yn:" + revisionData.test_standby_yn;
                    param[9] = "@item_proc_transfer_ck:" + revisionData.item_proc_transfer_ck;
                    param[10] = "@item_proc_worker1:" + revisionData.item_proc_worker1;
                    param[11] = "@item_proc_worker2:" + revisionData.item_proc_worker2;
                    param[12] = "@item_proc_inspector:" + revisionData.item_proc_inspector;
                    param[13] = "@item_proc_warehouse:" + revisionData.item_proc_warehouse;
                    param[14] = "@remark:" + revisionData.remark;
                    param[15] = "@item_proc_content:" + revisionData.item_proc_content;
                    param[16] = "@item_proc_standard_rate:" + revisionData.item_proc_standard_rate;
                    param[17] = "@item_proc_manage_rate:" + revisionData.item_proc_manage_rate;
                    param[18] = "@standard_proc_time:" + revisionData.standard_proc_time;
                    param[19] = "@workroom_cd:" + revisionData.workroom_cd;
                    param[20] = "@item_proc_manage_rate_max:" + revisionData.item_proc_manage_rate_max;
                    param[21] = "@item_proc_manage_rate_mIN:" + revisionData.item_proc_manage_rate_mIN;
                    param[22] = "@next_process_cd:" + revisionData.next_process_cd;
                    param[23] = "@item_proc_standard_weight_qty:" + revisionData.item_proc_standard_weight_qty;
                    param[24] = "@item_proc_standard_weight_unit:" + revisionData.item_proc_standard_weight_unit;
                    param[25] = "@ccp_cd:" + revisionData.ccp_cd;
                    param[26] = "@grouping_cd:" + revisionData.grouping_cd;

                    res = _bllSpExecute.SpExecuteString(sSpName, revisionData.gubun, param);

                    //if (revisionData.ccpChanged)
                    //{
                    //    param = new string[4];
                    //    param[0] = "@item_cd:" + revisionData.item_cd;
                    //    param[1] = "@revision_no:" + revisionData.revision_no;
                    //    param[2] = "@process_cd:" + revisionData.process_cd;
                    //    param[3] = "@ccp_cd:" + revisionData.ccp_cd;

                    //    _bllSpExecute.SpExecuteString("SP_Item_Workguide2", "IAX", param);
                    //}
                }
            }

            return res;
        }

        internal string ItemGuide2MoveProcess(RevisionData revisionData)
        {
            string sSpName = "SP_StandardItemGuide2";

            string[] pParam;

            if (revisionData.type.Equals("Standard"))
            {
                pParam = new string[6];
                pParam[0] = "@item_cd:" + revisionData.item_cd;
                pParam[1] = "@revision_no:" + revisionData.revision_no;
                pParam[2] = "@batch_size:" + "1";
                pParam[3] = "@detailproc_id:";
                pParam[4] = "@process_cd:" + revisionData.process_cd;
                pParam[5] = "@process_seq:" + revisionData.process_seq;

                DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, revisionData.gubun, pParam);
            }
            else if (revisionData.type.Equals("Detail"))
            {
                pParam = new string[7];
                pParam[0] = "@item_cd:" + revisionData.item_cd;
                pParam[1] = "@revision_no:" + revisionData.revision_no;
                pParam[2] = "@batch_size:" + "1";
                pParam[3] = "@detailproc_id:";
                pParam[4] = "@process_seq:" + revisionData.process_seq;
                pParam[5] = "@process_cd:" + revisionData.process_cd;
                pParam[6] = "@standard_process_cd:" + revisionData.standard_process_cd;

                DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, revisionData.gubun, pParam);
            }

            return "";
        }

        internal void SignCRUD(string gubun, string c_seq, string emp_cd, string item_cd, string revision_no, string validation_type)
        {
            string sSpName = "SP_StandardItemGuide2";

            DataTable dt = new DataTable();

            string[] pParam = new string[2];
            pParam[0] = "@c_seq:" + c_seq;
            pParam[1] = "@emp_cd:" + emp_cd;
            string str = _bllSpExecute.SpExecuteString(sSpName, "SR", pParam);

            pParam = new string[5];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@revision_no:" + revision_no;
            pParam[2] = "@emp_cd:" + emp_cd;
            pParam[3] = "@validation_type:" + validation_type;
            pParam[4] = "@type:" + c_seq;

            dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            //if (gubun.Equals("U"))
            //{
            //    string[] pParam = new string[2];
            //    pParam[0] = "@c_seq:" + c_seq;
            //    pParam[1] = "@emp_cd:" + emp_cd;
            //    string str = _bllSpExecute.SpExecuteString(sSpName, "SR", pParam);

            //    pParam = new string[5];
            //    pParam[0] = "@item_cd:" + item_cd;
            //    pParam[1] = "@revision_no:" + revision_no;
            //    pParam[2] = "@emp_cd:" + emp_cd;
            //    pParam[3] = "@validation_type:" + validation_type;
            //    pParam[4] = "@type:" + c_seq;

            //    dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);
            //}
            //else if (gubun.Equals("U3"))
            //{
            //    string[] pParam = new string[5];
            //    pParam[0] = "@item_cd:" + item_cd;
            //    pParam[1] = "@revision_no:" + revision_no;
            //    pParam[2] = "@emp_cd:" + emp_cd;
            //    pParam[3] = "@validation_type:" + validation_type;
            //    pParam[4] = "@type:" + c_seq;

            //    dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);
            //}
        }

        internal string finalApproval(string gubun, string item_cd, string revision_no, string revision_sign_end_state, string current_ck)
        {
            string sSpName = "SP_StandardItemGuide2";
            string[] pParam = new string[5];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@batch_size:" + "1";
            pParam[2] = "@revision_no:" + revision_no;
            pParam[3] = "@revision_sign_end_state:" + revision_sign_end_state;
            pParam[4] = "@revision_current_ck:" + current_ck;

            string  res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return res;
        }


        internal string ItemGuide2UseChk(string item_cd, string revision_no)
        {
            string sSpName = "SP_StandardItemGuide2";
            string[] pParam = new string[4];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[2] = "@revision_no:" + revision_no;
            pParam[1] = "@batch_size:" + "1";
            pParam[3] = "@revision_current_ck:" + "Y";

            string res = _bllSpExecute.SpExecuteString(sSpName, "YN", pParam);

            return res;
        }

        internal DataTable SelectBomData(string item_cd, string item_bom_no)
        {
            string sSpName = "SP_PackingBomFull2";
            string[] pParam = new string[2];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@item_bom_no:" + item_bom_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "SR", pParam);

            return dt;
        }

        internal DataTable SelectBomDetailData(string item_cd, string item_bom_no, string gubun)
        {
            string sSpName = "SP_PackingBomFull2";
            string[] pParam = new string[2];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@item_bom_no:" + item_bom_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal string BomTRX(Prescription bomData)
        {
            string sSpName = "SP_PackingBomFull2";
            string res = "";

            if (bomData.pGubun.Equals("ID") || bomData.pGubun.Equals("UID"))
            {
                string[] pParam = new string[11];

                pParam[0] = "@item_cd:" + bomData.item_cd;
                pParam[1] = "@item_bom_no:" + bomData.item_bom_no;
                pParam[2] = "@item_bom_seq:" + bomData.item_bom_seq;
                pParam[3] = "@process_cd:" + bomData.process_cd;
                pParam[4] = "@item_bom_child_cd:" + bomData.item_bom_child_cd;
                pParam[5] = "@item_bom_tea_unit:" + "mg";
                pParam[6] = "@item_bom_batch_qty:" + bomData.item_bom_batch_qty;
                pParam[7] = "@item_bom_standard_qty:" + bomData.item_bom_standard_qty;
                pParam[8] = "@item_bom_batch_unit:" + bomData.item_bom_batch_unit;
                pParam[9] = "@item_bom_use_rate:" + bomData.item_bom_use_rate;
                pParam[10] = "@remark:" + bomData.remark;

                res = _bllSpExecute.SpExecuteString(sSpName, bomData.pGubun, pParam);
            }
            else if (bomData.pGubun.Equals("UD"))
            {
                string[] pParam = new string[15];

                pParam[0] = "@item_cd:" + bomData.item_cd;
                pParam[1] = "@item_bom_no:" + bomData.item_bom_no;
                pParam[2] = "@item_bom_id:" + bomData.item_bom_id;
                pParam[3] = "@item_bom_seq:" + bomData.item_bom_seq;
                pParam[4] = "@process_cd:" + bomData.process_cd;
                pParam[5] = "@item_bom_child_cd:" + bomData.item_bom_child_cd;
                pParam[6] = "@item_bom_child_type:" + bomData.item_bom_child_type;
                pParam[7] = "@item_case_cd:" + bomData.item_case_cd;
                pParam[8] = "@item_bom_tea_qty:" + bomData.item_bom_tea_qty;
                pParam[9] = "@item_bom_tea_unit:" + bomData.item_bom_tea_unit;
                pParam[10] = "@item_bom_batch_qty:" + bomData.item_bom_batch_qty;
                pParam[11] = "@item_bom_standard_qty:" + bomData.item_bom_standard_qty;
                pParam[12] = "@item_bom_batch_unit:" + bomData.item_bom_batch_unit;
                pParam[13] = "@item_bom_use_rate:" + bomData.item_bom_use_rate;
                pParam[14] = "@remark:" + bomData.remark;

                res = _bllSpExecute.SpExecuteString(sSpName, bomData.pGubun, pParam);
            }
            else if (bomData.pGubun.Equals("DD"))
            {
                string[] pParam = new string[3];

                pParam[0] = "@item_cd:" + bomData.item_cd;
                pParam[1] = "@item_bom_no:" + bomData.item_bom_no;
                pParam[2] = "@item_bom_id:" + bomData.item_bom_id;

                res = _bllSpExecute.SpExecuteString(sSpName, bomData.pGubun, pParam);
            }

            return res;
        }

        //개정에 대한 대장이 있는지 확인
        internal string CheckWorkOrder(string item_cd, string revision_no)
        {
            string sSpName = "SP_StandardItemGuide2";
            string sGubun = "CHECK";

            string[] pParam = new string[2];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@revision_no:" + revision_no;


            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

    }
}