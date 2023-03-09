using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.RowMatln
{
    public class ReceiptCheckSOP
    {
        //SP 구분자
        public string ReceiptCheckSOP_pGubun { get; set; }
        public string gubun { get; set; }

        //검색조건
        public string item_gb { get; set; }

        //SP 결과값들
        public string receipt_check_id { get; set; }
        public string receipt_check_seq { get; set; }
        public string receipt_check_contents { get; set; }
        public string insert_user_cd { get; set; }
        public string update_user_cd { get; set; }

        public ReceiptCheckSOP()
        {

        }

        public ReceiptCheckSOP(DataRow row)
        {
            this.item_gb = row["item_gb"].ToString();
            this.receipt_check_id = row["receipt_check_id"].ToString();
            this.receipt_check_seq = row["receipt_check_seq"].ToString();
            this.receipt_check_contents = row["receipt_check_contents"].ToString();
            this.insert_user_cd = row["insert_user_cd"].ToString();
            this.update_user_cd = row["update_user_cd"].ToString();
        }
    }
}
