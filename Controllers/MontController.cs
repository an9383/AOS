using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using HACCP.Attribute;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models.Mont;
using HACCP.Models.ProductionMaster;
using HACCP.Services.Comm;
using HACCP.Services.Mont;
using HACCP.Services.ProductionMaster;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class MontController : Controller
    {
        #region 공통 서비스 설정
        private static readonly ILog log = LogManager.GetLogger(typeof(MontController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        private CodeHelpService codeHelpService = new CodeHelpService();        
        #endregion


        #region 설비 수집 데이터
        private EquipmentCollectDataService equipmentCollectDataService = new EquipmentCollectDataService();

        public ActionResult EquipmentCollectData(string equip_cd, string equip_nm, string workroom_cd, string workroom_nm, string equip_type, string start_date, string end_date)
        {

            if (equip_cd != null && equip_nm != null)
            {
                object receiptData = new
                {
                    equip_cd = equip_cd,
                    equip_nm = equip_nm,
                    equip_type = equip_type,
                    workroom_cd = workroom_cd,
                    workroom_nm = workroom_nm,
                    start_date = start_date,
                    end_date = end_date

                };

                ViewBag.receiptData = Json(receiptData);
            }
            else
            {
                object receiptData = new object();

                ViewBag.receiptData = Json(receiptData);
            }

            DataTable EquipmentCollectMenuData = equipmentCollectDataService.selectEquipment("Y", "%");

            // 셀렉트 박스 데이터 조회
            DataTable plantData_s = selectBoxService.GetSelectBox("사업장", "S", "사업장", "plantData_s");
            DataTable equipType2_s = selectBoxService.GetSelectBox("공통코드", "N", "SC041", "equipType2_s");

            // 박스데이터 ViewBag
            ViewBag.plantData_s = plantData_s;
            ViewBag.equipType2_s = equipType2_s;

            // 팝업 데이터
            DataTable equipPopupData = codeHelpService.GetCode(CodeHelpType.equipment, "", "");

            ViewBag.EquipmentCollectMenuData = Json(Public_Function.DataTableToJSON(EquipmentCollectMenuData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));


            return View();
        }


        [HttpPost]public ActionResult EquipMenuSearch(string equip_use_gb, string plant_cd)
        {
            DataTable EquipmentCollectMenuData = equipmentCollectDataService.selectEquipment(equip_use_gb, plant_cd);

            return ViewBag.EquipmentCollectMenuData = Json(Public_Function.DataTableToJSON(EquipmentCollectMenuData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult EquipDataCountCheck(string equip_cd, string equip_type, string s_date, string e_date, string s_time, string e_time)
        {
            String count = equipmentCollectDataService.EquipDataCountCheck(equip_cd, equip_type, s_date, e_date, s_time, e_time);



            var resJson = "{ \"messege\": \"" + count + "\" }";
            return Json(count);
        }


        [HttpPost]
        public ActionResult EquipDataSearch(string equip_cd, string equip_type, string s_date, string e_date, string s_time, string e_time)
        {
            DataTable equipData = equipmentCollectDataService.EquipDataSearch(equip_cd, equip_type, s_date, e_date, s_time, e_time);

            JsonResult returnJsonData = new JsonResult();
            returnJsonData = Json(Public_Function.DataTableToJSON(equipData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            returnJsonData.MaxJsonLength = 2147483647;


            return ViewBag.equipData = returnJsonData;

            //DataSet equipData = equipmentCollectDataService.EquipDataSearch(equip_cd, equip_type, s_date, e_date, s_time, e_time);
            //string[] jsonArr = new string[equipData.Tables.Count];

            //for (int i = 0; i < jsonArr.Length; i++)
            //{
            //    jsonArr[i] = Public_Function.DataTableToJSON(equipData.Tables[i]);
            //}

            //return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult EquipKeyDataSearch(string equip_type_nm, string s_date, string e_date, string s_time, string e_time)
        {
            DataTable equipData = equipmentCollectDataService.EquipKeyDataSearch(equip_type_nm, s_date, e_date, s_time, e_time);

            return ViewBag.equipData = Json(Public_Function.DataTableToJSON(equipData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [HttpPost]
        public string EquipDataUpdate(EquipmentCollectData edata) 
        {

            string msg = equipmentCollectDataService.EquipDataCRUD(edata);
            return msg;
        }

        [HttpPost]
        public ActionResult EquipSelectATData(EquipmentCollectData edata)
        {

            DataTable at_data = equipmentCollectDataService.EquipSelectATData(edata);
            return ViewBag.EquipmentATData = Json(Public_Function.DataTableToJSON(at_data).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        

        #endregion


        #region 설비 가동 현황
        private EquipmentOperationStatusService equipmentOperationStatusService = new EquipmentOperationStatusService();


        public ActionResult EquipmentOperationStatus()
        {
            DataTable EquipmentOperationStatusData = equipmentOperationStatusService.Select("%", "0");

            //// 셀렉트 박스 데이터 조회
            //DataTable plantData_s = selectBoxService.GetSelectBox("사업장", "S", "사업장", "plantData_s");
            ////DataTable equipType_s = selectBoxService.GetSelectBox("공통코드", "S", "SC011", "equipType_s");

            //// 박스데이터 ViewBag
            //ViewBag.plantData_s = plantData_s;
            ////ViewBag.equipType_s = equipType_s;

            //// 팝업 데이터
            //DataTable equipPopupData = codeHelpService.GetCode(CodeHelpType.equipment, "", "");

            ViewBag.EquipmentOperationStatusData = Json(Public_Function.DataTableToJSON(EquipmentOperationStatusData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            //ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));


            return View();
        }


        [HttpPost]
        public ActionResult OperationStatusSearch(string operate_type, string equipt_category)
        {
            DataTable EquipmentOperationStatusData = equipmentOperationStatusService.Select(operate_type, equipt_category);

            return ViewBag.EquipmentOperationStatusData = Json(Public_Function.DataTableToJSON(EquipmentOperationStatusData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult OperationGridSearch(string equip_cd, string equip_type)
        {
            DataTable OperationGridData = equipmentOperationStatusService.OperationGridSearch(equip_cd, equip_type);

            return ViewBag.OperationGridData = Json(Public_Function.DataTableToJSON(OperationGridData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        #endregion


        #region 작업실 환경 현황
        public ActionResult WorkroomEnvironmentStatus()
        {
            DataTable WorkroomEnvironmentStatusData = equipmentOperationStatusService.Select("%", "5");


            ViewBag.WorkroomEnvironmentStatusData = Json(Public_Function.DataTableToJSON(WorkroomEnvironmentStatusData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            return View();
        }


        [HttpPost]
        public JsonResult WorkroomEnvironmentChartSelect(string workroom_cd)
        {
            DataTable workroomEnvironmentChartData = equipmentOperationStatusService.WorkroomEnvironmentChartSelect(workroom_cd);

            return Json(Public_Function.DataTableToJSON(workroomEnvironmentChartData));
        }
        #endregion


        #region HACCP_CodeRegistration - GMP+CCP점검 항목등록
        private HACCP_CodeRegistrationService HACCP_CodeRegistrationService = new HACCP_CodeRegistrationService();
        [Route("Mont/HACCP_CodeRegistration")]
        [HttpGet]
        public ActionResult HACCP_CodeRegistration(HACCP_CodeRegistration hACCP_CodeRegistration, string use_yn)
        {            
            DataTable HACCP_CodeRegistrationData = HACCP_CodeRegistrationService.getHaccpDoc(use_yn);

            //담당자 팝업
            DataTable CR_empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");

            ViewBag.HACCP_CodeRegistrationData = Json(Public_Function.DataTableToJSON(HACCP_CodeRegistrationData));
            ViewBag.CR_empPopupData = Json(Public_Function.DataTableToJSON(CR_empPopupData));

            ViewBag.HACCP_CodeRegistrationAuth = Json(HttpContext.Session["HACCP_CodeRegistrationAuth"]);

            return View();
        }

        [HttpPost]
        public ActionResult getHaccpDoc(string use_yn)
        {
            DataTable getHaccpDoc = HACCP_CodeRegistrationService.getHaccpDoc(use_yn);

            return Json(Public_Function.DataTableToJSON(getHaccpDoc));        
        }
        
        [HttpPost]
        public ActionResult getGridItem(string HaccpCode)
        {
            DataTable getGridItem = HACCP_CodeRegistrationService.getGridItem(HaccpCode);

            return Json(Public_Function.DataTableToJSON(getGridItem));
        }

        [HttpPost]
        public ActionResult getHaccpDocPopup()
        {
            DataTable getHaccpDocPopup = HACCP_CodeRegistrationService.getHaccpDocPopup();

            string jsonData = Public_Function.DataTableToJSON(getHaccpDocPopup);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);

            return Json(jsonList.ToArray());
        }

        public ActionResult getMaxChgSerNo(HACCP_CodeRegistration hACCP_CodeRegistration)
        {
            DataTable getMaxChgSerNo = HACCP_CodeRegistrationService.getMaxChgSerNo(hACCP_CodeRegistration);

            return Json(Public_Function.DataTableToJSON(getMaxChgSerNo));
        }

        public ActionResult getHaccpItemByRevisionPopup(HACCP_CodeRegistration hACCP_CodeRegistration)
        {
            DataTable getHaccpItemByRevisionPopup = HACCP_CodeRegistrationService.getHaccpItemByRevisionPopup(hACCP_CodeRegistration);

            //Revision 번호 조회
            DataTable ChgSerNo = HACCP_CodeRegistrationService.getChgSerNo(hACCP_CodeRegistration);

            string jsonData = Public_Function.DataTableToJSON(getHaccpItemByRevisionPopup);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);
            jsonList.Add(Public_Function.DataTableToJSON(ChgSerNo));

            return Json(jsonList.ToArray());
        }

        public ActionResult getHaccpItemByRevisionSearch(HACCP_CodeRegistration hACCP_CodeRegistration)
        {
            DataTable getHaccpItemByRevisionSearch = HACCP_CodeRegistrationService.getHaccpItemByRevisionPopup(hACCP_CodeRegistration);
            
            string jsonData = Public_Function.DataTableToJSON(getHaccpItemByRevisionSearch);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);            

            return Json(jsonList.ToArray());
        }

        [HttpPost]
        public ActionResult getProcess_cd()
        {
            DataTable getProcess_cd = HACCP_CodeRegistrationService.getProcess_cd();

            string jsonData = Public_Function.DataTableToJSON(getProcess_cd);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);

            return Json(jsonList.ToArray());
        }

        [HttpPost]
        public ActionResult getProcess_exam_cd(HACCP_CodeRegistration hACCP_CodeRegistration)
        {
            DataTable getProcess_exam_cd = HACCP_CodeRegistrationService.getProcess_exam_cd(hACCP_CodeRegistration);

            string jsonData = Public_Function.DataTableToJSON(getProcess_exam_cd);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);

            return Json(jsonList.ToArray());
        }

        [HttpPost]
        public ActionResult getWorkroom_cd()
        {
            DataTable getWorkroom_cd = HACCP_CodeRegistrationService.getWorkroom_cd();

            string jsonData = Public_Function.DataTableToJSON(getWorkroom_cd);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);

            return Json(jsonList.ToArray());
        }
        
        public JsonResult setFinalConfirm(HACCP_CodeRegistration hACCP_CodeRegistration)
        {
            string res = HACCP_CodeRegistrationService.setFinalConfirm(hACCP_CodeRegistration);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        public JsonResult HaccpTRX([JsonBinder]List<HACCP_CodeRegistration> hACCP_CodeRegistration, string gubun)
        {
            for(int i = 0; i <  hACCP_CodeRegistration.Count; i++)
            {
                if (hACCP_CodeRegistration[i].gubun.Equals("I"))
                {
                    hACCP_CodeRegistration[i].gubun = "I";
                }
                else if (hACCP_CodeRegistration[i].gubun.Equals("I_D"))
                {
                    hACCP_CodeRegistration[i].gubun = "I_D";
                }
                else if (hACCP_CodeRegistration[i].gubun.Equals("U_D"))
                {
                    hACCP_CodeRegistration[i].gubun = "U_D";
                }
                else if (hACCP_CodeRegistration[i].gubun.Equals("D_D"))
                {
                    hACCP_CodeRegistration[i].gubun = "D_D";
                }
            }

            string res = HACCP_CodeRegistrationService.HaccpTRX(hACCP_CodeRegistration);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);

        }




        #endregion


        #region HACCP_ManagementScreen - GMP+CCP점검 일지 관리
        private HACCP_ManagementScreenService HACCP_ManagementScreenService = new HACCP_ManagementScreenService();
        private ReportDrawingService reportDrawingService = new ReportDrawingService();


        [Route("Mont/HACCP_ManagementScreen")]
        [HttpGet]
        public ActionResult HACCP_ManagementScreen(string use_yn, string FinalConfirm)
        {
            DataTable HACCP_ManagementScreenData = HACCP_ManagementScreenService.getHaccpHeaderDoc(use_yn, FinalConfirm);

            ViewBag.HACCP_ManagementScreenData = Json(Public_Function.DataTableToJSON(HACCP_ManagementScreenData));
            ViewBag.HACCP_ManagementScreenAuth = Json(HttpContext.Session["HACCP_ManagementScreenAuth"]);

            return View();
        }

        [HttpPost]
        public ActionResult getHaccpHeaderDoc(string use_yn, string FinalConfirm)
        {
            DataTable getHaccpHeaderDoc = HACCP_ManagementScreenService.getHaccpHeaderDoc(use_yn, FinalConfirm);

            return Json(Public_Function.DataTableToJSON(getHaccpHeaderDoc));
        }
                
        [HttpPost]
        public ActionResult getHaccpBaseItem(HACCP_ManagementScreen hACCP_ManagementScreen, string doc_code, string HaccpCode, string doc_name, string ChgSerNo)
        {
            DataTable CCP_formatData = HACCP_ManagementScreenService.getHaccpBaseItem(hACCP_ManagementScreen);            
            DataTable CCP_item_cd = codeHelpService.GetCode(CodeHelpType.makingitem, Public_Function.selectedPLANT, "");

            if (doc_code != null && !doc_code.Equals(""))
            {
                ViewBag.ReportCode = doc_code;
            }
            if (HaccpCode != null && !HaccpCode.Equals(""))
            {
                ViewBag.HaccpCode = hACCP_ManagementScreen.HaccpCode;
            }
            if(doc_name != null && !doc_name.Equals(""))
            {
                ViewBag.doc_name = hACCP_ManagementScreen.doc_name;
            }
            if (ChgSerNo != null && !ChgSerNo.Equals(""))
            {
                ViewBag.ChgSerNo = hACCP_ManagementScreen.ChgSerNo;
            }

            ViewBag.CCP_formatData = Json(Public_Function.DataTableToJSON(CCP_formatData));
            ViewBag.CCP_item_cd = Json(Public_Function.DataTableToJSON(CCP_item_cd));

            string viewName = "";
            
            if (hACCP_ManagementScreen.HaccpCode.Contains("CCP"))
            {
                viewName = "CCP/CCP_format";
            }
            else
            {
                viewName = "CCP/NullPage";
            }
            //string viewName = "CCP/imHaccp_" + hACCP_ManagementScreen.HaccpCode + "_Item";            
            
            //bool isExistFile = System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("/Views/Mont/" + viewName + ".cshtml"));

            //반환하는 CCP 페이지가 존재하지 않을때
            //if (!isExistFile)
            //{
            //    return View("CCP/NullPage");
            //}


            return View(viewName);
            
        }


        [HttpPost]
        public ActionResult getCCP_Type(HACCP_ManagementScreen hACCP_ManagementScreen)
        {
            DataTable getCCP_Type = HACCP_ManagementScreenService.getCCP_Type(hACCP_ManagementScreen);

            return Json(Public_Function.DataTableToJSON(getCCP_Type));
        }

        [HttpPost]
        public ActionResult getCCP_Order(HACCP_ManagementScreen hACCP_ManagementScreen)
        {
            DataTable getCCP_Order = HACCP_ManagementScreenService.getCCP_Order(hACCP_ManagementScreen);            

            return Json(Public_Function.DataTableToJSON(getCCP_Order));
        }

        [HttpPost]
        public JsonResult getCCP_preview(HACCP_ManagementScreen hACCP_ManagementScreen, string CCP_gubun)
        {
            List<string> getCCP_preview = HACCP_ManagementScreenService.getCCP_preview(hACCP_ManagementScreen, CCP_gubun);            

            if (getCCP_preview == null)
            {
                return null;
            }
            else
            {
                return Json(getCCP_preview.ToArray());
            }
        }

        [HttpPost]
        public JsonResult CCP_SignerSearch(string index_key, string sign_set_cd)
        {
            DataTable CCP_SignerSearch = HACCP_ManagementScreenService.CCP_SignerSearch(index_key, sign_set_cd);
            string jsonData = Public_Function.DataTableToJSON(CCP_SignerSearch);

            return Json(jsonData);
        }
        
        [HttpPost]
        public JsonResult CCP_format_SignSave(string sign_emp_cd, string sign_type, string representative_yn, string index_key, string sign_set_cd, string sign_set_dt_seq)
        {
            string result = "";

            bool check = HACCP_ManagementScreenService.CCP_format_SignSave(sign_emp_cd, sign_type, representative_yn, index_key, sign_set_cd, sign_set_dt_seq);

            if(check == true)
            {
                result = "Y";
            }
            else
            {
                result = "N";
            }

            return Json(result);
        }
        
        [HttpPost]
        public JsonResult CCP_format_SignCancel(string index_key, string sign_set_dt_seq, string sign_set_cd)
        {
            string result = "";

            bool check = HACCP_ManagementScreenService.CCP_format_SignCancel(index_key, sign_set_dt_seq, sign_set_cd);

            if (check == true)
            {
                result = "Y";
            }
            else
            {
                result = "N";
            }

            return Json(result);
        }

        [CheckSession]
        [HttpPost]
        public ActionResult getReportBaseItem(string HaccpCode, string doc_code, string doc_seq, string doc_name, string ChgSerNo)
        {
            if (doc_code != null && !doc_code.Equals(""))
            {
                ViewBag.ReportCode = doc_code;
            }
            if (doc_name != null && !doc_name.Equals(""))
            {
                ViewBag.doc_name = doc_name;
            }
            if (ChgSerNo != null && !ChgSerNo.Equals(""))
            {
                ViewBag.ChgSerNo = ChgSerNo;
            }
            if (HaccpCode != null && !HaccpCode.Equals(""))
            {
                ViewBag.HaccpCode = HaccpCode;
            }


            string viewName = "";

            if (doc_seq == null || doc_seq == "")
            {
                viewName = "Report/NullPage";
            }
            else
            {
                ViewBag.ReportSeq = doc_seq;
                viewName = "Report/Report_" + doc_seq;
            }

            //담당자 리스트
            DataTable empData = reportDrawingService.getEmployeeList();
            ViewBag.EmployeeData = Json(Public_Function.DataTableToJSON(empData));

            DataTable workroomData = null;
            //작업실 리스트
            if (doc_seq == "30001") //작업실
            {
                workroomData = reportDrawingService.getWorkroomList("", "G006");
            }
            else if (doc_seq == "30002") //부대시설
            {
                workroomData = reportDrawingService.getWorkroomList("", "G007");
            }
            else if (doc_seq == "30006") //창고
            {
                workroomData = reportDrawingService.getWorkroomList("창고", "%");
            }
            else if (doc_seq == "30005") //일일 점검표
            {
                workroomData = reportDrawingService.getWorkroomList("", "%");
            }
            ViewBag.WorkroomData = Json(Public_Function.DataTableToJSON(workroomData));


            return View(viewName);

        }

        [CheckSession]
        [HttpPost]
        public JsonResult getReportMainGrid(ReportDataFormat item_data)
        {
            DataTable dt = reportDrawingService.getReportMainItem(item_data);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        [HttpPost]
        public JsonResult getReportItem(ReportDataFormat item_data)
        {
            List<string> getCCP_preview = reportDrawingService.getReportItem(item_data);

            return Json(getCCP_preview.ToArray());
        }

        [HttpPost]
        public string setReportItem([JsonBinder] List<ReportDataFormat> mModel, string gubun, string V_TYPE)
        {
            string message = reportDrawingService.setReportItem(mModel, gubun, V_TYPE);

            return message;
        }

        

        #endregion


        #region HACCP_ProcessExam_Manage - CCP 공정검사 관리

        HACCP_ProcessExam_ManageService HACCP_ProcessExam_ManageService = new HACCP_ProcessExam_ManageService();

        public ActionResult HACCP_ProcessExam_Manage()
        {
            DateTime ed = new DateTime();
            ed = DateTime.Now;
            DateTime sd = new DateTime();
            sd = DateTime.Now.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");


            DataTable mainGrid = HACCP_ProcessExam_ManageService.Select(sDate, eDate);

            ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            return View();
        }


        // 메인 그리드 조회
        [HttpPost]
        public ActionResult HACCP_ProcessExam_ManageSearch(string sDate, string eDate)
        {
            DataTable mainGrid = HACCP_ProcessExam_ManageService.Select(sDate, eDate);

            return ViewBag.EquipmentCollectMenuData = Json(Public_Function.DataTableToJSON(mainGrid).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 제조번호 조회
        [HttpPost]
        public ActionResult HACCP_ProcessExam_ManageOrderSearch(string sDate, string eDate, string workroom_cd)
        {
            DataTable dt = HACCP_ProcessExam_ManageService.HACCP_ProcessExam_ManageOrderSearch(sDate, eDate, workroom_cd);

            return ViewBag.EquipmentCollectMenuData = Json(Public_Function.DataTableToJSON(dt).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 공정검사 조회
        [HttpPost]
        public ActionResult HACCP_ProcessExam_ManageProcessSearch(string order_no, string order_proc_id)
        {
            DataTable dt = HACCP_ProcessExam_ManageService.HACCP_ProcessExam_ManageProcessSearch(order_no, order_proc_id);

            return ViewBag.EquipmentCollectMenuData = Json(Public_Function.DataTableToJSON(dt).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 공정검사 조회
        [HttpPost]
        public ActionResult HACCP_ProcessExam_ManageWorkDataSearch(string order_no, string order_proc_id, string process_exam_cd)
        {
            DataTable dt = HACCP_ProcessExam_ManageService.HACCP_ProcessExam_ManageWorkDataSearch(order_no, order_proc_id, process_exam_cd);

            return ViewBag.EquipmentCollectMenuData = Json(Public_Function.DataTableToJSON(dt).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }
        #endregion


        #region 스마트팩토리 정보현황
        SmartFactory_InfomationStateService smartFactory_InfomationStateService = new SmartFactory_InfomationStateService();

        #region CHEBIGEN 용
        public ActionResult SmartFactory_InfomationStatePopUp_Che()
        {
            DataSet factoryInfomation = smartFactory_InfomationStateService.SelectFactoryInfomation_Che();

            ViewBag.factoryInfomation1 = Json(Public_Function.DataTableToJSON(factoryInfomation.Tables[0]).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.factoryInfomation2 = Json(Public_Function.DataTableToJSON(factoryInfomation.Tables[1]).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.factoryInfomation3 = Json(Public_Function.DataTableToJSON(factoryInfomation.Tables[2]).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            //ViewBag.factoryInfomation1 = Public_Function.DataTableToJSON(factoryInfomation.Tables[0]);
            //ViewBag.factoryInfomation2 = Public_Function.DataTableToJSON(factoryInfomation.Tables[1]);
            //ViewBag.factoryInfomation3 = Public_Function.DataTableToJSON(factoryInfomation.Tables[2]);


            return View();
        }


        // CHEBIGEN 용
        public ActionResult SmartFactory_InfomationState2_Che()
        {
            DataSet factoryInfomation = smartFactory_InfomationStateService.SelectFactoryInfomation_Che();

            string[] jsonArr = new string[3];
            //jsonArr[0] = Public_Function.DataTableToJSON(factoryInfomation.Tables[0]);
            //jsonArr[1] = Public_Function.DataTableToJSON(factoryInfomation.Tables[1]);
            //jsonArr[2] = Public_Function.DataTableToJSON(factoryInfomation.Tables[2]);

            jsonArr[0] = factoryInfomation.Tables[0].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(factoryInfomation.Tables[0]);
            jsonArr[1] = factoryInfomation.Tables[1].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(factoryInfomation.Tables[1]);
            jsonArr[2] = factoryInfomation.Tables[2].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(factoryInfomation.Tables[2]);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult BarSplineChartDataSearch_Che(string start_time, string stop_time)
        {
            DataTable ChartData = smartFactory_InfomationStateService.ChartDataSearch_Che(start_time, stop_time);

            return ViewBag.ChartData = Json(Public_Function.DataTableToJSON(ChartData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        #endregion

        public ActionResult SmartFactory_InfomationStatePopUp()
        {
            LoginService loginService = new LoginService();
            DataTable loginData = loginService.loginPageLoading();
            DataTable smartFactory_InfomationStatePopup_EquipList = smartFactory_InfomationStateService.EquipSelect();
            
            if(smartFactory_InfomationStatePopup_EquipList.Rows.Count == 0)
            {
                ViewBag.smartFactoryInfomationStateEquipList = Json(Public_Function.DataTableToJSON(smartFactory_InfomationStatePopup_EquipList).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
                ViewBag.smartFactoryInfomationStateData1 = Json(Public_Function.DataTableToJSON(null).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
                ViewBag.smartFactoryInfomationStateData2 = Json(Public_Function.DataTableToJSON(null).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

                ViewBag.loginData = Json(Public_Function.DataTableToJSON(loginData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

                return View();
            }
            else
            {
                string equip_cd = smartFactory_InfomationStatePopup_EquipList.Rows[0].ItemArray[0].ToString();


                DataSet smartFactoryInfomationStateData = smartFactory_InfomationStateService.SelectFactoryInfomation(equip_cd);

                ViewBag.smartFactoryInfomationStateEquipList = Json(Public_Function.DataTableToJSON(smartFactory_InfomationStatePopup_EquipList).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
                ViewBag.smartFactoryInfomationStateData1 = Json(Public_Function.DataTableToJSON(smartFactoryInfomationStateData.Tables[0]).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
                ViewBag.smartFactoryInfomationStateData2 = Json(Public_Function.DataTableToJSON(smartFactoryInfomationStateData.Tables[1]).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

                ViewBag.loginData = Json(Public_Function.DataTableToJSON(loginData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            }
            
            return View();
        }


        public ActionResult SmartFactory_InfomationState2(string equip_cd)
        {
            DataSet factoryInfomation = smartFactory_InfomationStateService.SelectFactoryInfomation(equip_cd);


            string[] jsonArr = new string[2];

            if (factoryInfomation == null)
            {
                jsonArr[0] = null;
                jsonArr[1] = null;
            }
            else
            {
                jsonArr[0] = factoryInfomation.Tables[0].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(factoryInfomation.Tables[0]);
                jsonArr[1] = factoryInfomation.Tables[1].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(factoryInfomation.Tables[1]);
            }


            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult ItemImgSearch(string item_cd)
        {
            DataTable ItemImgData = smartFactory_InfomationStateService.ItemImgSearch(item_cd);

            return ViewBag.ItemImgData = Json(Public_Function.DataTableToJSON(ItemImgData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult BarSplineChartDataSearch(string start_time, string stop_time, string equip_cd, string equip_type2, string item_cd)
        {
            DataTable ChartData = smartFactory_InfomationStateService.ChartDataSearch(start_time, stop_time, equip_cd, equip_type2, item_cd);

            return ViewBag.ChartData = Json(Public_Function.DataTableToJSON(ChartData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        public ActionResult SmartFactory_InfomationGrid()
        {
            DataTable Items = codeHelpService.GetCode(CodeHelpType.item, Public_Function.selectedPLANT, "");

            DateTime ed = new DateTime();
            ed = DateTime.Now;
            ed = ed.AddDays(1);
            DateTime sd = new DateTime();
            sd = ed.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");

            DataTable totalData = smartFactory_InfomationStateService.SelectTotalData(sDate, eDate, "");
            DataTable chartData = smartFactory_InfomationStateService.SelectChartData(sDate, eDate, "");


            ViewBag.totalData = Json(Public_Function.DataTableToJSON(totalData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.chartData = Json(Public_Function.DataTableToJSON(chartData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            ViewBag.Items = Json(Public_Function.DataTableToJSON(Items));

            return View();
        }


        [HttpPost]
        public ActionResult SmartFactory_InfomationGridSelect(string s_start_date, string s_end_date, string item_cd)
        {
            DataTable totalData = smartFactory_InfomationStateService.SelectTotalData(s_start_date, s_end_date, item_cd);
            DataTable chartData = smartFactory_InfomationStateService.SelectChartData(s_start_date, s_end_date, item_cd);

            string[] jsonArr = new string[2];
            jsonArr[0] = Public_Function.DataTableToJSON(totalData);
            jsonArr[1] = Public_Function.DataTableToJSON(chartData);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region HACCP_FileManage - 수동 파일업로드 관리
        private HACCP_FileManageService fileManageService = new HACCP_FileManageService();
        


        [HttpGet]
        public ActionResult HACCP_FileManage(string use_yn, string FinalConfirm)
        {
            DataTable HACCP_FileManageData = HACCP_ManagementScreenService.getFileList("Y", "Y");

            ViewBag.HACCP_FileManageData = Json(Public_Function.DataTableToJSON(HACCP_FileManageData));
            ViewBag.HACCP_FileManageAuth = Json(HttpContext.Session["HACCP_FileManageAuth"]);

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult getFileList(string use_yn, string FinalConfirm)
        {
            DataTable dt = HACCP_ManagementScreenService.getFileList(use_yn, FinalConfirm);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult getFileListDetail(string sdate, string edate, string doc_cd)
        {
            DataTable dt = HACCP_ManagementScreenService.getFileListDetail(sdate, edate, doc_cd);

            return Json(Public_Function.DataTableToJSON(dt));
        }
        #endregion


        #region 작업실적 모니터링(설비가동시간)
        EquipmentOperationResultMontService equipmentOperationResultMontService = new EquipmentOperationResultMontService();
        

        public ActionResult EquipmentOperationResultMont()
        {
            DateTime today = DateTime.Now.Date;
            string firstDay = string.Format("{0:yyyy/MM/dd HH:mm:ss}", (today.AddDays(1 - today.Day)));
            string toDay = string.Format("{0:yyyy/MM/dd HH:mm:ss}", today);

            DataTable EquipmentOperationResultMontData = equipmentOperationResultMontService.Select(firstDay, toDay);
            DataTable data1 = equipmentOperationResultMontService.Select1(firstDay, toDay);
            DataTable data2 = equipmentOperationResultMontService.Select2(firstDay, toDay);

            ViewBag.EquipmentOperationResultMontData = Json(Public_Function.DataTableToJSON(EquipmentOperationResultMontData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.data1 = Json(Public_Function.DataTableToJSON(data1).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.data2 = Json(Public_Function.DataTableToJSON(data2).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            return View();
        }


        [HttpPost]
        public ActionResult EquipmentOperationResultMontSearch(string startDate, string endDate)
        {
            DataTable EquipmentOperationResultMontData = equipmentOperationResultMontService.Select(startDate, endDate);

            return ViewBag.EquipmentOperationResultMontData = Json(Public_Function.DataTableToJSON(EquipmentOperationResultMontData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult EquipmentOperationResultMont_WorkroomSearch()
        {
            DataTable EquipmentOperationResultMontData = equipmentOperationResultMontService.WorkroomSelect();

            return ViewBag.EquipmentOperationResultMontData = Json(Public_Function.DataTableToJSON(EquipmentOperationResultMontData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult EquipmentOperationResultMont_WeighingSearch()
        {
            DataTable EquipmentOperationResultMontData = equipmentOperationResultMontService.WeighingSelect();

            return ViewBag.EquipmentOperationResultMontData = Json(Public_Function.DataTableToJSON(EquipmentOperationResultMontData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }



        [HttpPost]
        public ActionResult EquipmentOperationResultMont_WaitingSearch()
        {
            DataTable EquipmentOperationResultMontData = equipmentOperationResultMontService.WaitingSelect();

            return ViewBag.EquipmentOperationResultMontData = Json(Public_Function.DataTableToJSON(EquipmentOperationResultMontData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }



        [HttpPost]
        public ActionResult EquipmentOperationResultMont_EquipSearch()
        {
            DataTable EquipmentOperationResultMontData = equipmentOperationResultMontService.EquipSelect();

            return ViewBag.EquipmentOperationResultMontData = Json(Public_Function.DataTableToJSON(EquipmentOperationResultMontData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        #endregion


        [CheckSession]
        [HttpPost]
        public ActionResult FileAdd(string doc_cd, string ccp_emp, string doc_base_date)
        {
            string message = "";


            try
            {
                var uploadFile = Request.Files[0];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;
                //var fgubun = name.Substring(name.Length - 1, 1);

                var doc_name = fileName;
                var doc_type = contentType;

                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    message = fileManageService.FileAdd(binaryReader.ReadBytes(Request.Files[0].ContentLength), fileName, doc_cd, doc_name, ccp_emp, doc_base_date);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(message);
        }

        [CheckSession]
        [HttpPost]
        public ActionResult FileDelete(string doc_cd, string file_id)
        {
            string message = "";

            if (file_id == null)
                return Json(message);

            message = fileManageService.FileDelete(doc_cd, file_id);

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(message);
        }

        [HttpPost]
        public ActionResult FileOpen1(string file_id)
        {
            string message = "";

            message = fileManageService.FileOpen(file_id);


            //var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(message);
        }

        [HttpGet]
        public ActionResult FileOpen(string file_id)
        {
            DataTable dt = fileManageService.EditActionFileOpen(file_id);

            var tmp = Path.GetExtension(dt.Rows[0].ItemArray[1].ToString());

            string mimeType = GetMimeType(tmp);


            return File(
                (byte[])dt.Rows[0].ItemArray[0]
                , mimeType
                , dt.Rows[0].ItemArray[1].ToString()
                );
        }

        [HttpGet]
        public ActionResult EQUIP_FileOpen(string file_id)
        {
            DataTable dt = fileManageService.EditActionEQUIP_FileOpen(file_id);

            var tmp = Path.GetExtension(dt.Rows[0].ItemArray[1].ToString());

            string mimeType = GetMimeType(tmp);


            return File(
                (byte[])dt.Rows[0].ItemArray[0]
                , mimeType
                , dt.Rows[0].ItemArray[1].ToString()
                );
        }

        #region 파일 다운로드 확장자 정보
        private IDictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {

        #region Big freaking list of mime types
        // combination of values from Windows 7 Registry and 
        // from C:\Windows\System32\inetsrv\config\applicationHost.config
        // some added, including .7z and .dat
        {".323", "text/h323"},
        {".3g2", "video/3gpp2"},
        {".3gp", "video/3gpp"},
        {".3gp2", "video/3gpp2"},
        {".3gpp", "video/3gpp"},
        {".7z", "application/x-7z-compressed"},
        {".aa", "audio/audible"},
        {".AAC", "audio/aac"},
        {".aaf", "application/octet-stream"},
        {".aax", "audio/vnd.audible.aax"},
        {".ac3", "audio/ac3"},
        {".aca", "application/octet-stream"},
        {".accda", "application/msaccess.addin"},
        {".accdb", "application/msaccess"},
        {".accdc", "application/msaccess.cab"},
        {".accde", "application/msaccess"},
        {".accdr", "application/msaccess.runtime"},
        {".accdt", "application/msaccess"},
        {".accdw", "application/msaccess.webapplication"},
        {".accft", "application/msaccess.ftemplate"},
        {".acx", "application/internet-property-stream"},
        {".AddIn", "text/xml"},
        {".ade", "application/msaccess"},
        {".adobebridge", "application/x-bridge-url"},
        {".adp", "application/msaccess"},
        {".ADT", "audio/vnd.dlna.adts"},
        {".ADTS", "audio/aac"},
        {".afm", "application/octet-stream"},
        {".ai", "application/postscript"},
        {".aif", "audio/x-aiff"},
        {".aifc", "audio/aiff"},
        {".aiff", "audio/aiff"},
        {".air", "application/vnd.Adobe.air-application-installer-package+Zip"},
        {".amc", "application/x-mpeg"},
        {".application", "application/x-ms-application"},
        {".art", "image/x-jg"},
        {".asa", "application/xml"},
        {".asax", "application/xml"},
        {".ascx", "application/xml"},
        {".asd", "application/octet-stream"},
        {".asf", "video/x-ms-asf"},
        {".ashx", "application/xml"},
        {".asi", "application/octet-stream"},
        {".asm", "text/plain"},
        {".asmx", "application/xml"},
        {".aspx", "application/xml"},
        {".asr", "video/x-ms-asf"},
        {".asx", "video/x-ms-asf"},
        {".atom", "application/atom+xml"},
        {".au", "audio/basic"},
        {".avi", "video/x-msvideo"},
        {".axs", "application/olescript"},
        {".bas", "text/plain"},
        {".bcpio", "application/x-bcpio"},
        {".bin", "application/octet-stream"},
        {".bmp", "image/bmp"},
        {".c", "text/plain"},
        {".cab", "application/octet-stream"},
        {".caf", "audio/x-caf"},
        {".calx", "application/vnd.ms-office.calx"},
        {".cat", "application/vnd.ms-pki.seccat"},
        {".cc", "text/plain"},
        {".cd", "text/plain"},
        {".cdda", "audio/aiff"},
        {".cdf", "application/x-cdf"},
        {".cer", "application/x-x509-ca-cert"},
        {".chm", "application/octet-stream"},
        {".class", "application/x-Java-applet"},
        {".clp", "application/x-msclip"},
        {".cmx", "image/x-cmx"},
        {".cnf", "text/plain"},
        {".cod", "image/cis-cod"},
        {".config", "application/xml"},
        {".contact", "text/x-ms-contact"},
        {".coverage", "application/xml"},
        {".cpio", "application/x-cpio"},
        {".cpp", "text/plain"},
        {".crd", "application/x-mscardfile"},
        {".crl", "application/pkix-crl"},
        {".crt", "application/x-x509-ca-cert"},
        {".cs", "text/plain"},
        {".csdproj", "text/plain"},
        {".csh", "application/x-csh"},
        {".csproj", "text/plain"},
        {".css", "text/css"},
        {".csv", "text/csv"},
        {".cur", "application/octet-stream"},
        {".cxx", "text/plain"},
        {".dat", "application/octet-stream"},
        {".datasource", "application/xml"},
        {".dbproj", "text/plain"},
        {".dcr", "application/x-director"},
        {".def", "text/plain"},
        {".deploy", "application/octet-stream"},
        {".der", "application/x-x509-ca-cert"},
        {".dgml", "application/xml"},
        {".dib", "image/bmp"},
        {".dif", "video/x-dv"},
        {".dir", "application/x-director"},
        {".disco", "text/xml"},
        {".dll", "application/x-msdownload"},
        {".dll.config", "text/xml"},
        {".dlm", "text/dlm"},
        {".doc", "application/msword"},
        {".docm", "application/vnd.ms-Word.document.macroEnabled.12"},
        {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
        {".dot", "application/msword"},
        {".dotm", "application/vnd.ms-Word.template.macroEnabled.12"},
        {".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
        {".dsp", "application/octet-stream"},
        {".dsw", "text/plain"},
        {".dtd", "text/xml"},
        {".dtsConfig", "text/xml"},
        {".dv", "video/x-dv"},
        {".dvi", "application/x-dvi"},
        {".dwf", "drawing/x-dwf"},
        {".dwp", "application/octet-stream"},
        {".dxr", "application/x-director"},
        {".eml", "message/rfc822"},
        {".emz", "application/octet-stream"},
        {".eot", "application/octet-stream"},
        {".eps", "application/postscript"},
        {".etl", "application/etl"},
        {".etx", "text/x-setext"},
        {".evy", "application/envoy"},
        {".exe", "application/octet-stream"},
        {".exe.config", "text/xml"},
        {".fdf", "application/vnd.fdf"},
        {".fif", "application/fractals"},
        {".filters", "Application/xml"},
        {".fla", "application/octet-stream"},
        {".flr", "x-world/x-vrml"},
        {".flv", "video/x-flv"},
        {".fsscript", "application/fsharp-script"},
        {".fsx", "application/fsharp-script"},
        {".generictest", "application/xml"},
        {".gif", "image/gif"},
        {".group", "text/x-ms-group"},
        {".gsm", "audio/x-gsm"},
        {".gtar", "application/x-gtar"},
        {".gz", "application/x-gzip"},
        {".h", "text/plain"},
        {".hdf", "application/x-hdf"},
        {".hdml", "text/x-hdml"},
        {".hhc", "application/x-oleobject"},
        {".hhk", "application/octet-stream"},
        {".hhp", "application/octet-stream"},
        {".hlp", "application/winhlp"},
        {".hpp", "text/plain"},
        {".hqx", "application/mac-binhex40"},
        {".hta", "application/hta"},
        {".htc", "text/x-component"},
        {".htm", "text/html"},
        {".html", "text/html"},
        {".htt", "text/webviewhtml"},
        {".hwp", "application/haansofthwp"},
        {".hxa", "application/xml"},
        {".hxc", "application/xml"},
        {".hxd", "application/octet-stream"},
        {".hxe", "application/xml"},
        {".hxf", "application/xml"},
        {".hxh", "application/octet-stream"},
        {".hxi", "application/octet-stream"},
        {".hxk", "application/xml"},
        {".hxq", "application/octet-stream"},
        {".hxr", "application/octet-stream"},
        {".hxs", "application/octet-stream"},
        {".hxt", "text/html"},
        {".hxv", "application/xml"},
        {".hxw", "application/octet-stream"},
        {".hxx", "text/plain"},
        {".i", "text/plain"},
        {".ico", "image/x-icon"},
        {".ics", "application/octet-stream"},
        {".idl", "text/plain"},
        {".ief", "image/ief"},
        {".iii", "application/x-iphone"},
        {".inc", "text/plain"},
        {".inf", "application/octet-stream"},
        {".inl", "text/plain"},
        {".ins", "application/x-internet-signup"},
        {".ipa", "application/x-iTunes-ipa"},
        {".ipg", "application/x-iTunes-ipg"},
        {".ipproj", "text/plain"},
        {".ipsw", "application/x-iTunes-ipsw"},
        {".iqy", "text/x-ms-iqy"},
        {".isp", "application/x-internet-signup"},
        {".ite", "application/x-iTunes-ite"},
        {".itlp", "application/x-iTunes-itlp"},
        {".itms", "application/x-iTunes-itms"},
        {".itpc", "application/x-iTunes-itpc"},
        {".IVF", "video/x-ivf"},
        {".jar", "application/Java-archive"},
        {".Java", "application/octet-stream"},
        {".jck", "application/liquidmotion"},
        {".jcz", "application/liquidmotion"},
        {".jfif", "image/pjpeg"},
        {".jnlp", "application/x-Java-jnlp-file"},
        {".jpb", "application/octet-stream"},
        {".jpe", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".jpg", "image/jpeg"},
        {".js", "application/x-javascript"},
        {".json", "application/json"},
        {".jsx", "text/jscript"},
        {".jsxbin", "text/plain"},
        {".latex", "application/x-latex"},
        {".library-ms", "application/windows-library+xml"},
        {".lit", "application/x-ms-reader"},
        {".loadtest", "application/xml"},
        {".lpk", "application/octet-stream"},
        {".lsf", "video/x-la-asf"},
        {".lst", "text/plain"},
        {".lsx", "video/x-la-asf"},
        {".lzh", "application/octet-stream"},
        {".m13", "application/x-msmediaview"},
        {".m14", "application/x-msmediaview"},
        {".m1v", "video/mpeg"},
        {".m2t", "video/vnd.dlna.mpeg-tts"},
        {".m2ts", "video/vnd.dlna.mpeg-tts"},
        {".m2v", "video/mpeg"},
        {".m3u", "audio/x-mpegurl"},
        {".m3u8", "audio/x-mpegurl"},
        {".m4a", "audio/m4a"},
        {".m4b", "audio/m4b"},
        {".m4p", "audio/m4p"},
        {".m4r", "audio/x-m4r"},
        {".m4v", "video/x-m4v"},
        {".mac", "image/x-macpaint"},
        {".mak", "text/plain"},
        {".man", "application/x-troff-man"},
        {".manifest", "application/x-ms-manifest"},
        {".map", "text/plain"},
        {".master", "application/xml"},
        {".mda", "application/msaccess"},
        {".mdb", "application/x-msaccess"},
        {".mde", "application/msaccess"},
        {".mdp", "application/octet-stream"},
        {".me", "application/x-troff-me"},
        {".mfp", "application/x-shockwave-flash"},
        {".mht", "message/rfc822"},
        {".mhtml", "message/rfc822"},
        {".mid", "audio/mid"},
        {".midi", "audio/mid"},
        {".mix", "application/octet-stream"},
        {".mk", "text/plain"},
        {".mmf", "application/x-smaf"},
        {".mno", "text/xml"},
        {".mny", "application/x-msmoney"},
        {".mod", "video/mpeg"},
        {".mov", "video/quicktime"},
        {".movie", "video/x-sgi-movie"},
        {".mp2", "video/mpeg"},
        {".mp2v", "video/mpeg"},
        {".mp3", "audio/mpeg"},
        {".mp4", "video/mp4"},
        {".mp4v", "video/mp4"},
        {".mpa", "video/mpeg"},
        {".mpe", "video/mpeg"},
        {".mpeg", "video/mpeg"},
        {".mpf", "application/vnd.ms-mediapackage"},
        {".mpg", "video/mpeg"},
        {".mpp", "application/vnd.ms-project"},
        {".mpv2", "video/mpeg"},
        {".mqv", "video/quicktime"},
        {".ms", "application/x-troff-ms"},
        {".msi", "application/octet-stream"},
        {".mso", "application/octet-stream"},
        {".mts", "video/vnd.dlna.mpeg-tts"},
        {".mtx", "application/xml"},
        {".mvb", "application/x-msmediaview"},
        {".mvc", "application/x-miva-compiled"},
        {".mxp", "application/x-mmxp"},
        {".nc", "application/x-netcdf"},
        {".nsc", "video/x-ms-asf"},
        {".nws", "message/rfc822"},
        {".ocx", "application/octet-stream"},
        {".oda", "application/oda"},
        {".odc", "text/x-ms-odc"},
        {".odh", "text/plain"},
        {".odl", "text/plain"},
        {".odp", "application/vnd.oasis.opendocument.presentation"},
        {".ods", "application/oleobject"},
        {".odt", "application/vnd.oasis.opendocument.text"},
        {".one", "application/onenote"},
        {".onea", "application/onenote"},
        {".onepkg", "application/onenote"},
        {".onetmp", "application/onenote"},
        {".onetoc", "application/onenote"},
        {".onetoc2", "application/onenote"},
        {".orderedtest", "application/xml"},
        {".osdx", "application/opensearchdescription+xml"},
        {".p10", "application/pkcs10"},
        {".p12", "application/x-pkcs12"},
        {".p7b", "application/x-pkcs7-certificates"},
        {".p7c", "application/pkcs7-mime"},
        {".p7m", "application/pkcs7-mime"},
        {".p7r", "application/x-pkcs7-certreqresp"},
        {".p7s", "application/pkcs7-signature"},
        {".pbm", "image/x-portable-bitmap"},
        {".pcast", "application/x-podcast"},
        {".pct", "image/pict"},
        {".pcx", "application/octet-stream"},
        {".pcz", "application/octet-stream"},
        {".pdf", "application/pdf"},
        {".pfb", "application/octet-stream"},
        {".pfm", "application/octet-stream"},
        {".pfx", "application/x-pkcs12"},
        {".pgm", "image/x-portable-graymap"},
        {".pic", "image/pict"},
        {".pict", "image/pict"},
        {".pkgdef", "text/plain"},
        {".pkgundef", "text/plain"},
        {".pko", "application/vnd.ms-pki.pko"},
        {".pls", "audio/scpls"},
        {".pma", "application/x-perfmon"},
        {".pmc", "application/x-perfmon"},
        {".pml", "application/x-perfmon"},
        {".pmr", "application/x-perfmon"},
        {".pmw", "application/x-perfmon"},
        {".png", "image/png"},
        {".pnm", "image/x-portable-anymap"},
        {".pnt", "image/x-macpaint"},
        {".pntg", "image/x-macpaint"},
        {".pnz", "image/png"},
        {".pot", "application/vnd.ms-PowerPoint"},
        {".potm", "application/vnd.ms-PowerPoint.template.macroEnabled.12"},
        {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
        {".ppa", "application/vnd.ms-PowerPoint"},
        {".ppam", "application/vnd.ms-PowerPoint.addin.macroEnabled.12"},
        {".ppm", "image/x-portable-pixmap"},
        {".pps", "application/vnd.ms-PowerPoint"},
        {".ppsm", "application/vnd.ms-PowerPoint.slideshow.macroEnabled.12"},
        {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
        {".ppt", "application/vnd.ms-PowerPoint"},
        {".pptm", "application/vnd.ms-PowerPoint.presentation.macroEnabled.12"},
        {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
        {".prf", "application/pics-rules"},
        {".prm", "application/octet-stream"},
        {".prx", "application/octet-stream"},
        {".ps", "application/postscript"},
        {".psc1", "application/PowerShell"},
        {".psd", "application/octet-stream"},
        {".psess", "application/xml"},
        {".psm", "application/octet-stream"},
        {".psp", "application/octet-stream"},
        {".pub", "application/x-mspublisher"},
        {".pwz", "application/vnd.ms-PowerPoint"},
        {".qht", "text/x-html-insertion"},
        {".qhtm", "text/x-html-insertion"},
        {".qt", "video/quicktime"},
        {".qti", "image/x-quicktime"},
        {".qtif", "image/x-quicktime"},
        {".qtl", "application/x-quicktimeplayer"},
        {".qxd", "application/octet-stream"},
        {".ra", "audio/x-pn-realaudio"},
        {".ram", "audio/x-pn-realaudio"},
        {".rar", "application/octet-stream"},
        {".ras", "image/x-cmu-raster"},
        {".rat", "application/rat-file"},
        {".rc", "text/plain"},
        {".rc2", "text/plain"},
        {".rct", "text/plain"},
        {".rdlc", "application/xml"},
        {".resx", "application/xml"},
        {".rf", "image/vnd.rn-realflash"},
        {".rgb", "image/x-rgb"},
        {".rgs", "text/plain"},
        {".rm", "application/vnd.rn-realmedia"},
        {".rmi", "audio/mid"},
        {".rmp", "application/vnd.rn-rn_music_package"},
        {".roff", "application/x-troff"},
        {".rpm", "audio/x-pn-realaudio-plugin"},
        {".rqy", "text/x-ms-rqy"},
        {".rtf", "application/rtf"},
        {".rtx", "text/richtext"},
        {".ruleset", "application/xml"},
        {".s", "text/plain"},
        {".safariextz", "application/x-safari-safariextz"},
        {".scd", "application/x-msschedule"},
        {".sct", "text/scriptlet"},
        {".sd2", "audio/x-sd2"},
        {".sdp", "application/sdp"},
        {".sea", "application/octet-stream"},
        {".searchConnector-ms", "application/windows-search-connector+xml"},
        {".setpay", "application/set-payment-initiation"},
        {".setreg", "application/set-registration-initiation"},
        {".settings", "application/xml"},
        {".sgimb", "application/x-sgimb"},
        {".sgml", "text/sgml"},
        {".sh", "application/x-sh"},
        {".shar", "application/x-shar"},
        {".shtml", "text/html"},
        {".sit", "application/x-stuffit"},
        {".sitemap", "application/xml"},
        {".skin", "application/xml"},
        {".sldm", "application/vnd.ms-PowerPoint.slide.macroEnabled.12"},
        {".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"},
        {".slk", "application/vnd.ms-Excel"},
        {".sln", "text/plain"},
        {".slupkg-ms", "application/x-ms-license"},
        {".smd", "audio/x-smd"},
        {".smi", "application/octet-stream"},
        {".smx", "audio/x-smd"},
        {".smz", "audio/x-smd"},
        {".snd", "audio/basic"},
        {".snippet", "application/xml"},
        {".snp", "application/octet-stream"},
        {".sol", "text/plain"},
        {".sor", "text/plain"},
        {".spc", "application/x-pkcs7-certificates"},
        {".spl", "application/futuresplash"},
        {".src", "application/x-wais-source"},
        {".srf", "text/plain"},
        {".SSISDeploymentManifest", "text/xml"},
        {".ssm", "application/streamingmedia"},
        {".sst", "application/vnd.ms-pki.certstore"},
        {".stl", "application/vnd.ms-pki.stl"},
        {".sv4cpio", "application/x-sv4cpio"},
        {".sv4crc", "application/x-sv4crc"},
        {".svc", "application/xml"},
        {".swf", "application/x-shockwave-flash"},
        {".t", "application/x-troff"},
        {".tar", "application/x-tar"},
        {".tcl", "application/x-tcl"},
        {".testrunconfig", "application/xml"},
        {".testsettings", "application/xml"},
        {".tex", "application/x-tex"},
        {".texi", "application/x-texinfo"},
        {".texinfo", "application/x-texinfo"},
        {".tgz", "application/x-compressed"},
        {".thmx", "application/vnd.ms-officetheme"},
        {".thn", "application/octet-stream"},
        {".tif", "image/tiff"},
        {".tiff", "image/tiff"},
        {".tlh", "text/plain"},
        {".tli", "text/plain"},
        {".toc", "application/octet-stream"},
        {".tr", "application/x-troff"},
        {".trm", "application/x-msterminal"},
        {".trx", "application/xml"},
        {".ts", "video/vnd.dlna.mpeg-tts"},
        {".tsv", "text/tab-separated-values"},
        {".ttf", "application/octet-stream"},
        {".tts", "video/vnd.dlna.mpeg-tts"},
        {".txt", "text/plain"},
        {".u32", "application/octet-stream"},
        {".uls", "text/iuls"},
        {".user", "text/plain"},
        {".ustar", "application/x-ustar"},
        {".vb", "text/plain"},
        {".vbdproj", "text/plain"},
        {".vbk", "video/mpeg"},
        {".vbproj", "text/plain"},
        {".vbs", "text/vbscript"},
        {".vcf", "text/x-vcard"},
        {".vcproj", "Application/xml"},
        {".vcs", "text/plain"},
        {".vcxproj", "Application/xml"},
        {".vddproj", "text/plain"},
        {".vdp", "text/plain"},
        {".vdproj", "text/plain"},
        {".vdx", "application/vnd.ms-visio.viewer"},
        {".vml", "text/xml"},
        {".vscontent", "application/xml"},
        {".vsct", "text/xml"},
        {".vsd", "application/vnd.visio"},
        {".vsi", "application/ms-vsi"},
        {".vsix", "application/vsix"},
        {".vsixlangpack", "text/xml"},
        {".vsixmanifest", "text/xml"},
        {".vsmdi", "application/xml"},
        {".vspscc", "text/plain"},
        {".vss", "application/vnd.visio"},
        {".vsscc", "text/plain"},
        {".vssettings", "text/xml"},
        {".vssscc", "text/plain"},
        {".vst", "application/vnd.visio"},
        {".vstemplate", "text/xml"},
        {".vsto", "application/x-ms-vsto"},
        {".vsw", "application/vnd.visio"},
        {".vsx", "application/vnd.visio"},
        {".vtx", "application/vnd.visio"},
        {".wav", "audio/wav"},
        {".wave", "audio/wav"},
        {".wax", "audio/x-ms-wax"},
        {".wbk", "application/msword"},
        {".wbmp", "image/vnd.wap.wbmp"},
        {".wcm", "application/vnd.ms-works"},
        {".wdb", "application/vnd.ms-works"},
        {".wdp", "image/vnd.ms-photo"},
        {".webarchive", "application/x-safari-webarchive"},
        {".webtest", "application/xml"},
        {".wiq", "application/xml"},
        {".wiz", "application/msword"},
        {".wks", "application/vnd.ms-works"},
        {".WLMP", "application/wlmoviemaker"},
        {".wlpginstall", "application/x-wlpg-detect"},
        {".wlpginstall3", "application/x-wlpg3-detect"},
        {".wm", "video/x-ms-wm"},
        {".wma", "audio/x-ms-wma"},
        {".wmd", "application/x-ms-wmd"},
        {".wmf", "application/x-msmetafile"},
        {".wml", "text/vnd.wap.wml"},
        {".wmlc", "application/vnd.wap.wmlc"},
        {".wmls", "text/vnd.wap.wmlscript"},
        {".wmlsc", "application/vnd.wap.wmlscriptc"},
        {".wmp", "video/x-ms-wmp"},
        {".wmv", "video/x-ms-wmv"},
        {".wmx", "video/x-ms-wmx"},
        {".wmz", "application/x-ms-wmz"},
        {".wpl", "application/vnd.ms-wpl"},
        {".wps", "application/vnd.ms-works"},
        {".wri", "application/x-mswrite"},
        {".wrl", "x-world/x-vrml"},
        {".wrz", "x-world/x-vrml"},
        {".wsc", "text/scriptlet"},
        {".wsdl", "text/xml"},
        {".wvx", "video/x-ms-wvx"},
        {".x", "application/directx"},
        {".xaf", "x-world/x-vrml"},
        {".xaml", "application/xaml+xml"},
        {".xap", "application/x-silverlight-app"},
        {".xbap", "application/x-ms-xbap"},
        {".xbm", "image/x-xbitmap"},
        {".xdr", "text/plain"},
        {".xht", "application/xhtml+xml"},
        {".xhtml", "application/xhtml+xml"},
        {".xla", "application/vnd.ms-Excel"},
        {".xlam", "application/vnd.ms-Excel.addin.macroEnabled.12"},
        {".xlc", "application/vnd.ms-Excel"},
        {".xld", "application/vnd.ms-Excel"},
        {".xlk", "application/vnd.ms-Excel"},
        {".xll", "application/vnd.ms-Excel"},
        {".xlm", "application/vnd.ms-Excel"},
        {".xls", "application/vnd.ms-Excel"},
        {".xlsb", "application/vnd.ms-Excel.sheet.binary.macroEnabled.12"},
        {".xlsm", "application/vnd.ms-Excel.sheet.macroEnabled.12"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        {".xlt", "application/vnd.ms-Excel"},
        {".xltm", "application/vnd.ms-Excel.template.macroEnabled.12"},
        {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
        {".xlw", "application/vnd.ms-Excel"},
        {".xml", "text/xml"},
        {".xmta", "application/xml"},
        {".xof", "x-world/x-vrml"},
        {".XOML", "text/plain"},
        {".xpm", "image/x-xpixmap"},
        {".xps", "application/vnd.ms-xpsdocument"},
        {".xrm-ms", "text/xml"},
        {".xsc", "application/xml"},
        {".xsd", "text/xml"},
        {".xsf", "text/xml"},
        {".xsl", "text/xml"},
        {".xslt", "text/xml"},
        {".xsn", "application/octet-stream"},
        {".xss", "application/xml"},
        {".xtp", "application/octet-stream"},
        {".xwd", "image/x-xwindowdump"},
        {".z", "application/x-compress"},
        {".Zip", "application/x-Zip-compressed"},
        #endregion

        };

        public string GetMimeType(string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException("extension");
            }

            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string mime;

            return _mappings.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
        }

        #endregion
    }
}