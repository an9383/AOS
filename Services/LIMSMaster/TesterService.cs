using HACCP.Libs.Database;
using HACCP.Models.LIMSMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HACCP.Services.LIMSMaster
{
    public class TesterService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable TesterSelect(Tester.SrchParam param)
        {
            string sSpName = "SP_Tester";

            string[] pParam = new string[2];
            pParam[0] = "@tester_type:" + param.tester_type;
            pParam[1] = "@s_tester_cd:" + param.tester_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable TesterSelectParameter(string tester_cd)
        {
            string sSpName = "sp_Tester_parameter";

            string[] pParam = new string[1];
            pParam[0] = "@tester_cd:" + tester_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable TesterSelectTesterImage(string tester_cd)
        {
            string sSpName = "SP_Tester";

            string[] pParam = new string[1];
            pParam[0] = "@tester_cd:" + tester_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "P", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable table = new DataTable();
            table.Columns.Add("tester_image", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                if (row["tester_image"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["tester_image"]);
                    string url = "data:Image/png;base64," + str;
                    _row["tester_image"] = url;
                }
                else
                {
                    _row["tester_image"] = "/Content/image/defaultSign.png";
                }

                table.Rows.Add(_row);
            }

            return table;
        }

        internal DataTable TesterSelectTestItem(string tester_cd)
        {
            string sSpName = "SP_Tester";

            string[] pParam = new string[1];
            pParam[0] = "@tester_cd:" + tester_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string TesterTRX(Tester tester)
        {
            string sSpName = "SP_Tester";
            string[] param = null;

            switch (tester.gubun)
            {
                case "I":
                case "U":
                    param = GetParam(tester);

                    break;

                case "D":

                    param = new string[1];

                    param[0] = "@tester_cd:" + tester.tester_cd;

                    break;
            }

            string res = _bllSpExecute.SpExecuteString(sSpName, tester.gubun, param);

            return res;
        }

        private string[] GetParam(Tester tester)
        {
            string[] param = new string[63];

            param[0] = "@tester_cd:" + tester.tester_cd;
            param[1] = "@tester_nm:" + tester.tester_nm;
            param[2] = "@tester_enm:" + tester.tester_enm;
            param[3] = "@tester_item_nm:" + tester.tester_item_nm;
            param[4] = "@tester_model_num:" + tester.tester_model_num;
            param[5] = "@tester_serial_num:" + tester.tester_serial_num;
            param[6] = "@tester_manage_num:" + tester.tester_manage_num;
            param[7] = "@tester_volume:" + tester.tester_volume;
            param[8] = "@tester_property:" + tester.tester_property;
            param[9] = "@tester_gcd:" + tester.tester_gcd;
            param[10] = "@tester_response_emp:" + tester.tester_response_emp;
            param[11] = "@tester_buy_date:" + tester.tester_buy_date;
            param[12] = "@tester_buy_cust:" + tester.tester_buy_cust;
            param[13] = "@tester_buy_amt:" + tester.tester_buy_amt;
            param[14] = "@tester_prod_year:" + tester.tester_prod_year;
            param[15] = "@tester_prod_cust:" + tester.tester_prod_cust;
            param[16] = "@tester_prod_num:" + tester.tester_prod_num;
            param[17] = "@tester_install_date:" + tester.tester_install_date;
            param[18] = "@plant_cd:" + tester.plant_cd;
            param[19] = "@workroom_cd:" + tester.workroom_cd;
            param[20] = "@tester_manage_cust:" + tester.tester_manage_cust;
            param[21] = "@tester_work_date:" + tester.tester_work_date;
            param[22] = "@tester_disuse_date:" + tester.tester_disuse_date;
            param[23] = "@tester_use_gb:" + tester.tester_use_gb;
            param[24] = "@interface_cd:" + tester.interface_cd;
            param[25] = "@plc_node_no:" + tester.plc_node_no;
            param[26] = "@tester_mcd:" + tester.tester_mcd;
            param[27] = "@tester_type:" + tester.tester_type;
            param[28] = "@tester_period1:" + tester.tester_period1;
            param[29] = "@tester_period2:" + tester.tester_period2;
            param[30] = "@tester_period3:" + tester.tester_period3;
            param[31] = "@tester_period4:" + tester.tester_period4;
            param[32] = "@tester_period5:" + tester.tester_period5;
            param[33] = "@tester_long_period1:" + tester.tester_long_period1;
            param[34] = "@tester_long_period2:" + tester.tester_long_period2;
            param[35] = "@tester_long_period3:" + tester.tester_long_period3;
            param[36] = "@tester_long_period4:" + tester.tester_long_period4;
            param[37] = "@tester_long_period5:" + tester.tester_long_period5;
            param[38] = "@test_cnt:" + tester.test_cnt;
            param[39] = "@qc_inst_data1:" + tester.qc_inst_data1;
            param[40] = "@qc_inst_data2:" + tester.qc_inst_data2;
            param[41] = "@qc_inst_data3:" + tester.qc_inst_data3;
            param[42] = "@qc_inst_data4:" + tester.qc_inst_data4;
            param[43] = "@qc_inst_data5:" + tester.qc_inst_data5;
            param[44] = "@qc_inst_data6:" + tester.qc_inst_data6;
            param[45] = "@qc_inst_data7:" + tester.qc_inst_data7;
            param[46] = "@qc_inst_data8:" + tester.qc_inst_data8;
            param[47] = "@qc_inst_data9:" + tester.qc_inst_data9;
            param[48] = "@qc_inst_data10:" + tester.qc_inst_data10;
            param[49] = "@qc_inst_data11:" + tester.qc_inst_data11;
            param[50] = "@qc_inst_data12:" + tester.qc_inst_data12;
            param[51] = "@qc_inst_data13:" + tester.qc_inst_data13;
            param[52] = "@qc_inst_data14:" + tester.qc_inst_data14;
            param[53] = "@qc_inst_data15:" + tester.qc_inst_data15;
            param[54] = "@qc_inst_data16:" + tester.qc_inst_data16;
            param[55] = "@qc_inst_data17:" + tester.qc_inst_data17;
            param[56] = "@qc_inst_data18:" + tester.qc_inst_data18;
            param[57] = "@qc_inst_data19:" + tester.qc_inst_data19;
            param[58] = "@qc_inst_data20:" + tester.qc_inst_data20;
            param[59] = "@qc_inst_dis_text:" + tester.qc_inst_dis_text;
            param[60] = "@qc_inst_calc_text:" + tester.qc_inst_calc_text;
            param[61] = "@iq_data:" + tester.IQ_data;
            param[62] = "@oq_data:" + tester.OQ_data;

            return param;
        }

        internal string TesterParameterTRX(List<Tester.TesterParameter> testerParameter, string tester_cd)
        {
            string sSpName = "sp_Tester_parameter";
            string[] pParam = new string[3];
            string res = string.Empty;

            foreach (Tester.TesterParameter parameter in testerParameter)
            {
                pParam[0] = "@tester_cd:" + tester_cd;
                pParam[1] = "@tester_parameter_cd:" + parameter.tester_parameter_cd;
                pParam[2] = "@tester_parameter_nm:" + parameter.tester_parameter_nm;

                res = _bllSpExecute.SpExecuteString(sSpName, parameter.gubun, pParam);
            }

            return res;
        }

        internal void UploadTesterImage(byte[] myBytes, string tester_cd, string imageName)
        {
            string sSpName = "SP_Tester";

            string sGubun = "IU";

            string[] pParam = new string[1];
            pParam[0] = "@tester_cd:" + tester_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, "@" + imageName, myBytes, pParam);

            return;
        }

        internal string TestItemTRX(List<Tester.TestItem> testItems, string tester_cd)
        {
            string sSpName = "SP_Tester";
            string[] pParam = new string[2];
            string res = string.Empty;

            foreach (Tester.TestItem testItem in testItems)
            {
                string gubun = string.Empty;

                if (testItem.testitem_use_yn.Equals("true"))
                {
                    gubun = "IT";
                }
                else
                {
                    gubun = "DT";
                }

                pParam[0] = "@tester_cd:" + tester_cd;
                pParam[1] = "@testitem_cd:" + testItem.testitem_cd;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);
            }

            return res;
        }
    }
}