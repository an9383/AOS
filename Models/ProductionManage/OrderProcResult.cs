using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.ProductionManage
{
	public class OrderProcResult
	{
		//property > 조회결과SET 
		public string level { get; set; }
		public string pkey { get; set; }
		public string interface_field { get; set; }
		public string mykey { get; set; }
		public string mp_ck { get; set; }
		public string order_no { get; set; }
		public string m_order_no { get; set; }
		public string lot_no { get; set; }
		public string order_proc_id { get; set; }
		public string order_detailproc_id { get; set; }
		public string item_cd { get; set; }
		public string revision_no { get; set; }
		public string process_cd { get; set; }
		public string detailproc_cd { get; set; }
		public string code { get; set; }
		public string name { get; set; }
		public string order_proc_qc_ck { get; set; }
		public string test_type { get; set; }
		public string test_standby_yn { get; set; }
		public string order_proc_transfer_ck { get; set; }
		public string order_detailproc_ck { get; set; }
		public string order_proc_worker1 { get; set; }
		public string order_proc_worker2 { get; set; }
		public string order_proc_inspector { get; set; }
		public string order_detailproc_input_type { get; set; }
		public string seq { get; set; }
		public string order_proc_keep_method { get; set; }
		public string order_proc_make_status { get; set; }
		public string order_proc_qt_status { get; set; }
		public string order_proc_transfer_status { get; set; }
		public string order_detailproc_status { get; set; }
		public string workroom_cd { get; set; }
		public string workroom_nm { get; set; }
		public string order_proc_warehouse { get; set; }
		public string order_proc_warehouse_nm { get; set; }
		public string ord1 { get; set; }
		public string ord2 { get; set; }
		public string treefirstimageindex { get; set; }
		public string order_proc_remark { get; set; }
		public string testrequest_no { get; set; }
		public string order_proc_manage_rate_min { get; set; }
		public string order_proc_manage_rate_max { get; set; }
		public string order_proc_receive_time { get; set; }
		public string order_proc_enter_time { get; set; }
		public string item_nm { get; set; }
		public string pack_order_no { get; set; }
		public string weighing_yn { get; set; }
		public string weighing_end_time { get; set; }
		public string outsource { get; set; }
		public string trade_cd2 { get; set; }
		public string process_type { get; set; }
		public string order_batch_size_unit { get; set; }
		public string order_proc_content { get; set; }
		public string start_date { get; set; }
		public string order_qty { get; set; }
		public string work_order_qty { get; set; }
		public string order_proc_rate { get; set; }
		public string rate_yn { get; set; }
		public string weighing_yn2 { get; set; }
		public string item_content_unit { get; set; }
		public string item_pack_size { get; set; }
		public string qc_yn { get; set; }
		public string item_packunit2 { get; set; }
		public string packing_result_yn { get; set; }
		public string sale_item_cd { get; set; }
		public string item_type2 { get; set; }
		public string revision_id { get; set; }
		public string revision_id_p { get; set; }
		public string item_type { get; set; }
		public string order_proc_worker1_nm { get; set; }
		public string order_proc_worker2_nm { get; set; }
		public string order_proc_worker3_nm { get; set; }
		public string order_status { get; set; }
		public string sign_yn { get; set; }

		// default 생성자 
		public OrderProcResult()
		{
		}

		// DTO 설정
		public OrderProcResult(DataRow row)
		{
			level = row["level"].ToString();
			pkey = row["pkey"].ToString();
			interface_field = row["interface_field"].ToString();
			mykey = row["mykey"].ToString();
			mp_ck = row["mp_ck"].ToString();
			order_no = row["order_no"].ToString();
			m_order_no = row["m_order_no"].ToString();
			lot_no = row["lot_no"].ToString();
			order_proc_id = row["order_proc_id"].ToString();
			order_detailproc_id = row["order_detailproc_id"].ToString();
			item_cd = row["item_cd"].ToString();
			revision_no = row["revision_no"].ToString();
			process_cd = row["process_cd"].ToString();
			detailproc_cd = row["detailproc_cd"].ToString();
			code = row["code"].ToString();
			name = row["name"].ToString();
			order_proc_qc_ck = row["order_proc_qc_ck"].ToString();
			test_type = row["test_type"].ToString();
			test_standby_yn = row["test_standby_yn"].ToString();
			order_proc_transfer_ck = row["order_proc_transfer_ck"].ToString();
			order_detailproc_ck = row["order_detailproc_ck"].ToString();
			order_proc_worker1 = row["order_proc_worker1"].ToString();
			order_proc_worker2 = row["order_proc_worker2"].ToString();
			order_proc_inspector = row["order_proc_inspector"].ToString();
			order_detailproc_input_type = row["order_detailproc_input_type"].ToString();
			seq = row["seq"].ToString();
			order_proc_keep_method = row["order_proc_keep_method"].ToString();
			order_proc_make_status = row["order_proc_make_status"].ToString();
			order_proc_qt_status = row["order_proc_qt_status"].ToString();
			order_proc_transfer_status = row["order_proc_transfer_status"].ToString();
			order_detailproc_status = row["order_detailproc_status"].ToString();
			workroom_cd = row["workroom_cd"].ToString();
			workroom_nm = row["workroom_nm"].ToString();
			order_proc_warehouse = row["order_proc_warehouse"].ToString();
			order_proc_warehouse_nm = row["order_proc_warehouse_nm"].ToString();
			ord1 = row["ord1"].ToString();
			ord2 = row["ord2"].ToString();
			treefirstimageindex = row["treefirstimageindex"].ToString();
			order_proc_remark = row["order_proc_remark"].ToString();
			testrequest_no = row["testrequest_no"].ToString();
			order_proc_manage_rate_min = row["order_proc_manage_rate_min"].ToString();
			order_proc_manage_rate_max = row["order_proc_manage_rate_max"].ToString();
			order_proc_receive_time = row["order_proc_receive_time"].ToString();
			order_proc_enter_time = row["order_proc_enter_time"].ToString();
			item_nm = row["item_nm"].ToString();
			pack_order_no = row["pack_order_no"].ToString();
			weighing_yn = row["weighing_yn"].ToString();
			weighing_end_time = row["weighing_end_time"].ToString();
			outsource = row["outsource"].ToString();
			trade_cd2 = row["trade_cd2"].ToString();
			process_type = row["process_type"].ToString();
			order_batch_size_unit = row["order_batch_size_unit"].ToString();
			order_proc_content = row["order_proc_content"].ToString();
			start_date = row["start_date"].ToString();
			order_qty = row["order_qty"].ToString();
			work_order_qty = row["work_order_qty"].ToString();
			order_proc_rate = row["order_proc_rate"].ToString();
			rate_yn = row["rate_yn"].ToString();
			weighing_yn2 = row["weighing_yn2"].ToString();
			item_content_unit = row["item_content_unit"].ToString();
			item_pack_size = row["item_pack_size"].ToString();
			qc_yn = row["qc_yn"].ToString();
			item_packunit2 = row["item_packunit2"].ToString();
			packing_result_yn = row["packing_result_yn"].ToString();
			sale_item_cd = row["sale_item_cd"].ToString();
			item_type2 = row["item_type2"].ToString();
			revision_id = row["revision_id"].ToString();
			revision_id_p = row["revision_id_p"].ToString();
			item_type = row["item_type"].ToString();
			order_proc_worker1_nm = row["order_proc_worker1_nm"].ToString();
			order_proc_worker2_nm = row["order_proc_worker2_nm"].ToString();
			order_proc_worker3_nm = row["order_proc_worker3_nm"].ToString();
			order_status = row["order_status"].ToString();
			sign_yn = row["sign_yn"].ToString();
		}
	}

	public class OrderProcResultForSearch
	{
		public string process_cd { get; set; }
		public string sdate { get; set; }
		public string edate { get; set; }
		public string order_no { get; set; }
		public string item_cd { get; set; }
		public string order_status { get; set; }
		public string lot_no { get; set; }
		public string trade_cd3 { get; set; }
		public string outsource_YN { get; set; }

	}


	public class OrderProcResultForInsert1
	{
		public string order_no { get; set; }
		public string order_proc_id { get; set; }
		public string order_qty { get; set; }
		public string order_ea_qty { get; set; }
		public string order_proc_real_qty { get; set; }
		public string order_proc_real_ea_qty { get; set; }
		public string order_proc_sample_qty { get; set; }
		public string order_proc_sample_ea_qty { get; set; }
		public string order_proc_content { get; set; }
		public string receive_date { get; set; }
		public string receive_time { get; set; }
		public string order_enter_date { get; set; }
		public string enter_time { get; set; }
		public string order_proc_rate { get; set; }
		public string m_order_no { get; set; }
		public string test_deposit_qty { get; set; }
		public string customer_sample_qty { get; set; }
		public string rate_type { get; set; }
		public string process_cd { get; set; }

	}

	public class OrderProcResultForInsert2
	{
		public string m_order_no { get; set; }
		public string item_cd { get; set; }
		public string qcquest_date { get; set; }
		public string request_date { get; set; }
		public string qcquest_end_date { get; set; }
		public string result_hope_date { get; set; }
		public string item_form_cd { get; set; }
		public string order_proc_id { get; set; }
		public string worker { get; set; }
		public string process_cd { get; set; }
		public string packing_order_no { get; set; }
		public string order_no { get; set; }
		public string order_proc_qt_status { get; set; }
		public string order_proc_transfer_status { get; set; }
		public string mp_ck { get; set; }
		public string testrequest_no { get; set; }
		public string test_no { get; set; }
		public string request_no { get; set; }
		public string pack_cnt { get; set; }
		public string request_time { get; set; }

	}

	public class OrderProcResultAddPack
	{
		public string receipt_date { get; set; }
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string test_no { get; set; }
		public string pack_barcode { get; set; }
		public string remain_qty { get; set; }
		public string material_qty { get; set; }
		public string add_qty { get; set; }
		public string return_qty { get; set; }
		public string packing_order_id { get; set; }
	}

	public class OrderProcResultWorkerEquipment
	{
		public string order_no { get; set; }
		public string order_proc_id { get; set; }
		public string ew_flag { get; set; }
		public string result_seq { get; set; }
		public string order_gubun { get; set; }
		public string order_proc_sdate { get; set; }
		public string order_proc_stime { get; set; }
		public string order_proc_edate { get; set; }
		public string order_proc_etime { get; set; }
		public string total_time { get; set; }
		public string normal_time { get; set; }
		public string added_time { get; set; }
		public string night_time { get; set; }
		public string special_time { get; set; }
		public string ew_cd { get; set; }
		public string ew_nm { get; set; }
		public string ew_enm { get; set; }
		public string workroom_cd { get; set; }
		public string status { get; set; }
		public string worker_cnt { get; set; }
		public string gubun { get; set; }
	}

	public class OrderProcResultMaterial
	{
		public string use_qty { get; set; }
		public string disuse_qty { get; set; }
		public string return_qty { get; set; }
		public string return_issue_emp_cd { get; set; }
		public string packing_order_id { get; set; }
	}
}