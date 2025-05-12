using HACCP.Libs.Database;
using HACCP.Models.Uc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Uc
{
    public class MenuAuthService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// 해당폼의 Toobar영역에 출력할 버튼리스트 리턴
        /// </summary>
        /// <param name="req">Controller의 Request객체</param>
        /// <param name="menuAuthList">Menu전체건한 DataList</param>
        /// <param name="toolbarIndex">ToolbarIndex</param>
        /// <param name="isUnAuthBtnRemove">비권한버튼제거여부</param>
        /// <param name="visibleBtns">출력대상버튼리스트</param>
        /// <returns></returns>
        internal List<ToolbarFormAuth.FormAuth> getToolbarAuthList(HttpRequestBase req, DataTable menuAuthList,  int toolbarIndex, bool isUnAuthBtnRemove, string visibleBtns)
        {
            string reqFormCd = null;
            List<ToolbarFormAuth.FormAuth> btnAuths = null;
            string[] visibleBtnList = null;
            
            if (req.Url.Segments.Length > 1)
            {
                // 현재 form 코드
                reqFormCd = req.Url.Segments[req.Url.Segments.Length - 1];

                #region 현재 form권한 가져오기
                var menuAuth = menuAuthList.AsEnumerable()
                            .Where(s => s.Field<string>("form_cd") == reqFormCd)
                            .Select(s => new
                            {
                                authSearch = s.Field<bool>("authSearch")
                            ,
                                authInput = s.Field<bool>("authInput")
                            ,
                                authEdit = s.Field<bool>("authEdit")
                            ,
                                authDelete = s.Field<bool>("authDelete")
                            ,
                                authSave = s.Field<bool>("authSave")
                            ,
                                authUndo = s.Field<bool>("authUndo")
                            ,
                                authPrint = s.Field<bool>("authPrint")
                            ,
                                authPreview = s.Field<bool>("authPreview")
                            ,
                                authExcel = s.Field<bool>("authExcel")
                            ,
                                authFavorite = s.Field<bool>("authFavorite")
                            })
                            .FirstOrDefault()
                            ;
                #endregion

                #region 현재 form 매뉴권한 설정                                        
                ToolbarFormAuth.FormCd = reqFormCd;
                ToolbarFormAuth.ToolbarIndex = toolbarIndex;
                ToolbarFormAuth.IsUnAuthBtnRemove = isUnAuthBtnRemove;

                //출력대상 버튼리스트 
                visibleBtns = visibleBtns.Equals(string.Empty) ? "*" : visibleBtns;
                if (visibleBtns != "*")
                {
                    visibleBtnList = visibleBtns.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                }
                ToolbarFormAuth.VisibleBtns = visibleBtnList;

                if (menuAuth != null)
                {
                    // 매뉴권한 있을때. 
                    btnAuths = new List<ToolbarFormAuth.FormAuth>()
                    {
                         new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Search, isAuth = menuAuth.authSearch }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Input, isAuth = menuAuth.authInput }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Edit, isAuth = menuAuth.authEdit }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Delete, isAuth = menuAuth.authDelete }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Save, isAuth = menuAuth.authSave }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Undo, isAuth = menuAuth.authUndo }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Preview, isAuth = menuAuth.authPreview }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Print, isAuth = menuAuth.authPrint }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Excel, isAuth = menuAuth.authExcel }
                    };
                }
                else
                {
                    // 매뉴권한 자체가 아얘 없을때. 기본값 (권한없음)처리
                    btnAuths = new List<ToolbarFormAuth.FormAuth>()
                    {
                         new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Search, isAuth = false }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Input, isAuth = false }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Edit, isAuth = false }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Delete, isAuth = false }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Save, isAuth = false }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Undo, isAuth = false }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Preview, isAuth = false }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Print, isAuth = false }
                     ,   new ToolbarFormAuth.FormAuth() { BtnGb = ToolbarAuthEnum.Excel, isAuth = false }
                    };
                }
                #endregion
            }
            return btnAuths;
        }

    }
}