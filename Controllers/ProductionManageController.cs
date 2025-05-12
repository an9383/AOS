
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using HACCP.Attribute;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models;
using HACCP.Models.Common;
using HACCP.Models.ProductionManage;
using HACCP.Services.Comm;
using HACCP.Services.ProductionManage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HACCP.Controllers
{
    public class ProductionManageController : Controller
    {
        private WorkorderLedgerService WorkorderLedgerService = new WorkorderLedgerService();
        private SelectBoxService selectBoxService = new SelectBoxService();

        // 원료시험현황
        [CheckSession]
        public ActionResult TestProgressStatus(string search_number)
        {
            TestProgressStatusService testProgressStatusService = new TestProgressStatusService();

            DataTable testType = selectBoxService.GetSelectBox("공통코드", "S", "QC004", "testType");   // 시험종류
            DataTable progressStatus = selectBoxService.GetSelectBox("공통코드", "S", "QC007", "progressStatus");   // 진행상태
            DataTable fomulation = selectBoxService.GetSelectBox("공통코드", "S", "CM065", "fomulation");   // 제형
            DataTable gubun = selectBoxService.GetSelectBox("공통코드", "N", "QC074", "gubun");   // 구분

            DataTable testProgressStatus = testProgressStatusService.SelecttestProgressStatus("1", DateTime.Now.AddMonths(-1).ToShortDateString(), DateTime.Now.ToShortDateString(), "%", "%", "1", search_number, "%");

            ViewBag.testType = testType;
            ViewBag.progressStatus = progressStatus;
            ViewBag.fomulation = fomulation;
            ViewBag.gubun = gubun;

            ViewBag.search_number = search_number;

            return View("TestProgressStatus", Json(Public_Function.DataTableToJSON(testProgressStatus)));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectWorkorderTestProgressStatus(TestProgressStatus testProgressStatus)
        {
            TestProgressStatusService testProgressStatusService = new TestProgressStatusService();

            DataTable testProgressStatusData = testProgressStatusService.SelecttestProgressStatus(
                testProgressStatus.date_option
                , testProgressStatus.start_date
                , testProgressStatus.end_date
                , testProgressStatus.test_type
                , testProgressStatus.test_status
                , testProgressStatus.search_gubun
                , testProgressStatus.search_number
                , testProgressStatus.item_form_cd);

            return Json(Public_Function.DataTableToJSON(testProgressStatusData));
        }



        #region WorkorderLedger 제조지시 대장 작성 컨트롤러
        [CheckSession]
        public ActionResult WorkorderLedger()
        {
            List<Item> items = WorkorderLedgerService.SelectItem("", "");//제품 Lookup

            DataTable teamGubuns = selectBoxService.GetSelectBox("공통코드", "S", "MD001", "teamGubuns");//구분(상단 조회 조건)
            DataTable teamGubuns_1 = selectBoxService.GetSelectBox("공통코드", "P", "MD003", "teamGubuns_1");//제조부서(하단 제조지시대장작성 - visible:false)
            DataTable teamGubuns_2 = selectBoxService.GetSelectBox("공통코드", "P", "MD001", "teamGubuns_2");//구분(하단 제조지시대장작성)

            DataTable signGubuns = selectBoxService.GetSelectBox("공통코드", "S", "CM091", "signGubuns");//상태(상단 조회 조건)
            DataTable teamGubuns_3 = selectBoxService.GetSelectBox("공통코드", "S", "MD003", "teamGubuns_3");//제조부서(상단 조회 조건 - visible:false)
            DataTable unitGubuns = selectBoxService.GetSelectBox("공통코드", "N", "CM012", "unitGubuns");//단위(생산계획 그리드)

            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(items);

            ViewBag.items = Json(JSONresult);
            ViewBag.teamGubuns = teamGubuns;
            ViewBag.teamGubuns_1 = teamGubuns_1;
            ViewBag.teamGubuns_2 = teamGubuns_2;
            ViewBag.teamGubuns_3 = teamGubuns_3;
            ViewBag.signGubuns = signGubuns;
            ViewBag.unitGubuns = unitGubuns;

            ViewBag.WorkorderLedgerAuth = Json(HttpContext.Session["WorkorderLedgerAuth"]);

            return View();
        }


        [HttpPost]
        public JsonResult GetWorkOrder(string pSearchDate, string pDeptCd, string pDeptCd_1, string pSignStatus, string pSearchEndDate, string pItemCd, string pChkVisible)
        {

            DataTable aps = WorkorderLedgerService.SelectAPS("SelectAPS", pSearchDate, pSearchEndDate, pChkVisible);
            DataTable workAll = WorkorderLedgerService.SelectWorkOrderAll("SelectWorkOrderLedgerAll", pSearchDate, pDeptCd, pDeptCd_1, pSignStatus, pSearchEndDate, pItemCd);

            string jsonData = Public_Function.DataTableToJSON(aps);
            string jsonData_1 = Public_Function.DataTableToJSON(workAll);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);
            jsonList.Add(jsonData_1);

            return Json(jsonList.ToArray());
        }

        [CheckSession]
        [HttpPost]
        public JsonResult GetPopupItemDetail(string pItemCd, string pRevisionNo, string pOrderCnt, string pBatchSize)
        {
            DataTable item = WorkorderLedgerService.SelectPopupItemDetail("S2", pItemCd, pRevisionNo, pOrderCnt, pBatchSize);

            string jsonData = Public_Function.DataTableToJSON(item);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);

            return Json(jsonList.ToArray());
        }

        [CheckSession]
        [HttpPost]
        public JsonResult GetPopupItem(string pItemCd, string pDeptCd, string pBatchSize, string pType)
        {
            DataTable item = WorkorderLedgerService.SelectPopupItem("S", pItemCd, pDeptCd, pBatchSize, pType);

            string jsonData = Public_Function.DataTableToJSON(item);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);

            return Json(jsonList.ToArray());
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectWorkOrderDetail(string order_id, string item_cd)
        {
            DataTable workOrderDetail = WorkorderLedgerService.SelectWorkOrderDetail(order_id, item_cd);
            DataTable workOrderElectricSign = WorkorderLedgerService.SelectWorkOrderElectricSign("M", order_id, "2007");

            string[] jsonArr = new string[2];

            jsonArr[0] = Public_Function.DataTableToJSON(workOrderDetail);
            jsonArr[1] = Public_Function.DataTableToJSON(workOrderElectricSign);

            return Json(jsonArr);
        }

        [CheckSession]
        [HttpPost]
        public string GetValidDate(string pItemCd, string pOrderDate)
        {
            string validDate = "";
            validDate = WorkorderLedgerService.SelectValidDate("GetValidDate", pItemCd, pOrderDate);

            return validDate;
        }


        [CheckSession]
        [HttpPost]
        public JsonResult WorkOrderLedgerCRUD([JsonBinder]List<WorkorderLedger> workOrders, string order_id)
        {
            string messege = WorkorderLedgerService.WorkOrderLedgerCRUD(workOrders, order_id);

            var resJson = "{ \"messege\": \"" + messege + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public string GetOrderId(WorkorderLedger workorderLedger, string gubun)
        {
            string orderId = WorkorderLedgerService.GetOrderId(workorderLedger, gubun);

            return orderId;
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ClearOrderId(string order_id)
        {
            string res = WorkorderLedgerService.DeleteByOrderId(order_id);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult WorkorderSignCRUD(string gubun, string c_seq, string emp_cd, string order_id, string validation_type, int sign_status)
        {
            //string empCd = WorkorderLedgerService.GetSignEmpCd(c_seq, emp_cd, "2007");

            string res = WorkorderLedgerService.SignCRUD(gubun, order_id, emp_cd, validation_type, "emp"+c_seq, sign_status);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        #endregion


        #region Workorder 제조지시

        private WorkorderService workorderService = new WorkorderService();

        [CheckSession]
        public ActionResult Workorder()
        {
            DataTable manufactureState = selectBoxService.GetSelectBox("공통코드", "S", "RT005", "manufactureState");//제조상태
            DataTable workorderData = workorderService.SelectWorkOrder(
                DateTime.Now.AddDays(-(DateTime.Now.Day-1)).Date.ToString("yyyy-MM-dd")
                , DateTime.Now.Date.ToString("yyyy-MM-dd"), "", "%", "");
            DataTable itemPopupData = workorderService.SelectItemPopupData("%", "%");

            ViewBag.manufactureState = manufactureState;
            ViewBag.workorderData = Json(Public_Function.DataTableToJSON(workorderData));
            ViewBag.itemPopupData = Json(Public_Function.DataTableToJSON(itemPopupData));
            ViewBag.WorkorderAuth = Json(HttpContext.Session["WorkorderAuth"]);

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult WorkorderSelect(string sdate, string edate, string item_cd, string order_type, string lot_no_export)
        {
            DataTable workorderData = workorderService.SelectWorkOrder(sdate, edate, item_cd, order_type, lot_no_export);

            return Json(Public_Function.DataTableToJSON(workorderData));
        }


        #endregion


        #region Workorder_Request 원료불출지시

        private WorkorderRequestService workorderRequestService = new WorkorderRequestService();

        [CheckSession]
        public ActionResult Workorder_Request(string sdate, string edate, string order_no, string item_cd)
        {
            //시작일자
            if (sdate == null || sdate.Equals(""))
            {
                ViewBag.sdate = DateTime.Now.AddDays(-(DateTime.Now.Day - 1)).Date.ToString("yyyy-MM-dd");
                sdate = DateTime.Now.AddDays(-(DateTime.Now.Day - 1)).Date.ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.sdate = sdate;
            }

            //종료일자
            if (edate == null || edate.Equals(""))
            {
                ViewBag.edate = DateTime.Now.Date.ToString("yyyy-MM-dd");
                edate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.edate = edate;
            }

            //품목코드
            if (item_cd == null || item_cd.Equals(""))
            {
                ViewBag.item_cd = "";
                item_cd = "";
            }
            else
            {
                ViewBag.item_cd = item_cd;
            }

            DataTable workorderData = workorderRequestService.SelectWorkOrder(sdate, edate, item_cd, order_no);
            DataTable itemPopupData = workorderRequestService.SelectItemPopupData("%", "%");

            ViewBag.workorderData = Json(Public_Function.DataTableToJSON(workorderData));
            ViewBag.itemPopupData = Json(Public_Function.DataTableToJSON(itemPopupData));

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Workorder_RequestSelectReleaseStandard(string order_no, string item_cd, string batch_size, string real_order_qty)
        {
            DataTable releaseStandardData = workorderRequestService.SelectReleaseStandard(order_no, item_cd, batch_size, real_order_qty);

            return Json(Public_Function.DataTableToJSON(releaseStandardData));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectWorkorderRequest(string sdate, string edate, string item_cd, string order_no)
        {
            DataTable workorderData = workorderRequestService.SelectWorkOrder(sdate, edate, item_cd, order_no);

            return Json(Public_Function.DataTableToJSON(workorderData));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Workorder_RequestSelectUsage(string order_no, string req_order_gb, string req_order_id)
        {
            DataTable workorderData = workorderRequestService.SelectUsage(order_no, req_order_gb, req_order_id);

            return Json(Public_Function.DataTableToJSON(workorderData));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Workorder_RequestSelectCurrentStock(string req_order_child_cd, string item_cd, string order_no)
        {
            DataTable workorderData = workorderRequestService.SelectCurrentStock(req_order_child_cd, item_cd, order_no);

            return Json(Public_Function.DataTableToJSON(workorderData));
        }


        [HttpPost]
        public ActionResult Workorder_RequestAutoCalcUsage([JsonBinder]List<WorkorderRequest> workorderRequest, string order_no)
        {            
            string res = workorderRequestService.InsertUsage(workorderRequest, order_no);

            return Json(new { result = res });
        }

        [HttpPost]
        public ActionResult workorderRequestCalcSeperate([JsonBinder]List<WorkorderRequest> workorderRequest, string order_no, int separate_cnt)
        {
            string res = workorderRequestService.CalcSeperate(workorderRequest, order_no, separate_cnt);

            return Json(new { result = res });
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Workorder_RequestSelectQty(string order_no, string req_order_gb, string req_order_id)
        {
            string res = workorderRequestService.SelectQty(order_no, req_order_gb, req_order_id);

            string jsonRes = "{ \"result\": " + res + " }";

            return Json(jsonRes);

        }

        
        [CheckSession]
        [HttpPost]
        public JsonResult Workorder_RequestConfirm(string order_no, string req_order_gb, string req_order_id)
        {
            string res = "";
            string jsonRes = "";

            try
            {
                res = workorderRequestService.WorkorderRequestConfirm(order_no, req_order_gb, req_order_id);

            }
            catch
            {
                jsonRes = "{ \"result\": \"Fail\" }";

                return Json(jsonRes);
            }

            jsonRes = "{ \"result\": \"Success\" }";

            return Json(jsonRes);

        }
        

        [CheckSession]
        [HttpPost]
        public JsonResult Workorder_RequestFinalConfirm(string order_no)
        {
            string res = workorderRequestService.WorkorderRequestFinalConfirm(order_no);

            string jsonRes = "{ \"result\": " + res + " }";

            return Json(jsonRes);

        }


        [CheckSession]
        [HttpPost]
        public JsonResult CancelReleaseConfirmation(string order_no)
        {
            string status = workorderRequestService.WorkorderRequestCheckStatus(order_no);

            string jsonRes = "";

            if (status.Equals("완료"))
            {
                jsonRes = "{ \"result\": \"제조지시 승인 서명을 취소해주세요.\" }";

                return Json(jsonRes);
            }
            else
            {
                string res = workorderRequestService.CancelReleaseConfirmation(order_no);

                if (int.Parse(res) < 0)
                {
                    jsonRes = "{ \"result\": \"칭량작업이 완료된 원료는 확정취소를 할 수 없습니다!\" }";

                    return Json(jsonRes);
                }

                jsonRes = "{ \"result\": \"확정취소되었습니다.\" }";

                return Json(jsonRes);
            }

        }

        [CheckSession]
        [HttpPost]
        public JsonResult Workorder_RequestDeleteAllUsage(string order_no)
        {
            try
            {
                string res = workorderRequestService.WorkorderRequestDeleteAllUsage(order_no);

                string jsonRes = "{ \"result\": \"삭제되었습니다.\" }";

                return Json(jsonRes);
            }
            catch
            {
                string jsonRes = "{ \"result\": \"실패했습니다.\" }";

                return Json(jsonRes);
            }

        }

        [CheckSession]
        [HttpPost]
        public JsonResult Workorder_RequestDeleteUsage([JsonBinder]WorkorderRequest workorderRequest)
        {
            try
            {
                string res = workorderRequestService.WorkorderRequestDeleteUsage(workorderRequest);


                string jsonRes = "{ \"result\": \"삭제되었습니다.\" }";

                return Json(jsonRes);
            }
            catch
            {
                string jsonRes = "{ \"result\": \"실패했습니다.\" }";

                return Json(jsonRes);
            }
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Workorder_RequestInsertUsage([JsonBinder]WorkorderRequest workorderRequest)
        {
            try
            {
                string res = workorderRequestService.WorkorderRequestInsertUsage(workorderRequest);

                string jsonRes = "{ \"result\": \"입력되었습니다.\" }";

                return Json(jsonRes);
            }
            catch
            {
                string jsonRes = "{ \"result\": \"실패했습니다.\" }";

                return Json(jsonRes);
            }
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Workorder_RequestSeperateUsage([JsonBinder]WorkorderRequest workorderRequest, string seperateNum)
        {
            try
            {
                string res = workorderRequestService.WorkorderRequestSeperateUsage(workorderRequest, seperateNum);

                string jsonRes = "{ \"result\": \"분할되었습니다.\" }";

                return Json(jsonRes);
            }
            catch
            {
                string jsonRes = "{ \"result\": \"실패했습니다.\" }";

                return Json(jsonRes);
            }
        }


        #endregion


        #region OrderGuide 제조기록서 승인

        private OrderGuideService orderGuideService = new OrderGuideService();

        [CheckSession]
        public ActionResult OrderGuide(string order_no, string item_nm)
        {
            DataTable orderStatusGubun = selectBoxService.GetSelectBox("공통코드", "N", "RT005", "orderStatusGubun");//제조지시 상태 구분
            DataTable orderGuideWorkorderData = orderGuideService.SelectWorkorderData(order_no);
            DataTable orderGuideSignData = orderGuideService.SelectSignData("M", order_no, "2001");
            DataTable orderGuideItemProcessData = orderGuideService.SelectItemProcessData("M", order_no);
            DataTable orderGuideBomData = orderGuideService.SelectBomData(order_no);
            DataTable orderGuideReleaseData = orderGuideService.SelectReleaseData(order_no);

            //제조지시번호
            if (order_no == null || order_no.Equals(""))
            {
                ViewBag.order_no = "";
                ViewBag.item_nm = "";
                order_no = "";
            }
            else
            {
                ViewBag.order_no = order_no;    //제조지시번호
                ViewBag.item_nm = item_nm;      //품목명
            }

            ViewBag.OrderGuideAuth = Json(HttpContext.Session["OrderGuideAuth"]);

            ViewBag.orderStatusGubun = orderStatusGubun;
            ViewBag.orderGuideWorkorderData = Json(Public_Function.DataTableToJSON(orderGuideWorkorderData));
            ViewBag.orderGuideSignData = Json(Public_Function.DataTableToJSON(orderGuideSignData));
            ViewBag.orderGuideItemProcessData = Json(Public_Function.DataTableToJSON(orderGuideItemProcessData));
            ViewBag.orderGuideBomData = Json(Public_Function.DataTableToJSON(orderGuideBomData));
            ViewBag.orderGuideReleaseData = Json(Public_Function.DataTableToJSON(orderGuideReleaseData));

            return View();
        }

        [CheckSession]
        [HttpPost]
        public object GetSelectbox(DataSourceLoadOptions loadOptions, string gubun)
        {
            DataTable dt;

            dt = orderGuideService.GetSelectbox(gubun);

            List<SelectBoxGubun> list = new List<SelectBoxGubun>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[i];

                SelectBoxGubun temp;

                temp = new SelectBoxGubun(row, false);

                list.Add(temp);
            }

            return DataSourceLoader.Load(list, loadOptions);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult SelectItemProcess(string order_no)
        {
            DataTable orderGuideItemProcessData = orderGuideService.SelectItemProcessData("M", order_no);

            return Json(Public_Function.DataTableToJSON(orderGuideItemProcessData));
        }

        [HttpGet]
        public JsonResult SelectOrderGuideWorkorder(string sDate, string eDate)
        {
            DataTable orderGuideWorkorderPopup = orderGuideService.SelectWorkorderPopupData(sDate, eDate, "", "");

            return Json(Public_Function.DataTableToJSON(orderGuideWorkorderPopup), JsonRequestBehavior.AllowGet);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectOrderGuide(string order_no)
        {
            DataTable orderGuideWorkorderData = orderGuideService.SelectWorkorderData(order_no);
            DataTable orderGuideSignData = orderGuideService.SelectSignData("M", order_no, "2001");
            DataTable orderGuideItemProcessData = orderGuideService.SelectItemProcessData("M", order_no);
            DataTable orderGuideBomData = orderGuideService.SelectBomData(order_no);
            DataTable orderGuideReleaseData = orderGuideService.SelectReleaseData(order_no);

            string[] jsonArr = new string[5];

            jsonArr[0] = Public_Function.DataTableToJSON(orderGuideWorkorderData);
            jsonArr[1] = Public_Function.DataTableToJSON(orderGuideSignData);
            jsonArr[2] = Public_Function.DataTableToJSON(orderGuideItemProcessData);
            jsonArr[3] = Public_Function.DataTableToJSON(orderGuideBomData);
            jsonArr[4] = Public_Function.DataTableToJSON(orderGuideReleaseData);

            return Json(jsonArr);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult OrderGuideSignRepresentativeCk(string mp_ck, string order_no, string sign_set_cd, string sign_set_dt_id, string emp_cd)
        {
            string res = orderGuideService.OrderGuideSignRepresentativeCk(mp_ck, order_no, sign_set_cd, sign_set_dt_id, emp_cd);

            return Json("{ \"messege\": \""+res+"\" }");
        }

        [CheckSession]
        [HttpPost]
        public JsonResult OrderGuideSignCRUD(string gubun, string mp_ck, string order_no, string sign_set_cd, string sign_set_dt_id, string emp_cd, string representative_yn)
        {
            string res = orderGuideService.OrderGuideSignCRUD(gubun, mp_ck, order_no, sign_set_cd, sign_set_dt_id, emp_cd, representative_yn, "2");

            if (res.Length <= 0)
            {
                return Json("{ \"messege\": \"서명에 실패했습니다.\" }");

            }

            orderGuideService.OrderGuideUpdateSignStatus(mp_ck, order_no, sign_set_cd);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult OrderGuideAutoRelease(string order_no)
        {
            string res1 = orderGuideService.OrderGuideAutoReleaseCheck(order_no);

            if (!res1.Equals("Y"))
            {
                return Json("{ \"messege\": \"이미 불출 처리된 제조지시 입니다.\" }");
            }

            string res2 = orderGuideService.OrderGuideAutoRelease(order_no);

            var resJson = "{ \"messege\": \"" + res2 + "\" }";

            return Json(resJson);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult orderGuideAutoReleaseNotIncludeTransfer(string order_no)
        {
            string res1 = orderGuideService.OrderGuideAutoReleaseCheck(order_no);

            if (!res1.Equals("Y"))
            {
                return Json("{ \"messege\": \"이미 불출 처리된 제조지시 입니다.\" }");
            }

            string res2 = orderGuideService.orderGuideAutoReleaseNotIncludeTransfer(order_no);

            var resJson = "{ \"messege\": \"" + res2 + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult OrderGuideUpdateWorkOrder(WorkOrder workOrder)
        {
            string res = orderGuideService.OrderGuideUpdateWorkOrder(workOrder);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        //제조지시 작업중단 
        [HttpPost]
        public JsonResult OrderGuideStop(string order_no, string remark, string stopYN)
        {
            string res = orderGuideService.OrderGuideStop(order_no, remark, stopYN);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }
        #endregion


        #region OrderProcResult 공정실적등록

        private OrderProcResultService orderProcResultService = new OrderProcResultService();

        public ActionResult OrderProcResult()
        {
            DateTime today = DateTime.Now;
            string firstDay = string.Format("{0:yyyy/MM/dd}", (today.AddDays(1 - today.Day)));
            string toDay = string.Format("{0:yyyy/MM/dd}", today);



            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable folmulation = selectBoxService.GetSelectBox("공통코드", "S", "CM059", "folmulation");//제조지시 상태 구분
            DataTable itemPopupData = workorderService.SelectItemPopupData("%", "%");
            DataTable processData = orderProcResultService.GetProcessName(firstDay, toDay, "");

            DataTable Employees = codeHelpService.GetCode(CodeHelpType.employee, Public_Function.selectedPLANT, "");
            DataTable Equipments = codeHelpService.GetCode(CodeHelpType.equipment_type3, Public_Function.selectedPLANT, "");

            //string sdate = DateTime.Now.AddDays(-(DateTime.Now.Day - 1)).Date.ToString("yyyy-MM-dd");
            //string edate = DateTime.Now.Date.ToString("yyyy-MM-dd");

            //DataTable orderProcResultData = orderProcResultService.SelectOrderProcResultData("%", sdate, edate, "", "", "N", "", "%", "Y");

            ViewBag.folmulation = folmulation;
            ViewBag.processData = Json(Public_Function.DataTableToJSON(processData));
            ViewBag.itemPopupData = Json(Public_Function.DataTableToJSON(itemPopupData));
            //ViewBag.orderProcResultData = Json(Public_Function.DataTableToJSON(orderProcResultData));
            ViewBag.Employees = Json(Public_Function.DataTableToJSON(Employees));
            ViewBag.Equipments = Json(Public_Function.DataTableToJSON(Equipments));

            ViewBag.OrderProcResultAuth = Json(HttpContext.Session["OrderProcResultAuth"]);

            return View();
        }

        [HttpGet]
        public JsonResult SelectOrderProcResultDetail(string order_no, string order_proc_id, string m_order_no, string process_cd)
        {
            List<string> orderProcResultData = orderProcResultService.SelectOrderProcResultDetail(order_no, order_proc_id, m_order_no, process_cd);

            return Json(orderProcResultData.ToArray(), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult SelectOrderProcResult(OrderProcResultForSearch orderProcResult)
        {
            bool isNullVal = String.IsNullOrEmpty(orderProcResult.outsource_YN);

            DataTable processData = orderProcResultService.GetProcessName(orderProcResult.sdate, orderProcResult.edate, orderProcResult.order_no);

            //DataTable orderProcResultData = orderProcResultService.SelectOrderProcResultData(
            //    orderProcResult.process_cd
            //    , orderProcResult.sdate
            //    , orderProcResult.edate
            //    , orderProcResult.order_no
            //    , orderProcResult.item_cd
            //    , "N"
            //    , orderProcResult.lot_no
            //    , orderProcResult.trade_cd3
            //    , (!isNullVal && orderProcResult.outsource_YN.Equals("Y")) ? orderProcResult.outsource_YN : "N");


            var jsonResult = Json(Public_Function.DataTableToJSON(processData), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        [HttpGet]
        public JsonResult OrderProcResultProcessSelect(OrderProcResultForSearch orderProcResult)
        {
            bool isNullVal = String.IsNullOrEmpty(orderProcResult.outsource_YN);

            DataTable orderProcResultData = orderProcResultService.SelectOrderProcResultData(
                orderProcResult.process_cd
                , orderProcResult.sdate
                , orderProcResult.edate
                , orderProcResult.order_no
                , orderProcResult.item_cd
                , "N"
                , orderProcResult.lot_no
                , orderProcResult.trade_cd3
                , (!isNullVal && orderProcResult.outsource_YN.Equals("Y")) ? orderProcResult.outsource_YN : "N");


            var jsonResult = Json(Public_Function.DataTableToJSON(orderProcResultData), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        [HttpGet]
        public JsonResult OrderProcResultCheckLock(OrderProcResult orderProcResult)
        {
            List<string> orderProcResultLockData = orderProcResultService.OrderProcResultCheckLock(orderProcResult);

            return Json(orderProcResultLockData.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult OrderProcResultCheck(OrderProcResult orderProcResult)
        {
            string res = orderProcResultService.OrderProcResultCheck(orderProcResult);

            return Json("{ \"message\": \"" + res + "\" }", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult OrderProcResultGetMaterialStatus(string order_no, string order_proc_id)
        {
            string res = orderProcResultService.OrderProcResultGetMaterialStatus(order_no, order_proc_id);

            return Json("{ \"message\": \"" + res + "\" }", JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult OrderProcResultGetReceiveFieldUseYN()
        {
            string res = orderProcResultService.OrderProcResultGetReceiveFieldUseYN();

            return Json("{ \"message\": \"" + res + "\" }", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult OrderProcResultGetPackList(string item_cd, string test_no, string type, string packing_order_no, string process_cd)
        {
            DataTable dt = null;
            if (type == "Add_Pack")
                dt = orderProcResultService.OrderProcResultGetPackList_AddPack(item_cd, test_no);
            else
                dt = orderProcResultService.OrderProcResultGetPackList_ReturnPack(packing_order_no, process_cd);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult OrderProcResultPackCRUD([JsonBinder] List<OrderProcResultAddPack> paramData, string packing_order_id, string issue_emp_cd, string type, string packing_order_no, string process_cd)
        {
            string res = string.Empty;
            if (type == "Add_Pack")
                res = orderProcResultService.OrderProcResultAddPackCRUD(paramData, packing_order_no, packing_order_id, issue_emp_cd);
            else
            { 
                res = orderProcResultService.OrderProcResultReturnPackCRUD(paramData, packing_order_no, issue_emp_cd);
                orderProcResultService.OrderProcResultReturnEmpSign_Update(packing_order_no, process_cd, issue_emp_cd);
            }


            return Json("{ \"message\": \"" + res + "\" }", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult OrderProcResultPRCheck(OrderProcResult orderProcResult)
        {
            DataTable dt = orderProcResultService.OrderProcResultPRCheck(orderProcResult);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult OrderProcResultSelectSign(string item_cd, string m_order_no, string process_cd, string gubun)
        {
            string res = orderProcResultService.OrderProcResultSelectSign(item_cd, m_order_no, process_cd, gubun);

            return Json("{ \"selectSign\": \"" + res + "\" }", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult OrderProcResultDelete(string order_no1, string order_proc_id1, string testrequest_no, string m_order_no, string packing_result_id, string gubun)
        {
            string res = orderProcResultService.OrderProcResultDeleteTest(order_no1, order_proc_id1, testrequest_no, m_order_no, packing_result_id, gubun);

            return Json("{ \"cnt\": \"" + res + "\" }", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult OrderProcResultDeleteCheck(string packing_result_id)
        {
            DataTable dt = orderProcResultService.OrderProcResultDeleteCheck(packing_result_id);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        // 생산실적 저장
        [HttpPost]
        public JsonResult OrderProcResultInsert1(OrderProcResultForInsert1 orderProcResult, string gubun, string update_check)
        {
            string res = orderProcResultService.OrderProcResultInsert1(orderProcResult, gubun, update_check);

            return Json("{ \"message\": \"" + res + "\" }");
        }


        [HttpPost]
        public JsonResult OrderProcResultSQ(string order_no, string process_cd)
        {
            string res = orderProcResultService.OrderProcResultSQ(order_no, process_cd);

            return Json("{ \"message\": \"" + res + "\" }");
        }

        [HttpPost]
        public JsonResult OrderProcResultSave(string order_no, string gubun)
        {
            string res = orderProcResultService.OrderProcResultSave(order_no, gubun);

            return Json("{ \"message\": \"" + res + "\" }");
        }

        [HttpPost]
        public JsonResult OrderProcResultUQ(OrderProcResultForInsert2 procResultForInsert2, string gubun)
        {
            string res = orderProcResultService.OrderProcResultUQ(procResultForInsert2, gubun);

            return Json("{ \"message\": \"" + res + "\" }");
        }

        [HttpGet]
        public JsonResult OrderProcResultCodeHelpCk(string codeCaption, string tmpData, string codehelptype)
        {
            bool check = false;

            if (tmpData != "")
            {
                CodeHelpService codeHelpService = new CodeHelpService();

                //codehelp 종류, 사업장코드, default 코드 or 명, 반환값
                if (codehelptype.Equals("work_employee"))
                {
                    check = codeHelpService.CheckValidity(CodeHelpType.work_employee, "", tmpData);
                }
                else if (codehelptype.Equals("equipment_type3"))
                {
                    check = codeHelpService.CheckValidity(CodeHelpType.equipment_type3, "", tmpData);
                }

            }
            else
                check = true;

            return Json("{ \"check\": \"" + check + "\" }", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OrderProcResultWorkerTRX([JsonBinder]List<OrderProcResultWorkerEquipment> paramData)
        {
            string res = orderProcResultService.OrderProcResultWorkerTRX(paramData);

            return Json("{ \"message\": \"" + res + "\" }");
        }

        [HttpGet]
        public JsonResult OrderProcResultCalcPackingUsage(string m_order_no, string packing_result, string process_cd)
        {
            DataTable dt = orderProcResultService.OrderProcResultCalcPackingUsage(m_order_no, packing_result, process_cd);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string OrderProcResultMaterialTRX([JsonBinder]List<OrderProcResultMaterial> paramData)
        {
            string res = orderProcResultService.OrderProcResultMaterialTRX(paramData);

            return res;
        }

        [HttpGet]
        public ActionResult OrderProcResultSelectMaterialStatus(string order_no)
        {
            string materialStatus = orderProcResultService.OrderProcResultSelectMaterialStatus(order_no);

            return Json( new { material_status = materialStatus }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Warehouse_Situation 반제품보관실 모니터링

        public ActionResult Warehouse_Situation()
        {
            WarehouseSituationService warehouseSituationService = new WarehouseSituationService();
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable WarehouseSituations = warehouseSituationService.SelectWarehouseSituation("", "%", "%");
            DataTable MakingItems = codeHelpService.GetCode(CodeHelpType.makingitem, Public_Function.selectedPLANT, "");

            ViewBag.WarehouseSituations = Json(Public_Function.DataTableToJSON(WarehouseSituations));
            ViewBag.MakingItems = Json(Public_Function.DataTableToJSON(MakingItems));

            return View();
        }

        public JsonResult Warehouse_SituationSelect(string item_cd, string warehouse_cd, string transfer_status)
        {
            WarehouseSituationService warehouseSituationService = new WarehouseSituationService();

            DataTable WarehouseSituations = warehouseSituationService.SelectWarehouseSituation(item_cd, warehouse_cd, transfer_status);

            return Json(Public_Function.DataTableToJSON(WarehouseSituations));
        }

        public JsonResult Warehouse_SituationSelectSub(string order_no, string order_proc_id)
        {
            WarehouseSituationService warehouseSituationService = new WarehouseSituationService();

            DataTable WarehouseSituationDetail = warehouseSituationService.Warehouse_SituationSelectSub(order_no, order_proc_id);

            return Json(Public_Function.DataTableToJSON(WarehouseSituationDetail));
        }

        public JsonResult Warehouse_Situation_MontSelect(string item_cd, string warehouse_cd, string transfer_status)
        {
            WarehouseSituationService warehouseSituationService = new WarehouseSituationService();

            DataTable WarehouseSituations = warehouseSituationService.SelectWarehouseSituation(item_cd, warehouse_cd, transfer_status);

            return Json(Public_Function.DataTableToJSON(WarehouseSituations));
        }

        public JsonResult Warehouse_Situation_MontSelectSub(string order_no, string order_proc_id)
        {
            WarehouseSituationService warehouseSituationService = new WarehouseSituationService();

            DataTable WarehouseSituationDetail = warehouseSituationService.Warehouse_SituationSelectSub(order_no, order_proc_id);

            return Json(Public_Function.DataTableToJSON(WarehouseSituationDetail));
        }


        #endregion


        #region Monitor_process 제조지시별 공정실적현황

        public ActionResult Monitor_process()
        {
            MonitorProcessService monitorProcessService = new MonitorProcessService();
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable MonitorProcess = monitorProcessService.SelectMonitorProcess(DateTime.Now.AddMonths(-1).ToShortDateString(), DateTime.Now.ToShortDateString(), "", "%");
            DataTable MakingItems = codeHelpService.GetCode(CodeHelpType.making_packing_item, Public_Function.selectedPLANT, "");

            ViewBag.MonitorProcess = Json(Public_Function.DataTableToJSON(MonitorProcess));
            ViewBag.MakingItems = Json(Public_Function.DataTableToJSON(MakingItems));

            return View();
        }

        public JsonResult Monitor_processSelect(string sdate, string edate, string item_cd, string process_status)
        {
            MonitorProcessService monitorProcessService = new MonitorProcessService();

            DataTable MonitorProcess = monitorProcessService.SelectMonitorProcess(sdate, edate, item_cd, process_status);

            return Json(Public_Function.DataTableToJSON(MonitorProcess));
        }

        #endregion


        #region Monitor_InProcessDetail 제품별 모니터링

        public ActionResult Monitor_InProcessDetail()
        {
            MonitorInProcessDetailService monitorInProcessDetailService = new MonitorInProcessDetailService();
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable MonitorInProcessDetail = monitorInProcessDetailService.SelectMonitorInProcessDetail("", DateTime.Now.AddMonths(-1).ToShortDateString(), DateTime.Now.ToShortTimeString(), "");
            DataTable MakingItems = codeHelpService.GetCode(CodeHelpType.makingitem, Public_Function.selectedPLANT, "");

            ViewBag.MonitorInProcessDetail = Json(Public_Function.DataTableToJSON(MonitorInProcessDetail));
            ViewBag.MakingItems = Json(Public_Function.DataTableToJSON(MakingItems));

            return View();
        }


        public JsonResult Monitor_InProcessDetailSelect(string item_cd, string sdate, string edate, string lot_no)
        {
            MonitorInProcessDetailService monitorInProcessDetailService = new MonitorInProcessDetailService();

            DataTable MonitorInProcessDetail = monitorInProcessDetailService.SelectMonitorInProcessDetail(item_cd, sdate, edate, lot_no);

            return Json(Public_Function.DataTableToJSON(MonitorInProcessDetail));
        }

        [HttpGet]
        public JsonResult Monitor_InProcessDetailSelectDetail(string order_no)
        {
            MonitorInProcessDetailService monitorInProcessDetailService = new MonitorInProcessDetailService();

            DataTable dt1 = monitorInProcessDetailService.MonitorInProcessDetailSelectDetail1(order_no); // 표준공정 순서
            DataTable dt2 = monitorInProcessDetailService.MonitorInProcessDetailSelectDetail2(order_no); // 공정진행 이력

            string[] jsonArr = new string[2];

            jsonArr[0] = Public_Function.DataTableToJSON(dt1);
            jsonArr[1] = Public_Function.DataTableToJSON(dt2);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult MonitorInProcessDetailSelectProcessDetail(string order_no, string order_proc_id)
        {
            MonitorInProcessDetailService monitorInProcessDetailService = new MonitorInProcessDetailService();

            DataTable dt = monitorInProcessDetailService.MonitorInProcessDetailSelectProcessDetail(order_no, order_proc_id);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet); ;
        }

        #endregion


        #region OrderProcResult_Monitor 일일 작업 일보

        public ActionResult OrderProcResult_Monitor()
        {
            OrderProcResultMonitorService orderProcResultMonitorService = new OrderProcResultMonitorService();
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable OrderProcResultMonitor = orderProcResultMonitorService.SelectOrderProcResultMonitor(
                DateTime.Now.AddMonths(-1).ToShortDateString(), DateTime.Now.ToShortDateString(), "", "N", "%", "%");
            DataTable MakingItems = codeHelpService.GetCode(CodeHelpType.makingitem, Public_Function.selectedPLANT, "");

            ViewBag.OrderProcResultMonitor = Json(Public_Function.DataTableToJSON(OrderProcResultMonitor));
            ViewBag.MakingItems = Json(Public_Function.DataTableToJSON(MakingItems));

            return View();
        }

        [HttpGet]
        public JsonResult OrderProcResult_MonitorSelect(string sdate, string edate, string item_cd, string zero_work_yn, string trade_cd2, string trade_cd3)
        {
            OrderProcResultMonitorService orderProcResultMonitorService = new OrderProcResultMonitorService();

            DataTable OrderProcResultMonitor = orderProcResultMonitorService.SelectOrderProcResultMonitor(sdate, edate, item_cd, zero_work_yn, trade_cd2, trade_cd3);

            return Json(Public_Function.DataTableToJSON(OrderProcResultMonitor), JsonRequestBehavior.AllowGet);
        }

        public JsonResult OrderProcResultMonitorSelectChartData(string order_no)
        {
            OrderProcResultMonitorService orderProcResultMonitorService = new OrderProcResultMonitorService();

            DataTable OrderProcResultMonitorChartData = orderProcResultMonitorService.OrderProcResultMonitorSelectChartData(order_no);

            return Json(Public_Function.DataTableToJSON(OrderProcResultMonitorChartData), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region EquipPlan 설비 가동 계획

        EquipPlanService equipPlanService = new EquipPlanService();

        public ActionResult EquipPlan()
        {
            DataTable EquipPlan = equipPlanService.SelectEquipPlan(
                 DateTime.Now.AddMonths(-1).ToShortDateString(), DateTime.Now.ToShortDateString(), "");

            DataTable itemPopupData = equipPlanService.getItemPopupData();

            ViewBag.EquipPlan = Json(Public_Function.DataTableToJSON(EquipPlan));
            ViewBag.itemPopupData = Json(Public_Function.DataTableToJSON(itemPopupData));

            return View();
        }


        public JsonResult EquipPlanGridSelect(string sdate, string edate, string item_cd)
        {
            DataTable EquipPlan = equipPlanService.SelectEquipPlan(sdate, edate, item_cd);


            return Json(Public_Function.DataTableToJSON(EquipPlan));
        }


        public JsonResult EquipPlanOrderPopupSelect()
        {
            DataTable orderPopupData = equipPlanService.getOrderPopupData();


            return Json(Public_Function.DataTableToJSON(orderPopupData));
        }


        public JsonResult EquipPlanEquipPopupSelect(WorkorderLedger orderLedger)
        {
            DataTable equipPopupData = equipPlanService.getEquipPopupData(orderLedger);


            return Json(Public_Function.DataTableToJSON(equipPopupData));
        }

        public string EquipPlanGrid_CRUD(EquipPlan equipPlan)
        {
            string res = equipPlanService.EquipPlanGrid_CRUD(equipPlan);

            return res;
        }

        #endregion



        #region EquipResults 설비 가동 실적

        public ActionResult EquipResults()
        {
            CodeHelpService codeHelpService = new CodeHelpService();
            EquipResultsService equipResultsService = new EquipResultsService();

            DataTable EquipResults = equipResultsService.SelectEquipResults(
                 DateTime.Now.AddMonths(-1).ToShortDateString(), DateTime.Now.ToShortDateString(), "");
            DataTable Equipment = codeHelpService.GetCode(CodeHelpType.equipment, Public_Function.selectedPLANT, "");

            ViewBag.EquipResults = Json(Public_Function.DataTableToJSON(EquipResults));
            ViewBag.Equipment = Json(Public_Function.DataTableToJSON(Equipment));

            return View();
        }

        public JsonResult EquipResultsSelect(string sdate, string edate, string equip_cd)
        {
            EquipResultsService equipResultsService = new EquipResultsService();

            DataTable EquipResults = equipResultsService.SelectEquipResults(sdate, edate, equip_cd);

            return Json(Public_Function.DataTableToJSON(EquipResults));
        }

        #endregion


        #region WorkResult 생산공수현황

        public ActionResult WorkResult()
        {
            CodeHelpService codeHelpService = new CodeHelpService();
            WorkResultService workResultService = new WorkResultService();

            string year = DateTime.Now.Year.ToString();
            string month = (DateTime.Now.Month >= 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString());

            DataSet WorkResult = workResultService.SelectWorkResult(year, month, "");
            DataTable Item = codeHelpService.GetCode(CodeHelpType.makingitem, Public_Function.selectedPLANT, "");

            ViewBag.WorkResult = Json(JsonConvert.SerializeObject(WorkResult));
            ViewBag.Item = Json(Public_Function.DataTableToJSON(Item));

            return View();
        }

        [HttpGet]
        public JsonResult WorkResultSelect(string year, string month, string item_cd)
        {
            WorkResultService workResultService = new WorkResultService();

            DataSet WorkResult = workResultService.SelectWorkResult(year, month, item_cd);

            return Json(JsonConvert.SerializeObject(WorkResult), JsonRequestBehavior.AllowGet);
        }



        #endregion


        #region Order_CCPPrint 제조기록서 및 CCP일지
        private Order_CCPPrintService order_CCPPrintService = new Order_CCPPrintService();
        [Route("ProductionManage/Order_CCPPrint")]
        public ActionResult Order_CCPPrint(string order_no)
        {           

            //DataTable CCPHeaderData = order_CCPPrintService.getCCP(order_no);

            //ViewBag.CCPHeaderData = Json(Public_Function.DataTableToJSON(CCPHeaderData));
            ViewBag.Order_CCPPrintAuth = Json(HttpContext.Session["Order_CCPPrintAuth"]);

            return View();
        }

        [HttpPost]
        public ActionResult getCCP(string order_no)
        {
            DataTable getCCP = order_CCPPrintService.getCCP(order_no);

            return Json(Public_Function.DataTableToJSON(getCCP));
        }
        #endregion


        #region WeightingRecordCorrect 칭량 기록 조회
        WeightingRecordCorrectService weightingRecordCorrectService = new WeightingRecordCorrectService();

        [CheckSession]
        public ActionResult WeightingRecordCorrect()
        {
            DataTable InputOrderStatus = selectBoxService.GetSelectBox("공통코드", "D", "WH001", "input_order_status");
            ViewBag.InputOrderStatus = InputOrderStatus;
            DataTable InputStatus = selectBoxService.GetSelectBox("공통코드", "D", "WH002", "input_status");
            ViewBag.InputStatus = InputStatus;

            string start_date = DateTime.Today.AddDays(-(DateTime.Today.Day - 1)).ToShortDateString();
            string end_date = DateTime.Today.ToShortDateString();

            DataTable EquipmentOperationStatusData = weightingRecordCorrectService.Select("", "", start_date, end_date);
            ViewBag.WeightingRecordCorrectData = Json(Public_Function.DataTableToJSON(EquipmentOperationStatusData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            DataTable itemData = weightingRecordCorrectService.SelectItem();
            ViewBag.items = Json(Public_Function.DataTableToJSON(itemData));

            //ViewBag.WeightingRecordCorrectAuth = "";

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult WeightingRecordCorrectSelect(string item_cd, string order_no, string start_date, string end_date)
        {
            DataTable dt = weightingRecordCorrectService.Select(item_cd, order_no, start_date, end_date);


            return Json(Public_Function.DataTableToJSON(dt));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectWeighingInfo(string gubun, string order_no, string input_order_id, string weighing_no, string weighing_detailproc_id)
        { 

            List<string> getCCP_preview = weightingRecordCorrectService.SelectWeighingInfo(gubun, order_no, input_order_id, weighing_no, weighing_detailproc_id);

            return Json(getCCP_preview.ToArray());
        }

        [HttpPost]
        public JsonResult WeightingRecordCorrectSave(string order_no, string lot_no, string input_order_id, string weighing_no, string start_time, string end_time)
        {
            string res = weightingRecordCorrectService.WeightingRecordCorrectSave(order_no, lot_no, input_order_id, weighing_no, start_time, end_time);

            var resJson = "{ \"message\": \"" + res + "\" }";

            return Json(resJson);
        }


        #endregion

        #region 모니터링 모듈

        // 제품별 모니터링
        public ActionResult Monitor_InProcessDetail_Mont()
        {
            MonitorInProcessDetailService monitorInProcessDetailService = new MonitorInProcessDetailService();
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable MonitorInProcessDetail = monitorInProcessDetailService.SelectMonitorInProcessDetail("", DateTime.Now.AddMonths(-1).ToShortDateString(), DateTime.Now.ToShortTimeString(), "");
            DataTable MakingItems = codeHelpService.GetCode(CodeHelpType.makingitem, Public_Function.selectedPLANT, "");

            ViewBag.MonitorInProcessDetail = Json(Public_Function.DataTableToJSON(MonitorInProcessDetail));
            ViewBag.MakingItems = Json(Public_Function.DataTableToJSON(MakingItems));

            return View();
        }


        // 공정별 모니터링
        public ActionResult Monitor_process_Mont()
        {
            MonitorProcessService monitorProcessService = new MonitorProcessService();
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable MonitorProcess = monitorProcessService.SelectMonitorProcess(DateTime.Now.AddMonths(-1).ToShortDateString(), DateTime.Now.ToShortDateString(), "", "%");
            DataTable MakingItems = codeHelpService.GetCode(CodeHelpType.making_packing_item, Public_Function.selectedPLANT, "");

            ViewBag.MonitorProcess = Json(Public_Function.DataTableToJSON(MonitorProcess));
            ViewBag.MakingItems = Json(Public_Function.DataTableToJSON(MakingItems));

            return View();
        }


        // 반제품 보관실 모니터링
        public ActionResult Warehouse_Situation_Mont()
        {
            WarehouseSituationService warehouseSituationService = new WarehouseSituationService();
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable WarehouseSituations = warehouseSituationService.SelectWarehouseSituation("", "%", "%");
            DataTable MakingItems = codeHelpService.GetCode(CodeHelpType.makingitem, Public_Function.selectedPLANT, "");

            ViewBag.WarehouseSituations = Json(Public_Function.DataTableToJSON(WarehouseSituations));
            ViewBag.MakingItems = Json(Public_Function.DataTableToJSON(MakingItems));

            return View();
        }


        // 작업실적모니터링
        public ActionResult WorkResult_Mont()
        {
            CodeHelpService codeHelpService = new CodeHelpService();
            WorkResultService workResultService = new WorkResultService();

            string year = DateTime.Now.Year.ToString();
            string month = (DateTime.Now.Month >= 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString());

            DataSet WorkResult = workResultService.SelectWorkResult(year, month, "");
            DataTable Item = codeHelpService.GetCode(CodeHelpType.makingitem, Public_Function.selectedPLANT, "");

            ViewBag.WorkResult = Json(JsonConvert.SerializeObject(WorkResult));
            ViewBag.Item = Json(Public_Function.DataTableToJSON(Item));

            return View();
        }


        // 공정별 생산 진행현황
        public ActionResult WorkResult_Mont2()
        {
            CodeHelpService codeHelpService = new CodeHelpService();
            WorkResultService workResultService = new WorkResultService();

            string year = DateTime.Now.Year.ToString();
            string month = (DateTime.Now.Month >= 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString());

            DataSet WorkResult = workResultService.SelectWorkResult(year, month, "");
            DataTable Item = codeHelpService.GetCode(CodeHelpType.makingitem, Public_Function.selectedPLANT, "");

            ViewBag.WorkResult = Json(JsonConvert.SerializeObject(WorkResult));
            ViewBag.Item = Json(Public_Function.DataTableToJSON(Item));

            return View();
        }



        // 작업현황(칭량, 제조, 포장)

        OperationStatus_MontService operationStatus_MontService = new OperationStatus_MontService();

        public ActionResult OperationStatus_Mont()
        {
            DateTime today = DateTime.Now.Date;
            string firstDay = string.Format("{0:yyyy/MM/dd HH:mm:ss}", (today.AddDays(1 - today.Day)));
            string toDay = string.Format("{0:yyyy/MM/dd HH:mm:ss}", today);

            DataTable OperationSatus_Mont_data = operationStatus_MontService.Select();
            ViewBag.OperationSatus_Mont_data = Json(Public_Function.DataTableToJSON(OperationSatus_Mont_data).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            return View();
        }


        [HttpPost]
        public ActionResult OperationStatus_MontSearch()
        {
            DataTable OperationSatus_Mont_data = operationStatus_MontService.Select();

            return ViewBag.OperationSatus_Mont_data = Json(Public_Function.DataTableToJSON(OperationSatus_Mont_data).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult OperationSatus_Mont_weighing_search()
        {
            DataSet OperationSatus_Mont_data = operationStatus_MontService.WeighingSelect();

            string[] jsonArr = new string[2];
            jsonArr[0] = OperationSatus_Mont_data.Tables[0].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(OperationSatus_Mont_data.Tables[0]);
            jsonArr[1] = OperationSatus_Mont_data.Tables[1].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(OperationSatus_Mont_data.Tables[1]);

            JsonResult jsonResultData = Json(jsonArr, JsonRequestBehavior.AllowGet);
            jsonResultData.MaxJsonLength = int.MaxValue;

            return jsonResultData;
        }


        [HttpPost]
        public ActionResult OperationSatus_Mont_waiting_search()
        {
            DataSet OperationSatus_Mont_data = operationStatus_MontService.WaitingSelect();

            string[] jsonArr = new string[2];
            jsonArr[0] = OperationSatus_Mont_data.Tables[0].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(OperationSatus_Mont_data.Tables[0]);
            jsonArr[1] = OperationSatus_Mont_data.Tables[1].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(OperationSatus_Mont_data.Tables[1]);

            JsonResult jsonResultData = Json(jsonArr, JsonRequestBehavior.AllowGet);
            jsonResultData.MaxJsonLength = int.MaxValue;

            return jsonResultData;
        }


        [HttpPost]
        public ActionResult OperationSatus_Mont_working_search()
        {
            DataSet OperationSatus_Mont_data = operationStatus_MontService.WorkingSelect();

            string[] jsonArr = new string[2];
            jsonArr[0] = OperationSatus_Mont_data.Tables[0].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(OperationSatus_Mont_data.Tables[0]);
            jsonArr[1] = OperationSatus_Mont_data.Tables[1].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(OperationSatus_Mont_data.Tables[1]);

            JsonResult jsonResultData = Json(jsonArr, JsonRequestBehavior.AllowGet);
            jsonResultData.MaxJsonLength = int.MaxValue;

            return jsonResultData;
        }



        // 작업설비별 가동현황

        EquipmentOperation_MontService equipmentOperation_MontService = new EquipmentOperation_MontService();

        public ActionResult EquipmentOperation_Mont()
        {
            DateTime today = DateTime.Now.Date;
            string firstDay = string.Format("{0:yyyy/MM/dd HH:mm:ss}", (today.AddDays(1 - today.Day)));
            string toDay = string.Format("{0:yyyy/MM/dd HH:mm:ss}", today);

            DataTable EquipmentOperation_Mont_data = equipmentOperation_MontService.Select();

            ViewBag.EquipmentOperation_Mont_data = Json(Public_Function.DataTableToJSON(EquipmentOperation_Mont_data).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));



            return View();
        }


        [HttpPost]
        public ActionResult EquipmentOperation_MontSearch()
        {
            DataTable OperationSatus_Mont_data = equipmentOperation_MontService.Select();

            JsonResult returnJsonData = new JsonResult();
            returnJsonData = Json(Public_Function.DataTableToJSON(OperationSatus_Mont_data).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            returnJsonData.MaxJsonLength = int.MaxValue;

            return ViewBag.OperationSatus_Mont_data = returnJsonData;
        }



        [HttpPost]
        public ActionResult EquipmentOperation_Mont_equip_search()
        {
            DataTable OperationSatus_Mont_data = equipmentOperation_MontService.EquipSelect();

            return ViewBag.OperationSatus_Mont_data = Json(Public_Function.DataTableToJSON(OperationSatus_Mont_data).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 시험현황

        TestProgress_MontService testProgress_MontService = new TestProgress_MontService();

        public ActionResult TestProgress_Mont()
        {

            DataSet TestProgress_Mont_data = testProgress_MontService.Select();

            ViewBag.TestProgress_Mont_StateDetail = Json(Public_Function.DataTableToJSON(TestProgress_Mont_data.Tables[0]).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.TestProgress_Mont_State = Json(Public_Function.DataTableToJSON(TestProgress_Mont_data.Tables[1]).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.TestProgress_Mont_Detail = Json(Public_Function.DataTableToJSON(TestProgress_Mont_data.Tables[2]).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));


            return View();
        }


        [HttpPost]
        public ActionResult TestProgress_MontSearch()
        {
            DataSet TestProgress_Mont_data = testProgress_MontService.Select();


            string[] jsonArr = new string[3];
            jsonArr[0] = TestProgress_Mont_data.Tables[0].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(TestProgress_Mont_data.Tables[0]);
            jsonArr[1] = TestProgress_Mont_data.Tables[1].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(TestProgress_Mont_data.Tables[1]);
            jsonArr[2] = TestProgress_Mont_data.Tables[2].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(TestProgress_Mont_data.Tables[2]);

            JsonResult jsonResultData = Json(jsonArr, JsonRequestBehavior.AllowGet);
            jsonResultData.MaxJsonLength = int.MaxValue;

            return jsonResultData;
        }



        // 테스트설비별 가동현황

        EquipmentOperation_MontService quipmentOperation_MontService = new EquipmentOperation_MontService();

        public ActionResult TestEquipmentOperation_Mont()
        {
            DateTime today = DateTime.Now.Date;
            string firstDay = string.Format("{0:yyyy/MM/dd HH:mm:ss}", (today.AddDays(1 - today.Day)));
            string toDay = string.Format("{0:yyyy/MM/dd HH:mm:ss}", today);

            DataTable TestEquipmentOperation_Mont_data = equipmentOperation_MontService.TestSelect();
            ViewBag.TestEquipmentOperation_Mont_data = Json(Public_Function.DataTableToJSON(TestEquipmentOperation_Mont_data).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            return View();
        }


        [HttpPost]
        public ActionResult TestEquipmentOperation_MontSearch()
        {
            DataTable TestEquipmentOperation_Mont_data = equipmentOperation_MontService.TestSelect();

            return ViewBag.TestEquipmentOperation_Mont_data = Json(Public_Function.DataTableToJSON(TestEquipmentOperation_Mont_data).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult TestEquipmentOperation_Mont_equip_search()
        {
            DataTable TestEquipmentOperation_Mont_data = equipmentOperation_MontService.TestEquipSelect();

            return ViewBag.TestEquipmentOperation_Mont_data = Json(Public_Function.DataTableToJSON(TestEquipmentOperation_Mont_data).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }



        // 제품별 공정별 작업진행

        WorkProgress_MontService workProgress_MontService = new WorkProgress_MontService();

        public ActionResult WorkProgress_Mont()
        {

            DataSet workProgress_Mont_data = workProgress_MontService.Select();

            ViewBag.workProgress_Mont_data = Json(JsonConvert.SerializeObject(workProgress_Mont_data));

            return View();
        }


        [HttpPost]
        public ActionResult WorkProgress_MontSearch()
        {
            DataSet workProgress_Mont_data = workProgress_MontService.Select();


            string[] jsonArr = new string[2];
            jsonArr[0] = workProgress_Mont_data.Tables[0].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(workProgress_Mont_data.Tables[0]);
            jsonArr[1] = workProgress_Mont_data.Tables[1].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(workProgress_Mont_data.Tables[1]);

            JsonResult jsonResultData = Json(jsonArr, JsonRequestBehavior.AllowGet);
            jsonResultData.MaxJsonLength = int.MaxValue;

            return jsonResultData;
        }


        // 시험종류별 시험진행현황

        LimsTestProgress_MontService limsTestProgress_MontService = new LimsTestProgress_MontService();

        public ActionResult LimsTestProgress_Mont()
        {

            DataSet LimsTestProgress_Mont_data = limsTestProgress_MontService.Select();

            ViewBag.LimsTestProgress_Mont_State = Json(Public_Function.DataTableToJSON(LimsTestProgress_Mont_data.Tables[0]).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.LimsTestProgress_Mont_chart = Json(Public_Function.DataTableToJSON(LimsTestProgress_Mont_data.Tables[1]).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.LimsTestProgress_Mont_result = Json(Public_Function.DataTableToJSON(LimsTestProgress_Mont_data.Tables[2]).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));


            return View();
        }


        [HttpPost]
        public ActionResult LimsTestProgress_MontSearch()
        {
            DataSet LimsTestProgress_Mont_data = limsTestProgress_MontService.Select();


            string[] jsonArr = new string[3];
            jsonArr[0] = LimsTestProgress_Mont_data.Tables[0].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(LimsTestProgress_Mont_data.Tables[0]);
            jsonArr[1] = LimsTestProgress_Mont_data.Tables[1].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(LimsTestProgress_Mont_data.Tables[1]);
            jsonArr[2] = LimsTestProgress_Mont_data.Tables[2].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(LimsTestProgress_Mont_data.Tables[2]);

            JsonResult jsonResultData = Json(jsonArr, JsonRequestBehavior.AllowGet);
            jsonResultData.MaxJsonLength = int.MaxValue;

            return jsonResultData;
        }


        // 생산현황

        ProductionStatus_MontService productionStatus_MontService = new ProductionStatus_MontService();

        public ActionResult ProductionStatus_Mont()
        {

            DataTable productionStatus_MontData = productionStatus_MontService.Select();

            ViewBag.productionStatus_MontData = Json(Public_Function.DataTableToJSON(productionStatus_MontData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));


            return View();
        }


        [HttpPost]
        public JsonResult ProductionStatus_MontSearch()
        {
            DataTable productionStatus_MontData = productionStatus_MontService.Select();

            return Json(Public_Function.DataTableToJSON(productionStatus_MontData));
        }


        #endregion
    }
}