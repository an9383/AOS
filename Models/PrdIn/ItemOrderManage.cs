using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrdIn
{
    public class ItemOrderManage
    {
        public string gubun { get; set; }

        /// <summary>
        /// 주문번호
        /// </summary>
        public string order_no { get; set; }

        /// <summary>
        /// 주문일자
        /// </summary>
        public string order_date { get; set; }

        /// <summary>
        /// 구분
        /// </summary>
        public string order_gb { get; set; }

        /// <summary>
        /// 영업사원코드
        /// </summary>
        public string sales_emp_cd { get; set; }

        /// <summary>
        /// 거래처코드
        /// </summary>
        public string cust_cd { get; set; }

        /// <summary>
        /// ?
        /// </summary>
        public string pass_cust_cd { get; set; }

        /// <summary>
        /// 입력자
        /// </summary>
        public string insert_user_cd { get; set; }

        /// <summary>
        /// 비고
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 주문순서?
        /// </summary>
        public string order_seq { get; set; }

        /// <summary>
        /// 주문 품목코드
        /// </summary>
        public string order_item_cd { get; set; }

        /// <summary>
        /// 수량
        /// </summary>
        public string order_qty { get; set; }

        /// <summary>
        /// 요구납기일자
        /// </summary>
        public string order_require_date { get; set; }

        /// <summary>
        /// 단가
        /// </summary>
        public string order_price { get; set; }

        /// <summary>
        /// 주문상태
        /// </summary>
        public string order_status { get; set; }

        /// <summary>
        /// 외주금액
        /// </summary>
        public string outsource_amt { get; set; }

        /// <summary>
        /// 주문 우선도
        /// </summary>
        public string order_priority { get; set; }

        //검색조건
        public string search_date_S { get; set; } //'0'=주문일자/'1'=요구납기일자
        public string start_date_S { get; set; }
        public string end_date_S { get; set; }
        public string item_cd_S { get; set; }
        public string item_nm_S { get; set; }
        public string order_state_S { get; set; }

        //vender update
        public string commercial_personnel { get; set; }
        public string vender_phone { get; set; }
        public string vender_fax { get; set; }
        public string vender_cd { get; set; }
    }
}