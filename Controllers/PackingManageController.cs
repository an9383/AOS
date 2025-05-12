
using HACCP.Attribute;
using HACCP.Libs;
using HACCP.Models.PackingManage;
using HACCP.Models.ProductionManage;
using HACCP.Services.Comm;
using HACCP.Services.PackingManage;
using HACCP.Services.ProductionManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class PackingManageController : Controller
    {

        #region PackingOrderLedger 포장지시 대장 작성

        public ActionResult PackingOrderLedger(PackingOrderLedger.SrchParam packingOrder)
        {
            PackingOrderLedgerService packingOrderLedgerService = new PackingOrderLedgerService();
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable PackingOrders = packingOrderLedgerService.SelectPackingOrder(packingOrder);
            DataTable Items = codeHelpService.GetCode(CodeHelpType.item, Public_Function.selectedPLANT, "");

            ViewBag.PackingOrders = Json(Public_Function.DataTableToJSON(PackingOrders)); 
            ViewBag.Items = Json(Public_Function.DataTableToJSON(Items));

            ViewBag.PackingOrderLedgerAuth = Json(HttpContext.Session["PackingOrderLedgerAuth"]);

            return View();
        }

        [HttpPost]
        public JsonResult GetPackingOrder(PackingOrderLedger.SrchParam packingOrder)
        {
            PackingOrderLedgerService packingOrderLedgerService = new PackingOrderLedgerService();

            DataTable PackingOrders = packingOrderLedgerService.SelectPackingOrder(packingOrder);

            return Json(Public_Function.DataTableToJSON(PackingOrders), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SelectPackingOrderLedgerDetail(string order_id, string item_cd)
        {
            PackingOrderLedgerService packingOrderLedgerService = new PackingOrderLedgerService();

            DataTable packingOrderDetail = packingOrderLedgerService.SelectPackingOrderDetail(order_id, item_cd);
            DataTable packingOrderElectricSign = packingOrderLedgerService.SelectPackingOrderElectricSign("P", order_id, "2008");

            string[] jsonArr = new string[2];

            jsonArr[0] = Public_Function.DataTableToJSON(packingOrderDetail);
            jsonArr[1] = Public_Function.DataTableToJSON(packingOrderElectricSign);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PackingOrderLedgerCRUD([JsonBinder]List<PackingOrderLedger> packingOrders, string order_date, string making_dept_cd, string order_id, string gubun)
        {
            PackingOrderLedgerService packingOrderLedgerService = new PackingOrderLedgerService();

            for (int i = 0; i < packingOrders.Count; i++)
            {
                if (gubun.Equals("D"))
                {
                    packingOrders[i].gubun = "D";

                    int cnt = packingOrderLedgerService.GetInputOrderStatus(packingOrders[i].order_no);

                    if (cnt > 0)
                    {
                        var failJson = "{ \"failMessege\": \"이미 불출지시가 된 포장지시는 삭제하실 수 없습니다.\" }";

                        return Json(failJson);
                    }
                }
                else if(packingOrders[i].Equals("I") || packingOrders[i].Equals("U"))
                {
                    packingOrders[i].item_pack_size = packingOrderLedgerService.GetPackSize(packingOrders[i].item_cd);

                    int item_pack_size = int.Parse(packingOrders[i].item_pack_size);
                    int order_qty = int.Parse(packingOrders[i].order_qty);
                    int order_batch_size = int.Parse(packingOrders[i].order_batch_size);

                    if (item_pack_size * order_qty > order_batch_size)
                    {
                        string res = (i + 1).ToString() + "번째줄 pack_size_sum = " + (item_pack_size * order_qty) + " batch_size_sum = " + order_batch_size
                           + "\\n" + "\\n" + "포장지시량이 제조지시량보다 많습니다.";

                        var failJson = "{ \"failMessege\": \"" + res + "\" }";

                        return Json(failJson);
                    }
                }
            }

            order_id = packingOrderLedgerService.GetPackingOrderId(order_date, making_dept_cd, "1", order_id);

            string result = packingOrderLedgerService.PackingOrderLedgerCRUD(packingOrders, order_date, "1", order_id);

            var resJson = "{ \"resultMessege\": \"" + (!String.IsNullOrEmpty(result) ? "성공하였습니다." : "저장중 문제가 발생했습니다.") + "\" }";

            return Json(resJson);
        }

        public ActionResult SelectItemPopup(string item_cd, string dept_cd, string batch_size)
        {
            PackingOrderLedgerService packingOrderLedgerService = new PackingOrderLedgerService();

            DataTable packingOrderItem = packingOrderLedgerService.SelectItemPopup(item_cd, dept_cd, "1", batch_size);

            ViewBag.batch_size = batch_size;

            return View("SelectItemPopup", Json(Public_Function.DataTableToJSON(packingOrderItem), JsonRequestBehavior.AllowGet));
        }

        [HttpGet]
        public JsonResult SelectItemDetail(string item_cd, string revision_no, string batch_size)
        {
            PackingOrderLedgerService packingOrderLedgerService = new PackingOrderLedgerService();

            DataTable packingOrderItemDetail = packingOrderLedgerService.SelectItemDetail(item_cd, revision_no, "1", batch_size);

            return Json(Public_Function.DataTableToJSON(packingOrderItemDetail), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectOrderPopup(string item_cd)
        {
            PackingOrderLedgerService packingOrderLedgerService = new PackingOrderLedgerService();

            DataTable packingOrderItem = packingOrderLedgerService.SelectOrderPopup(item_cd, (DateTime.Now.AddMonths(-3).AddDays(-DateTime.Now.Day + 1)).ToShortDateString(), DateTime.Now.ToShortDateString());

            ViewBag.item_cd = item_cd;
            ViewBag.sdate = (DateTime.Now.AddMonths(-3).AddDays(-DateTime.Now.Day + 1)).ToShortDateString();
            ViewBag.edate = DateTime.Now.ToShortDateString();

            return View("SelectOrderPopup", Json(Public_Function.DataTableToJSON(packingOrderItem), JsonRequestBehavior.AllowGet));
        }

        [HttpGet]
        public JsonResult GetValidDate(string item_cd, string lot_no)
        {
            PackingOrderLedgerService packingOrderLedgerService = new PackingOrderLedgerService();

            string[] dates = packingOrderLedgerService.GetValidDate(item_cd, lot_no);

            return Json(dates, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelectOrderPopupSearch(string item_cd, string sdate, string edate)
        {
            PackingOrderLedgerService packingOrderLedgerService = new PackingOrderLedgerService();

            DataTable packingOrderItem = packingOrderLedgerService.SelectOrderPopup(item_cd, sdate, edate);

            return Json(Public_Function.DataTableToJSON(packingOrderItem), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PackingOrderSignCRUD(string gubun, string c_seq, string emp_cd, string order_id, string validation_type, int sign_status)
        {
            PackingOrderLedgerService packingOrderLedgerService = new PackingOrderLedgerService();

            //string empCd = packingOrderLedgerService.GetSignEmpCd(c_seq, emp_cd, "2008");

            string res = packingOrderLedgerService.SignCRUD(gubun, order_id, emp_cd, validation_type, "emp" + c_seq, sign_status);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        #endregion


        #region Packingorder_order 포장지시

        public ActionResult Packingorder_order(PackingOrder.SrchParam packingOrder)
        {
            CodeHelpService codeHelpService = new CodeHelpService();
            PackingOrderService packingOrderService = new PackingOrderService();

            DataTable Items = codeHelpService.GetCode(CodeHelpType.item, Public_Function.selectedPLANT, "");
            DataTable PackingOrders = packingOrderService.SelectPackingOrder(packingOrder);

            ViewBag.Items = Json(Public_Function.DataTableToJSON(Items));
            ViewBag.PackingOrders = Json(Public_Function.DataTableToJSON(PackingOrders));

            ViewBag.Packingorder_orderAuth = Json(HttpContext.Session["Packingorder_orderAuth"]);

            return View();
        }

        [HttpGet]
        public JsonResult SelectPackingOrderDetail(PackingOrder.SrchParam packingOrder)
        {
            PackingOrderService packingOrderService = new PackingOrderService();

            DataTable PackingOrderDetail = packingOrderService.SelectPackingOrderDetail(packingOrder);

            return Json(Public_Function.DataTableToJSON(PackingOrderDetail), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelectPackingOrder(PackingOrder.SrchParam packingOrder)
        {
            PackingOrderService packingOrderService = new PackingOrderService();

            DataTable PackingOrders = packingOrderService.SelectPackingOrder(packingOrder);

            return Json(Public_Function.DataTableToJSON(PackingOrders), JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region Packingorder_Request 자재불출지시

        public ActionResult Packingorder_Request(PackingOrderRequest.SrchParam packingOrderRequest)
        {
            PackingorderRequestService packingorderRequestService = new PackingorderRequestService();
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable PackingOrderRquest = packingorderRequestService.SelectPackingOrderRquest(packingOrderRequest);
            DataTable Items = codeHelpService.GetCode(CodeHelpType.item, Public_Function.selectedPLANT, "");

            //ViewBag.sdate = packingOrderRequest.sdate;
            //ViewBag.edate = packingOrderRequest.edate;

            //시작일자
            if (packingOrderRequest.sdate == null || packingOrderRequest.sdate.Equals(""))
            {
                ViewBag.sdate = DateTime.Now.AddDays(-(DateTime.Now.Day - 1)).Date.ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.sdate = packingOrderRequest.sdate;
            }
            //종료일자
            if (packingOrderRequest.edate == null || packingOrderRequest.edate.Equals(""))
            {
                ViewBag.edate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.edate = packingOrderRequest.edate;
            }

            ViewBag.packing_order_no = packingOrderRequest.packing_order_no;

            //품목코드
            if (packingOrderRequest.item_cd == null || packingOrderRequest.item_cd.Equals(""))
            {
                ViewBag.item_cd = "";
            }
            else
            {
                ViewBag.item_cd = packingOrderRequest.item_cd;
            }

            ViewBag.PackingOrderRquest = Json(Public_Function.DataTableToJSON(PackingOrderRquest));
            ViewBag.Items = Json(Public_Function.DataTableToJSON(Items));

            return View();
        }

        [HttpGet]
        public JsonResult SelectPackingOrderRequest(PackingOrderRequest.SrchParam packingOrderRequest)
        {
            PackingorderRequestService packingorderRequestService = new PackingorderRequestService();

            DataTable PackingOrderRquest = packingorderRequestService.SelectPackingOrderRquest(packingOrderRequest);

            return Json(Public_Function.DataTableToJSON(PackingOrderRquest), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult PackingOrderRequestSelectReleaseStandard(PackingOrderRequest.SrchParam packingOrderRequest)
        {
            PackingorderRequestService packingorderRequestService = new PackingorderRequestService();

            DataTable releaseStandardData = packingorderRequestService.SelectReleaseStandard(packingOrderRequest);

            return Json(Public_Function.DataTableToJSON(releaseStandardData), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult PackingOrderRequestSelectUsage(PackingOrderRequest.SrchParam packingOrderRequest)
        {
            PackingorderRequestService packingorderRequestService = new PackingorderRequestService();

            DataTable usageData = packingorderRequestService.SelectUsage(packingOrderRequest);

            return Json(Public_Function.DataTableToJSON(usageData), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult PackingOrderRequestSelectCurrentStock(PackingOrderRequest.SrchParam packingOrderRequest)
        {
            PackingorderRequestService packingorderRequestService = new PackingorderRequestService();

            DataTable currentStockData = packingorderRequestService.SelectCurrentStock(packingOrderRequest);

            return Json(Public_Function.DataTableToJSON(currentStockData), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PackingOrderRequestAutoCalcUsage([JsonBinder]List<PackingOrderRequest> packingOrderRequests, string packing_order_no, string sale_item_cd)
        {
            PackingorderRequestService packingorderRequestService = new PackingorderRequestService();

            bool isInserted = packingorderRequestService.InsertUsage(packingOrderRequests, packing_order_no, sale_item_cd);

            return Json(new { result = isInserted });
        }

        [HttpPost]
        public JsonResult PackingOrderRequestDeleteAllUsage(string order_no)
        {
            PackingorderRequestService packingorderRequestService = new PackingorderRequestService();

            string res = packingorderRequestService.PackingOrderRequestDeleteAllUsage(order_no);

            string jsonRes = "{ \"result\": \"삭제되었습니다.\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult PackingOrderRequestDeleteUsage([JsonBinder]PackingOrderRequest packingOrderRequest)
        {
            PackingorderRequestService packingorderRequestService = new PackingorderRequestService();

            string res = packingorderRequestService.PackingOrderRequestDeleteUsage(packingOrderRequest);

            string jsonRes = "{ \"result\": \"삭제되었습니다.\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult PackingOrderRequestInsertUsage([JsonBinder]PackingOrderRequest packingOrderRequest)
        {
            PackingorderRequestService packingorderRequestService = new PackingorderRequestService();

            string res = packingorderRequestService.pakcingOrderRequestInsertUsage(packingOrderRequest);

            string jsonRes = "{ \"result\": \"입력되었습니다.\" }";

            return Json(jsonRes);
        }


        [HttpPost]
        public JsonResult PackingOrderRequestSelectQty([JsonBinder]List<PackingOrderRequest> packingOrderRequests)
        {
            PackingorderRequestService packingorderRequestService = new PackingorderRequestService();

            string res = packingorderRequestService.PackingOrderRequestConfirm(packingOrderRequests);

            string jsonRes = "{ \"result\": \"" + res + "\" }";

            return Json(jsonRes);
        }


        [HttpPost]
        public JsonResult CancelReleaseConfirmation(string packing_order_no, string process_cd)
        {
            PackingorderRequestService packingorderRequestService = new PackingorderRequestService();

            string res = packingorderRequestService.CancelReleaseConfirmation(packing_order_no, process_cd);

            string jsonRes = "{ \"result\": \"" + res + "\" }";

            return Json(jsonRes);
        }

        #endregion


        #region OrderGuide2 포장기록서 승인

        public ActionResult OrderGuide2(string packing_order_no, string order_no)
        {
            OrderGuideService orderGuideService = new OrderGuideService();
            OrderGuide2Service orderGuide2Service = new OrderGuide2Service();

            DataTable OrderGuidePackingOrderData = orderGuide2Service.SelectPackingOrderData(packing_order_no);
            DataTable OrderGuide2SignData = orderGuideService.SelectSignData("P", packing_order_no, "2002");
            DataTable OrderGuide2ItemProcessData = orderGuide2Service.SelectItemProcessData(order_no, packing_order_no);
            DataTable OrderGuide2ReleaseData = orderGuide2Service.SelectReleaseData(packing_order_no);



            ViewBag.OrderGuide2Auth = Json(HttpContext.Session["OrderGuide2Auth"]);

            ViewBag.OrderGuidePackingOrderData = Json(Public_Function.DataTableToJSON(OrderGuidePackingOrderData));
            ViewBag.OrderGuide2SignData = Json(Public_Function.DataTableToJSON(OrderGuide2SignData));
            ViewBag.OrderGuide2ItemProcessData = Json(Public_Function.DataTableToJSON(OrderGuide2ItemProcessData));
            ViewBag.OrderGuide2ReleaseData = Json(Public_Function.DataTableToJSON(OrderGuide2ReleaseData));

            return View();
        }


        [HttpGet]
        public JsonResult SelectOrderGuide2PackingOrder(string sDate, string eDate)
        {
            
            OrderGuide2Service orderGuide2Service = new OrderGuide2Service();

            DataTable orderGuideWorkorderPopup = null;
            if (Public_Function.project.Equals("CHEBIGEN"))
            {
                orderGuideWorkorderPopup = orderGuide2Service.SelectWorkorderPopupData2(sDate, eDate, "");
            }
            else
            {
                orderGuideWorkorderPopup = orderGuide2Service.SelectWorkorderPopupData(sDate, eDate, "");
            }
                

            return Json(Public_Function.DataTableToJSON(orderGuideWorkorderPopup), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelectOrderGuide2(string order_no, string packing_order_no)
        {
            OrderGuideService orderGuideService = new OrderGuideService();
            OrderGuide2Service orderGuide2Service = new OrderGuide2Service();

            DataTable orderGuideWorkorderData = orderGuide2Service.SelectPackingOrderData(packing_order_no);
            DataTable orderGuideSignData = orderGuideService.SelectSignData("P", packing_order_no, "2002");

            if (String.IsNullOrEmpty(order_no)) {
                order_no = orderGuideWorkorderData.Rows[0].ItemArray[0].ToString();
            }

            DataTable orderGuideItemProcessData = orderGuide2Service.SelectItemProcessData(order_no, packing_order_no);
            DataTable OrderGuide2ReleaseData = orderGuide2Service.SelectReleaseData(packing_order_no);

            string[] jsonArr = new string[4];

            jsonArr[0] = Public_Function.DataTableToJSON(orderGuideWorkorderData);
            jsonArr[1] = Public_Function.DataTableToJSON(orderGuideSignData);
            jsonArr[2] = Public_Function.DataTableToJSON(orderGuideItemProcessData);
            jsonArr[3] = Public_Function.DataTableToJSON(OrderGuide2ReleaseData);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }
        

        [HttpPost]
        public JsonResult OrderGuide2AutoRelease(string order_no)
        {
            OrderGuide2Service orderGuide2Service = new OrderGuide2Service();

            string res = orderGuide2Service.OrderGuide2AutoRelease(order_no);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        [HttpPost]
        public JsonResult OrderGuide2UpdatePackingOrder(WorkOrder workOrder)
        {
            OrderGuide2Service orderGuide2Service = new OrderGuide2Service();

            string res = orderGuide2Service.OrderGuideUpdateWorkOrder(workOrder);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        //제조지시 작업중단 
        [HttpPost]
        public JsonResult OrderGuide2Stop(string packing_order_no, string remark)
        {
            OrderGuide2Service orderGuide2Service = new OrderGuide2Service();

            string res = orderGuide2Service.OrderGuide2Stop(packing_order_no, remark);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        #endregion


        #region ItemLabelPrint 팔레트 포장 실적 등록

        public ActionResult ItemLabelPrint(ItemLabelPrint.SrchParam srchParam)
        {
            ItemLabelPrintService itemLabelPrintService = new ItemLabelPrintService();
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable PackingOrderData = itemLabelPrintService.SelectPackingOrderData(srchParam);
            DataTable Items = codeHelpService.GetCode(CodeHelpType.item, Public_Function.selectedPLANT, "");

            ViewBag.ItemLabelPrintAuth = Json(HttpContext.Session["ItemLabelPrintAuth"]);

            ViewBag.PackingOrderData = Json(Public_Function.DataTableToJSON(PackingOrderData));
            ViewBag.Items = Json(Public_Function.DataTableToJSON(Items));

            return View();
        }

        [HttpGet]
        public JsonResult SelectItemLabelPrintPackingOrder(ItemLabelPrint.SrchParam srchParam)
        {
            ItemLabelPrintService itemLabelPrintService = new ItemLabelPrintService();

            DataSet ProductReceivingData = itemLabelPrintService.SelectProductReceivingData(srchParam);

            string[] jsonArr = new string[3];

            jsonArr[0] = Public_Function.DataTableToJSON(ProductReceivingData.Tables[0]);
            jsonArr[1] = Public_Function.DataTableToJSON(ProductReceivingData.Tables[1]);
            jsonArr[2] = "";

            if (ProductReceivingData.Tables[0].Rows.Count > 0)
            {
                string item_stock_id = ProductReceivingData.Tables[0].Rows[0].ItemArray[2].ToString();

                DataTable PalletData = itemLabelPrintService.SelectPalletData(item_stock_id);

                jsonArr[2] = Public_Function.DataTableToJSON(PalletData);
            }

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ItemLabelPrintSelectPackingOrder(ItemLabelPrint.SrchParam srchParam)
        {
            ItemLabelPrintService itemLabelPrintService = new ItemLabelPrintService();

            DataTable PackingOrderData = itemLabelPrintService.SelectPackingOrderData(srchParam);

            return Json(Public_Function.DataTableToJSON(PackingOrderData), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ItemLabelPrintTransferStatus(string packing_order_no, string order_no)
        {
            ItemLabelPrintService itemLabelPrintService = new ItemLabelPrintService();

            DataTable dt = itemLabelPrintService.ItemLabelPrintTransferStatus(packing_order_no, order_no);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ItemLabelPrintPalletCRUD(ItemLabelPrint palletMain)
        {
            ItemLabelPrintService itemLabelPrintService = new ItemLabelPrintService();

            string item_stock_id = itemLabelPrintService.ItemLabelPrintPalletCRUD(palletMain);

            return Json("{ \"result\": \"" + item_stock_id + "\" }");
        }

        [HttpPost]
        public JsonResult ItemLabelPrintPalletDetailCRUD([JsonBinder]List<ItemLabelPrint> palletData, string item_stock_id, string pallet_unit)
        {
            ItemLabelPrintService itemLabelPrintService = new ItemLabelPrintService();

            string res = itemLabelPrintService.ItemLabelPrintPalletDetailCRUD(palletData, item_stock_id, pallet_unit);

            return Json("{ \"result\": \"" + res + "\" }");
        }

        [HttpGet]
        public JsonResult ItemLabelPrintSelectPalletDate(string item_stock_id)
        {
            ItemLabelPrintService itemLabelPrintService = new ItemLabelPrintService();

            DataTable dt = itemLabelPrintService.ItemLabelPrintSelectPalletDate(item_stock_id);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet); ;
        }

        [HttpPost]
        public JsonResult RequestTest([JsonBinder]OrderProcResultForInsert2 param, string gubun)
        {
            OrderProcResultService orderProcResultService = new OrderProcResultService();

            string res = orderProcResultService.OrderProcResultUQ(param, gubun);

            return Json("{ \"result\": \"" + res + "\" }");
        }
        

        [HttpPost]
        public JsonResult CancelRequestTest([JsonBinder]OrderProcResultForInsert2 param)
        {
            ItemLabelPrintService itemLabelPrintService = new ItemLabelPrintService();

            string res = itemLabelPrintService.CancelRequestTest(param);

            return Json("{ \"result\": \"" + res + "\" }");
        }

        [HttpGet]
        public JsonResult SelectPalletCnt(string item_stock_id)
        {
            ItemLabelPrintService itemLabelPrintService = new ItemLabelPrintService();

            string res = itemLabelPrintService.SelectPalletCnt(item_stock_id);

            return Json("{ \"result\": \"" + res + "\" }", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PerformanceComplete(ItemLabelPrint itemLabelPrint)
        {
            ItemLabelPrintService itemLabelPrintService = new ItemLabelPrintService();

            string res = itemLabelPrintService.PerformanceComplete(itemLabelPrint);

            return Json("{ \"result\": \"" + res + "\" }", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ItemLabelPrintPackingStatusChange(string lot_no, string packing_order_no)
        {
            ItemLabelPrintService itemLabelPrintService = new ItemLabelPrintService();

            string res = itemLabelPrintService.ItemLabelPrintPackingStatusChange(lot_no, packing_order_no);

            return null;
        }


        #endregion


        #region PackingResult_Edit2 포장실적등록

        public ActionResult PackingResult_Edit2(PackingResult.SrchParam srchParam)
        {
            PackingResultEdit2Service packingResultEdit2Service = new PackingResultEdit2Service();
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable PackingResultData = packingResultEdit2Service.SelectPackingResultData(srchParam);
            DataTable Items = codeHelpService.GetCode(CodeHelpType.item, Public_Function.selectedPLANT, "");

            ViewBag.PackingResult_Edit2Auth = Json(HttpContext.Session["PackingResult_Edit2Auth"]);

            ViewBag.PackingResultData = Json(Public_Function.DataTableToJSON(PackingResultData));
            ViewBag.Items = Json(Public_Function.DataTableToJSON(Items));

            return View();
        }

        [HttpGet]
        public JsonResult PackingResultEdit2SelectPackingResult(string packing_order_no)
        {
            PackingResultEdit2Service packingResultEdit2Service = new PackingResultEdit2Service();

            DataTable PackingResultDetail = packingResultEdit2Service.SelectPackingResultDetail(packing_order_no);

            return Json(Public_Function.DataTableToJSON(PackingResultDetail), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult PackingResultEdit2SelectPackingOrder(PackingResult.SrchParam srchParam)
        {
            PackingResultEdit2Service packingResultEdit2Service = new PackingResultEdit2Service();

            DataTable PackingResultData = packingResultEdit2Service.SelectPackingResultData(srchParam);

            return Json(Public_Function.DataTableToJSON(PackingResultData), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string PackingResult_Edit2TRX(PackingResult packingResult, string gubun)
        {
            PackingResultEdit2Service packingResultEdit2Service = new PackingResultEdit2Service();

            string res = packingResultEdit2Service.PackingResult_Edit2TRX(packingResult, gubun);

            return res;
        }

        [HttpPost]
        public string PackingResultEditRequestTest([JsonBinder]PackingResult param, string gubun)
        {
            PackingResultEdit2Service packingResultEdit2Service = new PackingResultEdit2Service();

            string res = packingResultEdit2Service.PackingResultEditRequestTest(param, gubun);

            return res;
        }

        [HttpPost]
        public string PackingResultEdit2LotEnd(string packing_order_no)
        {
            PackingResultEdit2Service packingResultEdit2Service = new PackingResultEdit2Service();

            string res = packingResultEdit2Service.PackingResultEdit2LotEnd(packing_order_no);

            return res;
        }

        #endregion



        #region 포장지시 대장 작성2

        PackingOrderLedgerService PackingOrderLedger2Service = new PackingOrderLedgerService();

        public ActionResult PackingOrderLedger2(PackingOrderLedger.SrchParam packingOrder)
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable PackingOrders = PackingOrderLedger2Service.SelectPackingOrder(packingOrder);
            DataTable Items = codeHelpService.GetCode(CodeHelpType.item, Public_Function.selectedPLANT, "");
            DataTable employeePopupData = PackingOrderLedger2Service.GetEmployeePopupData();

            ViewBag.PackingOrders = Json(Public_Function.DataTableToJSON(PackingOrders));
            ViewBag.Items = Json(Public_Function.DataTableToJSON(Items));
            ViewBag.employeePopupData = Json(Public_Function.DataTableToJSON(employeePopupData));

            ViewBag.PackingOrderLedger2Auth = Json(HttpContext.Session["PackingOrderLedger2Auth"]);

            return View();
        }



        [HttpPost]
        public JsonResult GetItemInfo(string item_cd, string packing_order_no)
        {
            DataTable itemData = PackingOrderLedger2Service.GetItemInfo(item_cd);
            DataSet planData = PackingOrderLedger2Service.GetPlanInfo(packing_order_no);

            string[] jsonArr = new string[4];

            jsonArr[0] = Public_Function.DataTableToJSON(itemData);
            jsonArr[1] = planData.Tables[0].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(planData.Tables[0]);
            jsonArr[2] = planData.Tables[1].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(planData.Tables[1]);
            jsonArr[3] = planData.Tables[2].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(planData.Tables[2]);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetPackingOrderSignData(string packing_order_no)
        {
            OrderGuideService orderGuideService = new OrderGuideService();
            DataTable PackingOrderSignData = orderGuideService.SelectSignData("P", packing_order_no, "2002");

            return ViewBag.PackingOrderSignData = Json(Public_Function.DataTableToJSON(PackingOrderSignData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }



        [HttpPost]
        public JsonResult PackingOrderLedger_Plan_Add(string packing_order_no, string start_hour, string stop_hour, string worker_qty, string UPH_qty, string UPS_qty, string MAN_qty, string plan_emp_cd)
        {
            PackingOrderLedgerService packingOrderLedgerService = new PackingOrderLedgerService();

            string res = PackingOrderLedger2Service.plan_add(packing_order_no, start_hour, stop_hour, worker_qty, UPH_qty, UPS_qty, MAN_qty, plan_emp_cd);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }



        [HttpPost]
        public JsonResult PackingOrderLedger_Result_Add(string packing_order_no, string start_time, string stop_time, string worker_qty)
        {
            PackingOrderLedgerService packingOrderLedgerService = new PackingOrderLedgerService();

            string res = PackingOrderLedger2Service.result_add(packing_order_no, start_time, stop_time, worker_qty);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }


        [HttpPost]
        public JsonResult PackingOrderLedger_Check_Add(string packing_order_no, string goal_qty, string progress_rate, string UPH_qty, string UPS_qty, string start_time, string stop_time,
                                                        string gd_rate, string defect_qty, string defect_rate, string item_content_avg, string item_error_range, string worker_qty, string MAN_qty, string check_remark)
        {
            PackingOrderLedgerService packingOrderLedgerService = new PackingOrderLedgerService();

            string res = PackingOrderLedger2Service.check_add(packing_order_no, goal_qty, progress_rate, UPH_qty, UPS_qty, gd_rate, defect_qty, defect_rate, item_content_avg, item_error_range, start_time, stop_time, worker_qty, MAN_qty, check_remark);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }


        [HttpPost]
        public JsonResult PackingOrderLedger_Check_Total(string start_time, string stop_time, string packing_order_no)
        {
            PackingOrderLedgerService packingOrderLedgerService = new PackingOrderLedgerService();

            string res = PackingOrderLedger2Service.check_total(packing_order_no, start_time, stop_time);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        # endregion
    }

}