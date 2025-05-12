using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.SysCom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysCom
{
    public class PlantService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable selectPlantData(string use_yn)
        {
            string sSpName = "SP_Plant";

            string gubun = "S";

            string[] param = new string[1];

            param[0] = "@s_use_yn:" + use_yn;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getEmpData()
        {
            string strsql = "SELECT emp_cd, emp_nm, dept_cd, dept_nm";
            strsql += " FROM V_EMPLOYEE";
            strsql += " WHERE (emp_cd  LIKE '%%' OR emp_nm  LIKE '%%')";
            strsql += " AND emp_cd  LIKE '%%' AND emp_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal string CRUD(Plant plant, string pGubun)
        {
            string sSpName = "SP_Plant";

            string gubun = pGubun;
            string res = "";

            if (pGubun.Equals("D"))
            {
                string[] param = new string[2];

                param[0] = "@plant_cd:" + plant.plant_cd;
                param[1] = "@at_emp_cd:" + Public_Function.User_cd;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }
            else
            {
                string[] param = getParam(plant);

                if (pGubun == "I")
                {
                    string message = _bllSpExecute.SpExecuteString(sSpName, "CPC", param);
                    if (message == "Y")
                        res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
                }
                else
                    res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }

            return res;
        }

        internal DataTable selectCompanyIcon(string plant_cd)
        {
            string sSpName = "SP_Plant";

            string gubun = "SI";

            string[] param = new string[1];

            param[0] = "@plant_cd:" + plant_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable iconDt = new DataTable("iconDt");

            DataColumn fullImageColumn = new DataColumn("company_full_image", typeof(string));
            DataColumn smallImageColumn = new DataColumn("company_small_image", typeof(string));
            iconDt.Columns.Add(fullImageColumn);
            iconDt.Columns.Add(smallImageColumn);

            DataRow row;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = iconDt.NewRow();

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i].ItemArray[j] != System.DBNull.Value)
                    {
                        string str = "data:Image/png;base64," + Convert.ToBase64String((byte[])(dt.Rows[i].ItemArray[j]));

                        row[j] = str;
                    }
                    //else
                    //{
                    //    string str = "data:Image/png;base64,/Content/image/defaultSign.png";

                    //    row[j] = str;
                    //}
                }

                iconDt.Rows.Add(row);
            }

            return iconDt;
        }

        internal DataTable MainPlantCountCheck()
        {
            string sSpName = "SP_Plant";

            string gubun = "MCK";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal void updateComapnyImage(byte[] myBytes, string plant_cd, string imageName)
        {
            string sSpName = "SP_Plant";

            string sGubun = "";

            if (imageName.Equals("company_full_image"))
            {
                sGubun = "UI1";
            }else if (imageName.Equals("company_small_image"))
            {
                sGubun = "UI2";
            }

            string[] pParam = new string[1];
            pParam[0] = "@plant_cd:" + plant_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, "@"+ imageName , myBytes, pParam);

            return;
        }

        private string[] getParam(Plant plant)
        {
            string[] param = new string[19];

            param[0] = "@plant_cd:" + plant.plant_cd;
            param[1] = "@plant_nm:" + plant.plant_nm;
            param[2] = "@plant_busin_no:" + plant.plant_busin_no;
            param[3] = "@plant_corpo_no:" + plant.plant_corpo_no;
            param[4] = "@plant_emp_cd:" + plant.plant_emp_cd;
            param[5] = "@plant_catgo:" + plant.plant_catgo;
            param[6] = "@plant_busin_nm:" + plant.plant_busin_nm;
            param[7] = "@plant_post_cd:" + plant.plant_post_cd;
            param[8] = "@plant_adress1:" + plant.plant_adress1;
            param[9] = "@plant_adress2:" + plant.plant_adress2;
            param[10] = "@plant_telephone:" + plant.plant_telephone;
            param[11] = "@plant_fax:" + plant.plant_fax;
            param[12] = "@plant_office:" + plant.plant_office;
            param[13] = "@plant_open_date:" + plant.plant_open_date;
            param[14] = "@plant_close_date:" + plant.plant_close_date;

            if(plant.main_yn == null)
            {
                param[15] = "@main_yn:N";
            }
            else
            {
                param[15] = "@main_yn:" + plant.main_yn;
            }

            param[16] = "@use_yn:" + plant.use_yn;
            param[17] = "@insert_emp:" + Public_Function.User_cd;
            param[18] = "@at_emp_cd:" + Public_Function.User_cd;

            return param;
        }
    }
}
