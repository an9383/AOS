using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrdLoc
{
    public class LocationItemSelect
    {												

        /// <summary>
        /// ID
        /// </summary>
        public string item_stock_id { get; set; }

        /// <summary>
        /// item_nm / item_cd
        /// </summary>
        public string item { get; set; }

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
        /// 단위
        /// </summary>
        public string item_pack_size { get; set; }

        /// <summary>
        /// 팔레트바코드
        /// </summary>
        public string box_barcode_no { get; set; }

        /// <summary>
        /// 재고량
        /// </summary>
        public string stock_qty { get; set; }

        /// <summary>
        /// 위치
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// receipt_status
        /// </summary>
        public string receipt_status { get; set; }

        /// <summary>
        /// 입고예정량
        /// </summary>
        public string prod_qty_plan { get; set; }

        /// <summary>
        /// 일반/보관 구분
        /// </summary>
        public string item_gubun { get; set; }

        /// <summary>
        /// 구분
        /// </summary>
        public string item_gubun_nm { get; set; }

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

        /// <summary>
        /// 랙(셀) 명
        /// </summary>
        public string cell_isle { get; set; }
    }
}