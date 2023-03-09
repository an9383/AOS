using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.RowMatLoc
{
    public class StackManage
    {
        /// <summary>
        /// 입고번호
        /// </summary>
        public string receipt_no { get; set; }

        /// <summary>
        /// 입고순번
        /// </summary>
        public string receipt_id { get; set; }

        /// <summary>
        /// 시험번호
        /// </summary>
        public string test_no { get; set; }

        /// <summary>
        /// 품목코드
        /// </summary>
        public string item_cd { get; set; }

        /// <summary>
        /// 품목명
        /// </summary>
        public string item_nm { get; set; }

        /// <summary>
        /// 팩단위
        /// </summary>
        public string receipt_pack_unit { get; set; }

        /// <summary>
        /// 원자재 구분
        /// </summary>
        public string item_gb { get; set; }

        /// <summary>
        /// 바코드
        /// </summary>
        public string receipt_pack_barcode { get; set; }

        /// <summary>
        /// 입고량
        /// </summary>
        public string receipt_pack_qty { get; set; }

        /// <summary>
        /// 팩 재고량
        /// </summary>
        public string receipt_pack_remain_qty { get; set; }

        /// <summary>
        /// 팩 상태
        /// </summary>
        public string receipt_pack_status { get; set; }

        /// <summary>
        /// 체크 상태
        /// </summary>
        public string chk { get; set; }

        /// <summary>
        /// 창고 코드
        /// </summary>
        public string workroom_cd { get; set; }

        /// <summary>
        /// 창고명
        /// </summary>
        public string workroom_nm { get; set; }

        /// <summary>
        /// 구역 코드
        /// </summary>
        public string zone_cd { get; set; }

        /// <summary>
        /// 구역명
        /// </summary>
        public string zone_nm { get; set; }

        /// <summary>
        /// 셀 코드
        /// </summary>
        public string cell_cd { get; set; }

        /// <summary>
        /// 셀명
        /// </summary>
        public string cell_nm { get; set; }

        /// <summary>
        /// 팔렛트 코드
        /// </summary>
        public string pallet_cd { get; set; }

        /// <summary>
        /// 팔렛트 명
        /// </summary>
        public string pallet_nm { get; set; }
    }
}
