using HACCP.Libs.Database;
using HACCP.Models.QualityControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.QualityControl
{
    public class BackDataDownloadService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_BackDataDownload";

        internal DataTable BackDataDownloadSelect(BackDataDownload backDataDownload)
        {
            string[] pParam = new string[4];
            pParam[0] = "@start_date:" + backDataDownload.start_date;
            pParam[1] = "@end_date:" + backDataDownload.end_date;
            pParam[2] = "@test_type:" + backDataDownload.test_type;
            //pParam[3] = "@test_status:" + backDataDownload.test_status;
            pParam[3] = "@file_type:" + backDataDownload.file_type;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable BackDataDownloadFile(string doc_file_id)
        {
            string sGubun = "S2";

            string[] pParam = new string[1];
            pParam[0] = "@doc_file_id:" + doc_file_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }
    }
}