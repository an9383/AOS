using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.RowMatLoc
{
    public class CellStackStatus
    {
        //cell_column	cell_height_1	item_gubun_1

        /// <summary>
        /// Cell Column
        /// </summary>
        public string cell_column { get; set; }

        /// <summary>
        /// Cell 높이 (1층)
        /// </summary>
        public string cell_height_1 { get; set; }

        /// <summary>
        /// 아이템 구분 (1층)
        /// </summary>
        public string item_gubun_1 { get; set; }

        /// <summary>
        /// Cell 높이 (2층)
        /// </summary>
        public string cell_height_2 { get; set; }

        /// <summary>
        /// 아이템 구분 (2층)
        /// </summary>
        public string item_gubun_2 { get; set; }

        /// <summary>
        /// Cell 높이 (3층)
        /// </summary>
        public string cell_height_3 { get; set; }

        /// <summary>
        /// 아이템 구분 (3층)
        /// </summary>
        public string item_gubun_3 { get; set; }

        /// <summary>
        /// Cell 높이 (4층)
        /// </summary>
        public string cell_height_4 { get; set; }

        /// <summary>
        /// 아이템 구분 (4층)
        /// </summary>
        public string item_gubun_4 { get; set; }

        /// <summary>
        /// Cell 높이 (5층)
        /// </summary>
        public string cell_height_5 { get; set; }

        /// <summary>
        /// 아이템 구분 (5층)
        /// </summary>
        public string item_gubun_5 { get; set; }

        /// <summary>
        /// Cell 높이 (6층)
        /// </summary>
        public string cell_height_6 { get; set; }

        /// <summary>
        /// 아이템 구분 (6층)
        /// </summary>
        public string item_gubun_6 { get; set; }

        /// <summary>
        /// Cell 높이 (7층)
        /// </summary>
        public string cell_height_7 { get; set; }

        /// <summary>
        /// 아이템 구분 (7층)
        /// </summary>
        public string item_gubun_7 { get; set; }

        /// <summary>
        /// Cell 높이 (8층)
        /// </summary>
        public string cell_height_8 { get; set; }

        /// <summary>
        /// 아이템 구분 (8층)
        /// </summary>
        public string item_gubun_8 { get; set; }

        /// <summary>
        /// 창고코드
        /// </summary>
        public string workroom_cd { get; set; }

        /// <summary>
        /// 창고명
        /// </summary>
        public string workroom_nm { get; set; }

        /// <summary>
        /// 구역코드
        /// </summary>
        public string zone_cd { get; set; }

        /// <summary>
        /// 구역명
        /// </summary>
        public string zone_nm { get; set; }

        /// <summary>
        /// 셀코드
        /// </summary>
        public string cell_cd { get; set; }

        /// <summary>
        /// 셀명
        /// </summary>
        public string cell_nm { get; set; }

        /// <summary>
        /// 랙 명
        /// </summary>
        public string cell_isle { get; set; }

        /// <summary>
        /// 랙 높이
        /// </summary>
        public string max_cell_height { get; set; }


        public string cell_height { get; set; }				

        /// <summary>
        /// 품목코드
        /// </summary>
        public string item_cd { get; set; }

        /// <summary>
        /// 품목명
        /// </summary>
        public string item_nm { get; set; }

        /// <summary>
        /// 제조번호
        /// </summary>
        public string lot_no { get; set; }

        /// <summary>
        /// 수량
        /// </summary>
        public string item_qty { get; set; }

        /// <summary>
        /// 시험의뢰번호
        /// </summary>
        public string receipt_qc_no1 { get; set; }

        /// <summary>
        /// 시험번호
        /// </summary>
        public string receipt_qc_no3 { get; set; }

        /// <summary>
        /// 입고일자
        /// </summary>
        public string receipt_time { get; set; }

    }
}
