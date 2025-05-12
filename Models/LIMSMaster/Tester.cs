using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.LIMSMaster
{
	public class Tester
	{
		public class SrchParam
		{
			public string tester_type { get; set; }
			public string tester_cd { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.tester_type = "%";
				this.tester_cd = "";
			}
		}

		public class TesterParameter
		{
			public string gubun { get; set; }
			public string tester_cd { get; set; }
			public string tester_parameter_cd { get; set; }
			public string tester_parameter_nm { get; set; }
		}

		public class TestItem
		{
			public string testitem_cd { get; set; }
			public string testitem_nm { get; set; }
			public string testitem_use_yn { get; set; }
		}

		public SrchParam srch;
		public TesterParameter testerParameter;
		public TestItem testItem;

		public string gubun { get; set; }
		public string tester_cd { get; set; }
		public string tester_nm { get; set; }
		public string tester_enm { get; set; }
		public string tester_item_nm { get; set; }
		public string tester_model_num { get; set; }
		public string tester_serial_num { get; set; }
		public string tester_manage_num { get; set; }
		public string tester_volume { get; set; }
		public string tester_property { get; set; }
		public string tester_gcd { get; set; }
		public string tester_response_emp { get; set; }
		public string tester_buy_date { get; set; }
		public string tester_buy_cust { get; set; }
		public string tester_buy_amt { get; set; }
		public string tester_prod_year { get; set; }
		public string tester_prod_cust { get; set; }
		public string tester_prod_num { get; set; }
		public string tester_install_date { get; set; }
		public string plant_cd { get; set; }
		public string workroom_cd { get; set; }
		public string tester_manage_cust { get; set; }
		public string tester_work_date { get; set; }
		public string tester_disuse_date { get; set; }
		public string tester_use_gb { get; set; }
		public string interface_cd { get; set; }
		public string plc_node_no { get; set; }
		public string tester_mcd { get; set; }
		public string tester_type { get; set; }
		public string tester_period1 { get; set; }
		public string tester_period2 { get; set; }
		public string tester_period3 { get; set; }
		public string tester_period4 { get; set; }
		public string tester_period5 { get; set; }
		public string tester_long_period1 { get; set; }
		public string tester_long_period2 { get; set; }
		public string tester_long_period3 { get; set; }
		public string tester_long_period4 { get; set; }
		public string tester_image { get; set; }
		public string tester_long_period5 { get; set; }
		public string interface_nm { get; set; }
		public string tester_type_nm { get; set; }
		public string tester_response_emp_nm { get; set; }
		public string workroom_nm { get; set; }
		public string plant_nm { get; set; }
		public string tester_mcd_nm { get; set; }
		public string qc_inst_data1 { get; set; }
		public string qc_inst_data2 { get; set; }
		public string qc_inst_data3 { get; set; }
		public string qc_inst_data4 { get; set; }
		public string qc_inst_data5 { get; set; }
		public string qc_inst_data6 { get; set; }
		public string qc_inst_data7 { get; set; }
		public string qc_inst_data8 { get; set; }
		public string qc_inst_data9 { get; set; }
		public string qc_inst_data10 { get; set; }
		public string qc_inst_data11 { get; set; }
		public string qc_inst_data12 { get; set; }
		public string qc_inst_data13 { get; set; }
		public string qc_inst_data14 { get; set; }
		public string qc_inst_data15 { get; set; }
		public string qc_inst_data16 { get; set; }
		public string qc_inst_data17 { get; set; }
		public string qc_inst_data18 { get; set; }
		public string qc_inst_data19 { get; set; }
		public string qc_inst_data20 { get; set; }
		public string qc_inst_dis_text { get; set; }
		public string qc_inst_calc_text { get; set; }
		public string IQ_data { get; set; }
		public string OQ_data { get; set; }
		public string test_cnt { get; set; }
	}
}