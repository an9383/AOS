using System.Data;

public class ProcessExamMaster
{
	/// <summary> 
	/// 1. 화면 검색 Parameter CLass 
	/// </summary> 
	public class SrchParam
	{
		/// <summary> 
		/// SP 검색구분 
		/// </summary> 
		public string Gubun { get; set; }
		/// <summary> 
		/// Param1 
		/// </summary> 
		public string Param1 { get; set; }
		/// <summary> 
		/// Param2 
		/// </summary> 
		public string Param2 { get; set; }

		// default 검색 생성자 
		public SrchParam()
		{
			this.Gubun = "S";
			this.Param1 = "S";
			this.Param2 = "S";
		}
	}

	// 2. 검색파라미터 property
	public SrchParam srch;

	// 3. 조회결과SET 
	/// <summary> 
	/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
	/// </summary> 
	public string row_status { get; set; }
	public string ccp_cd { get; set; }
	public string ccp_nm { get; set; }
	public string ccp_description { get; set; }
	public string process_exam_manu { get; set; }
	public string process_exam_qc { get; set; }
	public string equip_cd { get; set; }
	public string insert_emp { get; set; }
	public string insert_time { get; set; }
	public string update_emp { get; set; }
	public string update_time { get; set; }
	public string grand_item { get; set; }
	public string middle_item { get; set; }
	public string sys_plant_cd { get; set; }
	public string audittrail_id { get; set; }
	public string cause_harm { get; set; }
	public string limit_standard { get; set; }
	public string element_harm { get; set; }
	public string monitoring_way { get; set; }

	// 4. default 생성자 
	public ProcessExamMaster()
	{
	}

	// 5. DTO 설정
	public ProcessExamMaster(DataRow row)
	{
		row_status = row["row_status"].ToString();
		ccp_cd = row["ccp_cd"].ToString();
		ccp_nm = row["ccp_nm"].ToString();
		ccp_description = row["ccp_description"].ToString();
		process_exam_manu = row["process_exam_manu"].ToString();
		process_exam_qc = row["process_exam_qc"].ToString();
		equip_cd = row["equip_cd"].ToString();
		insert_emp = row["insert_emp"].ToString();
		insert_time = row["insert_time"].ToString();
		update_emp = row["update_emp"].ToString();
		update_time = row["update_time"].ToString();
		grand_item = row["grand_item"].ToString();
		middle_item = row["middle_item"].ToString();
		sys_plant_cd = row["sys_plant_cd"].ToString();
		audittrail_id = row["audittrail_id"].ToString();
		cause_harm = row["cause_harm"].ToString();
		limit_standard = row["limit_standard"].ToString();
		element_harm = row["element_harm"].ToString();
		monitoring_way = row["monitoring_way"].ToString();
	}
}

