using HACCP.Libs.Database;
using HACCP.Models.SysOp;
using System;
using System.Collections.Generic;
using System.Data;

namespace HACCP.Services.SysOp
{
    public class SignSet_InputService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectSignitrueInfo(string use_yn)
        {
            string sSpName = "SP_SignSet_Input";
            string sGubun = "S";
            string[] pParam = new string[1];
            pParam[0] = "@use_yn_S:" + use_yn;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal string CRDU(ElectronicSigniture electronicSigniture, string gubun)
        {
            string sSpName = "SP_SignSet_Input";
            string sGubun = gubun;

            string[] pParam = new string[1];

            if (gubun.Equals("D"))
            {
                pParam[0] = "@sign_set_cd:" + electronicSigniture.sign_set_cd;
            }
            else
            {
                pParam = SetParam(electronicSigniture);
            }

            string res = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return res;
        }

        private string[] SetParam(ElectronicSigniture electronicSigniture)
        {
            string[] tmp = new string[14];

            tmp[0] = "@sign_set_cd:" + electronicSigniture.sign_set_cd;
            tmp[1] = "@sign_set_nm:" + electronicSigniture.sign_set_nm;
            tmp[2] = "@sign_set_nm_dis:" + electronicSigniture.sign_set_nm_dis;
            tmp[3] = "@sign_set_content:" + electronicSigniture.sign_set_content;
            tmp[4] = "@test_module_yn:" + electronicSigniture.test_module_yn;
            tmp[5] = "@doc_module_yn:" + electronicSigniture.doc_module_yn;
            tmp[6] = "@use_yn:" + electronicSigniture.use_yn;
            tmp[7] = "@link_point:" + electronicSigniture.link_point;
            tmp[8] = "@linked_module:" + electronicSigniture.linked_module;
            tmp[9] = "@linked_table:" + electronicSigniture.linked_table;
            tmp[10] = "@linked_remark:" + electronicSigniture.linked_remark;
            tmp[11] = "@linked_value1:" + electronicSigniture.linked_value1;
            tmp[12] = "@linked_value2:" + electronicSigniture.linked_value2;
            tmp[13] = "@linked_value3:" + electronicSigniture.linked_value3;

            return tmp;
        }
    }
}
