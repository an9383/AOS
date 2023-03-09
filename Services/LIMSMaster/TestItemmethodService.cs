using HACCP.Libs.Database;
using HACCP.Models.LIMSMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.LIMSMaster
{
    public class TestItemmethodService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_TestItemmethod";

        internal DataTable TestItemmethodSelect(TestItemmethod.SrchParam param)
        {
            string[] pParam = new string[2];
            pParam[0] = "@testitem_type:" + param.testitem_type;
            pParam[1] = "@testitem_cd:" + param.testitem_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable TestItemmethodSelectTestMethod(string testitem_cd)
        {
            string[] pParam = new string[1];
            pParam[0] = "@testitem_cd:" + testitem_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string TestItemUpdate(string testitem_cd, string calculate_nm_master, string calculate_formula_master)
        {
            string[] pParam = new string[3];
            pParam[0] = "@testitem_cd:" + testitem_cd;
            pParam[1] = "@calculate_nm:" + calculate_nm_master;
            pParam[2] = "@calculate_formula:" + calculate_formula_master;

            string res = _bllSpExecute.SpExecuteString(sSpName, "U", pParam);

            return res;
        }

        internal string TestMethodTRX(List<TestItemmethod> testMethods, string testitem_cd)
        {
            string gubun = "";
            string res = "";
            string[] pParam = null;

            foreach (TestItemmethod testMethod in testMethods)
            {
                switch (testMethod.gubun)
                {
                    case "I":
                        gubun = "I";

                        testMethod.testitem_cd = testitem_cd;
                        testMethod.testmethod_id = "";

                        pParam = GetParam(testMethod);

                        break;

                    case "U":
                        gubun = "U2";

                        pParam = GetParam(testMethod);

                        break;

                    case "D":
                        gubun = "D";

                        pParam = new string[2];

                        pParam[0] = "@testitem_cd:" + testMethod.testitem_cd;
                        pParam[1] = "@testmethod_id:" + testMethod.testmethod_id;

                        break;
                }

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);
            }

            return res;
        }

        private string[] GetParam(TestItemmethod testMethod)
        {
            string[] pParam = new string[14];

            pParam[0] = "@testitem_cd:" + testMethod.testitem_cd;
            pParam[1] = "@testmethod_id:" + testMethod.testmethod_id;
            pParam[2] = "@testmethod_seq:" + testMethod.testmethod_seq;
            pParam[3] = "@testmethod_nm:" + testMethod.testmethod_nm;
            pParam[4] = "@data_type:" + testMethod.data_type;
            pParam[5] = "@data_min_manage:" + testMethod.data_min_manage;
            pParam[6] = "@data_max_manage:" + testMethod.data_max_manage;
            pParam[7] = "@test_parameter_cd:" + testMethod.test_parameter_cd;
            pParam[8] = "@calculate_nm:" + testMethod.calculate_nm;
            pParam[9] = "@calculate_formula:" + testMethod.calculate_formula;
            pParam[10] = "@tester_cd:" + testMethod.tester_cd;
            pParam[11] = "@reagent_cd:" + testMethod.reagent_cd;
            pParam[12] = "@tester_parameter_cd:" + testMethod.tester_parameter_cd;
            pParam[13] = "@use_gb:" + testMethod.use_gb;

            return pParam;
        }

        internal DataTable TestItemmethodSelectSpecialCharacter(string operator_type)
        {
            string[] pParam = new string[1];
            pParam[0] = "@operator_type:" + operator_type;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_MathOperator", "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string TestItemmethodFomulaCheck(string calculate_formula)
        {
            calculate_formula = FunctionConversion(calculate_formula);

            string[] pParam = new string[1];
            pParam[0] = "@calculate_formula:" + calculate_formula;

            string res = _bllSpExecute.SpExecuteString(sSpName, "C", pParam);

            return res;
        }

        private string FunctionConversion(string formula)
        {
            int s_point = 0;
            int e_point = 0;

            //합계
            if (formula.IndexOf("SUM") >= 0)
            {
                while (formula.IndexOf("SUM") >= 0)
                {
                    s_point = formula.IndexOf("SUM");
                    e_point = formula.IndexOf(")", s_point);

                    formula = formula.Replace(formula.Substring(s_point, e_point - s_point + 1), "1.00");
                }
            }

            //최소
            if (formula.IndexOf("MIN") >= 0)
            {
                while (formula.IndexOf("MIN") >= 0)
                {
                    s_point = formula.IndexOf("MIN");
                    e_point = formula.IndexOf(")", s_point);

                    formula = formula.Replace(formula.Substring(s_point, e_point - s_point + 1), "1.00");
                }
            }

            //최대
            if (formula.IndexOf("MAX") >= 0)
            {
                while (formula.IndexOf("MAX") >= 0)
                {
                    s_point = formula.IndexOf("MAX");
                    e_point = formula.IndexOf(")", s_point);

                    formula = formula.Replace(formula.Substring(s_point, e_point - s_point + 1), "1.00");
                }
            }

            //평균
            if (formula.IndexOf("AVERAGE") >= 0)
            {
                while (formula.IndexOf("AVERAGE") >= 0)
                {
                    s_point = formula.IndexOf("AVERAGE");
                    e_point = formula.IndexOf(")", s_point);

                    formula = formula.Replace(formula.Substring(s_point, e_point - s_point + 1), "1.00");
                }
            }

            //분산
            if (formula.IndexOf("VARIANCE") >= 0)
            {
                while (formula.IndexOf("VARIANCE") >= 0)
                {
                    s_point = formula.IndexOf("VARIANCE");
                    e_point = formula.IndexOf(")", s_point);

                    formula = formula.Replace(formula.Substring(s_point, e_point - s_point + 1), "1.00");
                }
            }

            //표준편차
            if (formula.IndexOf("RSD") >= 0)
            {
                while (formula.IndexOf("RSD") >= 0)
                {
                    s_point = formula.IndexOf("RSD");
                    e_point = formula.IndexOf(")", s_point);

                    formula = formula.Replace(formula.Substring(s_point, e_point - s_point + 1), "1.00");
                }
            }

            return formula;
        }

        internal string TestItemmethodCopy(string testitem_cd, string copy_testitem_cd)
        {
            string[] pParam = new string[2];
            pParam[0] = "@testitem_cd:" + testitem_cd;
            pParam[1] = "@copy_testitem_cd:" + copy_testitem_cd;

            string res = _bllSpExecute.SpExecuteString(sSpName, "COPY", pParam);

            return res;
        }
    }
}