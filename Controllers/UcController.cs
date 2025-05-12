using HACCP.Models.Uc;
using HACCP.Services.Comm;
using HACCP.Services.Uc;
using log4net;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class UcController : Controller
    {
        #region 공통 서비스 설정
        private static readonly ILog log = LogManager.GetLogger(typeof(CommController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        #endregion

        #region 공통변수 선언
               
        #endregion

        #region 공통 toolbar partial설정 Action
        /// <summary>
        /// 툴바설정 공통
        /// </summary>
        /// <param name="toolbarIndex">화면에 툴바가 여러개 있을때 index를 지정
        ///   (1) 화면에 툴바 하나인경우 -> 0을 지정함
        ///   (2) 화면에 툴바 두개이상   -> 1이상의 값을 지정, index는 _(숫자)로 만들어지며 Id와 onclick 펑션명에 index가 붙음..</param>
        /// <param name="isUnAuthBtnRemove">권한이 없는경우에만 버튼을 삭제할지 여부
        ///   (1)true :  버튼 삭제
        ///   (2)false : 버튼 비활성  
        /// </param>
        /// <param name="visibleBtns">출력할 버튼 리스트 지정(세미콜론(;)으로 버튼 구분, 전체출력 이면 *로 표기함)  </param>
        /// <returns></returns>
        [ChildActionOnly]       
        public PartialViewResult SetToolbar(int toolbarIndex, bool isUnAuthBtnRemove, string visibleBtns)
        {
            // 매뉴서비스 선언
            MenuAuthService menuAuthService = new MenuAuthService();
            
            #region 서비스 호출
            DataTable menuAuthList = (DataTable)HttpContext.Session["loginMenuAuthList"];
            List<ToolbarFormAuth.FormAuth> btnAuths = menuAuthService.getToolbarAuthList(Request, menuAuthList, toolbarIndex, isUnAuthBtnRemove, visibleBtns);
            #endregion
                       
            return PartialView("Uc/_SetToolbar", btnAuths);
        }
        #endregion

    }
}