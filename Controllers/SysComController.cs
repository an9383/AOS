using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DevExpress.Web.Internal;
using DevExpress.XtraRichEdit.Model;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models.SysCom;
using HACCP.Models.SysOp;
using HACCP.Services.Comm;
using HACCP.Services.SysCom;

namespace HACCP.Controllers
{
    public class SysComController : Controller
    {

        private SelectBoxService selectBoxService = new SelectBoxService();


        #region Plant 사업장 관리

        private PlantService plantService = new PlantService();

        [CheckSession]
        public ActionResult Plant()
        {
            DataTable plantData = plantService.selectPlantData("%");
            DataTable empData = plantService.getEmpData();
            DataTable taxOffice = selectBoxService.GetSelectBox("공통코드", "N", "CM004", "taxOffice");

            ViewBag.plantData = Json(Public_Function.DataTableToJSON(plantData));
            ViewBag.empData = Json(Public_Function.DataTableToJSON(empData));
            ViewBag.taxOffice = taxOffice;

            return View();
        }

        [HttpPost]
        public string plantCRUD(Plant plant, string gubun, HttpPostedFileBase company_full_image, HttpPostedFileBase company_small_image)
        {
            string res = plantService.CRUD(plant, gubun);

            if (company_full_image != null)
            {
                saveCompanyImage(company_full_image, plant.plant_cd, "company_full_image");
            }
            if (company_small_image != null)
            {
                saveCompanyImage(company_small_image, plant.plant_cd, "company_small_image");
            }

            return res;
        }

        private void saveCompanyImage(HttpPostedFileBase imageFile, string plant_cd, string imageName)
        {
            try
            {
                var myBytes = GetByteArrayFromImage(imageFile);

                byte[] GetByteArrayFromImage(HttpPostedFileBase file)
                {
                    var length = file.InputStream.Length;
                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        fileData = binaryReader.ReadBytes(file.ContentLength);
                    }

                    return fileData;
                }

                plantService.updateComapnyImage(myBytes, plant_cd, imageName);
            }

            catch
            {
                Response.StatusCode = 400;
            }
        }

        [CheckSession]
        [HttpPost]
        public JsonResult displayCompanyIcon(string plant_cd)
        {
            if (plant_cd == null)
            {
                return null;
            }

            DataTable imageSrc = plantService.selectCompanyIcon(plant_cd);

            return Json(Public_Function.DataTableToJSON(imageSrc));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult selectPlant(string use_yn)
        {
            DataTable plantData = plantService.selectPlantData(use_yn);

            return Json(Public_Function.DataTableToJSON(plantData));
        }

        [HttpGet]
        public JsonResult MainPlantCountCheck()
        {
            DataTable mainPlantCnt = plantService.MainPlantCountCheck();

            return Json(Public_Function.DataTableToJSON(mainPlantCnt), JsonRequestBehavior.AllowGet);
        }


        #endregion



        #region Department 부서 관리

        private DepartmentService departmentService = new DepartmentService();

        [CheckSession]
        public ActionResult Department()
        {
            DataTable departmentData = departmentService.selectDepartmentData("%");
            DataTable departmentPopupData = departmentService.getDepartmentPopupData();
            DataTable departmentGubun = selectBoxService.GetSelectBox("공통코드", "D", "DS009", "departmentGubun");
            DataTable plantGubun = selectBoxService.GetSelectBox("사업장", "D", "", "plantGubun");

            ViewBag.departmentData = Json(Public_Function.DataTableToJSON(departmentData));
            ViewBag.departmentPopupData = Json(Public_Function.DataTableToJSON(departmentPopupData));
            ViewBag.departmentGubun = departmentGubun;
            ViewBag.plantGubun = plantGubun;

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult selectDepartment(string dept_use_gb)
        {
            DataTable departmentData = departmentService.selectDepartmentData(dept_use_gb);

            return Json(Public_Function.DataTableToJSON(departmentData));
        }

        [CheckSession]
        [HttpPost]
        public string departmentCRUD(Department department, string gubun)
        {
            string res = departmentService.CRUD(department, gubun);

            return res;
        }

        #endregion



        #region Employee 사원 관리

        private EmployeeService employeeService = new EmployeeService();

        [CheckSession]
        public ActionResult Employee()
        {
            DataTable employeeData = employeeService.SelectEmployee("","","");
            DataTable departmentData = employeeService.getDepartmentData();
            DataTable empPopupData = employeeService.getEmpData();
            DataTable empPostGubun = selectBoxService.GetSelectBox("공통코드", "D", "CM005", "empPostGubun");
            DataTable empRairGubun = selectBoxService.GetSelectBox("공통코드", "D", "CM006", "empRairGubun");
            DataTable empTypeGubun = selectBoxService.GetSelectBox("공통코드", "D", "CM014", "empTypeGubun");

            ViewBag.employeeData = Json(Public_Function.DataTableToJSON(employeeData));
            ViewBag.departmentData = Json(Public_Function.DataTableToJSON(departmentData));
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));
            ViewBag.empPostGubun = empPostGubun;
            ViewBag.empRairGubun = empRairGubun;
            ViewBag.empTypeGubun = empTypeGubun;

            return View();
        }

        [CheckSession]
        [HttpPost]
        public string employeeCRUD(Employee employee, string gubun)
        {
            string res = employeeService.CRUD(employee, gubun);

            return res;
        }

        [CheckSession]
        [HttpPost]
        public JsonResult selectEmployee(string search_dept_cd, string search_emp_cd, string employee_use_yn_radio)
        {
            DataTable employeeData = employeeService.SelectEmployee(search_dept_cd, search_emp_cd, employee_use_yn_radio);

            return Json(Public_Function.DataTableToJSON(employeeData).Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        #endregion



        #region EmpBarcode 사원 바코드

        [CheckSession]
        public ActionResult EmpBarcode()
        {


            return View();
        }


        #endregion



        #region WorkRoom 작업실 관리

        private WorkRoomService workRoomService = new WorkRoomService();

        [CheckSession]
        public ActionResult WorkRoom()
        {
            DataTable plantGubun = selectBoxService.GetSelectBox("사업장", "D", "", "plantGubun");
            DataTable workroom_area = selectBoxService.GetSelectBox("공통코드", "D", "workroom_area", "workroom_area");
            DataTable cleanliness = selectBoxService.GetSelectBox("청정도", "D", "", "cleanliness");
            DataTable workRoomType = selectBoxService.GetSelectBox("공통코드", "D", "SC012", "workRoomType");
            DataTable warehouseType = selectBoxService.GetSelectBox("공통코드", "D", "WR013", "warehouseType");
            DataTable ProductionBuilding = selectBoxService.GetSelectBox("공통코드", "D", "CM027", "ProductionBuilding");
            DataTable team = selectBoxService.GetSelectBox("공통코드", "D", "CM059", "team");

            DataTable workRoomData = workRoomService.selectWorkRoom("", "%");
            DataTable workRoomPopupData = workRoomService.selectWorkRoomPopupData();
            DataTable workRoomProductData = workRoomService.selectWorkRoomProductData();

            ViewBag.plantGubun = plantGubun;
            ViewBag.workroom_area = workroom_area;
            ViewBag.cleanliness = cleanliness;
            ViewBag.workRoomType = workRoomType;
            ViewBag.warehouseType = warehouseType;
            ViewBag.ProductionBuilding = ProductionBuilding;
            ViewBag.team = team;

            ViewBag.workRoomData = Json(Public_Function.DataTableToJSON(workRoomData));
            ViewBag.workRoomPopupData = Json(Public_Function.DataTableToJSON(workRoomPopupData));
            ViewBag.workRoomProductData = Json(Public_Function.DataTableToJSON(workRoomProductData));

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectWorkRoom(string workroom_cd, string use_yn)
        {
            DataTable workRoomData = workRoomService.selectWorkRoom(workroom_cd, use_yn);

            return Json(Public_Function.DataTableToJSON(workRoomData));
        }

        [CheckSession]
        [HttpPost]
        public string WorkroomCRUD(WorkRoom workRoom, string gubun)
        {
            string res = workRoomService.CRUD(workRoom, gubun);

            return res;
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectWorkRoomProduct(string workroom_cd)
        {
            DataTable workRoomProductData = workRoomService.selectWorkRoomProductData(workroom_cd);

            return Json(Public_Function.DataTableToJSON(workRoomProductData));
        }

        [CheckSession]
        [HttpPost]
        public string WorkRommProductCRUD(string workroom_cd, string item_cd, string item_seq, string gubun)
        {
            string res = workRoomService.ProductCRUD(workroom_cd, item_cd, item_seq, gubun);

            return res;
        }


        #endregion



        #region Equipment 설비 관리

        private EquipmentService equipmentService = new EquipmentService();

        [CheckSession]
        public ActionResult Equipment()
        {
            DataTable manufacturingEquipment = selectBoxService.GetSelectBox("공통코드", "S", "SC011", "manufacturingEquipment");
            DataTable plantGubun = selectBoxService.GetSelectBox("사업장", "D", "", "plantGubun");
            DataTable facilityType = selectBoxService.GetSelectBox("공통코드", "D", "SC011", "facilityType");
            DataTable interfaceType = selectBoxService.GetSelectBox("공통코드", "D", "SC040", "interfaceType");
            DataTable interfaceCode = selectBoxService.GetSelectBox("공통코드", "D", "IS001", "interfaceCode");
            DataTable COMPort = selectBoxService.GetSelectBox("공통코드", "D", "EQ001", "COMPort");
            DataTable COMBaudRate = selectBoxService.GetSelectBox("공통코드", "D", "EQ002", "COMBaudRate");
            DataTable COMDataBits = selectBoxService.GetSelectBox("공통코드", "D", "EQ003", "COMDataBits");
            DataTable COMParity = selectBoxService.GetSelectBox("공통코드", "D", "EQ004", "COMParity");
            DataTable COMStopBits = selectBoxService.GetSelectBox("공통코드", "D", "EQ005", "COMStopBits");
            DataTable COMHandshaking = selectBoxService.GetSelectBox("공통코드", "D", "EQ006", "COMHandshaking");
            DataTable unit = selectBoxService.GetSelectBox("공통코드", "D", "CM012", "COMHandshaking");
            DataTable facilityCriticality = selectBoxService.GetSelectBox("공통코드", "D", "SC018", "facilityCriticality");
            DataTable disposalReason = selectBoxService.GetSelectBox("공통코드", "D", "SC019", "disposalReason");

            DataTable equipmentData = equipmentService.selectEquipment("", "%", "Y");

            DataTable facilityPopupData = equipmentService.selectfacilityPopupData();
            DataTable workroomPopupData = equipmentService.selectWorkroomPopupData();
            DataTable equipmentPopupData = equipmentService.selectEquipmentPopupData();
            DataTable lineEquipPopupData = equipmentService.selectLineEquipPopupData();
            DataTable reponseEmpPopupData = equipmentService.selectReponseEmpPopupData();
            DataTable deptPopupData = equipmentService.selectDeptPopupData();
            DataTable workroomResponseEmpPopupData = equipmentService.selectWorkRoomReponseEmpPopupData();
            DataTable equipManageCustPopupData = equipmentService.selectEquipManageCustPopupData();
            DataTable equipProdCustPopupData = equipmentService.selectEquipProdCustPopupData();
            DataTable equipBuyCustPopupData = equipmentService.selectEquipBuyCustPopupData();
            DataTable equipAlarmData = equipmentService.selectAlarmList();

            ViewBag.manufacturingEquipment = manufacturingEquipment;
            ViewBag.plantGubun = plantGubun;
            ViewBag.facilityType = facilityType;
            ViewBag.interfaceType = interfaceType;
            ViewBag.interfaceCode = interfaceCode;
            ViewBag.COMPort = COMPort;
            ViewBag.COMBaudRate = COMBaudRate;
            ViewBag.COMDataBits = COMDataBits;
            ViewBag.COMParity = COMParity;
            ViewBag.COMStopBits = COMStopBits;
            ViewBag.COMHandshaking = COMHandshaking;
            ViewBag.unit = unit;
            ViewBag.facilityCriticality = facilityCriticality;
            ViewBag.disposalReason = disposalReason;

            //var serializer = new JavaScriptSerializer();

            //serializer.MaxJsonLength = Int32.MaxValue;

            //var jsonResult = new JsonResult
            //{
            //    Data = serializer.Serialize(Public_Function.DataTableToJSON(equipmentData)),
            //    ContentType = "application/json"
            //};

            var jsonResult = Json(Public_Function.DataTableToJSON(equipmentData), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            ViewBag.equipmentData = jsonResult;

            ViewBag.facilityPopupData = Json(Public_Function.DataTableToJSON(facilityPopupData));
            ViewBag.workroomPopupData = Json(Public_Function.DataTableToJSON(workroomPopupData));
            ViewBag.equipmentPopupData = Json(Public_Function.DataTableToJSON(equipmentPopupData));
            ViewBag.lineEquipPopupData = Json(Public_Function.DataTableToJSON(lineEquipPopupData));
            ViewBag.reponseEmpPopupData = Json(Public_Function.DataTableToJSON(reponseEmpPopupData));
            ViewBag.deptPopupData = Json(Public_Function.DataTableToJSON(deptPopupData));
            ViewBag.workroomResponseEmpPopupData = Json(Public_Function.DataTableToJSON(workroomResponseEmpPopupData));
            ViewBag.equipManageCustPopupData = Json(Public_Function.DataTableToJSON(equipManageCustPopupData));
            ViewBag.equipProdCustPopupData = Json(Public_Function.DataTableToJSON(equipProdCustPopupData));
            ViewBag.equipBuyCustPopupData = Json(Public_Function.DataTableToJSON(equipBuyCustPopupData));
            ViewBag.equipAlarmData = equipAlarmData;

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectEquipment(string equip_cd, string equip_type, string use_yn)
        {
            DataTable equipmentData = equipmentService.selectEquipment(equip_cd, equip_type, use_yn);

            var jsonResult = Json(Public_Function.DataTableToJSON(equipmentData));
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        [CheckSession]
        [HttpPost]
        public string SelectEquipImage(string equip_cd)
        {
            if (equip_cd == null)
            {
                return null;
            }

            string imageSrc = equipmentService.selectEquipImage(equip_cd);

            return imageSrc;
        }

        [CheckSession]
        [HttpPost]
        public ActionResult UploadEquipImage(string equip_cd)
        {

            try
            {
                using (var binaryReader = new BinaryReader(Request.Files["equipment-image"].InputStream))
                {
                    equipmentService.uploadImage(binaryReader.ReadBytes(Request.Files["equipment-image"].ContentLength), equip_cd);
                }

                //var myBytes = GetByteArrayFromImage(Request.Form.Files["equipment-image"]);

                //byte[] GetByteArrayFromImage(IFormFile file)
                //{
                //    using (var target = new MemoryStream())
                //    {
                //        file.CopyTo(target);
                //        return target.ToArray();
                //    }
                //}

                //equipmentService.uploadImage(myBytes, equip_cd);

            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        [CheckSession]
        [HttpPost]
        public string EquipmentCRUD(Equipment equipment, string gubun)
        {
            string res = equipmentService.equipmentCRUD(equipment, gubun);

            return res;
        }

        [CheckSession]
        [HttpPost]
        public ActionResult UploadEquipAttachedFile(string equip_cd, string name, string gubun, string equip_doc_id)
        {

            try
            {
                var uploadFile = Request.Files[name];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;
                var fgubun = name.Substring(name.Length - 1, 1);

                using (var binaryReader = new BinaryReader(Request.Files[name].InputStream))
                {
                    equipmentService.uploadFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), equip_cd, fileName, contentType, fgubun, gubun, equip_doc_id);
                }

                //var myBytes = GetByteArrayFromFile(uploadFile);
                //byte[] GetByteArrayFromFile(IFormFile file)
                //{
                //    using (var target = new MemoryStream())
                //    {
                //        file.CopyTo(target);
                //        return target.ToArray();
                //    }
                //}

                //equipmentService.uploadFile(myBytes, equip_cd, fileName, contentType, fgubun, gubun, equip_doc_id);

            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        [CheckSession]
        [HttpPost]
        public string DeleteAttachedFile(string equip_cd, string fgubun, string equip_doc_id)
        {
            string res = equipmentService.deleteAttachedFile(equip_cd, fgubun, equip_doc_id);

            return res;
        }

        [CheckSession]
        [HttpGet]
        public ActionResult DownloadAttachedFile(string equip_doc_id)
        {
            DataTable attachmentFileData = equipmentService.selectAttachmentFile(equip_doc_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[1].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0].ItemArray[0]
                , mimeType
                , attachmentFileData.Rows[0].ItemArray[1].ToString()
                );
        }

        #endregion



        #region Vender 거래처 관리

        private VenderService venderService = new VenderService();

        [CheckSession]
        public ActionResult Vender()
        {
            DataTable venderGubun = selectBoxService.GetSelectBox("공통코드", "S", "CM100", "venderGubun");
            DataTable formVenderGubun = selectBoxService.GetSelectBox("공통코드", "D", "CM100", "venderGubun");
            DataTable venderData = venderService.selectVender("", "Y", "%");
            DataTable venderPopupData = venderService.selectVenderPopupData();

            ViewBag.venderGubun = venderGubun;
            ViewBag.formVenderGubun = formVenderGubun;
            ViewBag.venderData = Json(Public_Function.DataTableToJSON(venderData));
            ViewBag.venderPopupData = Json(Public_Function.DataTableToJSON(venderPopupData));

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectVender(string vender_cd, string vender_gb, string use_yn)
        {
            DataTable venderData = venderService.selectVender(vender_cd, use_yn, vender_gb);

            return Json(Public_Function.DataTableToJSON(venderData));
        }

        [CheckSession]
        [HttpPost]
        public string VenderCdValidation(string vender_cd)
        {
            string res = venderService.venderCdValidation(vender_cd);

            return res;
        }

        [CheckSession]
        [HttpPost]
        public JsonResult VenderCRUD(Vender vender, string gubun)
        {
            string res = venderService.venderCRUD(vender, gubun);

            var resJson = "{ \"messege\": \"" + res+ "\" }";

            return Json(resJson);
        }


        #endregion



        #region Process 공정관리

        private ProcessService processService = new ProcessService();

        [CheckSession]
        public ActionResult Process()
        {
            DataTable processType = selectBoxService.GetSelectBox("공통코드", "D", "RT011", "processType");
            DataTable processRateType = selectBoxService.GetSelectBox("공통코드", "D", "RT015", "processRateType");
            DataTable formulation = selectBoxService.GetSelectBox("공통코드", "D", "CM002", "formulation");
            DataTable ProcessData = processService.SelectProcess("");
            DataTable processCCPData = processService.selectCCPData();
            DataTable processWorkroomPopupData = processService.selectWorkroomPopupData();
            DataTable processEmpPopupData = processService.selectEmpPopupData(); 

            ViewBag.processType = processType;
            ViewBag.processRateType = processRateType;
            ViewBag.formulation = formulation;
            ViewBag.CCP_CD = processCCPData;
            ViewBag.ProcessData = Json(Public_Function.DataTableToJSON(ProcessData));
            ViewBag.ProcessWorkRoomData = Json(Public_Function.DataTableToJSON(processWorkroomPopupData));
            ViewBag.ProcessEmpData = Json(Public_Function.DataTableToJSON(processEmpPopupData));

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectProcessRateType(string process_rate_type)
        {
            DataTable ProcessRateTypeData = processService.SelectProcessRateType(process_rate_type);

            return Json(Public_Function.DataTableToJSON(ProcessRateTypeData));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectProcessManager(string process_cd)
        {
            DataTable ProcessManager = processService.SelectProcessManager(process_cd);

            return Json(Public_Function.DataTableToJSON(ProcessManager));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectProcess(string process_cd)
        {
            DataTable ProcessData = processService.SelectProcess(process_cd);

            return Json(Public_Function.DataTableToJSON(ProcessData));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ProcessManagerCRUD(string process_cd, string user_id, string gubun)
        {
            string res = processService.processManagerCRUD(process_cd, user_id, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ProcessCRUD(Process process, string gubun)
        {
            string res = processService.processCRUD(process, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }


        #endregion



        #region TareMaster 반제품용기관리

        private TareMasterService tareMasterService = new TareMasterService();

        [CheckSession]
        public ActionResult TareMaster()
        {
            DataTable tareMasterType = selectBoxService.GetSelectBox("공통코드", "S", "TR001", "tareMasterType");
            DataTable tareMasterMaterial = selectBoxService.GetSelectBox("공통코드", "S", "TR002", "tareMasterMaterial");
            DataTable tareMasterClass = selectBoxService.GetSelectBox("공통코드", "S", "TR003", "tareMasterClass");
            DataTable tareMasterTypeForm = selectBoxService.GetSelectBox("공통코드", "D", "TR001", "tareMasterTypeForm");
            DataTable tareMasterMaterialForm = selectBoxService.GetSelectBox("공통코드", "D", "TR002", "tareMasterMaterialForm");
            DataTable tareMasterClassForm = selectBoxService.GetSelectBox("공통코드", "D", "TR003", "tareMasterClassForm");
            DataTable tareMasterData = tareMasterService.SelectTareMaster("%", "%", "%");

            ViewBag.tareMasterType = tareMasterType;
            ViewBag.tareMasterMaterial = tareMasterMaterial;
            ViewBag.tareMasterClass = tareMasterClass;
            ViewBag.tareMasterTypeForm = tareMasterTypeForm;
            ViewBag.tareMasterMaterialForm = tareMasterMaterialForm;
            ViewBag.tareMasterClassForm = tareMasterClassForm;
            ViewBag.tareMasterData = Json(Public_Function.DataTableToJSON(tareMasterData));

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectTare(string taremaster_type, string taremaster_material, string taremaster_class)
        {
            DataTable tareMasterData = tareMasterService.SelectTareMaster(taremaster_type, taremaster_material, taremaster_class);

            return Json(Public_Function.DataTableToJSON(tareMasterData));
        }

        [HttpPost]
        public string TareMasterTRX(Tare tare)
        {
            string res = tareMasterService.TareMasterTRX(tare);

            return res;
        }


        #endregion

    }
}