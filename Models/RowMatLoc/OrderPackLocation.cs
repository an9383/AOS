using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.RowMatLoc
{
    public class OrderPackLocation
    {
        /// <summary>
        /// 포장지시번호
        /// </summary>
        public string order_no { get; set; }

        /// <summary>
        /// 품목코드
        /// </summary>
        public string item_cd { get; set; }

        /// <summary>
        /// 품목명
        /// </summary>
        public string item_nm { get; set; }

        /// <summary>
        /// 단위
        /// </summary>
        public string item_packunit { get; set; }

        /// <summary>
        /// 제조번호
        /// </summary>
        public string lot_no { get; set; }

        /// <summary>
        /// 지시수량
        /// </summary>
        public string order_qty { get; set; }

        /// <summary>
        /// 지시일자
        /// </summary>
        public string input_order_date { get; set; }

        /// <summary>
        /// 상태
        /// </summary>
        public string input_order_status { get; set; }

        /// <summary>
        /// 상태 명
        /// </summary>
        public string input_order_status_nm { get; set; }

        /// <summary>
        /// 단위 (아래 그리드)
        /// </summary>
        public string data_type { get; set; }

        /// <summary>
        /// 시험번호
        /// </summary>
        public string test_no { get; set; }

        /// <summary>
        /// 재고량
        /// </summary>
        public string remain_qty { get; set; }

        /// <summary>
        /// 위치
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// 검색 시작일
        /// </summary>
        public string s_sdate { get; set; }

        /// <summary>
        /// 검색 종료일
        /// </summary>
        public string s_edate { get; set; }

        /// <summary>
        /// 검색 input_order_status
        /// </summary>
        public string s_out_status { get; set; }

        /// <summary>
        /// 상태 (DB 테이블)
        /// </summary>
        public string material_status { get; set; }

    }
}
