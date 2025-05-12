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
        #region ���� ���� ����
        private static readonly ILog log = LogManager.GetLogger(typeof(CommController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        #endregion

        #region ���뺯�� ����
               
        #endregion

        #region ���� toolbar partial���� Action
        /// <summary>
        /// ���ټ��� ����
        /// </summary>
        /// <param name="toolbarIndex">ȭ�鿡 ���ٰ� ������ ������ index�� ����
        ///   (1) ȭ�鿡 ���� �ϳ��ΰ�� -> 0�� ������
        ///   (2) ȭ�鿡 ���� �ΰ��̻�   -> 1�̻��� ���� ����, index�� _(����)�� ��������� Id�� onclick ��Ǹ� index�� ����..</param>
        /// <param name="isUnAuthBtnRemove">������ ���°�쿡�� ��ư�� �������� ����
        ///   (1)true :  ��ư ����
        ///   (2)false : ��ư ��Ȱ��  
        /// </param>
        /// <param name="visibleBtns">����� ��ư ����Ʈ ����(�����ݷ�(;)���� ��ư ����, ��ü��� �̸� *�� ǥ����)  </param>
        /// <returns></returns>
        [ChildActionOnly]       
        public PartialViewResult SetToolbar(int toolbarIndex, bool isUnAuthBtnRemove, string visibleBtns)
        {
            // �Ŵ����� ����
            MenuAuthService menuAuthService = new MenuAuthService();
            
            #region ���� ȣ��
            DataTable menuAuthList = (DataTable)HttpContext.Session["loginMenuAuthList"];
            List<ToolbarFormAuth.FormAuth> btnAuths = menuAuthService.getToolbarAuthList(Request, menuAuthList, toolbarIndex, isUnAuthBtnRemove, visibleBtns);
            #endregion
                       
            return PartialView("Uc/_SetToolbar", btnAuths);
        }
        #endregion

    }
}