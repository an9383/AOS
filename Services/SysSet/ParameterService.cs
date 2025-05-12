using HACCP.Libs.Database;
using System.Data;
using HACCP.Models.SysSet;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HACCP.Services.SysSet
{
    public class ParameterService
    {
        private const string V = "";
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="pParameter_code"></param>
        /// <param name="pParameter_remark"></param>
        /// <returns></returns>
        internal DataTable Select(Parameter.SrchParam param)
        {
            string sSpName = "SP_Parameter";
            string sGubun = "S";
            string[] pParam = new string[2];
            pParam[0] = "@s_parameter_code:" + param.s_parameter_code;
            pParam[1] = "@s_parameter_remark:" + param.s_parameter_remark;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);
            return dt;
        }

        /// <summary>
        /// 조회 Form
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        internal DataTable SelectForm(FormCollection form)
        {
            string sSpName = "SP_Parameter";
            string sGubun = "S";
            string[] pParam = new string[2];
            pParam[0] = "@s_parameter_code:" + form["pParameter_code"];
            pParam[1] = "@s_parameter_remark:" + form["pParameter_remark"];

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);
            return dt;

        }

        /// <summary>
        /// 저장
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string Save(Parameter parameter)
        {
            string sSpName = "SP_Parameter";
            string sGubun = parameter.Gubun;
            string[] pParam = GetParam(parameter);
            string sRtn = "";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);
            sRtn = "OK";

            return sRtn;
        }

        /// <summary>
        /// 삭제
        /// 
        /// </summary>
        /// <param name="pParameter_code"></param>
        /// <returns></returns>
        internal bool Delete(string pParameter_code)
        {
            string sSpName = "SP_Parameter";
            string sGubun = "D";
            string[] pParam = new string[1];
            pParam[0] = "@parameter_code:" + pParameter_code;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return (message != null ? true : false);
        }

        private string[] GetParam(Parameter parameter)
        {
            string[] param = new string[3];

            //기본정보
            param[0] = "@parameter_code:" + parameter.parameter_code;
            param[1] = "@parameter_value:" + parameter.parameter_value;
            param[2] = "@parameter_remark:" + parameter.parameter_remark;

            return param;
        }
    }
}
