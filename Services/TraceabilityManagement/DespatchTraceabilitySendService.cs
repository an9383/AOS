using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.TraceabilityManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.TraceabilityManagement
{
    public class DespatchTraceabilitySendService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable DespatchTraceabilitySendSearch(string start_date_S, string end_date_S, string send_status_S)
        {
            string[] param = new string[3];
            param[0] = "@start_date_S:" + start_date_S;
            param[1] = "@end_date_S:" + end_date_S;
            param[2] = "@send_status_S:" + send_status_S;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_DespatchTraceabilitySend", "S_All", param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        //정보실패나 재전송준비 상태일때, 다른상태로 변경
        internal void DespatchStatusChange(string gms_pdtlot_seq, string send_status)
        {
            string[] param = new string[3];
            param[0] = "@gms_seq:" + gms_pdtlot_seq;
            param[1] = "@send_status:" + send_status;
            param[2] = "@update_user_cd:" + Public_Function.User_cd;

            _bllSpExecute.SpExecuteString("SP_DespatchTraceabilitySend", "ChangeStatus", param);
        }

        //전송 전 서명
        internal void DespatchSendSign(string emp_cd, string gms_pdtlot_seq)
        {
            string[] param = new string[4];
            param[0] = "@emp_cd:" + emp_cd;
            param[1] = "@validation_type:" + "2";
            param[2] = "@update_user_cd:" + Public_Function.User_cd;
            param[3] = "@gms_seq:" + gms_pdtlot_seq;

            _bllSpExecute.SpExecuteString("SP_DespatchTraceabilitySend", "Sign", param);
        }

        //식품이력추적 데이터 전송
        internal string CallDespatchPDT_PROC(List<DespatchInformation> despatchData)
        {
            string returnMsg = "";

            //서비스인증키
            DataSet serviceInfo = _bllSpExecute.SpExecuteDataSet("SP_LotTraceabilitySend", "S_Info_and_Time");
            string servicekey = serviceInfo.Tables[3].Rows[0]["SERVICE_KEY"].ToString();
            if (string.IsNullOrEmpty(servicekey))
            {
                returnMsg = "서비스 인증키가 유효하지 않거나 없습니다.";
                return returnMsg;
            }

            //PDT : 최상위 클래스
            stdSvc.pdt pdt = new stdSvc.pdt();
            pdt.API_KEY = servicekey;
            pdt.RESULT_DETAIL = "";

            //PDT_DETAIL : PDT 하위 상세 엘리먼트
            stdSvc.pdtDETAIL detail = new stdSvc.pdtDETAIL();

            //출고정보 설정 ========================================================================================================
            stdSvc.foodTGOW[] tgowArray = new stdSvc.foodTGOW[despatchData.Count];
            
            for(var i = 0; i < despatchData.Count; i++)
            {
                stdSvc.foodTGOW tgow = new stdSvc.foodTGOW();

                tgow.ROW_ID = despatchData[i].gms_seq;
                tgow.FOOD_HISTRACE_NUM = despatchData[i].food_histrace_num;
                tgow.QTYSpecified = true;
                tgow.QTY = Convert.ToDecimal(despatchData[i].qty);
                tgow.REG_NUM = despatchData[i].reg_num;
                tgow.TGOW_DT = despatchData[i].tgow_dt;
                tgow.TO_PST_ADDR = despatchData[i].to_pst_addr;
                tgow.TO_PST_CD = despatchData[i].to_pst_cd;
                tgow.TO_PST_NM = despatchData[i].to_pst_nm;
                tgow.TO_PST_LCNS_NO = despatchData[i].to_pst_lcns_no;
                tgow.TEMP_1 = despatchData[i].temp_1;
                tgow.TEMP_2 = despatchData[i].temp_2;
                tgow.TEMP_3 = despatchData[i].temp_3;

                tgowArray[i] = tgow;
            }
            detail.FOOD_TGOW = tgowArray;
            //======================================================================================================================
           
            pdt.PDT_DETAIL = detail; //출고정보

            //서비스호출
            stdSvc.FtmsWsdlService stdSvc = new stdSvc.FtmsWsdlService();
            stdSvc.result result = new stdSvc.result();

            try
            {
                result = stdSvc.PDT_PROC(pdt);                     //서비스결과
                returnMsg = result.RESULT_MSG;
            }
            catch (Exception ex)
            {
                returnMsg = "통신 중 에러가 발생하였습니다." + ex.Message;
            }
            finally
            {
                Return_Save(despatchData, result);              //DB서비스결과 저장
            }
            return returnMsg;
        }

        internal void Return_Save(List<DespatchInformation> despatchData, stdSvc.result result)
        {
            //요청에 대한 결과 저장(한 번)
            string gubun = "Save_Return";
            string ret_cd = "";

            if (result.RESULT_CODE == "S01")
            {
                ret_cd = "0";
            }
            else
            {
                ret_cd = "-1";
            }

            string[] param = new string[10];
            if (result.RESULT_DETAIL != null && result.RESULT_DETAIL.Length > 0)
            {
                param[0] = "@GET_CNT:" + "";                                //정보수신건수 Content/Data의 Row수 - 제공안함
                param[1] = "@MON_SEQ:" + "";                                //서버의 처리 순번 - 제공안함
                param[2] = "@RECV_TIME:" + "";                              //서버의 문서 수신시간 - 제공안함
                param[3] = "@RET_CD:" + ret_cd;                             //결과값 코드 0:성공, -1:실패
                param[4] = "@RET_CNT:" + "";                                //처리건수로 추정됨 - 제공안함
                param[5] = "@RET_MSG:" + result.RESULT_MSG;                 //결과 메시지
                param[6] = "@SRC_SEQ:" + result.RESULT_DETAIL[0].WS_DEQ;    //Client에서 전송한 SEQ
                param[7] = "@WS_ID:" + result.RESULT_DETAIL[0].WS_FG;        //WS_ID(웹서비스 id)
                param[8] = "@return_msg:" + result.RESULT_DETAIL[0].ERR_MSG; //리턴메세지(결과메시지보다 하위 상세메세지)
                param[9] = "@insert_user_cd:" + Public_Function.User_cd;
            }
            else
            {
                param[0] = "@GET_CNT:" + "";
                param[1] = "@MON_SEQ:" + "";
                param[2] = "@RECV_TIME:" + "";
                param[3] = "@RET_CD:" + ret_cd;
                param[4] = "@RET_CNT:" + "";
                param[5] = "@RET_MSG:" + result.RESULT_MSG;
                param[6] = "@SRC_SEQ:" + "";
                param[7] = "@WS_ID:" + "";
                param[8] = "@return_msg:" + "";
                param[9] = "@insert_user_cd:" + Public_Function.User_cd;
            }
            _bllSpExecute.SpExecuteString("SP_DespatchTraceabilitySend", gubun, param);

            //전송상태 수정
            if (result.RESULT_CODE == "S01") //결과값 코드
            {
                gubun = "Send_Success";
            }
            else
            {
                gubun = "Send_Fail";
            }

            string[] paramS = new string[1];
            
            //출고정보별 상태 업데이트
            for (int i = 0; i < despatchData.Count; i++)
            {
                paramS[0] = "@gms_seq:" + despatchData[i].gms_seq;

                _bllSpExecute.SpExecuteString("SP_DespatchTraceabilitySend", gubun, paramS);
            }

        }

        internal string GetSEQ()
        {
            string[] param = new string[1];
            param[0] = "@insert_user_cd:" + Public_Function.User_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_DespatchTraceabilitySend", "GetSEQ", param);
            string send_seq = "";

            if (ds.Tables[0] != null)
            {
                send_seq = ds.Tables[0].Rows[0]["send_seq"].ToString();
            }

            return send_seq;
        }

    }
}