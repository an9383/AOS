using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.ProductionMaster
{
    public class SetProcExam
    {
		//공정검사 설정
		public string pGubun { get; set; }
		public string mp_ck { get; set; }
		public string item_cd { get; set; }
		public string revision_no { get; set; }
		public string batch_size { get; set; }
		public string process_cd { get; set; }
		public string exam_cd { get; set; }                     //@process_exam_cd				조회된 항목명과 파라미터에서 사용된 명칭이 서로 다름

		public string process_exam_standard { get; set; }       //@item_process_exam_standard	조회된 항목명과 파라미터에서 사용된 명칭이 서로 다름
		public string process_exam_min { get; set; }            //@item_process_exam_min		조회된 항목명과 파라미터에서 사용된 명칭이 서로 다름
		public string process_exam_max { get; set; }            //@item_process_exam_max		조회된 항목명과 파라미터에서 사용된 명칭이 서로 다름
		public string process_exam_period { get; set; }         //@item_process_exam_period		조회된 항목명과 파라미터에서 사용된 명칭이 서로 다름
		public string process_exam_qty { get; set; }            //@item_process_exam_qty		조회된 항목명과 파라미터에서 사용된 명칭이 서로 다름
		public string exam_remark { get; set; }
		public string item_process_exam_start { get; set; }
		public string item_process_grand_item { get; set; }
		public string item_process_middle_item { get; set; }
		public string item_process_exam_unit { get; set; }
		public string item_process_item_seq { get; set; }

		public string o_application_ck { get; set; }
		public string application_ck { get; set; }

	}
}