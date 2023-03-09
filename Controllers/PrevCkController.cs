using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using HACCP.Attribute;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models.Common;
using HACCP.Models.PrevCk;
using HACCP.Services.Comm;
using HACCP.Services.PrevCk;
using HACCP.Services.SysCom;
using log4net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace HACCP.Controllers
{

    public class PrevCkController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PrevCkController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        private CodeHelpService codeHelpService = new CodeHelpService();
        private EquipmentService equipmentService = new EquipmentService();

        #region ScheduleMaster 예방점검 대상 등록

        private ScheduleMasterService scheduleMasterService = new ScheduleMasterService();

        [HttpGet]
        public ActionResult ScheduleMaster()
        {
            DataTable programSet = scheduleMasterService.GetProgramSet();

            string workType = programSet.DataSet.Tables[1].Rows[1].ItemArray[1].ToString();//Option2
            string commonCd = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString();//Option1

            DataTable obj_type = selectBoxService.GetSelectBox("공통코드", "S", commonCd, "obj_type");
            DataTable work_type = selectBoxService.GetSelectBox("공통코드", "S", "SC003", "work_type");
            DataTable schedule_type = selectBoxService.GetSelectBox("공통코드", "S", "SC004", "schedule_type");

            DataTable obj_type_1 = selectBoxService.GetSelectBox("공통코드", "D", commonCd, "obj_type_1");
            DataTable work_type_1 = selectBoxService.GetSelectBox("공통코드", "D", "SC003", "work_type_1");
            DataTable schedule_type_1 = selectBoxService.GetSelectBox("공통코드", "D", "SC004", "schedule_type_1");
            DataTable period_type = selectBoxService.GetSelectBox("공통코드", "N", "SC038", "period_type");

            DataTable scheduleMasterObjlist = scheduleMasterService.GetObjCd();
            DataTable scheduleMasterDept = scheduleMasterService.GetDept();
            DataTable scheduleMasterEmp = scheduleMasterService.GetEmp();

            //상단 검색조건 데이터
            ViewBag.obj_type = obj_type;
            ViewBag.work_type = work_type;
            ViewBag.schedule_type = schedule_type;

            //우측 폼 데이터 
            ViewBag.obj_type_1 = obj_type_1;//대상구분
            ViewBag.work_type_1 = work_type_1;//대상코드
            ViewBag.schedule_type_1 = schedule_type_1;//작업구분
            ViewBag.period_type = period_type;//주기구분

            //우측 폼 팝업 데이터
            ViewBag.scheduleMasterObjlist = Json(Public_Function.DataTableToJSON(scheduleMasterObjlist));
            ViewBag.scheduleMasterDept = Json(Public_Function.DataTableToJSON(scheduleMasterDept));
            ViewBag.scheduleMasterEmp = Json(Public_Function.DataTableToJSON(scheduleMasterEmp));

            ViewBag.workType = workType;//상단 검색영역 작업데이터 초기 설정 값
            ViewBag.scheduleMasterAuth = Json(HttpContext.Session["ScheduleMasterAuth"]);

            return View();
        }

        /// <summary>
        /// 예방점검 대상 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ScheduleMaster_GridSelect(string obj_type, string work_type, string schedule_type, string obj_cd)
        {
            DataTable scheduleMasterData = scheduleMasterService.GridSelect(obj_type, work_type, schedule_type, obj_cd);
            string jsonData = Public_Function.DataTableToJSON(scheduleMasterData);

            return Json(jsonData);
        }

        /// <summary>
        /// 예방점검 대상 삭제
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ScheduleMaster_GridDelete(string obj_type, string work_type, string schedule_type, string obj_cd, string schedule_master_id)
        {
            string result = scheduleMasterService.GridDelete(obj_type, work_type, schedule_type, obj_cd, schedule_master_id);
            
            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 예방점검 대상 입력
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ScheduleMaster_GridInsert(ScheduleMaster schedule)
        {
            string result = scheduleMasterService.GridInsert(schedule);

            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 예방점검 대상 수정
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ScheduleMaster_GridUpdate(ScheduleMaster schedule)
        {
            string result = scheduleMasterService.GridUpdate(schedule);

            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 금일 스케줄 생성
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ScheduleMaster_CreateSchedule(string schedule_master_id)
        {
            string result = scheduleMasterService.ScheduleCreate(schedule_master_id);

            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);
        }

        #endregion

        #region ScheduleGuide 예방점검 체크사항 작성

        private ScheduleGuideService scheduleGuideService = new ScheduleGuideService();

        [HttpGet]
        public ActionResult ScheduleGuide()
        {
            //공통 사용 파라미터
            DataTable programSet = scheduleGuideService.GetProgramSet();

            string workType = programSet.DataSet.Tables[1].Rows[1].ItemArray[1].ToString();//Option2
            string commonCd = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString();//Option1

            //콤보박스
            //대상 (상단 검색조건)
            DataTable obj_type = selectBoxService.GetSelectBox("공통코드", "S", commonCd, "obj_type");
            //작업 (상단 검색조건)
            DataTable work_type = selectBoxService.GetSelectBox("공통코드", "S", "SC003", "work_type");
            //주기 (상단 검색조건)
            DataTable schedule_type = selectBoxService.GetSelectBox("공통코드", "S", "SC004", "schedule_type");      

            //팝업
            //담당자 (상단 검색조건)
            DataTable scheduleGuideEmp = codeHelpService.GetCode(CodeHelpType.employee, "", "");
            //원료코드 (그리드)
            DataTable scheduleGuideMaterial = codeHelpService.GetCode(CodeHelpType.item, "", "");
            //기계코드 (그리드)
            DataTable scheduleGuideMachine = codeHelpService.GetCode(CodeHelpType.equipment, "", "");


            ViewBag.obj_type = obj_type;
            ViewBag.work_type = work_type;
            ViewBag.schedule_type = schedule_type;

            ViewBag.scheduleGuideEmp = Json(Public_Function.DataTableToJSON(scheduleGuideEmp));
            ViewBag.scheduleGuideMaterial = Json(Public_Function.DataTableToJSON(scheduleGuideMaterial));
            ViewBag.scheduleGuideMachine = Json(Public_Function.DataTableToJSON(scheduleGuideMachine));
            ViewBag.ScheduleGuideAuth = Json(HttpContext.Session["ScheduleGuideAuth"]);

            return View();
        }

        /// <summary>
        /// 예방점검 체크대상 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ScheduleGuide_GridSelect(string obj_type, string work_type, string schedule_type, string responsibility_worker, string obj_cd)
        {

            DataTable item = scheduleGuideService.GridSelect(obj_type, work_type, schedule_type, responsibility_worker, obj_cd);
            string result = Public_Function.DataTableToJSON(item);

            return Json(result);
        }

        /// <summary>
        /// 예방점검 체크사항 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ScheduleGuide_GridSelect2(string schedule_master_id)
        {

            DataTable item = scheduleGuideService.GridSelect2(schedule_master_id);
            string result = Public_Function.DataTableToJSON(item);

            return Json(result);
        }

        /// <summary>
        /// 예방점검 체크사항 입력, 수정, 삭제
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public string ScheduleGuide_TRX([JsonBinder] List<ScheduleGuide> paramData)
        {
            string res = scheduleGuideService.ScheduleGuide_TRX(paramData);

            return res;
        }


        /// <summary>
        /// 예방점검 복사
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ScheduleGuide_Copy(string schedule_copy_id, string schedule_master_id)
        {

            string message = scheduleGuideService.Copy(schedule_copy_id, schedule_master_id);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }


        #endregion

        #region Schedule 예방점검 스케줄 생성

        private ScheduleService scheduleService = new ScheduleService();

        [HttpGet]
        public ActionResult Schedule()
        {
            //공통 사용 파라미터
            DataTable programSet = scheduleService.GetProgramSet();

            string commonCd = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString();//Option1

            //콤보박스
            //대상 (상단 검색조건)
            DataTable obj_type = selectBoxService.GetSelectBox("공통코드", "S", commonCd, "obj_type");
            //작업 (상단 검색조건)
            DataTable work_type = selectBoxService.GetSelectBox("공통코드", "S", "SC003", "work_type");
            //주기 (상단 검색조건)
            DataTable schedule_type = selectBoxService.GetSelectBox("공통코드", "S", "SC004", "schedule_type");

            //팝업
            //담당자 (상단 검색조건, 그리드)
            DataTable scheduleEmp = codeHelpService.GetCode(CodeHelpType.employee, "", "");
            //스케줄코드 (그리드)
            DataTable schedule = codeHelpService.GetCode(CodeHelpType.schedulecode, "", "");
            //부서 (상단)
            DataTable scheduleDept = codeHelpService.GetCode(CodeHelpType.alldepartment, "", "");
            
            ViewBag.commonCd = commonCd;
            ViewBag.obj_type = obj_type;
            ViewBag.work_type = work_type;
            ViewBag.schedule_type = schedule_type;

            ViewBag.scheduleEmp = Json(Public_Function.DataTableToJSON(scheduleEmp));
            ViewBag.schedule = Json(Public_Function.DataTableToJSON(schedule)); ;
            ViewBag.scheduleDept = Json(Public_Function.DataTableToJSON(scheduleDept)); ;
            ViewBag.ScheduleAuth = Json(HttpContext.Session["ScheduleAuth"]);

            return View();
        }


        /// <summary>
        /// 예방점검 스케줄 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult Schedule_GridSelect(Schedule.SrchParam srch)
        {

            List<Schedule> items = scheduleService.GridSelect(srch);
            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }

        /// <summary>
        /// 예방점검 스케줄 삭제
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult Schedule_GridDelete(Schedule schedule)
        {

            string message = scheduleService.GridDelete(schedule);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 예방점검 스케줄 전체삭제
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult Schedule_GridDeleteAll(Schedule.SrchParam srch)
        {
            string message = scheduleService.GridDeleteAll(srch);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 예방점검 스케줄 입력 및 수정
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public string Schedule_TRX([JsonBinder] List<Schedule> paramData)
        {
            string res = scheduleService.Schedule_TRX(paramData);

            return res;
        }

        /// <summary>
        /// 예방점검 스케줄 입력 및 수정
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult Schedule_SaveNodes(Schedule schedule)
        {

            string message = scheduleService.SaveNodes(schedule);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 예방점검 스케줄 계획생성
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult Schedule_CreateSchedule(Schedule.SrchParam srch)
        {
            string message = scheduleService.CreateSchedule(srch);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 예방점검 스케줄 확인처리
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult Schedule_CheckSchedule(Schedule schedule)
        {
            string message = scheduleService.CheckSchedule(schedule);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }

        #endregion

        #region DayScheduleList 점검 기록서

        private DayScheduleListService dayScheduleListService = new DayScheduleListService();

        [HttpGet]
        public ActionResult DayScheduleList()
        {
            //공통 사용 파라미터
            DataTable programSet = dayScheduleListService.GetProgramSet();

            string commonCd = programSet.DataSet.Tables[1].Rows[1].ItemArray[1].ToString();//Option1
            string workType = programSet.DataSet.Tables[1].Rows[2].ItemArray[1].ToString();//Option2

            //콤보박스
            //대상 (상단 검색조건)
            DataTable obj_type = selectBoxService.GetSelectBox("공통코드", "S", commonCd, "obj_type");
            //작업 (상단 검색조건)
            DataTable work_type = selectBoxService.GetSelectBox("공통코드", "S", "SC003", "work_type");
            //주기 (상단 검색조건)
            DataTable schedule_type = selectBoxService.GetSelectBox("공통코드", "S", "SC004", "schedule_type");
            
            ViewBag.commonCd = commonCd;
            ViewBag.workType = workType;

            ViewBag.obj_type = obj_type;
            ViewBag.work_type = work_type;
            ViewBag.schedule_type = schedule_type;

            ViewBag.DayScheduleListAuth = Json(HttpContext.Session["DayScheduleListAuth"]);

            return View();
        }

        /// <summary>
        /// 점검기록서 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult DayScheduleList_GridSelect(DayScheduleList.SrchParam srch)
        {
            List<DayScheduleList> items = dayScheduleListService.GridSelect(srch);
            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }


        /// <summary>
        /// 점검기록서 체크사항 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult DayScheduleList_GridSelect2(DayScheduleList.SrchParam srch)
        {
            DataTable items = dayScheduleListService.GridSelect2(srch);
            string result = Public_Function.DataTableToJSON(items);

            return Json(result);
        }

        /// <summary>
        /// 점검기록서 삭제
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult DayScheduleList_GridDelete(string schedule_id)
        {
            string message = dayScheduleListService.GridDelete(schedule_id);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 점검기록서 비고 수정
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult DayScheduleList_SaveMaster(DayScheduleList dayScheduleList)
        {
            string message = dayScheduleListService.SaveMaster(dayScheduleList);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 체크사항 수정
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        //public JsonResult DayScheduleList_SaveChecklist(string schedule_id, string work_seq, string schedule_result_manual_data)
        //{
        //    string message = dayScheduleListService.SaveChecklist(schedule_id, work_seq, schedule_result_manual_data);
        //    var resJson = "{ \"messege\": \"" + message + "\" }";

        //    return Json(resJson);
        //}

        public JsonResult DayScheduleList_TRX([JsonBinder]List<DayScheduleList> paramData)
        {
            string message = dayScheduleListService.DayScheduleList_TRX(paramData);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 점검기록서 파일리스트 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult DayScheduleList_GetFileList(string schedule_id)
        {
            DataTable items = dayScheduleListService.GetFileList(schedule_id);
            string result = Public_Function.DataTableToJSON(items);

            return Json(result);
        }

        /// <summary>
        /// 점검기록서 전자서명 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult DayScheduleList_SignerSearch(string schedule_id, string sign_set_cd, string sign_seq)
        {
            DataTable items = dayScheduleListService.SignerSearch(schedule_id, sign_set_cd, sign_seq);
            string result = Public_Function.DataTableToJSON(items);

            return Json(result);
        }

        /// <summary>
        /// 점검기록서 대리자 권한 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult DayScheduleList_GetRepresentativeAuthority(string emp_cd, string signSeq, string signCode)
        {
            string result = "";

            bool check = dayScheduleListService.GetRepresentativeAuthority(emp_cd, signSeq, signCode);

            if (check == true) result = "Y";
            else result = "N";

            var resJson = "{ \"result\": \"" + result + "\" }";
            return Json(resJson);
        }


        /// <summary>
        /// 점검기록서 전자서명 저장
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult DayScheduleList_SettingSign(string schedule_id, string emp_cd, string gubun)
        {
            string message = dayScheduleListService.SettingSign(schedule_id, emp_cd, gubun);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }


        /// <summary>
        /// 점검기록서 전자서명 취소
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult DayScheduleList_CancelSign(string schedule_id, string gubun)
        {
            string message = dayScheduleListService.CancelSign(schedule_id, gubun);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }


        /// <summary>
        /// 점검기록서 첨부파일 등록
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public ActionResult DayScheduleList_UploadFile(string name, string schedule_id)
        {
            string result = "";
            try
            {
                var uploadFile = Request.Files[name];

                var fileName = uploadFile.FileName;
                //var fileName_without_extension = Path.GetFileNameWithoutExtension(fileName);
                var contentType = equipmentService.GetMimeType(fileName);

                using (var binaryReader = new BinaryReader(Request.Files[name].InputStream))
                {
                    //result = dayScheduleListService.InsertFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), schedule_id, fileName_without_extension, contentType);
                    result = dayScheduleListService.InsertFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), schedule_id, fileName, contentType);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }
            var resJson = "{ \"messege\": \"" + result + "\" }";
            return Json(resJson);
        }

        [HttpGet]
        public ActionResult DayScheduleList_OpenFile(string schedule_id, string file_id)
        {

            DataTable attachmentFileData = dayScheduleListService.OpenFile(schedule_id, file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[0].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0].ItemArray[2] //byte
                , mimeType
                , attachmentFileData.Rows[0].ItemArray[0].ToString() //파일 full name
                );

            //string message = "";
            //message = dayScheduleListService.OpenFile(schedule_id, file_id);

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            //return Json(resJson);
        }


        [HttpPost]
        public ActionResult DayScheduleList_DeleteFile(string schedule_id, string file_id)
        {
            string message = "";
            message = dayScheduleListService.DeleteFile(schedule_id, file_id);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }
        #endregion

        #region MonthScheduleList 월간 점검표

        MonthScheduleListService monthScheduleListService = new MonthScheduleListService();

        public ActionResult MonthScheduleList()
        {

            //공통 사용 파라미터
            DataTable programSet = monthScheduleListService.GetProgramSet();

            string commonCd = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString();//Option1

            //콤보박스
            //대상 (상단 검색조건)
            DataTable obj_type = selectBoxService.GetSelectBox("공통코드", "S", commonCd, "obj_type");
            //작업 (상단 검색조건)
            DataTable work_type = selectBoxService.GetSelectBox("공통코드", "S", "SC003", "work_type");
            //주기 (상단 검색조건)
            DataTable schedule_type = selectBoxService.GetSelectBox("공통코드", "S", "SC004", "schedule_type");

            ViewBag.obj_type = obj_type;
            ViewBag.work_type = work_type;
            ViewBag.schedule_type = schedule_type;
            ViewBag.commonCd = monthScheduleListService.common_cd;
            ViewBag.MonthScheduleListAuth = Json(HttpContext.Session["MonthScheduleListAuth"]);

            return View();
        }

        /// <summary>
        /// 점검기록서 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult MonthScheduleList_GridSelect(MonthScheduleList.SrchParam srch)
        {
            List<MonthScheduleList> items = monthScheduleListService.GridSelect(srch);
            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }

        /// <summary>
        /// 점검기록서 월간점검표 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult MonthScheduleList_GridSelect2(MonthScheduleList.SrchParam srch)
        {
            DataTable items = monthScheduleListService.GridSelect2(srch);
            string result = Public_Function.DataTableToJSON(items);

            return Json(result);
        }

        /// <summary>
        /// 점검기록서 리포트 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public void MonthScheduleList_ReportPreview(MonthScheduleList.SrchParam srch)
        {
            monthScheduleListService.ReportPreview(srch);
        }

        /// <summary>
        /// 점검기록서 주별 리포트 데이터
        /// </summary>        
        /// <returns></returns>
        public JsonResult MonthSchedule_ReportByWeekSP(string s_year, string s_month, string s_week, string schedule_master_id) {
            DataTable items = monthScheduleListService.MonthSchedule_ReportByWeekSP(s_year, s_month, s_week, schedule_master_id);
            string result = Public_Function.DataTableToJSON(items);

            return Json(result);
        }
        #endregion



        #region YearScheduleList 연간 일정표

        YearScheduleListService yearScheduleListService = new YearScheduleListService();

        [HttpGet]
        public ActionResult YearScheduleList()
        {
            //공통 사용 파라미터
            DataTable programSet = yearScheduleListService.GetProgramSet();

            string commonCd = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString();//Option1

            //콤보박스
            //대상 (상단 검색조건)
            DataTable obj_type = selectBoxService.GetSelectBox("공통코드", "S", commonCd, "obj_type");
            //작업 (상단 검색조건)
            DataTable work_type = selectBoxService.GetSelectBox("공통코드", "S", "SC003", "work_type");
            //주기 (상단 검색조건)
            DataTable schedule_type = selectBoxService.GetSelectBox("공통코드", "S", "SC004", "schedule_type");

            //팝업
            //스케줄코드 (그리드)
            DataTable schedule = codeHelpService.GetCode(CodeHelpType.schedulecode, "", "");

            ViewBag.commonCd = commonCd;
            ViewBag.obj_type = obj_type;
            ViewBag.work_type = work_type;
            ViewBag.schedule_type = schedule_type;

            ViewBag.schedule = Json(Public_Function.DataTableToJSON(schedule));
            ViewBag.YearScheduleListAuth = Json(HttpContext.Session["YearScheduleListAuth"]);

            return View();
        }

        /// <summary>
        /// 연간 일정표 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult YearScheduleList_GridSelect(YearScheduleList.SrchParam srch)
        {
            List<YearScheduleList> items = yearScheduleListService.GridSelect(srch);
            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }
        #endregion

        #region WorkScheduleList 점검 대장 

        WorkScheduleListService workScheduleListService = new WorkScheduleListService();
        public ActionResult WorkScheduleList()
        {
            //콤보박스
            //대상 (상단 검색조건)
            DataTable obj_type = selectBoxService.GetSelectBox("공통코드", "S", "SC001", "obj_type");
            //작업 (상단 검색조건)
            DataTable work_type = selectBoxService.GetSelectBox("공통코드", "S", "SC003", "work_type");
            //주기 (상단 검색조건)
            DataTable schedule_type = selectBoxService.GetSelectBox("공통코드", "S", "SC004", "schedule_type");

            ViewBag.obj_type = obj_type;
            ViewBag.work_type = work_type;
            ViewBag.schedule_type = schedule_type;

            ViewBag.WorkScheduleListAuth = Json(HttpContext.Session["WorkScheduleListAuth"]);
            return View();
        }

        /// <summary>
        /// 점검대장 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult WorkScheduleList_GridSelect(WorkScheduleList.SrchParam srch)
        {
            List<WorkScheduleList> items = workScheduleListService.GridSelect(srch);
            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }
        #endregion

    }

    #region 예방점검 Lookup용 컨트롤러

    [System.Web.Http.Route("api/PrevCkData/{action}", Name = "PrevCk")]
    public class PrevCkDataController : ApiController
    {
        [System.Web.Http.HttpGet]
        public HttpResponseMessage ScheduleGuide_GetRepository(DataSourceLoadOptions loadOptions)
        {
            ScheduleGuideService scheduleGuideService = new ScheduleGuideService();

            DataTable dt;
            dt = scheduleGuideService.GetRepositoryItem("L");

            List<SelectBoxGubun> list = new List<SelectBoxGubun>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[i];

                SelectBoxGubun temp;

                temp = new SelectBoxGubun(row, false);

                list.Add(temp);
            }

            return Request.CreateResponse(DataSourceLoader.Load(list, loadOptions));

        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage Schedule_GetRepository(DataSourceLoadOptions loadOptions, string gubun)
        {
            ScheduleService scheduleService = new ScheduleService();

            DataTable dt;
            dt = scheduleService.GetRepositoryItem(gubun);

            List<SelectBoxGubun> list = new List<SelectBoxGubun>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[i];

                SelectBoxGubun temp;

                temp = new SelectBoxGubun(row, false);

                list.Add(temp);
            }

            return Request.CreateResponse(DataSourceLoader.Load(list, loadOptions));

        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage DayScheduleList_GetRepository(DataSourceLoadOptions loadOptions, string gubun)
        {
            DayScheduleListService dayScheduleListService = new DayScheduleListService();

            DataTable dt;
            dt = dayScheduleListService.GetRepositoryItem(gubun);

            List<SelectBoxGubun> list = new List<SelectBoxGubun>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[i];

                SelectBoxGubun temp;

                temp = new SelectBoxGubun(row, false);

                list.Add(temp);
            }

            return Request.CreateResponse(DataSourceLoader.Load(list, loadOptions));

        }
    }
    #endregion



    }

