using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.RowMatLoc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HACCP.Services.RowMatLoc
{
    public class StackManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_StackManage";

        internal DataTable Select(StackManage sModel)
        {
            string sGubun = "S";
            string[] param = new string[3];

            param[0] = "@s_item_cd:" + "";  //원자재코드
            param[1] = "@s_test_no:" + "";    //시험번호
            param[2] = "@s_gubun:" + sModel.item_gb;  // 원료 3, 자재 2

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable StackManage_Loc_Select(StackManage sModel, string receipt_no, string receipt_id)
        {
            string sGubun = "S2";
            string[] param = new string[2];

            param[0] = "@receipt_no:" + receipt_no;  //입고번호
            param[1] = "@receipt_id:" + receipt_id;  //입고순번

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        /// <summary>
        //  창고 드랍박스
        /// </summary>
        internal DataTable SelectWorkroom(StackManage sModel)
        {
            string sGubun = "W";
            // 원료
            if (sModel.item_gb == "3")
            {
                sGubun = "W_M";
            } else { //자재

                sGubun = "W_P";
            }
            
            
            string[] param = new string[1];
            param[0] = "@workroom_cd:" + sModel.workroom_cd;  // 구역코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable StackManage_Zone_Select(string workroom_cd)
        {
            string sGubun = "Z";
            string[] param = new string[1];

            param[0] = "@workroom_cd:" + workroom_cd;  //창고코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable StackManage_Cell_Select(string zone_cd)
        {
            string sGubun = "C";
            string[] param = new string[1];

            param[0] = "@zone_cd:" + zone_cd;  // 구역코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal string StackManageCRUD(StackManage cModel, string[] selectedKeys)
        {
            string sGubun = "U";
            string message = "";

            for (int i=0; i< selectedKeys.Length; i++)
            {
                string[] param = new string[7];
                param[0] = "@workroom_cd:" + cModel.workroom_cd; //창고코드
                param[1] = "@zone_cd:" + cModel.zone_cd;         //구역코드
                param[2] = "@cell_cd:" + cModel.cell_cd;         //셀코드   
                param[3] = "@pallet_cd:" + cModel.pallet_cd;    //팔레트코드
                param[4] = "@receipt_pack_barcode:" + selectedKeys[i];     //바코드코드
                param[5] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];     //수정자
                param[6] = "@log_user_id :" + HttpContext.Current.Session["loginCD"];

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }

            return message;
        }
    }
}

