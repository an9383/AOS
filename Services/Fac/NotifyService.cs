using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.XtraSpreadsheet.Model;
using HACCP.Libs.Database;
using HACCP.Models;

namespace HACCP.Services.Fac
{
    public class NotifyService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        
        public List<Notify> Select(Notify pNotify)
        {
            string sSpName = "SP_Notify";
            string sGubun = pNotify.pGubun;
            string[] pParam = GetParam(pNotify);

            //string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            //return (message.Equals("1") ? true : false);

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);
            List<Notify> notifys = new List<Notify>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                Notify notify = new Notify(row);
                notifys.Add(notify);
            }

            return notifys;
        }

        private string[] GetParam(Notify pNotify)
        {
            string[] param = new string[5];

            //기본정보
            param[0] = "@period:" + pNotify.period;
            param[1] = "@sdate:" + pNotify.sdate;
            param[2] = "@edate:" + pNotify.edate;
            param[3] = "@status:" + pNotify.status;
            param[4] = "@equip:" + pNotify.equip;

            return param;
        }

    }
}
