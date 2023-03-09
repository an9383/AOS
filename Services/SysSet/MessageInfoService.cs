using HACCP.Libs.Database;
using HACCP.Models.SysSet;
using System.Collections.Generic;
using System.Data;

namespace HACCP.Services.SysSet
{
    public class MessageInfoService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable Select(MessageInfo.SrchParam param)
        {
            string sSpName = "SP_MessageInfo";
            string sGubun = "S";
            string[] pParam = new string[1];
            pParam[0] = "@S_MSG_NAME:" + param.sMsg_name;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        public string Save(MessageInfo messageInfo)
        {
            string sSpName = "SP_MessageInfo";
            string sGubun = messageInfo.Gubun;
            string[] pParam = GetParam(messageInfo);
            string sRtn = "";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);
            sRtn = "OK";

            return sRtn;
        }

        internal bool Delete(string sMsg_code)
        {
            string sSpName = "SP_MessageInfo";
            string sGubun = "D";
            string[] pParam = new string[1];
            pParam[0] = "@MSG_CODE:" + sMsg_code;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return (message != null ? true : false);
        }

        private string[] GetParam(MessageInfo messageInfo)
        {
            string[] param = new string[4];

            //기본정보
            param[0] = "@MSG_CODE:" + messageInfo.MSG_CODE;
            param[1] = "@MSG_TITLE:" + messageInfo.MSG_TITLE;
            param[2] = "@MSG_NAME:" + messageInfo.MSG_NAME;
            param[3] = "@MSG_BIGO:" + messageInfo.MSG_BIGO;

            return param;
        }

    }
}
