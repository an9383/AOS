using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HACCP.Services.SysOp
{
    public class NewsWriteR2Service
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectNews(string query_gubun, string news_gb, string sdate, string edate)
        {
            string sSpName = "SP_NEWS_WRITE";

            string gubun = "S";

            string[] param = new string[4];

            param[0] = "@query_gubun:" + query_gubun;
            param[1] = "@news_gb:" + news_gb;
            param[2] = "@edate:" + edate;
            param[3] = "@sdate:" + sdate;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectNoticeTarget(string news_cd, string news_gb)
        {

            string sSpName = "SP_NEWS_WRITE";

            string gubun = "P";

            if (!news_cd.Equals(""))
            {
                gubun += ("V" + news_gb);
            }
            else
            {
                gubun += news_gb;
            }

            string[] param = new string[2];

            param[0] = "@news_cd:" + news_cd;
            param[1] = "@news_gb:" + news_gb;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string GetNewsCd()
        {
            string sSpName = "SP_NEWS_WRITE";

            string gubun = "M";

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun);

            return res;
        }

        internal string NewsCRUD(News news, string targetType, string pGubun)
        {
            string sSpName = "SP_NEWS_WRITE";
            string gubun = pGubun;

            string[] param = new string[11];

            if (pGubun.Equals("D"))
            {
                param = new string[1];

                param[0] = "@news_cd:" + news.news_cd;
            }
            else
            {
                param = GetParam(news, targetType);
            }

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }

        private string[] GetParam(News news, string targetType)
        {
            string[] param = new string[11];

            param[0] = "@news_cd:" + news.news_cd;
            param[1] = "@news_gb:" + news.news_gb;
            param[2] = "@news_seq_id:" + news.news_seq_id;
            param[3] = "@"+ targetType + ":" + news.target_cd;
            param[4] = "@news_reader:" + news.target_cd;
            param[5] = "@news_title:" + news.news_title;
            param[6] = "@news_content:" + news.news_content;
            param[7] = "@news_start_date:" + news.news_start_date;
            param[8] = "@news_end_date:" + news.news_end_date;
            param[9] = "@news_read_cnt:" + 0;
            param[10] = "@news_writer:" + HttpContext.Current.Session["loginCD"].ToString();

            return param;
        }
    }
}
