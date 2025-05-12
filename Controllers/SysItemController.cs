using DevExpress.CodeParser;
using HACCP.Attribute;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models.SysItem;
using HACCP.Services.Comm;
using HACCP.Services.SysItem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HACCP.Controllers
{
    public class SysItemController : Controller
    {
        private SelectBoxService selectBoxService = new SelectBoxService();

        #region MaterialMaster2 원료관리

        private MaterialMaster2Service materialMaster2Service = new MaterialMaster2Service();

        /// <summary>
        /// 원료 관리
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        public ActionResult MaterialMaster2()
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable venderPopupData = codeHelpService.GetCode(CodeHelpType.vender_buy, HttpContext.Session["plantCD"].ToString(), "");

            // 메인 그리드 불러오기
            DataTable materials = materialMaster2Service.Select("", "", "", "");

            // 셀렉트 박스 옵션들 불러오기             
            DataTable productionBuildingGubuns = selectBoxService.GetSelectBox("공통코드", "S", "CM027", "productionBuildingGubuns");
            DataTable usedGubuns = selectBoxService.GetSelectBox("공통코드", "S", "CM031", "usedGubuns");
            DataTable teamGubuns = selectBoxService.GetSelectBox("공통코드", "S", "CM059", "teamGubuns");
            DataTable abcGubuns = selectBoxService.GetSelectBox("공통코드", "D", "CM042", "abcGubuns");
            DataTable workplaceGubuns = selectBoxService.GetSelectBox("사업장", "D", "", "workplaceGubuns");
            DataTable materialGubuns = selectBoxService.GetSelectBox("공통코드", "PW", "CM067", "materialGubuns");
            DataTable unitGubuns = selectBoxService.GetSelectBox("공통코드", "D", "CM012", "unitGubuns");
            DataTable periodGubuns = selectBoxService.GetSelectBox("공통코드", "N", "CM057", "periodGubuns");
            DataTable storageConditions = selectBoxService.GetSelectBox("공통코드", "D", "CM039", "storageConditions");
            DataTable storageTemperatures = selectBoxService.GetSelectBox("공통코드", "D", "CM040", "storageTemperatures");
            DataTable storageWarehouses = selectBoxService.GetSelectBox("창고", "D", "", "storageWarehouses");
            DataTable testStandards = selectBoxService.GetSelectBox("공통코드", "N", "CM003", "testStandards");
            DataTable samplingGubuns = selectBoxService.GetSelectBox("공통코드", "N", "CM071", "samplingGubuns");
            //DataTable suppliers = selectBoxService.GetSelectBox("공통코드", "D", "CM064", "suppliers");
            DataTable suppliers = materialMaster2Service.SelectSuppliers("3");
            DataTable retiredReason = selectBoxService.GetSelectBox("공통코드", "D", "CM060", "retiredReason");

            DataTable standardCodes = materialMaster2Service.GetStandardCd("standard");
            DataTable countryCodes = materialMaster2Service.GetStandardCd("country");
            DataTable vendorCodes = materialMaster2Service.GetStandardCd("vendor");

            ViewBag.productionBuildingGubuns = productionBuildingGubuns;
            ViewBag.usedGubuns = usedGubuns;
            ViewBag.teamGubuns = teamGubuns;
            ViewBag.abcGubuns = abcGubuns;
            ViewBag.workplaceGubuns = workplaceGubuns;
            ViewBag.materialGubuns = materialGubuns;
            ViewBag.unitGubuns = unitGubuns;
            ViewBag.periodGubuns = periodGubuns;
            ViewBag.storageConditions = storageConditions;
            ViewBag.storageTemperatures = storageTemperatures;
            ViewBag.storageWarehouses = storageWarehouses;
            ViewBag.testStandards = testStandards;
            ViewBag.samplingGubuns = samplingGubuns;
            ViewBag.retiredReason = retiredReason;

            ViewBag.materials = materials;

            ViewBag.standardCodes = standardCodes;
            ViewBag.countryCodes = Json(Public_Function.DataTableToJSON(countryCodes));
            ViewBag.vendorCodes = vendorCodes;

            ViewBag.venderPopupData = Json(Public_Function.DataTableToJSON(venderPopupData));
            ViewBag.suppliersPopupData = Json(Public_Function.DataTableToJSON(suppliers));

            return View();
        }

        /// <summary>
        /// 조회결과 원료 리스트
        /// </summary>
        /// <param name="pMaterial"></param>
        /// <returns></returns>
        [HttpPost]
        public string SelectMaterials(Material pMaterial)
        {
            DataTable materials = materialMaster2Service.Select(pMaterial.item_nm, pMaterial.item_type1, pMaterial.item_type2, pMaterial.prod_end);

            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(materials);

            //return Json(JSONresult);
            return JSONresult;
        }

        [HttpPost]
        public JsonResult SelectSuppliers(string vender_gb)
        {
            DataTable materialImage = materialMaster2Service.SelectSuppliers(vender_gb);

            return Json(materialImage);
        }

        [HttpGet]
        public JsonResult SelectMaterialImage(string item_cd)
        {
            DataTable materialImage = materialMaster2Service.SelectMaterialImage(item_cd);

            return Json(Public_Function.DataTableToJSON(materialImage), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 원료코드 중복 검사
        /// </summary>
        /// <param name="pItemCd"></param>
        /// <returns></returns>
        //[HttpPost]
        public JsonResult ItemCdDuplicateCheck(string pItemCd, string pGubun, string pSelectedItemCd)
        {
            string[] res = new string[2];
            int cnt = Int16.Parse(materialMaster2Service.CdDuplicateCheck(pItemCd));

            if (pGubun.Equals("I"))
            {
                if (cnt > 0)
                {
                    res[0] = "0";
                    res[1] = "이미 사용중인 코드입니다.";
                }
                else
                {
                    res[0] = "1";
                    res[1] = "사용 가능한 코드입니다.";
                }

            }
            else if (pGubun.Equals("U"))
            {
                if (!pItemCd.Equals(pSelectedItemCd) && cnt > 0)
                {
                    res[0] = "0";
                    res[1] = "이미 사용중인 코드입니다.";
                }
                else
                {
                    res[0] = "1";
                    res[1] = "사용 가능한 코드입니다.";
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 원료 수정
        /// </summary>
        /// <param name="pMaterial"></param>
        [HttpPost]
        public string MaterialCRUD(Material pMaterial)
        {
            //기존, 입력,수정,삭제시 rowcount만 리턴
            //bool isInserted = materialMaster2Service.CRUD(pMaterial);
            //return Json(new { Ok = isInserted });

            //수정, 입력시 rowcount,item_cd 리턴 / 수정,삭제시 rowcount만 리턴
            string message = materialMaster2Service.CRUD(pMaterial);
            return message;

            //if (ModelState.IsValid)
            //{
            //    materialMaster2Service.CRUD(pMaterial);
            //    string gubun = pMaterial.pGubun;
            //    string item_cd = pMaterial.item_cd;

            //    return Json(new { Ok = true });
            //}
            //else
            //{
            //    return Json(new { Ok = false });
            //}
        }

        /// <summary>
        /// 원료 삭제
        /// </summary>
        /// <param name="pMaterial"></param>
        [HttpPost]
        public string MaterialDelete(string item_cd)
        {
            string isSucceed = materialMaster2Service.DeleteMaterial(item_cd);

            return isSucceed;
        }


        [HttpPost]
        public JsonResult MaterialMaster2SelectMaker(string material_cd)
        {
            DataTable dt = materialMaster2Service.MaterialMaster2SelectMaker(material_cd);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult MaterialMaster2SelectContent(string material_cd)
        {
            DataTable dt = materialMaster2Service.MaterialMaster2SelectContent(material_cd);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string MaterialMakerCRUD([JsonBinder] List<Material> materials)
        {
            string res = materialMaster2Service.MaterialMakerCRUD(materials);

            return res;
        }

        [HttpPost]
        public string MaterialContentCRUD([JsonBinder] List<Material> materials)
        {
            string res = materialMaster2Service.MaterialContentCRUD(materials);

            return res;
        }

        [HttpPost]
        public ActionResult UploadMaterialImage(string item_cd, int gubun, int imageCnt)
        {
            try
            {
                var fileName = "";

                switch (gubun)
                {
                    case 1:
                        fileName = "upper_image";
                        break;

                    case 2:
                        fileName = "middle_image";
                        break;

                    case 3:
                        fileName = "lower_image";
                        break;

                }

                using (var binaryReader = new BinaryReader(Request.Files[fileName].InputStream))
                {
                    materialMaster2Service.uploadImage(binaryReader.ReadBytes(Request.Files[fileName].ContentLength), item_cd, gubun, fileName, imageCnt);
                }

            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        [HttpPost]
        public string MaterialMaster2DeleteImage(string item_cd, int gubun)
        {
            string res = materialMaster2Service.MaterialMaster2DeleteImage(item_cd, gubun);

            return res;
        }

        #endregion


        #region PackMaterialMaster2 자재관리

        private PackMaterialMaster2Service packMaterialMaster2Service = new PackMaterialMaster2Service();

        [CheckSession]
        public ActionResult PackMaterialMaster2()
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable venderPopupData = codeHelpService.GetCode(CodeHelpType.vender_buy, HttpContext.Session["plantCD"].ToString(), "");

            DataTable workplaceGubuns = selectBoxService.GetSelectBox("사업장", "D", "", "workplaceGubuns");
            DataTable productionBuildingGubuns1 = selectBoxService.GetSelectBox("공통코드", "S", "CM027", "productionBuildingGubuns1");
            DataTable teamGubuns = selectBoxService.GetSelectBox("공통코드", "D", "CM059", "teamGubuns");
            DataTable samplingGubuns = selectBoxService.GetSelectBox("공통코드", "D", "CM071", "samplingGubuns");
            DataTable abcGubuns = selectBoxService.GetSelectBox("공통코드", "D", "CM042", "abcGubuns");
            DataTable periodGubuns = selectBoxService.GetSelectBox("공통코드", "D", "CM057", "periodGubuns");
            DataTable unitGubuns = selectBoxService.GetSelectBox("공통코드", "D", "CM012", "unitGubuns");
            DataTable productionBuildingGubuns2 = selectBoxService.GetSelectBox("공통코드", "D", "CM027", "productionBuildingGubuns2");
            DataTable retiredReason = selectBoxService.GetSelectBox("공통코드", "D", "CM060", "retiredReason");
            DataTable storageWarehouses = selectBoxService.GetSelectBox("창고", "D", "", "storageWarehouses");
            DataTable managementUnit = selectBoxService.GetSelectBox("공통코드", "D", "CM024", "managementUnit");
            DataTable decimalCalculationcMethod = selectBoxService.GetSelectBox("공통코드", "D", "RT030", "decimalCalculationcMethod");
            DataTable storageConditions = selectBoxService.GetSelectBox("공통코드", "D", "CM039", "storageConditions");
            DataTable storageTemperatures = selectBoxService.GetSelectBox("공통코드", "D", "CM040", "storageTemperatures");
            DataTable usedGubuns = selectBoxService.GetSelectBox("공통코드", "S", "CM031", "usedGubuns");
            DataTable packingMaterial = selectBoxService.GetSelectBox("공통코드", "D", "CM058", "packingMaterial");
            DataTable fomulation = selectBoxService.GetSelectBox("공통코드", "D", "CM002", "fomulation");
            DataTable brandFomulation = selectBoxService.GetSelectBox("공통코드", "D", "CM065", "brandFomulation");
            DataTable managementType = selectBoxService.GetSelectBox("공통코드", "D", "CM066", "managementType");
            DataTable fomulationType = selectBoxService.GetSelectBox("공통코드", "PB", "CM067", "fomulationType");
            DataTable specType = selectBoxService.GetSelectBox("공통코드", "D", "CM112", "specType");
            //DataTable suppliers = selectBoxService.GetSelectBox("공통코드", "D", "CM064", "suppliers");
            DataTable suppliers = materialMaster2Service.SelectSuppliers("2"); //2:자재

            DataTable PackingMaterials = packMaterialMaster2Service.SelectPackingMaterial("", "%");
            DataTable PackingMaterialsPopupData = packMaterialMaster2Service.SelectPackingMaterialPopupData();
            //DataTable PackingMaterialsBrandData = packMaterialMaster2Service.SelectPackingMaterialBrandData();

            ViewBag.workplaceGubuns = workplaceGubuns;
            ViewBag.productionBuildingGubuns1 = productionBuildingGubuns1;
            ViewBag.teamGubuns = teamGubuns;
            ViewBag.samplingGubuns = samplingGubuns;
            ViewBag.abcGubuns = abcGubuns;
            ViewBag.periodGubuns = periodGubuns;
            ViewBag.unitGubuns = unitGubuns;
            ViewBag.productionBuildingGubuns2 = productionBuildingGubuns2;
            ViewBag.retiredReason = retiredReason;
            ViewBag.storageWarehouses = storageWarehouses;
            ViewBag.managementUnit = managementUnit;
            ViewBag.decimalCalculationcMethod = decimalCalculationcMethod;
            ViewBag.storageConditions = storageConditions;
            ViewBag.storageTemperatures = storageTemperatures;
            ViewBag.usedGubuns = usedGubuns;
            ViewBag.packingMaterial = packingMaterial;
            ViewBag.fomulation = fomulation;
            ViewBag.brandFomulation = brandFomulation;
            ViewBag.managementType = managementType;
            ViewBag.fomulationType = fomulationType;
            ViewBag.specType = specType;

            //ViewBag.PackingMaterials = Json(Public_Function.DataTableToJSON(PackingMaterials));
            ViewBag.PackingMaterials = PackingMaterials;
            ViewBag.PackingMaterialsPopupData = Json(Public_Function.DataTableToJSON(PackingMaterialsPopupData));
            ViewBag.PackingMaterialsVenderPopupData = Json(Public_Function.DataTableToJSON(venderPopupData));
            //ViewBag.PackingMaterialsBrandData = Json(Public_Function.DataTableToJSON(PackingMaterialsBrandData)); //브랜드 제외 (코스맥스 바이오에서만사용)
            ViewBag.suppliersPopupData = Json(Public_Function.DataTableToJSON(suppliers));

            return View();
        }

        [CheckSession]
        [HttpPost]
        public string SelectPackMaterial(string item_cd, string prod_end)
        {
            DataTable PackingMaterials = packMaterialMaster2Service.SelectPackingMaterial(item_cd, prod_end);

            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(PackingMaterials);

            //return Json(JSONresult);
            return JSONresult;
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectSupllier(string gb)
        {
            DataTable Suplliers = packMaterialMaster2Service.Select_Supllier(gb);

            return Json(Public_Function.DataTableToJSON(Suplliers));
        }

        [HttpGet]
        public JsonResult PackMaterialMaster2SelectMaker(string packMaterial_cd)
        {
            DataTable dt = packMaterialMaster2Service.PackMaterialMaster2SelectMaker(packMaterial_cd);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult PackItemCdDuplicateCheck(string gubun, string item_cd, string selected_item_cd)
        {
            string[] res = new string[2];
            int cnt = Int16.Parse(packMaterialMaster2Service.itemCdDuplicateCheck(item_cd));

            if (gubun.Equals("I"))
            {
                if (cnt > 0)
                {
                    res[0] = "0";
                    res[1] = "이미 사용중인 코드입니다.";
                }
                else
                {
                    res[0] = "1";
                    res[1] = "사용 가능한 코드입니다.";
                }

            }
            else if (gubun.Equals("U"))
            {
                if (!item_cd.Equals(selected_item_cd) && cnt > 0)
                {
                    res[0] = "0";
                    res[1] = "이미 사용중인 코드입니다.";
                }
                else
                {
                    res[0] = "1";
                    res[1] = "사용 가능한 코드입니다.";
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [CheckSession]
        [HttpPost]
        public string PackMaterialCRUD(PackMaterial packMaterial, string gubun)
        {
            //if (!gubun.Equals("D"))
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return Json(new { Ok = false });
            //    }
            //}

            string res = packMaterialMaster2Service.PackMaterialCRUD(packMaterial, gubun);

            return res;
        }

        [HttpPost]
        public string PackMaterialMakerCRUD([JsonBinder] List<PackMaterial> packMaterials)
        {
            string res = packMaterialMaster2Service.PackMaterialMakerCRUD(packMaterials);

            return res;
        }

        #endregion


        #region Item_Standard 제조제품 관리

        private ItemStandardService itemStandardService = new ItemStandardService();

        [CheckSession]
        public ActionResult Item_Standard()
        {
            DataTable workplaceGubuns = selectBoxService.GetSelectBox("사업장", "D", "", "workplaceGubuns");
            DataTable unitGubuns = selectBoxService.GetSelectBox("공통코드", "D", "CM012", "unitGubuns");
            DataTable storageConditions = selectBoxService.GetSelectBox("공통코드", "D", "CM039", "storageConditions");
            DataTable storageTemperatures = selectBoxService.GetSelectBox("공통코드", "D", "CM040", "storageTemperatures");
            DataTable periodGubuns = selectBoxService.GetSelectBox("공통코드", "D", "CM057", "periodGubuns");
            DataTable productionBuildingGubuns = selectBoxService.GetSelectBox("공통코드", "D", "CM027", "productionBuildingGubuns");
            DataTable retiredReason = selectBoxService.GetSelectBox("공통코드", "D", "CM060", "retiredReason");
            DataTable usedGubuns = selectBoxService.GetSelectBox("공통코드", "S", "CM031", "usedGubuns");
            DataTable fomulation = selectBoxService.GetSelectBox("공통코드", "D", "CM059", "fomulation");
            DataTable productType = selectBoxService.GetSelectBox("공통코드", "D", "CM201", "productType");
            DataTable certificationType = selectBoxService.GetSelectBox("공통코드", "D", "CM069", "certificationType");

            DataTable items = itemStandardService.SelectItemStandard("", "%", "5,6");
            DataTable warehousePopupData = itemStandardService.SelectWarehousePopupData();

            ViewBag.workplaceGubuns = workplaceGubuns;
            ViewBag.unitGubuns = unitGubuns;
            ViewBag.storageConditions = storageConditions;
            ViewBag.storageTemperatures = storageTemperatures;
            ViewBag.periodGubuns = periodGubuns;
            ViewBag.productionBuildingGubuns = productionBuildingGubuns;
            ViewBag.retiredReason = retiredReason;
            ViewBag.usedGubuns = usedGubuns;
            ViewBag.fomulation = fomulation;
            ViewBag.productType = productType;
            ViewBag.certificationType = certificationType;

            ViewBag.items = Json(Public_Function.DataTableToJSON(items));
            ViewBag.warehousePopupData = Json(Public_Function.DataTableToJSON(warehousePopupData));
            ViewBag.Item_StandardAuth = Json(HttpContext.Session["Item_StandardAuth"]);

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectItemDetail(string item_cd)
        {
            DataTable itemDetail = itemStandardService.SelectItemDetail(item_cd);
            DataTable itemLicenseNo = itemStandardService.SelectItemLicenseNo(item_cd);

            string itemDetailJson = Public_Function.DataTableToJSON(itemDetail);
            string itemLicenseNoJson = Public_Function.DataTableToJSON(itemLicenseNo);

            List<string> jsonList = new List<string>();
            jsonList.Add(itemDetailJson);
            jsonList.Add(itemLicenseNoJson);

            return Json(jsonList.ToArray());
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Item_StandardSelectItem(string prod_end, string item_gb)
        {
            DataTable items = itemStandardService.SelectItemStandard("", prod_end, item_gb);

            return Json(Public_Function.DataTableToJSON(items));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Item_StandardCRUD(ItemStandard itemStandard)
        {
            string res = itemStandardService.ItemStandardCRUD(itemStandard);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Item_StandardDuplicateCheck(string item_cd)
        {
            string[] res = new string[2];
            int cnt = Int16.Parse(itemStandardService.itemCdDuplicateCheck(item_cd));

            if (cnt > 0)
            {
                res[0] = "0";
                res[1] = "이미 사용중인 코드입니다.";
            }
            else
            {
                res[0] = "1";
                res[1] = "사용 가능한 코드입니다.";
            }

            return Json(res);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Item_StandardGetLicenseNo(string item_cd)
        {
            string licenseNo = itemStandardService.GetLicenseNo(item_cd);

            return Json(licenseNo);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectItemImage(string item_cd)
        {
            DataTable itemlImage = itemStandardService.selectItemImage(item_cd);

            return Json(Public_Function.DataTableToJSON(itemlImage));
        }

        [CheckSession]
        [HttpPost]
        public ActionResult UploadItemImage(string item_cd, string fGubun)
        {
            string fileInputName = "";

            switch (fGubun)
            {
                case "1":
                    fileInputName = "upper_image";
                    break;
                case "2":
                    fileInputName = "middle_image";
                    break;
                case "3":
                    fileInputName = "lower_image";
                    break;
            }

            try
            {
                using (var binaryReader = new BinaryReader(Request.Files[fileInputName].InputStream))
                {
                    itemStandardService.uploadImage(item_cd, fGubun, fileInputName, binaryReader.ReadBytes(Request.Files[fileInputName].ContentLength));
                }

            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult DeleteItemImage(string item_cd, string fGubun)
        {
            string res = itemStandardService.deleteImage(item_cd, fGubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        #endregion


        #region Packing_Item  제품관리

        [CheckSession]
        public ActionResult Packing_Item()
        {
            DataTable workplaceGubuns = selectBoxService.GetSelectBox("사업장", "D", "", "workplaceGubuns");
            DataTable unitGubuns = selectBoxService.GetSelectBox("공통코드", "D", "CM012", "unitGubuns");
            DataTable productionBuildingGubuns = selectBoxService.GetSelectBox("공통코드", "D", "CM027", "productionBuildingGubuns");
            DataTable retiredReason = selectBoxService.GetSelectBox("공통코드", "D", "CM060", "retiredReason");
            DataTable usedGubuns = selectBoxService.GetSelectBox("공통코드", "S", "CM031", "usedGubuns");
            DataTable fomulation = selectBoxService.GetSelectBox("공통코드", "D", "CM059", "fomulation");
            DataTable abcGubuns = selectBoxService.GetSelectBox("공통코드", "D", "CM042", "abcGubuns");

            DataTable items = itemStandardService.SelectItemStandard("", "%", "1,4");
            DataTable makeItemPopupData = itemStandardService.SelectMAkeItemPopupData();

            ViewBag.workplaceGubuns = workplaceGubuns;
            ViewBag.unitGubuns = unitGubuns;
            ViewBag.productionBuildingGubuns = productionBuildingGubuns;
            ViewBag.retiredReason = retiredReason;
            ViewBag.usedGubuns = usedGubuns;
            ViewBag.fomulation = fomulation;
            ViewBag.abcGubuns = abcGubuns;

            ViewBag.items = Json(Public_Function.DataTableToJSON(items));
            ViewBag.makeItemPopupData = Json(Public_Function.DataTableToJSON(makeItemPopupData));
            ViewBag.Packing_ItemAuth = Json(HttpContext.Session["Packing_ItemAuth"]);

            return View();
        }
       
        #endregion


        #region Etc_Item2 기타제품 관리

        public ActionResult Etc_Item2()
        {
            return View();
        }

        public JsonResult Etc_Item2Select(EtcItem.SrchParam param)
        {
            Etc_Item2Service etc_Item2Service = new Etc_Item2Service();

            DataTable dt = etc_Item2Service.Etc_Item2Select(param);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        [HttpPost]
        public string Etc_Item2TRX(EtcItem etcItem)
        {
            Etc_Item2Service etc_Item2Service = new Etc_Item2Service();

            string res = etc_Item2Service.Etc_Item2TRX(etcItem);

            return res;
        }
        #endregion
    }
}