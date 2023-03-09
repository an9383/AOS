using HACCP.Libs.Database;
using HACCP.Models.Cp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Cp
{
    public class ClaimReceiptService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        //불만사항 리스트 조회
        public List<ClaimReceipt> GridSelect(ClaimReceipt.SrchParam srch)
        {
            string sSpName = "SP_ClaimReceipt";

            string[] pParam = new String[6];
            pParam[0] = "@select_S:" + srch.select_S;
            pParam[1] = "@Sdate_S:" + srch.Sdate_S;
            pParam[2] = "@Edate_S:" + srch.Edate_S;
            pParam[3] = "@select_gubun_S:" + srch.select_gubun_S;
            pParam[4] = "@searchtext_S:" + srch.searchtext_S;
            pParam[5] = "@claim_status_S:" + srch.claim_status_S;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S", pParam);

            List<ClaimReceipt> claimReceiptList = new List<ClaimReceipt>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                ClaimReceipt claimReceipt = new ClaimReceipt(row);
                claimReceiptList.Add(claimReceipt);
            }

            return claimReceiptList;
        }

        //불만사항 등록
        public string GridInsert(ClaimReceipt ClaimReceipt)
        {
            string sSpName = "SP_ClaimReceipt";

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, "I", GetParam(ClaimReceipt));

            return message;
        }

        //불만사항 수정
        public string GridUpdate(ClaimReceipt ClaimReceipt)
        {
            string sSpName = "SP_ClaimReceipt";

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, "U", GetParam(ClaimReceipt));

            return message;
        }

        //불만사항 삭제
        public string GridDelete(string claim_id)
        {
            string sSpName = "SP_ClaimReceipt";

            string[] pParam = new string[1];
            pParam[0] = "@claim_id:" + claim_id;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, "D", pParam);

            return message;
        }

        public string[] GetParam(ClaimReceipt claimReceipt)
        {
            string[] param = new string[4];
            param[0] = "@claim_id:" + claimReceipt.claim_id;
            param[1] = "@receive_date:" + claimReceipt.receive_date;
            param[2] = "@receive_emp_cd:" + claimReceipt.receive_emp_cd;
            param[3] = "@claim_title:" + claimReceipt.claim_title;
            return param;
            
        }

    }
}