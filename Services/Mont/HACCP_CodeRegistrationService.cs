using HACCP.Libs.Database;
using HACCP.Models.Mont;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Mont
{
    public class HACCP_CodeRegistrationService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable getHaccpDoc(string use_yn)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getHaccpDoc";
            string[] pParam = new string[1];

            pParam[0] = "@use_yn:" + "Y";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getGridItem(string HaccpCode)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getGridItem";
            string[] pParam = new string[1];
            
            pParam[0] = "@HaccpCode:" + HaccpCode;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getHaccpDocPopup()
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getHaccpDocPopup";
            string[] pParam = new string[0];

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getMaxChgSerNo(HACCP_CodeRegistration hACCP_CodeRegistration)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getMaxChgSerNo";
            string[] pParam = new string[1];

            pParam[0] = "@HaccpCode:" + hACCP_CodeRegistration.HaccpCode;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }
        
        internal DataTable getHaccpItemByRevisionPopup(HACCP_CodeRegistration hACCP_CodeRegistration)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getHaccpItemByRevisionPopup";
            string[] pParam = new string[2];
            
            pParam[0] = "@HaccpCode:" + hACCP_CodeRegistration.HaccpCode;
            pParam[1] = "@ChgSerNo:" + hACCP_CodeRegistration.ChgSerNo;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getChgSerNo(HACCP_CodeRegistration hACCP_CodeRegistration)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getChgSerNo";
            string[] pParam = new string[1];

            pParam[0] = "@HaccpCode:" + hACCP_CodeRegistration.HaccpCode;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);            

            return dt;
        }

        internal DataTable getProcess_cd()
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getProcess_cd";
            string[] pParam = new string[0];

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getProcess_exam_cd(HACCP_CodeRegistration hACCP_CodeRegistration)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getProcess_exam_cd";
            string[] pParam = new string[1];

            pParam[0] = "@ParentCode:" + hACCP_CodeRegistration.ParentCode;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getWorkroom_cd()
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string gubun = "getWorkroom_cd";
            string[] pParam = new string[0];

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }        

        internal string setFinalConfirm(HACCP_CodeRegistration hACCP_CodeRegistration)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string sGubun = "setFinalConfirm";
            string message = "";

            string[] pParam = new string[2];

            pParam[0] = "@HaccpCode:" + hACCP_CodeRegistration.HaccpCode;
            pParam[1] = "@ChgSerNo:" + hACCP_CodeRegistration.ChgSerNo;

            message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal string HaccpTRX(List<HACCP_CodeRegistration> hACCP_CodeRegistration)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string sGubun = "";
            string message = "";

            for(int i = 0; i < hACCP_CodeRegistration.Count; i++)
            {
                if (hACCP_CodeRegistration[i].gubun.Equals("I"))
                {
                    sGubun = "setHaccpDoc";
                    string[] pParam = new string[6];
                    
                    pParam[0] = "@HaccpCode:" + hACCP_CodeRegistration[i].HaccpCode;                    
                    pParam[1] = "@ChgSerNo:" + hACCP_CodeRegistration[i].ChgSerNo;
                    pParam[2] = "@EmpCode:" + hACCP_CodeRegistration[i].EmpCode;
                    pParam[3] = "@BDate:" + hACCP_CodeRegistration[i].BDate;
                    pParam[4] = "@EDate:" + hACCP_CodeRegistration[i].EDate;
                    if(hACCP_CodeRegistration[i].CCP_yn == "Y" || hACCP_CodeRegistration[i].CCP_yn.Equals("true"))
                    {
                        pParam[5] = "@CCP_yn:" + "Y";
                    }
                    else
                    {
                        pParam[5] = "@CCP_yn:" + "N";
                    }

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

                }
                else if (hACCP_CodeRegistration[i].gubun.Equals("I_D"))
                {
                    sGubun = "setHaccpItem";
                    string[] pParam = new string[7];
                    
                    pParam[0] = "@HaccpCode:" + hACCP_CodeRegistration[i].HaccpCode;
                    pParam[1] = "@ChgSerNo:" + hACCP_CodeRegistration[i].ChgSerNo;
                    pParam[2] = "@CodeCode:" + hACCP_CodeRegistration[i].CodeCode;
                    pParam[3] = "@CodeName:" + hACCP_CodeRegistration[i].CodeName;
                    if(hACCP_CodeRegistration[i].CodeUse == "Y" || hACCP_CodeRegistration[i].CodeUse.Equals("true"))
                    {
                        pParam[4] = "@CodeUse:" + "Y";
                    }
                    else
                    {
                        pParam[4] = "@CodeUse:" + "N";
                    }
                    pParam[5] = "@ParentCode:" + hACCP_CodeRegistration[i].ParentCode;
                    pParam[6] = "@CodeDescr:" + hACCP_CodeRegistration[i].CodeDescr;


                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

                }
                else if (hACCP_CodeRegistration[i].gubun.Equals("U_D"))
                {
                    sGubun = "updateHaccpItem";
                    string[] pParam = new string[7];

                    pParam[0] = "@HaccpCode:" + hACCP_CodeRegistration[i].HaccpCode;
                    pParam[1] = "@ChgSerNo:" + hACCP_CodeRegistration[i].ChgSerNo;
                    pParam[2] = "@CodeCode:" + hACCP_CodeRegistration[i].CodeCode;
                    pParam[3] = "@CodeName:" + hACCP_CodeRegistration[i].CodeName;
                    if (hACCP_CodeRegistration[i].CodeUse == "Y" || hACCP_CodeRegistration[i].CodeUse.Equals("true"))
                    {
                        pParam[4] = "@CodeUse:" + "Y";
                    }
                    else
                    {
                        pParam[4] = "@CodeUse:" + "N";
                    }
                    pParam[5] = "@ParentCode:" + hACCP_CodeRegistration[i].ParentCode;
                    pParam[6] = "@CodeDescr:" + hACCP_CodeRegistration[i].CodeDescr;


                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

                }
                else if (hACCP_CodeRegistration[i].gubun.Equals("D_D"))
                {
                    sGubun = "deleteHaccpItem";
                    string[] pParam = new string[3];

                    pParam[0] = "@HaccpCode:" + hACCP_CodeRegistration[i].HaccpCode;
                    pParam[1] = "@ChgSerNo:" + hACCP_CodeRegistration[i].ChgSerNo;
                    pParam[2] = "@CodeCode:" + hACCP_CodeRegistration[i].CodeCode;


                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

                }
            }

            return message;

        }



    }
}