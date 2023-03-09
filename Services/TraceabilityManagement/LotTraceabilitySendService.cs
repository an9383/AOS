using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.TraceabilityManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace HACCP.Services.TraceabilityManagement
{
    public class LotTraceabilitySendService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private string send_seq = "";
        internal DataTable LotTraceabilitySendSearch(string start_date_S, string end_date_S, string send_status_S)
        {
            string[] param = new string[3];
            param[0] = "@start_date_S:" + start_date_S;
            param[1] = "@end_date_S:" + end_date_S;
            param[2] = "@send_status_S:" + send_status_S;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_LotTraceabilitySend", "S_All", param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }


        internal DataTable LotTraceabilitySendDetailSearch(string gms_pdtlot_seq)
        {
            string[] param = new string[1];
            param[0] = "@gms_pdtlot_seq:" + gms_pdtlot_seq;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_LotTraceabilitySend", "S_Material", param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        //정보실패나 재전송준비 상태일때, 다른상태로 변경
        internal void LotStatusChange(string gms_pdtlot_seq, string send_status)
        {
            string[] param = new string[3];
            param[0] = "@gms_pdtlot_seq:" + gms_pdtlot_seq;
            param[1] = "@send_status:" + send_status;
            param[2] = "@update_user_cd:" + Public_Function.User_cd;

            _bllSpExecute.SpExecuteString("SP_LotTraceabilitySend", "ChangeStatus", param);
        }

        //동일 생산정보에 대한  추가 생산량 처리
        internal void LotSameUpdate(string gms_seq)
        {
            string[] param = new string[2];
            param[0] = "@gms_seq:" + gms_seq;
            param[1] = "@temp_3:" + "UPDATE";

            _bllSpExecute.SpExecuteString("SP_LotTraceabilitySend", "UPDATE", param);
        }

        //동일 생산정보에 대한  추가 생산량 처리 초기화
        internal void LotSameRefresh(string gms_seq)
        {
            string[] param = new string[2];
            param[0] = "@gms_seq:" + gms_seq;
            param[1] = "@temp_3:" + "";

            _bllSpExecute.SpExecuteString("SP_LotTraceabilitySend", "UPDATE", param);
        }

        //전송 전 서명
        internal void LotSendSign(string emp_cd, string gms_pdtlot_seq)
        {
            string[] param = new string[4];
            param[0] = "@emp_cd:" + emp_cd;
            param[1] = "@validation_type:" + "2";
            param[2] = "@update_user_cd:" + Public_Function.User_cd;
            param[3] = "@gms_pdtlot_seq:" + gms_pdtlot_seq;

            _bllSpExecute.SpExecuteString("SP_LotTraceabilitySend", "Sign", param);
        }


        //식품이력추적 데이터 전송
        internal string CallPDT_PROC(List<LotInformation> lotData, List<MaterialInformation> materialData,  string gubun)
        {
            string returnMsg = "";
            send_seq = "";

            //send_seq 가져오기
            send_seq = GetSEQ();

            //서비스인증키
            DataSet serviceInfo = _bllSpExecute.SpExecuteDataSet("SP_LotTraceabilitySend", "S_Info_and_Time");
            string servicekey = serviceInfo.Tables[3].Rows[0]["SERVICE_KEY"].ToString();           
            if(string.IsNullOrEmpty(servicekey))
            {
                returnMsg = "서비스 인증키가 유효하지 않거나 없습니다."; 
                return returnMsg;
            }

            //PDT : 최상위 클래스
            stdSvc.pdt pdt = new stdSvc.pdt();
            pdt.API_KEY = servicekey;
            pdt.RESULT_DETAIL = "0";

            //PDT_DETAIL : PDT 하위 상세 엘리먼트
            stdSvc.pdtDETAIL detail = new stdSvc.pdtDETAIL();

            //로트, 원료정보 전송
            if (gubun == "All")
            {
                //로트정보 설정 ========================================================================================================
                stdSvc.pdtLOT[] lotArray = new stdSvc.pdtLOT[lotData.Count];

                #region xml backup
                // PDT 
                //     API_KEY, RESULT_DETAIL, PDT_DETAIL
                //                             [PDT_LOT](배열)

                //XmlDocument xml = new XmlDocument();
                //XmlNode PDT = xml.CreateElement("PDT");
                //xml.AppendChild(PDT);

                //XmlNode API = xml.CreateElement("API_KEY");
                //API.InnerText = "CCACBAHBJBGDBABLSXQODMZKFKFGKH";
                //PDT.AppendChild(API);

                //XmlNode PDT_DETAIL = xml.CreateElement("PDT_DETAIL");
                //PDT.AppendChild(PDT_DETAIL);
                #endregion

                string seq = "";
                string[] param = new string[3];

                for (var i = 0; i < lotData.Count; i++)
                {
                    seq += lotData[i].gms_seq;
                    seq += ",";

                    stdSvc.pdtLOT lot = new stdSvc.pdtLOT();

                    lot.ROW_ID = lotData[i].gms_seq;                      //정보전송 자료 ID
                    lot.REG_NUM = lotData[i].reg_num;                     //식품이력등록증번호
                    lot.FOOD_HISTRACE_NUM = lotData[i].food_histrace_num; //식품이력추적관리번호
                    lot.MNFT_DAY = lotData[i].mnft_day;                   //제조일자(yyyymmdd)
                    lot.LOT_NUM = lotData[i].lot_num;                     //제품LOT번호 (이력추적관리번호 유형이 LOTNO 필수 경우 해당)
                    lot.CRCL_PRD_DAY = lotData[i].crcl_prd_day;           //유통기한(yyyymmdd)
                    lot.IMCM_YN = "N";                                    // "Y : 수입, N : 국내"
                    lot.MNFT_FACT = lotData[i].mnft_fact;                 //국내 : 업체명(한글) / 수입: 국외 업체명(영문)
                    lot.MNFT_FACT_ADDR = lotData[i].mnft_fact_addr;       //제조공장 주소
                    //lot.INCM_DECL_OGN = lotData[i].incm_decl_ogn;       //수입신고접수기관(지방청)
                    //lot.INCM_DECL_NUM = lotData[i].incm_decl_num;       //수입신고접수번호
                    //lot.MNFT_PRV = lotData[i].mnft_prv;                 //제조국(ex:KR)
                    lot.GMO_YN = lotData[i].gmo_yn;                       //GMO여부 (예 =‘Y’,아니오 =‘N’, 해당없음 = ‘’)
                    lot.PROD_QTY = lotData[i].prod_qty;                   //생산수량
                    lot.PIAW_DT = lotData[i].piaw_dt;                     //수입일자(입고일자)
                    lot.TEMP_1 = lotData[i].temp_1;                       //임시1
                    lot.TEMP_2 = lotData[i].temp_2;                       //임시2
                    lot.TEMP_3 = lotData[i].temp_3;

                    lotArray[i] = lot;

                    SaveHistory("PDTLOT", lotData[i].gms_seq
                                , lotData[i].reg_num, lotData[i].food_histrace_num, send_seq);
                    #region xml backup
                    ////lot객체를 xml에 담는다.=========================================
                    //XmlDocument xmlDoc = new XmlDocument();      
                    //XmlSerializer xmlSerializer = new XmlSerializer(lot.GetType());

                    //using (MemoryStream xmlStream = new MemoryStream())
                    //{
                    //    xmlSerializer.Serialize(xmlStream, lot); //객체를 직렬화
                    //    xmlStream.Position = 0;
                    //    //Loads the XML document from the specified string.
                    //    xmlDoc.Load(xmlStream);
                    //}
                    ////=================================================================

                    ////PDT_DETAIL에 PDT_LOT를 담는다(PDT_LOT는 여러개가 될 수 있음)=====
                    //XmlNodeList lotInfo = xmlDoc.SelectSingleNode("pdtLOT").ChildNodes;
                    //XmlNode PDT_LOT = xml.CreateElement("PDT_LOT");

                    //for (var j = 0; j < lotInfo.Count; j++)
                    //{
                    //    PDT_LOT.AppendChild(xml.ImportNode(lotInfo[j], true));
                    //}  
                    //PDT_DETAIL.AppendChild(PDT_LOT);
                    ////=================================================================
                    #endregion

                }

                detail.PDT_LOT = lotArray;

            //=====================================================================================================================

            //원료정보 설정========================================================================================================

                if (seq.Substring(seq.Length - 1, 1) == ",")
                {
                    seq = seq.Substring(0, seq.Length - 1);
                }
                param[0] = "@gms_pdtlot_seq_2:" + seq;
                param[1] = "@send_seq:" + send_seq;
                param[2] = "@send_ws_id:" + "PDTLOT_ORM_INFO";

                DataTable dtMaterialInfoList = _bllSpExecute.SpExecuteTable("SP_LotTraceabilitySend", "makeMaterialInfo", param);//전송 로트들에 대한 원료정보

                stdSvc.pdtLOTORMINFO[] ormArray = new stdSvc.pdtLOTORMINFO[dtMaterialInfoList.Rows.Count];          //전송할 원료정보배열

                for (var i = 0; i < dtMaterialInfoList.Rows.Count; i++)
                {
                    stdSvc.pdtLOTORMINFO orm = new stdSvc.pdtLOTORMINFO();

                    orm.ROW_ID = dtMaterialInfoList.Rows[i]["gms_seq"].ToString();                           //정보전송 자료 ID
                    orm.REG_NUM = dtMaterialInfoList.Rows[i]["reg_num"].ToString();                          //식품이력 등록번호
                    orm.FOOD_HISTRACE_NUM = dtMaterialInfoList.Rows[i]["food_histrace_num"].ToString();      //식품이력추적관리번호
                    orm.ORM_STD_CD = dtMaterialInfoList.Rows[i]["orm_std_cd"].ToString();                    //원재료 코드
                    orm.ORM_NM = dtMaterialInfoList.Rows[i]["orm_nm"].ToString();                            //원재료 명
                    orm.ORM_NM_ENG = dtMaterialInfoList.Rows[i]["orm_nm_eng"].ToString();                    //수입 : 원부자재명(영문)
                    orm.PRV_NATN_CD = dtMaterialInfoList.Rows[i]["prv_natn_cd"].ToString();                  //원산지 국가코드(ex:KR)
                    orm.SUP_BUSINESS_REG_NO = dtMaterialInfoList.Rows[i]["sup_business_reg_no"].ToString();  //거래처 사업자번호
                    orm.SUP_PSN = dtMaterialInfoList.Rows[i]["sup_psn"].ToString();                          //공급자명
                    orm.SUP_PSN_ADDR1 = dtMaterialInfoList.Rows[i]["sup_psn_addr1"].ToString();              //공급자 주소1
                    orm.GMO_YN = dtMaterialInfoList.Rows[i]["gmo_yn"].ToString();                            //GMO여부  (예 =‘Y’ ,아니오 =‘N’,해당없음 = ‘’)
                    orm.TEMP_1 = dtMaterialInfoList.Rows[i]["temp_1"].ToString();                            //임시1
                    orm.TEMP_2 = dtMaterialInfoList.Rows[i]["temp_2"].ToString();                            //임시2
                    orm.TEMP_3 = dtMaterialInfoList.Rows[i]["temp_3"].ToString();                            //임시3

                    ormArray[i] = orm;
                }
                detail.ORM_INFO = ormArray;
                //======================================================================================================================

            }
            else
            {
                //원료만 전송

                stdSvc.pdtLOTORMINFO[] ormArray = new stdSvc.pdtLOTORMINFO[materialData.Count];          //전송할 원료정보배열

                for (var i = 0; i < materialData.Count; i++)
                {
                    stdSvc.pdtLOTORMINFO orm = new stdSvc.pdtLOTORMINFO();

                    orm.ROW_ID = materialData[i].gms_seq;                           //정보전송 자료 ID
                    orm.REG_NUM = materialData[i].reg_num;                          //식품이력 등록번호
                    orm.FOOD_HISTRACE_NUM = materialData[i].food_histrace_num;      //식품이력추적관리번호
                    orm.ORM_STD_CD = materialData[i].orm_std_cd;                    //원재료 코드
                    orm.ORM_NM = materialData[i].orm_nm;                            //원재료 명
                    orm.ORM_NM_ENG = materialData[i].orm_nm_eng;                    //수입 : 원부자재명(영문)
                    orm.PRV_NATN_CD = materialData[i].prv_natn_cd;                  //원산지 국가코드(ex:KR)
                    orm.SUP_BUSINESS_REG_NO = materialData[i].sup_business_reg_no;  //거래처 사업자번호
                    orm.SUP_PSN = materialData[i].sup_psn;                          //공급자명
                    orm.SUP_PSN_ADDR1 = materialData[i].sup_psn_addr1;              //공급자 주소1
                    orm.GMO_YN = materialData[i].gmo_yn;                            //GMO여부  (예 =‘Y’ ,아니오 =‘N’,해당없음 = ‘’)
                    orm.TEMP_1 = materialData[i].temp_1;                            //임시1
                    orm.TEMP_2 = materialData[i].temp_2;                            //임시2
                    orm.TEMP_3 = materialData[i].temp_3;                            //임시3

                    ormArray[i] = orm;
                }

                SaveHistory("ORM_INFO", materialData[0].gms_seq
            , materialData[0].reg_num, materialData[0].food_histrace_num, send_seq);

                detail.ORM_INFO = ormArray;
            }

            pdt.PDT_DETAIL = detail; //로트정보, 원료정보 설정
           
            stdSvc.FtmsWsdlService stdSvc = new stdSvc.FtmsWsdlService();    //서비스호출
            stdSvc.result result = new stdSvc.result();
            try
            {
                result = stdSvc.PDT_PROC(pdt);                     //서비스결과
                returnMsg = result.RESULT_MSG;
            }
            catch(Exception ex)
            {
                returnMsg = "통신 중 에러가 발생하였습니다." + ex.Message;
            }
            finally
            {
                Return_Save(lotData, materialData, result, gubun);              //DB서비스결과 저장
            }

            return returnMsg;
        }


        //전송결과 받아와서 DB에 입력
        internal void Return_Save(List<LotInformation> lotInformation, List<MaterialInformation> materialData, stdSvc.result result, string check)
        {
            string gubun = "Save_Return";
            string ret_cd = "";

            if(result.RESULT_CODE == "S01")
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
            _bllSpExecute.SpExecuteString("SP_LotTraceabilitySend", gubun, param);

            //전송상태 수정
            if (result.RESULT_CODE == "S01") //결과값 코드
            {
                gubun = "Send_Success";
            }
            else
            {
                gubun = "Send_Fail";
            }

            //전송상태 변경
            if (check == "All")
            {

                for (int i = 0; i < lotInformation.Count; i++)
                {
                    SendStatusChange(gubun, lotInformation[i].gms_seq);
                }
            }
            else if (check == "Material")
            {
                SendStatusChange(gubun, materialData[0].gms_pdtlot_seq.ToString());
            }
        }

        internal void SaveHistory(string ws_id, string gms_seq, string reg_num, string food_histrace_num, string send_seq)
        {
            string gubun = "SaveHistory";

            string[] param = new string[5];

            param[0] = "@send_ws_id:" + ws_id;
            param[1] = "@gms_pdtlot_seq:" + gms_seq;
            param[2] = "@reg_num:" + reg_num;
            param[3] = "@food_histrace_num:" + food_histrace_num;
            param[4] = "@send_seq:" + send_seq;

            _bllSpExecute.SpExecuteString("SP_LotTraceabilitySend", gubun, param);

        }

        //구분하여 전송상태 변경
        internal void SendStatusChange(string gubun, string part)
        {
            string[] param = new string[2];

            param[0] = "@gms_pdtlot_seq:" + part;
            param[1] = "@update_user_cd:" + Public_Function.User_cd;

            _bllSpExecute.SpExecuteString("SP_LotTraceabilitySend", gubun, param);

        }

        internal string GetSEQ()
        {
            string[] param = new string[1];
            param[0] = "@insert_user_cd:" + Public_Function.User_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_LotTraceabilitySend", "GetSEQ", param);
            string send_seq = "";

            if (ds.Tables[0] != null)
            {
                send_seq = ds.Tables[0].Rows[0]["send_seq"].ToString();
            }

            return send_seq;
        }

        //식품이력추적 계정정보 받아오기
        internal DataSet GetAccessInfo()
        {
            string[] param = new string[1];
            param[0] = "@temp_1:";

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_LotTraceabilitySend", "S_Info_and_Time", param);      

            return ds;
        }
    }
}