using HACCP.Libs.Database;
using HACCP.Models.Cp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Cp
{
    public class ClaimRequestService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        //불만사항 리스트 조회
        public List<ClaimRequest> GridSelect(ClaimRequest.SrchParam srch)
        {
            string sSpName = "SP_ClaimRequest_Y";

            string[] pParam = new String[6];
            pParam[0] = "@select_S:" + srch.select_S;
            pParam[1] = "@Sdate_S:" + srch.Sdate_S;
            pParam[2] = "@Edate_S:" + srch.Edate_S;
            pParam[3] = "@select_gubun_S:" + srch.select_gubun_S;
            pParam[4] = "@searchtext_S:" + srch.searchtext_S;
            pParam[5] = "@claim_status_S:" + srch.claim_status_S;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S", pParam);

            List<ClaimRequest> claimRequestList = new List<ClaimRequest>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                ClaimRequest claimRequest = new ClaimRequest(row);
                claimRequestList.Add(claimRequest);
            }

            return claimRequestList;
        }

        //불만사항 등록
        public string GridInsert(ClaimRequest claimRequest)
        {
            string sSpName = "SP_ClaimRequest_Y";

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, "I", GetParam(claimRequest, "I"));

            return message;
        }

        //불만사항 수정
        public string GridUpdate(ClaimRequest claimRequest)
        {
            string sSpName = "SP_ClaimRequest_Y";

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, "U", GetParam(claimRequest, "U"));

            return message;
        }

        //불만사항 삭제
        public string GridDelete(string claim_id)
        {
            string sSpName = "SP_ClaimRequest_Y";

            string[] pParam = new string[1];
            pParam[0] = "@claim_id:" + claim_id;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, "D", pParam);

            return message;
        }

        //불만번호 생성
        public string GetNo()
        {
            string sSpName = "SP_ClaimRequest_Y";

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, "MN");

            return message;
        }

        //불만번호 중복 체크
        public string CheckNo(string claim_no)
        {
            string sSpName = "SP_ClaimRequest_Y";

            string[] pParam = new string[1];
            pParam[0] = "@claim_no:" + claim_no;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, "NO_C", pParam);

            return message;
        }

        //리포트 or 프린트
        public void PreviewOrPrint(string claim_id)
        {
            string sSpName = "SP_ClaimRequest_Y";

            string[] pParam = new string[1];
            pParam[0] = "@claim_id:" + claim_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "SR", pParam);
            dt.TableName = "ClaimRequestDB2";

        }

        public string[] GetParam(ClaimRequest ClaimRequest, string state)
        {
            string[] param = new string[37];

            // 저장/수정시 사용되는 파라미터값
            if (state == "U")
            {
                param[0] = "@claim_id:" + ClaimRequest.claim_id;
            }
            else
            {
                param[0] = "@claim_id:" + "";
            }
            param[1] = "@claim_no:" + ClaimRequest.claim_no;
            param[2] = "@item_cd:" + ClaimRequest.item_cd;
            param[3] = "@lot_no:" + ClaimRequest.lot_no;
            param[4] = "@cust_cd:" + ClaimRequest.cust_cd;
            param[5] = "@cust_nm:" + ClaimRequest.cust_nm;
            param[6] = "@claim_date:" + ClaimRequest.claim_date;
            param[7] = "@claim_nm:" + ClaimRequest.claim_nm;
            param[8] = "@claim_content:" + ClaimRequest.claim_content;
            param[9] = "@claim_emp_cd:" + ClaimRequest.claim_emp_cd;
            param[10] = "@claim_emp_nm:" + ClaimRequest.claim_emp_nm;
            param[11] = "@inform_date:" + ClaimRequest.inform_date;
            param[12] = "@claim_address:" + ClaimRequest.claim_address;
            param[13] = "@claim_phone_no:" + ClaimRequest.claim_phone_no;
            param[14] = "@etc_qty:" + ClaimRequest.etc_qty;
            param[15] = "@etc_remark:" + ClaimRequest.etc_remark;
            param[16] = "@claim_information:" + ClaimRequest.claim_information;
            param[17] = "@claim_response:" + ClaimRequest.claim_response;
            param[18] = "@claim_hope:" + ClaimRequest.claim_hope;
            param[19] = "@time_limit:" + ClaimRequest.time_limit;
            param[20] = "@attach_yn:" + ClaimRequest.attach_yn;
            param[21] = "@claim_item:" + ClaimRequest.claim_item;
            param[22] = "@relation_dept_cd:" + ClaimRequest.relation_dept_cd;
            param[23] = "@serial_no:" + ClaimRequest.serial_no;
            param[24] = "@circulation_period:" + ClaimRequest.circulation_period;
            param[25] = "@product_line:" + ClaimRequest.product_line;
            param[26] = "@product_time:" + ClaimRequest.product_time;
            param[27] = "@spot_recall_date:" + ClaimRequest.spot_recall_date;
            param[28] = "@company_recall_date:" + ClaimRequest.company_recall_date;
            param[29] = "@recall_quantity:" + ClaimRequest.recall_quantity;
            param[30] = "@open_or_not:" + ClaimRequest.open_or_not;
            param[31] = "@first_form:" + ClaimRequest.first_form;
            param[32] = "@recall_form:" + ClaimRequest.recall_form;
            param[33] = "@substance_kind:" + ClaimRequest.substance_kind;
            param[34] = "@substance_form:" + ClaimRequest.substance_form;
            param[35] = "@substance_discovery:" + ClaimRequest.substance_discovery;
            param[36] = "@substance_keeping_environment:" + ClaimRequest.substance_keeping_environment;

            return param;
            
        }

    }
}