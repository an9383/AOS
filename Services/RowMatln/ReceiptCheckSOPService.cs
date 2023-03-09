using HACCP.Libs.Database;
using HACCP.Libs;
using HACCP.Models.RowMatln;
using HACCP.Models.ProductionMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HACCP.Services.RowMatln
{
    public class ReceiptCheckSOPService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable Select(ReceiptCheckSOP rModel)
        {
            string sSpName = "SP_ReceiptCheckSOP";
            string sGubun = "S";
            string[] pParam = new string[1];
            pParam[0] = "@item_gb:" + rModel.item_gb;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;

        }
        
        internal string ReceiptCheckSOPCRUD(List<ReceiptCheckSOP> rModel)
        {
            string sSpName = "SP_ReceiptCheckSOP";
            string sGubun = "";
            string message = "";

            for(int i = 0; i < rModel.Count; i++)
            {
                if (rModel[i].gubun.Equals("D"))
                {
                    sGubun = "D";
                    string[] pParam = new string[1];

                    pParam[0] = "@receipt_check_id: " + rModel[i].receipt_check_id;

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if(rModel[i].gubun.Equals("I"))
                {
                    sGubun = "I";
                    string[] pParam = new string[4];
                    
                    pParam[0] = "@item_gb:" + rModel[i].item_gb;
                    pParam[1] = "@receipt_check_seq:" + rModel[i].receipt_check_seq;
                    pParam[2] = "@receipt_check_contents:" + rModel[i].receipt_check_contents;
                    pParam[3] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (rModel[i].gubun.Equals("U"))
                {
                    sGubun = "U";
                    string[] pParam = new string[4];
                    
                    pParam[0] = "@receipt_check_seq:" + rModel[i].receipt_check_seq;
                    pParam[1] = "@receipt_check_contents:" + rModel[i].receipt_check_contents;
                    pParam[2] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
                    pParam[3] = "@receipt_check_id:" + rModel[i].receipt_check_id;

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
            }
            return message;
        }

    }
}
