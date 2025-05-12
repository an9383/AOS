using HACCP.Libs.Database;
using HACCP.Models.QualityControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.QualityControl
{
    public class PickingCompleteEService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_PickingCompleteE";
        private string program_cd = "PickingCompleteE";
        private string sign_set_cd = "1004";

        internal DataTable PickingCompleteESelect(PickingCompleteE.SrchParam srchParam)
        {
            string[] pParam = new string[8];
            pParam[0] = "@selectdate:" + srchParam.selectdate;
            pParam[1] = "@sdate:" + srchParam.de_Sdate;
            pParam[2] = "@edate:" + srchParam.de_Edate;
            pParam[3] = "@test_type:" + srchParam.test_type;
            pParam[4] = "@sample_status:" + srchParam.test_status;
            pParam[5] = "@search_gubun:" + srchParam.search_gubun;
            pParam[6] = "@search_number:" + srchParam.search_number;
            pParam[7] = "@item_form_cd:" + srchParam.item_form_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable PickingCompleteESelectSign(string testcontrol_id, string test_type, string process_kind)
        {
                       

            string[] pParam = new string[4];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@process_kind:" + process_kind;
            pParam[3] = "@program_cd:" + "PickingCompleteE";

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_Test_ES_Manage", "SE", pParam);

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

        internal DataTable PickingCompleteESelectFile(string testcontrol_id)
        {
            string[] pParam = new string[1];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_AttachFile", "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string PickingCompleteETRX(PickingCompleteE dto)
        {
            string[] pParam = null;

            switch (dto.gubun)
            {
                case "I":
                case "U":
                    pParam = GetParam(dto);
                    break;

                case "D":

                    pParam = new string[3];

                    pParam[0] = "@testcontrol_id:" + dto.testcontrol_id;
                    pParam[1] = "@program_cd:" + program_cd;
                    pParam[2] = "@test_type:" + dto.test_type;

                    break;
            }

            string res = _bllSpExecute.SpExecuteString(sSpName, dto.gubun, pParam);

            return res;
        }

        private string[] GetParam(PickingCompleteE dto)
        {
            string stability_sampling_no_value = "";

            //[완제품인 경우에만 데이터 사용]            

            //if ("6".Equals(testRequest.test_type) ) { }
            //    stability_sampling_no_value = (testRequest.stability_sample_qty > 0 ? testRequest.stability_sam : "");
            //}
            

            string[] pParam = new string[18];

            pParam[0] = "@picking_date:" + dto.picking_date;
            pParam[1] = "@picking_emp_cd:" + dto.picking_emp_cd;
            pParam[2] = "@picking_qty:" + dto.picking_qty;
            pParam[3] = "@picking_qty_unit:" + dto.picking_qty_unit;
            pParam[4] = "@picking_workroom_cd:" + dto.picking_workroom_cd;

            pParam[5] = "@picking_workroom_nm:" + dto.picking_workroom_nm;
            pParam[6] = "@picking_method:" + dto.picking_method;
            pParam[7] = "@picking_sop1:" + dto.picking_sop1;
            pParam[8] = "@picking_sop2:" + dto.picking_sop2;
            pParam[9] = "@testcontrol_id:" + dto.testcontrol_id;
            pParam[10] = "@test_sample_qty:" + dto.test_sample_qty;
            pParam[11] = "@deposit_sample_qty:" + dto.deposit_sample_qty;
            pParam[12] = "@stability_sample_qty:" + dto.stability_sample_qty;
            pParam[13] = "@valid_period:" + dto.valid_period;
            pParam[14] = "@pack_sampling_qty:" + dto.pack_sampling_qty;
            pParam[15] = "@stability_sampling_no:" + stability_sampling_no_value;
            pParam[16] = "@sample_microbe_qty:" + dto.sample_microbe_qty;
            pParam[17] = "@container_material:" + dto.container_material;                      

            return pParam;
        }

        internal string PickingCompleteELabelCnt(string testcontrol_id, string test_type, string report_cd, string print_num)
        {
            string[] param = new string[5];

            param[0] = "@testcontrol_id:" + testcontrol_id;
            param[1] = "@test_type:" + test_type;
            param[2] = "@report_cd:" + report_cd;
            param[3] = "@emp_cd:" + HttpContext.Current.Session["LoginCD"];
            param[4] = "@print_page_count:" + print_num;

            string res = _bllSpExecute.SpExecuteString("SP_Test_Rpt_Manage", "UR", param);

            return res;
        }

        internal DataTable PickingCompleteEPackSamplingSelect(string testcontrol_id)
        {
            string[] pParam = new string[1];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_PackSamplingWindow", "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string PickingCompleteEPackSamplingSave(List<PickingCompleteE.PackSampling> packSampling, string testcontrol_id)
        {
            string[] param = new string[6];
            string res = "";

            foreach (PickingCompleteE.PackSampling pack in packSampling)
            {
                param[0] = "@testcontrol_id:" + testcontrol_id;
                param[1] = "@receipt_no:" + pack.receipt_no;
                param[2] = "@receipt_id:" + pack.receipt_id;
                param[3] = "@receipt_pack_seq:" + pack.receipt_pack_seq;
                param[4] = "@sampling_yn:" + ((!String.IsNullOrEmpty(pack.sampling_yn) && (pack.sampling_yn.Equals("Y") || pack.sampling_yn.Equals("true"))) ? "Y" : "N");
                param[5] = "@sampling_qty:" + pack.sampling_qty;

                res = _bllSpExecute.SpExecuteString("SP_PackSamplingWindow", "U", param);
            }

            return res;
        }
    }
}