using DevExpress.Web.ASPxSpreadsheet.Internal.Forms;
using DevExpress.XtraRichEdit.Commands;
using HACCP.Libs;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Uc
{
    /// <summary>
    /// 공통 toolbar 권한관리
    /// </summary>

    public enum ToolbarAuthEnum
    {
        Search,
        Input,
        Edit,        
        Delete,
        Save,
        Undo,
        Preview,
        Print,        
        Excel,
        Favorite
    }
    /// <summary>
    /// 툴바버튼 description
    /// </summary>
    public class ToolbarAuthEnumDesc
    {
        public static IDictionary<ToolbarAuthEnum, string> toolbarAuthEnumDesc = new Dictionary<ToolbarAuthEnum, string>()
            {
                {ToolbarAuthEnum.Search, "조회" },
                {ToolbarAuthEnum.Input, "입력" },
                {ToolbarAuthEnum.Edit, "수정" },
                {ToolbarAuthEnum.Delete, "삭제" },
                {ToolbarAuthEnum.Save, "저장" },
                {ToolbarAuthEnum.Undo, "취소" },
                {ToolbarAuthEnum.Preview, "미리보기" },
                {ToolbarAuthEnum.Print, "인쇄" },                
                {ToolbarAuthEnum.Excel, "엑셀" },
                {ToolbarAuthEnum.Favorite, "즐겨찾기" },
            };        
    }

    public class ToolbarFormAuth
    {
        /// <summary>
        /// 폼code
        /// </summary>
        public static string FormCd { get; set; }

        /// <summary>
        /// 화면에 툴바 여러개 지정시, index순번
        /// </summary>
        public static int? ToolbarIndex { get; set; }

        /// <summary>
        /// 권한이 없는경우, 버튼을 삭제할지 여부
        /// </summary>
        public static bool IsUnAuthBtnRemove { get; set; }

        public static string[] VisibleBtns { get; set; }
                    
        //public static string[] setVisibleBtns(string strBtn)
        //{
        //    string[] splitbtns = null;
        //    if (strBtn != "*")
        //    {
        //        splitbtns = strBtn.Split(new char[';'], StringSplitOptions.RemoveEmptyEntries);
        //    }
        //    return splitbtns;
        //}

        /// <summary>
        /// inner class > 폼권한관리
        /// </summary>
        public class FormAuth
        {          

            /// <summary>
            /// 버튼구분 : ToolbarAuthEnum 
            /// </summary>
            public ToolbarAuthEnum BtnGb { get; set; }
            /// <summary>
            /// 궈한여부 Y/N
            /// </summary>
            public bool isAuth { get; set; }
            /// <summary>
            /// toolbar ID
            /// </summary>            
            public string ToolBarId { get => getPagePrefix() + "Toolbar"; }
            /// <summary>
            /// 버튼 ID
            /// </summary>            
            public string BtnId { get => getPagePrefix() + BtnGb; }
            /// <summary>
            /// 버튼 Icon - 자동
            /// </summary>
            public string BtnIcon { get => getBtnIcon(); }
            /// <summary>
            /// 버튼 onlick - 자동
            /// </summary>
            public string BtnOnclick {
                //get => getPagePrefix() + BtnGb + 
                get {
                    var btnGb = BtnGb;
                    // preivew 일때는 print event로 공통화.
                    if(BtnGb == ToolbarAuthEnum.Preview)
                    {
                        btnGb = ToolbarAuthEnum.Print;
                    }
                    return getPagePrefix() + btnGb;
                    
                }

            }
            /// <summary>
            /// 버튼 text - 자동
            /// </summary>
            public string BtnText { get => ToolbarAuthEnumDesc.toolbarAuthEnumDesc[BtnGb]; }         //ToolbarAuthEnum code = (ToolbarAuthEnum)Enum.Parse(typeof(ToolbarAuthEnum), BtnGb);

           /// <summary>
           /// pageprefix를 구한다. toolbarIndex 적용
           /// </summary>
           /// <returns></returns>
           private string getPagePrefix()
            {
                string pagePrefix = FormCd;
                ToolbarIndex = ToolbarIndex ?? 0;
                if (ToolbarIndex > 0)
                {
                    pagePrefix = FormCd + "_"+ Convert.ToString(ToolbarIndex);
                }
                return pagePrefix;
            }

            /// <summary>
            /// 버튼의 icon명 가져오기
            /// </summary>
            /// <returns></returns>
            private string getBtnIcon()
            {
                string retIcon = null;

                switch (BtnGb)
                {                    
                    case ToolbarAuthEnum.Input:
                        retIcon = "plus";
                        break;                    
                    case ToolbarAuthEnum.Delete:
                        retIcon = "trash";
                        break;
                    case ToolbarAuthEnum.Preview:
                        retIcon = "photo";
                        break;
                    case ToolbarAuthEnum.Excel:
                        retIcon = "xlsxfile";
                        break;
                    case ToolbarAuthEnum.Favorite:
                        retIcon = "favorites";
                        break;
                    default:
                        retIcon = BtnGb.ToString("G").ToLower();
                        break;
                }
                return retIcon;
            }

            /// <summary>
            /// static class 접근 - toolbarindex
            /// </summary>
            /// <returns></returns>
            public int? getToolbarIndex()
            {
                return ToolbarIndex;
            }

            /// <summary>
            /// /// static class 접근 - IsUnAuthBtnRemove
            /// </summary>
            /// <returns></returns>
            public bool getIsUnAuthBtnRemove()
            {
                return IsUnAuthBtnRemove;
            }

            /// <summary>
            /// static class 접근 visibleBtn 여부 체크 (사용자가 지정한 버튼이 있을때 해당버튼만 출력한다)
            /// </summary>
            /// <returns></returns>
            public bool isVisibleBtn()
            {
                // 기본값은 true (즉, 해당 매뉴 권한이 있으면 설정된 버튼 모두 나옴)
                bool visible = true;               

                if (VisibleBtns != null)
                {
                    visible = (Array.IndexOf(VisibleBtns, BtnGb.ToString()) > -1);                    
                }
                return visible;
            }
        }

    }
}