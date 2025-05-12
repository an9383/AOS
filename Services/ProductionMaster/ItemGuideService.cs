using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.ProductionMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.ProductionMaster
{
    public class ItemGuideService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal List<Item> SelectItem(string item_cd, string item_nm)
        {
            string sSpName = "CODEHELP";
            string strsql = "SELECT item_cd, item_nm FROM v_item_makingstandard WHERE ("
                + "item_cd  LIKE '%" + item_cd + "%' OR "
                + "item_nm LIKE '%" + item_nm + "%') AND "
                + "item_cd  LIKE '%%' AND item_nm  LIKE '%%' ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, strsql);

            List<Item> items = new List<Item>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                Item item = new Item(row, "");

                items.Add(item);
            }

            return items;
        }

        internal DataTable selectItemDetail(string pSpName, string pGubun, string pItemCd, string pBatchSize, string pRevisionNo)
        {
            string sSpName = pSpName;
            string[] pParam = new string[3];
            pParam[0] = "@item_cd:" + pItemCd;
            pParam[1] = "@batch_size:" + pBatchSize;
            pParam[2] = "@revision_no:" + pRevisionNo;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            //DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable GetWorkRooms(string viewName)
        {
            string strsql = "SELECT workroom_cd, workroom_nm, common_part_nm FROM ";
            strsql += viewName;
            strsql += " WHERE (workroom_cd  LIKE '%%' OR workroom_nm  LIKE '%%') AND workroom_cd  LIKE '%%' AND workroom_nm  LIKE '%%' ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable GetMaterials(string pSpName)
        {
            string strsql = "SELECT";
            strsql += " item_cd";
            strsql += ", item_nm";
            strsql += ", trade_nm";
            strsql += ", trade_cd";
            strsql += ", item_type3_nm";
            strsql += ", item_type3 ";
            strsql += "FROM  v_item_material3";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable GetEquip(string pSpName)
        {
            string strsql = "SELECT";
            strsql += " equip_cd";
            strsql += ", equip_nm";
            strsql += " FROM  equipment";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }


        internal DataTable GetCcp(string pSpName)
        {
            string strsql = "SELECT '' as ccp_cd, '' as ccp_nm, '' as ccp_description";
            strsql += " UNION";
            strsql += " SELECT";
            strsql += " ccp_cd, ccp_nm, ccp_description";
            strsql += " FROM  process_exam_master";
            strsql += " ORDER BY ccp_cd";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }


        internal DataTable selectBatchSize(string pSpName, string pGubun, string pItemCd)
        {
            string sSpName = pSpName;
            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + pItemCd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, pGubun, pParam);

            return dt;
        }

        internal DataTable selectSignInfo(string pSpName, string pGubun, string pSignSet, string pItemCd, string pMpCk, string pRevisionNo)
        {
            string sSpName = pSpName;
            string[] pParam = new string[4];
            pParam[0] = "@item_cd:" + pItemCd;
            pParam[1] = "@sign_set_cd:" + pSignSet;
            pParam[2] = "@mp_ck:" + pMpCk;
            pParam[3] = "@revision_no:" + pRevisionNo;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

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

        internal DataTable selectProcessInfo(string pSpName, string pGubun, string pItemCd, string pRevisionNo, string pBatchSize)
        {
            string sSpName = pSpName;
            string[] pParam = new string[3];
            pParam[0] = "@item_cd:" + pItemCd;
            pParam[1] = "@revision_no:" + pRevisionNo;
            pParam[2] = "@batch_size:" + pBatchSize;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal List<DataTable> selectItemProcess(string pSpName, string pGubun, string pItemCd)
        {
            string sSpName = pSpName;
            List<DataTable> dtList = new List<DataTable>();

            if (pGubun.Equals("S4"))
            {
                DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun);

                if (ds != null)
                {
                    dtList.Add(ds.Tables[0]);
                    dtList.Add(ds.Tables[2]);
                }
            }
            else if (pGubun.Equals("S10"))
            {
                string[] pParam = new string[1];
                pParam[0] = "@item_cd:" + pItemCd;

                DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

                if (ds != null)
                {
                    dtList.Add(ds.Tables[0]);
                    dtList.Add(ds.Tables[1]);
                }
            }

            return dtList;
        }

        internal DataTable selectprescriptionInfo(string pSpName, string pGubun, string pItemCd, string pBatchSize, string pRevisionNo)
        {
            string sSpName = pSpName;
            string[] pParam = new string[3];
            pParam[0] = "@item_cd:" + pItemCd;
            pParam[1] = "@batch_size:" + pBatchSize;
            pParam[2] = "@item_bom_no:" + pRevisionNo;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable selectRevisionData(string pSpName, string pItemCd, bool pLatestRevisionCk, bool pRevisionSignEndCk, bool pRevisionCurrentCk)
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

            if (pLatestRevisionCk)
            {
                strsql += " inner join (";
                strsql += " select item_cd as item_cd, max(revision_no) as revision_no ";
                strsql += " from item_process_revision ";
                strsql += " where revision_sign_end_ck = ";

                if(pRevisionSignEndCk)
                    strsql += "'Y'";
                else
                    strsql += "'N'";

                strsql += " and mp_ck = 'M'";
                strsql += " group by item_cd) c on c.revision_no = a.revision_no and c.item_cd = a.item_cd";
            }

            if (pRevisionSignEndCk)
            {
                strsql += " WHERE a.revision_sign_end_ck = ";
                strsql += "'Y'";
                strsql += " and a.sys_plant_cd = ";
                strsql += ("'"+Public_Function.selectedPLANT+ "'");
            }
            else
            {
                strsql += " WHERE a.revision_sign_end_ck = ";
                strsql += "'N'";
                strsql += " and a.sys_plant_cd = ";
                strsql += ("'" + Public_Function.selectedPLANT + "'");
            }

            strsql += " and a.mp_ck = 'M'";

            if (pRevisionCurrentCk)
            {
                strsql += " and a.revision_current_ck = 'Y'";
            }

            strsql += " ORDER BY item_cd, revision_no";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal string ItemGuideUseChk(string item_cd, string revision_no, string batch_size)
        {
            string sSpName = "SP_StandardItemGuide";
            string[] pParam = new string[4];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[2] = "@revision_no:" + revision_no;
            pParam[1] = "@batch_size:" + batch_size;
            pParam[3] = "@revision_current_ck:" + "Y";

            string res = _bllSpExecute.SpExecuteString(sSpName, "YN", pParam);

            return res;
        }

        internal int CRUD(RevisionData revisionData)
        {
            if (revisionData.gubun.Equals("C"))
            {
                string sSpName = "SP_StandardItemGuide";
                string[] pParam = new string[3];

                pParam[0] = "@item_cd:" + revisionData.item_cd;
                pParam[1] = "@batch_size:" + revisionData.batch_size;
                pParam[2] = "@revision_num:" + revisionData.revision_num;
               

                string count = _bllSpExecute.SpExecuteString(sSpName, "S11", pParam);

                if(Int32.Parse(count) > 0)
                {
                    return 1;
                }

                pParam = GetParam(revisionData);
                pParam[1] = "@revision_no:" + "NEW";

                string revision_no = "";

                try
                {
                    revision_no = _bllSpExecute.SpExecuteString(sSpName, revisionData.gubun, pParam);
                }
                catch
                {
                    return 2;
                }

                pParam[1] = "@revision_no:" + revision_no;

                DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "U2", pParam);
            } else if (revisionData.gubun.Equals("U0"))
            {
                string sSpName = "SP_StandardItemGuide";

                string[] param = new string[15];

                param[0] = "@item_cd:" + revisionData.item_cd;
                param[1] = "@revision_no:" + revisionData.revision_no;
                param[2] = "@batch_size:" + revisionData.batch_size;
                param[3] = "@revision_num:" + revisionData.revision_num;
                param[4] = "@revision_use_date:" + revisionData.revision_use_date;
                param[5] = "@revision_remark:" + revisionData.revision_remark;
                param[6] = "@mbr_type:" + revisionData.mbr_type;
                param[7] = "@export_words:" + revisionData.export_words;
                param[8] = "@batch_weight:" + revisionData.batch_weight;
                param[9] = "@batch_size_sub:" + revisionData.batch_size_sub;
                param[10] = "@permit_standard_qty:" + revisionData.permit_standard_qty;
                param[11] = "@permit_standard_qty_unit:" + revisionData.permit_standard_qty_unit;
                param[12] = "@revision_special_mention:" + revisionData.revision_special_mention;
                param[13] = "@t_making_dept_cd:" + revisionData.t_making_dept_cd;
                param[14] = "@t_making_dept_cd2:" + revisionData.t_making_dept_cd2;

                DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, revisionData.gubun, param);
            }

            return 0;
        }

        internal void SignCRUD(string gubun, string c_seq, string emp_cd, string item_cd, string revision_no, string batch_size, string validation_type)
        {
            string sSpName = "SP_StandardItemGuide";

            DataTable dt = new DataTable();

            if (gubun.Equals("U"))
            {
                string[] pParam = new string[2];
                pParam[0] = "@c_seq:" + c_seq;
                pParam[1] = "@emp_cd:" + emp_cd;
                string str = _bllSpExecute.SpExecuteString(sSpName, "SR", pParam);

                pParam = new string[6];
                pParam[0] = "@item_cd:" + item_cd;
                pParam[1] = "@revision_no:" + revision_no;
                pParam[2] = "@batch_size:" + batch_size;
                pParam[3] = "@emp_cd:" + emp_cd;
                pParam[4] = "@validation_type:" + validation_type;
                pParam[5] = "@type:" + c_seq;

                dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);
            }
            else if (gubun.Equals("U3"))
            {
                string[] pParam = new string[6];
                pParam[0] = "@item_cd:" + item_cd;
                pParam[1] = "@revision_no:" + revision_no;
                pParam[2] = "@batch_size:" + batch_size;
                pParam[3] = "@emp_cd:" + emp_cd;
                pParam[4] = "@validation_type:" + validation_type;
                pParam[5] = "@type:" + c_seq;

                dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);
            }

        }

        internal void finalApproval(string gubun, string item_cd, string batch_size, string revision_no, string revision_sign_end_state, string current_ck)
        {
            string sSpName = "SP_StandardItemGuide";
            string[] pParam = new string[5];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@batch_size:" + batch_size;
            pParam[2] = "@revision_no:" + revision_no;
            pParam[3] = "@revision_sign_end_state:" + revision_sign_end_state;
            pParam[4] = "@revision_current_ck:" + current_ck;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

        }

        internal void moveProcess(Models.ProductionMaster.Process process, string type)
        {
            string sSpName = "SP_StandardItemGuide";

            string[] pParam;

            if (type.Equals("Standard"))
            {
                pParam = new string[5];
                pParam[0] = "@item_cd:" + process.item_cd;
                pParam[1] = "@revision_no:" + process.revision_no;
                pParam[2] = "@batch_size:" + process.batch_size;
                pParam[3] = "@process_cd:" + process.process_cd;
                pParam[4] = "@process_seq:" + process.process_seq;

                DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, process.pGubun, pParam);
            } else if (type.Equals("Detail"))
            {
                pParam = new string[6];
                pParam[0] = "@item_cd:" + process.item_cd;
                pParam[1] = "@revision_no:" + process.revision_no;
                pParam[2] = "@batch_size:" + process.batch_size;
                pParam[3] = "@process_seq:" + process.process_seq;
                pParam[4] = "@process_cd:" + process.process_cd;
                pParam[5] = "@standard_process_cd:" + process.standard_process_cd;

                DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, process.pGubun, pParam);
            }
        }

        internal DataTable copyProcessData(Models.ProductionMaster.Process process)
        {
            string sSpName = "SP_StandardItemGuide";
            string[] pParam = new string[15];
            pParam[0] = "@item_cd:" + process.item_cd;
            pParam[1] = "@t_item_cd:" + process.t_item_cd;
            pParam[2] = "@t_making_dept_cd:" + process.t_making_dept_cd;
            pParam[3] = "@t_making_dept_cd2:" + process.t_making_dept_cd2;
            pParam[4] = "@new_revision_no:" + process.new_revision_no;
            pParam[5] = "@revision_no:" + process.revision_no;
            pParam[6] = "@batch_size:" + process.batch_size;
            pParam[7] = "@mbr_type:" + process.mbr_type;
            pParam[8] = "@export_words:" + process.export_words;
            pParam[9] = "@batch_weight:" + process.batch_weight;
            pParam[10] = "@batch_size_unit:" + process.batch_size_unit;
            pParam[11] = "@batch_size_sub:" + process.batch_size_sub;
            pParam[12] = "@permit_standard_qty:" + process.permit_standard_qty;
            pParam[13] = "@permit_standard_qty_unit:" + process.permit_standard_qty_unit;
            pParam[14] = "@s_batch_size:" + process.s_batch_size;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, process.pGubun, pParam);

            return dt;
        }

        internal void CRUD(Models.ProductionMaster.Process process)
        {

            if (process.process_cd == null)
            {
                process.process_cd = "";
            }

            if (process.item_proc_qc_ck != null)
            {
                if (process.item_proc_qc_ck.Equals("true"))
                {
                    process.item_proc_qc_ck = "Y";
                }
                else if (process.item_proc_qc_ck.Equals("false"))
                {
                    process.item_proc_qc_ck = "N";

                }

            }
            else
            {
                process.item_proc_qc_ck = "N";
            }

            if (process.item_proc_transfer_ck != null)
            {
                if (process.item_proc_transfer_ck.Equals("true"))
                {
                    process.item_proc_transfer_ck = "Y";
                }
                else if (process.item_proc_transfer_ck.Equals("false"))
                {
                    process.item_proc_transfer_ck = "N";
                }
            }
            else
            {
                process.item_proc_transfer_ck = "N";
            }

            string sSpName = "";
            string[] pParam = new string[5];
            pParam[0] = "@item_cd:" + process.item_cd;
            pParam[1] = "@revision_no:" + process.revision_no;
            pParam[2] = "@batch_size:" + process.batch_size;
            pParam[3] = "@process_cd:" + process.process_cd;

            if (process.pGubun.Equals("MC1") || process.pGubun.Equals("MC4"))
            {
                sSpName = "SP_StandardItemGuide";
                pParam[4] = "@startingkey:" + process.startingkey;
            }else if (process.pGubun.Equals("D"))
            {
                sSpName = "SP_Item_Process";
                pParam[4] = "@mp_ck:M";
            }
            else if (process.pGubun.Equals("U"))
            {
                sSpName = "SP_Item_Process";
                pParam = GetParam(process);

                pParam[26] = "@mp_ck:M";
            }

            string str = _bllSpExecute.SpExecuteString(sSpName, process.pGubun, pParam);

            //if (process.ccpChanged)
            //{
            //    pParam = new string[5];
            //    pParam[0] = "@item_cd:" + process.item_cd;
            //    pParam[1] = "@revision_no:" + process.revision_no;
            //    pParam[2] = "@batch_size:" + process.batch_size;
            //    pParam[3] = "@process_cd:" + process.process_cd;
            //    pParam[4] = "@ccp_cd:" + process.ccp_cd;

            //    _bllSpExecute.SpExecuteString("SP_Item_Workguide", "IAX", pParam);
            //}
        }

        internal DataTable selectPrescriptionData(string pSpName, string pGubun, string pItemCd, string pItemBomNo, string pBatchSize)
        {
            string sSpName = pSpName;
            string[] pParam = new string[3];
            pParam[0] = "@item_cd:" + pItemCd;
            pParam[1] = "@item_bom_no:" + pItemBomNo;
            pParam[2] = "@batch_size:" + pBatchSize;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable selectItemBOMData(string pSpName, string pGubun, string pItemCd, string pBatchSize, string pRevisionNo)
        {
            string sSpName = pSpName;
            string[] pParam = new string[3];
            pParam[0] = "@item_cd:" + pItemCd;
            pParam[1] = "@batch_size:" + pBatchSize;
            pParam[2] = "@revision_no:" + pRevisionNo;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal bool CRUD(Prescription prescription)
        {
            string sSpName = "SP_ItemBomFull2";
            string sGubun = prescription.pGubun;
            string[] pParam;

            if ((!String.IsNullOrEmpty(prescription.sum_ck)) && (prescription.sum_ck.Equals("true") || prescription.sum_ck.Equals("Y")))
            {
                prescription.sum_ck = "Y";
            }
            else
            {
                prescription.sum_ck = "";
            }

            if ((!String.IsNullOrEmpty(prescription.item_bom_last_type)) && (prescription.item_bom_last_type.Equals("true") || prescription.item_bom_last_type.Equals("Y")))
            {
                prescription.item_bom_last_type = "Y";
            }
            else
            {
                prescription.item_bom_last_type = "";
            }

            if (prescription.pGubun.Equals("DD"))
            {
                pParam = new string[5];
                pParam[0] = "@item_cd:" + prescription.item_cd;
                pParam[1] = "@batch_size:" + prescription.batch_size;
                pParam[2] = "@item_bom_no:" + prescription.item_bom_no;
                pParam[3] = "@item_bom_id:" + prescription.item_bom_id;
                pParam[4] = "@mp_ck:M";
            }
            else
            {
                pParam = GetParam(prescription);
            }

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return (!message.Equals("") ? true : false);
        }

        internal string ItemGuideDeleteAll(string mp_ck, string item_cd, string batch_size, string item_bom_no)
        {
            string res = "";

            if (mp_ck.Equals("M"))
            {
                string[] pParam = new string[4];
                pParam[0] = "@mp_ck:" + mp_ck;
                pParam[1] = "@item_cd:" + item_cd;
                pParam[2] = "@batch_size:" + batch_size;
                pParam[3] = "@item_bom_no:" + item_bom_no;

                res = _bllSpExecute.SpExecuteString("SP_ItemBomFull2", "Delete_All", pParam);
            }
            else if (mp_ck.Equals("P"))
            {
                string[] pParam = new string[3];
                pParam[0] = "@mp_ck:" + mp_ck;
                pParam[1] = "@item_cd:" + item_cd;
                pParam[2] = "@item_bom_no:" + item_bom_no;

                res = _bllSpExecute.SpExecuteString("SP_PackingBomFull2", "Delete_All", pParam);
            }

            return res;
        }

        private string[] GetParam(Prescription prescription)
        {
            string[] param = new string[33];

            param[0] = "@mp_ck:M";
            param[1] = "@item_cd:" + prescription.item_cd;
            param[2] = "@batch_size:" + prescription.batch_size;
            param[3] = "@item_bom_no:" + prescription.item_bom_no;
            param[4] = "@item_bom_seq:" + prescription.item_bom_seq;
            param[5] = "@item_bom_child_cd:" + prescription.item_bom_child_cd;
            param[6] = "@item_bom_child_type:" + prescription.item_bom_child_type;
            param[7] = "@item_case_cd:" + prescription.item_case_cd;
            param[8] = "@item_bom_tea_qty:" + prescription.item_bom_tea_qty;
            param[9] = "@item_bom_tea_unit:" + prescription.item_bom_tea_unit;
            param[10] = "@item_bom_batch_qty:" + prescription.item_bom_batch_qty;
            param[11] = "@item_bom_standard_qty:" + prescription.item_bom_standard_qty;
            param[12] = "@item_bom_batch_unit:" + prescription.item_bom_batch_unit;
            param[13] = "@item_bom_standard_ucl_qty:" + prescription.item_bom_standard_ucl_qty;
            param[14] = "@item_bom_use_rate:" + prescription.item_bom_use_rate;
            param[15] = "@item_bom_calc_type:" + prescription.item_bom_calc_type;
            param[16] = "@item_bom_last_type:" + prescription.item_bom_last_type;
            param[17] = "@process_cd:" + prescription.process_cd;
            param[18] = "@remark:" + prescription.remark;
            param[19] = "@sum_ck:" + prescription.sum_ck;
            param[20] = "@item_bom_liquid_material_yn:" + prescription.item_bom_liquid_material_yn;
            param[21] = "@item_bom_density:" + prescription.item_bom_density;
            param[22] = "@item_bom_rounding:" + prescription.item_bom_rounding;
            param[23] = "@min_titer:" + prescription.min_titer;
            param[24] = "@item_bom_standard_rpt_qty:" + prescription.item_bom_standard_rpt_qty;
            param[25] = "@item_case_cd2:" + prescription.item_case_cd2;
            param[26] = "@material_percent:" + prescription.material_percent;
            param[27] = "@manufacture_percent:" + prescription.manufacture_percent;
            param[28] = "@item_bom_manufacture_qty:" + prescription.item_bom_manufacture_qty;
            param[29] = "@item_bom_specific_gravity:" + prescription.item_bom_specific_gravity;
            param[30] = "@item_bom_main_material_ck:" + prescription.item_bom_main_material_ck;
            param[31] = "@item_bom_id:" + prescription.item_bom_id;
            param[32] = "@target_process_cd:" + (prescription.target_process_cd == null ? "" : prescription.target_process_cd);

            return param;
        }

        private string[] GetParam(RevisionData revisionData)
        {
            string[] param = new string[17];

            param[0] = "@item_cd:" + revisionData.item_cd;
            param[1] = "@revision_no:" + revisionData.revision_no;
            param[2] = "@batch_size:" + revisionData.batch_size;
            param[3] = "@batch_size_unit:" + revisionData.batch_size_unit;
            param[4] = "@revision_num:" + revisionData.revision_num;
            param[5] = "@revision_use_date:" + revisionData.revision_use_date;
            param[6] = "@revision_remark:" + revisionData.revision_remark;
            param[7] = "@mbr_type:" + revisionData.mbr_type;
            param[8] = "@export_words:" + revisionData.export_words;
            param[9] = "@batch_weight:" + revisionData.batch_weight;
            param[10] = "@batch_size_sub:" + revisionData.batch_size_sub;
            param[11] = "@permit_standard_qty:" + revisionData.permit_standard_qty;
            param[12] = "@permit_standard_qty_unit:" + revisionData.permit_standard_qty_unit;
            param[13] = "@revision_special_mention:" + revisionData.revision_special_mention;
            param[14] = "@t_making_dept_cd:" + revisionData.t_making_dept_cd;
            param[15] = "@t_making_dept_cd2:" + revisionData.t_making_dept_cd2;
            param[16] = "@s_dept_cd:" + revisionData.s_dept_cd;

            return param;
        }

        private string[] GetParam(Models.ProductionMaster.Process process)
        {
            string[] pParam = new string[27];

            pParam[0] = "@item_cd:" + process.item_cd;
            pParam[1] = "@revision_no:" + process.revision_no;
            pParam[2] = "@batch_size:" + process.batch_size;
            pParam[3] = "@process_cd:" + process.process_cd;
            pParam[4] = "@item_proc_seq:" + process.item_proc_seq;
            pParam[5] = "@item_proc_qc_ck:" + process.item_proc_qc_ck;
            pParam[6] = "@test_type:" + process.test_type;
            pParam[7] = "@test_standby_yn:" + process.test_standby_yn;
            pParam[8] = "@item_proc_transfer_ck:" + process.item_proc_transfer_ck;
            pParam[9] = "@item_proc_worker1:" + process.item_proc_worker1;
            pParam[10] = "@item_proc_worker2:" + process.item_proc_worker2;
            pParam[11] = "@item_proc_inspector:" + process.item_proc_inspector;
            pParam[12] = "@item_proc_warehouse:" + process.item_proc_warehouse;
            pParam[13] = "@remark:" + process.remark;
            pParam[14] = "@item_proc_content:" + process.item_proc_content;
            pParam[15] = "@item_proc_standard_rate:" + process.item_proc_standard_rate;
            pParam[16] = "@item_proc_manage_rate:" + process.item_proc_manage_rate;
            pParam[17] = "@standard_proc_time:" + process.standard_proc_time;
            pParam[18] = "@workroom_cd:" + process.workroom_cd;
            pParam[19] = "@item_proc_manage_rate_max:" + process.item_proc_manage_rate_max;
            pParam[20] = "@item_proc_manage_rate_mIN:" + process.item_proc_manage_rate_mIN;
            pParam[21] = "@next_process_cd:" + process.next_process_cd;
            pParam[22] = "@item_proc_standard_weight_qty:" + process.item_proc_standard_weight_qty;
            pParam[23] = "@item_proc_standard_weight_unit:" + process.item_proc_standard_weight_unit;
            pParam[24] = "@ccp_cd:" + process.ccp_cd;
            pParam[25] = "@grouping_cd:" + process.grouping_cd;

            return pParam;
        }

        #region 공정검사 설정
        //공정검사 가져오기
        internal DataTable selectProcExam(string pSpName, string pGubun, string pItemCd, string pRevisionNo, string pBatchSize, string pProcess_cd)
        {
            string[] pParam = new string[4];
            pParam[0] = "@item_cd:" + pItemCd;
            pParam[1] = "@revision_no:" + pRevisionNo;
            pParam[2] = "@batch_size:" + pBatchSize;
            pParam[3] = "@process_cd:" + pProcess_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(pSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        //공정검사 수정
        internal bool CRUD(SetProcExam setProcExam)
        {
            string sSpName = "SP_Item_Workguide";
            string sGubun = setProcExam.pGubun;
            string[] pParam;

            pParam = GetParam(setProcExam);

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return (!message.Equals("") ? true : false);
        }

        //공정검사 수정 : 포장
        internal bool ItemGuide2CRUD(SetProcExam setProcExam)
        {
            string sSpName = "SP_Item_Workguide2";
            string sGubun = setProcExam.pGubun;
            string[] pParam;

            pParam = GetParam(setProcExam);

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return (!message.Equals("") ? true : false);
        }

        //공정검사 파라미터
        private string[] GetParam(SetProcExam setProcExam)
        {
            string[] pParam = new string[17];

            pParam[0] = "@item_cd:" + setProcExam.item_cd;
            pParam[1] = "@revision_no:" + setProcExam.revision_no;
            pParam[2] = "@batch_size:" + setProcExam.batch_size;
            pParam[3] = "@process_cd:" + setProcExam.process_cd;

            pParam[4] = "@process_exam_cd:" + setProcExam.exam_cd;
            pParam[5] = "@item_process_exam_standard:" + setProcExam.process_exam_standard;
            pParam[6] = "@item_process_exam_min:" + setProcExam.process_exam_min;
            pParam[7] = "@item_process_exam_max:" + setProcExam.process_exam_max;
            pParam[8] = "@item_process_exam_period:" + setProcExam.process_exam_period;
            pParam[9] = "@item_process_exam_qty:" + setProcExam.process_exam_qty;
            pParam[10] = "@exam_remark:" + setProcExam.exam_remark;
            pParam[11] = "@item_process_exam_start:" + setProcExam.item_process_exam_start;
            pParam[12] = "@item_process_grand_item:" + setProcExam.item_process_grand_item;
            pParam[13] = "@item_process_middle_item:" + setProcExam.item_process_middle_item;
            pParam[14] = "@item_process_exam_unit:" + setProcExam.item_process_exam_unit;
            pParam[15] = "@item_process_item_seq:" + setProcExam.item_process_item_seq;

            pParam[16] = "@mp_ck:" + setProcExam.mp_ck;

            return pParam;
        }
        #endregion

        #region 작업방법
        //작업방법 가져오기
        internal DataTable selectWorkGuide(string pSpName, string pGubun, string pItemCd, string pRevisionNo, string pBatchSize, string pProcess_cd)
        {
            string[] pParam = new string[4];
            pParam[0] = "@item_cd:" + pItemCd;
            pParam[1] = "@revision_no:" + pRevisionNo;
            pParam[2] = "@batch_size:" + pBatchSize;
            pParam[3] = "@process_cd:" + pProcess_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(pSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        //작업방법 추가
        internal bool CRUD(ItemWorkGuide itemWorkGuide)
        {
            string sSpName = "SP_Item_Workguide";
            string sGubun = itemWorkGuide.pGubun;
            string[] pParam;

            pParam = GetParam(itemWorkGuide);

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return (!message.Equals("") ? true : false);
        }

        //작업방법 추가 : 포장
        internal bool ItemGuide2CRUD(ItemWorkGuide itemWorkGuide)
        {
            string sSpName = "SP_Item_Workguide2";
            string sGubun = itemWorkGuide.pGubun;
            string[] pParam;

            pParam = GetParam(itemWorkGuide);

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return (!message.Equals("") ? true : false);
        }


        // 작업방법 복사 팝업
        internal DataTable SelectWorkGuide(string mp_ck)
        {
            string sSpName = "SP_Item_Workguide";
            string pGubun = "PS";
            string pParam = "@mp_ck:" + mp_ck;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        //작업방법 파라미터
        private string[] GetParam(ItemWorkGuide itemWorkGuide)
        {
            string[] pParam = new string[22];

            pParam[0] = "@item_cd:" + itemWorkGuide.item_cd;
            pParam[1] = "@revision_no:" + itemWorkGuide.revision_no;
            pParam[2] = "@batch_size:" + itemWorkGuide.batch_size;
            pParam[3] = "@process_cd:" + itemWorkGuide.process_cd;

            pParam[4] = "@detailproc_id:" + itemWorkGuide.detailproc_id;
            pParam[5] = "@workguide_id:" + itemWorkGuide.workguide_id;
            pParam[6] = "@workguide_seq:" + itemWorkGuide.workguide_seq;
            pParam[7] = "@detailproc_cd:" + itemWorkGuide.detailproc_cd;
            pParam[8] = "@workguide_method:" + itemWorkGuide.workguide_method;
            pParam[9] = "@workguide_data_type:" + itemWorkGuide.workguide_data_type;
            pParam[10] = "@workguide_min_manage:" + itemWorkGuide.workguide_min_manage;
            pParam[11] = "@workguide_max_manage:" + itemWorkGuide.workguide_max_manage;
            pParam[12] = "@workguide_min_permit:" + itemWorkGuide.workguide_min_permit;
            pParam[13] = "@workguide_max_permit:" + itemWorkGuide.workguide_max_permit;
            pParam[14] = "@equip_cd:" + itemWorkGuide.equip_cd;
            pParam[15] = "@workguide_sub_item:" + itemWorkGuide.workguide_sub_item;
            pParam[16] = "@equip_parameter:" + itemWorkGuide.equip_parameter;
            pParam[17] = "@workguide_es_yn:" + itemWorkGuide.workguide_es_yn;
            pParam[18] = "@workguide_unnecessary_yn:" + itemWorkGuide.workguide_unnecessary_yn;
            pParam[19] = "@decimal_cnt:" + itemWorkGuide.decimal_cnt;

            if (itemWorkGuide.proc_rep_yn != null && (itemWorkGuide.proc_rep_yn.Equals("true") || itemWorkGuide.proc_rep_yn.Equals("Y")))
            {
                pParam[20] = "@proc_rep_yn:Y";
            }
            else
            {
                pParam[20] = "@proc_rep_yn:N";
            }

            if (itemWorkGuide.check_proc != null && (itemWorkGuide.check_proc.Equals("true") || itemWorkGuide.check_proc.Equals("Y")))
            {
                pParam[21] = "@check_proc:Y";
            }
            else
            {
                pParam[21] = "@check_proc:N";
            }

            return pParam;
        }
        #endregion



        /// <summary>
        ///  <para>공정 정보를 다이어그램으로 가지고 오기 위한 DataSet 조회</para>
        /// </summary>
        /// <param name="item_cd"></param>
        /// <param name="revision_no"></param>
        /// <param name="order_type"></param>
        /// <returns></returns>
        internal DataSet GetProcessInfomationSet(string item_cd, string revision_no, string order_type)
        {
            string sSpName = "SP_StandardItemGuide";
            string sGubun = "PIS";
            string[] pParam = new string[3];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@revision_no:" + revision_no;
            pParam[2] = "@mp_ck:" + order_type;


            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            return ds;
        }

        //개정에 대한 대장이 있는지 확인
        internal string CheckWorkOrder(string item_cd, string revision_no)
        {
            string sSpName = "SP_StandardItemGuide";
            string sGubun = "CHECK";

            string[] pParam = new string[2];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@revision_no:" + revision_no;


            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }



    }
}

