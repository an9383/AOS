using HACCP.Libs.Database;
using HACCP.Models.SysSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysSet
{
    public class ComplexParameterService
    {
        private const string V = "";
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="pParameter_code"></param>
        /// <param name="pParameter_remark"></param>
        /// <returns></returns>
        internal DataTable Select(ComplexParameter.SrchParam param)
        {
            string sSpName = "SP_ComplexParameter";
            string sGubun = "S";
            string[] pParam = new string[1];
            pParam[0] = "@complex_cd:" + param.sComplex_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        public string Save(ComplexParameter complexParameter)
        {
            string sSpName = "SP_ComplexParameter";
            string sGubun = complexParameter.Gubun;
            string[] pParam = GetParam(complexParameter);
            string sRtn = "";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);
            sRtn = "OK";

            return sRtn;
        }

        private string[] GetParam(ComplexParameter complexParameter)
        {
            string[] param = new string[14];

            //기본정보
            param[0] = "@complex_cd:" + complexParameter.complex_cd;
            param[1] = "@complex_id:" + complexParameter.complex_id;
            param[2] = "@code1:" + complexParameter.code1;
            param[3] = "@code2:" + complexParameter.code2;
            param[4] = "@code3:" + complexParameter.code3;
            param[5] = "@code4:" + complexParameter.code4;
            param[6] = "@code5:" + complexParameter.code5;
            param[7] = "@code6:" + complexParameter.code6;
            param[8] = "@code7:" + complexParameter.code7;
            param[9] = "@code8:" + complexParameter.code8;
            param[10] = "@code9:" + complexParameter.code9;
            param[11] = "@code10:" + complexParameter.code10;
            param[12] = "@code11:" + complexParameter.code11;
            param[13] = "@code12:" + complexParameter.code12;

            return param;
        }

        internal bool Delete(string complex_cd, string complex_id)
        {
            string sSpName = "SP_ComplexParameter";
            string sGubun = "D";
            string[] pParam = new string[2];
            pParam[0] = "@complex_cd:" + complex_cd;
            pParam[1] = "@complex_id:" + complex_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return (message != null ? true : false);
        }
    }
}
