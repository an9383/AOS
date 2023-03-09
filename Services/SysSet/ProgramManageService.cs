using HACCP.Libs.Database;
using HACCP.Models.SysSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysSet
{
    public class ProgramManageService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable Select(string form_cd)
        {
            string spName = "SP_FormManage";
            string spGubun = "S";
            string[] param = new string[1];
            
            param[0] = "@form_cd:" + form_cd;

            DataTable dt = _bllSpExecute.SpExecuteDataSet(spName, spGubun, param).Tables[0];

            return dt;
        }

        internal string programManageCRUD(FormManage pModel, string pGubun)
        {
            string sSpName = "SP_FormManage";
            string sGubun = pGubun;

            string message = "";

            if (pGubun.Equals("D"))
            {
                string[] pParam = new string[1];
                pParam[0] = "@form_cd:" + pModel.form_cd;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            else
            {
                string[] pParam = GetParam(pModel);
                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }

            return message;
        }
        private string[] GetParam(FormManage pModel)
        {
            string[] param = new string[12];

            //기본정보
            param[0] = "@form_cd:" + pModel.form_cd;
            param[1] = "@form_nm:" + pModel.form_nm;
            param[2] = "@form_url:" + pModel.form_url;
            param[3] = "@form_security:" + pModel.form_security;
            param[4] = "@form_doc:" + pModel.form_doc;
            param[5] = "@form_remark:" + pModel.form_remark;
            param[6] = "@form_main_ck:" + pModel.form_main_ck;
            param[7] = "@form_interface:" + pModel.form_interface;
            param[8] = "@form_seq_yn:" + pModel.form_seq_yn;
            param[9] = "@form_seq_type:" + pModel.form_seq_type;
            param[10] = "@form_seq_prefix:" + pModel.form_seq_prefix;
            param[11] = "@form_seq_size:" + pModel.form_seq_size;
            return param;
        }

        internal DataTable ParameterSelect(string form_cd)
        {
            string spName = "SP_FormManage";
            string spGubun = "SP";
            string[] param = new string[1];

            param[0] = "@form_cd:" + form_cd;

            DataTable dt = _bllSpExecute.SpExecuteDataSet(spName, spGubun, param).Tables[0];

            return dt;
        }

        internal string ProgramParamCRUD(List<ProgramParameter> programParameters)
        {
            string spName = "SP_FormManage";
            string spGubun = "";
            string[] param = null;

            string res = "";

            int cnt = 0;

            foreach (ProgramParameter programParameter in programParameters)
            {
                switch (programParameter.spGubun)
                {
                    case "I":
                        spGubun = "I3";

                        param = GetParam(programParameter);

                        break;

                    case "U":
                        spGubun = "U3";

                        param = GetParam(programParameter);

                        break;

                    case "D":
                        spGubun = "D3";

                        param = new string[2];

                        param[0] = "@form_cd:" + programParameter.form_cd;
                        param[1] = "@parameter_cd:" + programParameter.parameter_cd;

                        break;
                }

                res = _bllSpExecute.SpExecuteString(spName, spGubun, param);

                if (!String.IsNullOrEmpty(res))
                {
                    cnt++;
                }

            }

            return cnt + "건이 수정되었습니다.";
        }

        private string[] GetParam(ProgramParameter programParameter)
        {
            if (String.IsNullOrEmpty(programParameter.apply_yn))
            {
                programParameter.apply_yn = "N";
            }
            else if (programParameter.apply_yn.Equals("Y") || programParameter.apply_yn.Equals("true"))
            {
                programParameter.apply_yn = "Y";
            }
            else
            {
                programParameter.apply_yn = "N";
            }

            string[] param = new string[9];

            param[0] = "@form_cd:" + programParameter.form_cd;
            param[1] = "@flow_cd:" + programParameter.flow_cd;
            param[2] = "@parameter_cd:" + programParameter.parameter_cd;
            param[3] = "@flow_nm:" + programParameter.flow_nm;
            param[4] = "@parameter_value:" + programParameter.parameter_value;
            param[5] = "@apply_yn:" + programParameter.apply_yn;
            param[6] = "@parameter_remark:" + programParameter.parameter_remark;
            param[7] = "@gb:" + programParameter.gubun;
            param[8] = "@param_dll:" + programParameter.param_dll;

            return param;
        }
    }
}