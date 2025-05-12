using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HACCP.Libs.Views
{
    /// <summary>
    /// View 화면에서 사용되는 Helper확장 class > 폼 권한 설정 
    /// </summary>
    public static class HelperExtendAuth
    {
        static JArray jsonFormAuth = null;
        static Uri url;
        static string formCd;

        static HelperExtendAuth()
        {
            #region 현재 form설정
            url = HttpContext.Current.Request.Url;
            formCd = url.Segments[url.Segments.Length - 1];
            #endregion

            #region 현재 form권한 가져오기
            string strFormAuth = (string)HttpContext.Current.Session[formCd + "Auth"];            
            jsonFormAuth = JArray.Parse(strFormAuth);
            //var test = jsonFormAuth[0].Value<string>("form_query");           
            #endregion
        }

        /// <summary>
        /// 현재 프로그램에 대해 권한에 맞게 toolbar 설정
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="toolbarIndex">화면에 툴바가 여러개 있을때 index를 지정
        ///   (1) 화면에 툴바 하나인경우 -> 0을 지정함
        ///   (2) 화면에 툴바 두개이상   -> 1이상의 값을 지정, index는 _(숫자)로 만들어지며 Id와 onclick 펑션명에 index가 붙음..</param>
        /// <param name="isUnAuthBtnRemove">권한이 없는경우에만 버튼을 삭제할지 여부
        ///   (1)true :  버튼 삭제
        ///   (2)false : 버튼 비활성  
        /// </param>
        /// <param name="visibleBtns">출력할 버튼 리스트 지정(세미콜론(;)으로 버튼 구분, 전체출력 이면 *로 표기함)  </param>        
        public static void SetToolbar(this HtmlHelper htmlHelper, int toolbarIndex, bool isUnAuthBtnRemove, string visibleBtns )
        {
            htmlHelper.RenderAction("SetToolbar", "Uc"
                , new{ 
                      toolbarIndex = toolbarIndex
                    , isUnAuthBtnRemove = isUnAuthBtnRemove
                    , visibleBtns = visibleBtns 
                });            
        }

        #region 조회권한 여부 체크
        /// <summary>
        /// 조회권한 여부
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static bool IsAuthSearch(this HtmlHelper htmlHelper)
        {
            string authYn = jsonFormAuth[0].Value<string>("form_query");
            return authYn.Equals("Y") ? true : false ;            
        }

        /// <summary>
        /// 입력권한 여부
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static bool IsAuthInput(this HtmlHelper htmlHelper)
        {
            string authYn = jsonFormAuth[0].Value<string>("form_insert");
            return authYn.Equals("Y") ? true : false;
        }

        /// <summary>
        /// 수정권한 여부
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static bool IsAuthEdit(this HtmlHelper htmlHelper)
        {
            string authYn = jsonFormAuth[0].Value<string>("form_edit");
            return authYn.Equals("Y") ? true : false;
        }

        /// <summary>
        /// 삭제권한 여부
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static bool IsAuthDelete(this HtmlHelper htmlHelper)
        {
            string authYn = jsonFormAuth[0].Value<string>("form_delete");
            return authYn.Equals("Y") ? true : false;
        }

        /// <summary>
        /// 저장권한 여부 : 입력 또는 수정 권한중 하나만 있어도 true임
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static bool IsAuthSave(this HtmlHelper htmlHelper)
        {
            string authYn1 = jsonFormAuth[0].Value<string>("form_insert");
            string authYn2 = jsonFormAuth[0].Value<string>("form_edit");

            return (authYn1.Equals("Y") || authYn2.Equals("Y")) ? true : false;
        }

        /// <summary>
        /// 인쇄권한 여부
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static bool IsAuthPreview(this HtmlHelper htmlHelper)
        {
            string authYn = jsonFormAuth[0].Value<string>("form_preview");
            return authYn.Equals("Y") ? true : false;
        }

        /// <summary>
        /// 인쇄권한 여부
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static bool IsAuthPrint(this HtmlHelper htmlHelper)
        {
            string authYn = jsonFormAuth[0].Value<string>("form_print");
            return authYn.Equals("Y") ? true : false;
        }

        /// <summary>
        /// 엑셀권한 여부
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static bool IsAuthExcel(this HtmlHelper htmlHelper)
        {
            string authYn = jsonFormAuth[0].Value<string>("form_transmission");
            return authYn.Equals("Y") ? true : false;
        }

        #endregion
    }
}