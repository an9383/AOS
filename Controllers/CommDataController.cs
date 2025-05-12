using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using HACCP.Models;
using HACCP.Models.Common;
using HACCP.Services.Comm;
using HACCP.Services.ProductionManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.SessionState;

namespace HACCP.Controllers
{
    [System.Web.Http.Route("api/CommData/{action}", Name = "Comm")]
    public class CommDataController : ApiController
    {
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetMenuData(DataSourceLoadOptions loadOptions)
        {
            var cd = HttpContext.Current.Session["loginCD"];
            MenuService menuService = new MenuService();
            var list = menuService.GetMenu(HttpContext.Current.Session["loginCD"].ToString());

            return Request.CreateResponse(DataSourceLoader.Load(list, loadOptions));
            //List<Menu> temp = menuService.GetMenu((string)HttpContext.Current.Session["loginCD"]);

            //return Request.CreateResponse(temp);
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetNews(DataSourceLoadOptions loadOptions)
        {
            MenuService menuService = new MenuService();

            return Request.CreateResponse(DataSourceLoader.Load(menuService.GetNews(HttpContext.Current.Session["loginCD"].ToString()), loadOptions));
        }


        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCommon(DataSourceLoadOptions loadOptions, string pGubun, string pDiv, string pStrWhere, string pTableName)
        {
            SelectBoxService selectBoxService = new SelectBoxService();

            DataTable dt;

            dt = selectBoxService.GetSelectBox(pGubun, pDiv, pStrWhere, pTableName);

            List<SelectBoxGubun> list = new List<SelectBoxGubun>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[i];

                SelectBoxGubun temp;

                temp = new SelectBoxGubun(row, false);

                list.Add(temp);
            }

            return Request.CreateResponse(DataSourceLoader.Load(list, loadOptions));
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCommon(DataSourceLoadOptions loadOptions, string gubun)
        {
            OrderGuideService orderGuideService = new OrderGuideService();

            DataTable dt;

            dt = orderGuideService.GetSelectbox(gubun);

            List<SelectBoxGubun> list = new List<SelectBoxGubun>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[i];

                SelectBoxGubun temp;

                temp = new SelectBoxGubun(row, false);

                list.Add(temp);
            }

            return Request.CreateResponse(DataSourceLoader.Load(list, loadOptions));
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCommon2(DataSourceLoadOptions loadOptions, string gubun)
        {
            OrderGuideService orderGuideService = new OrderGuideService();

            DataTable dt;

            dt = orderGuideService.GetSelectbox(gubun);

            List<SelectBoxGubun> list = new List<SelectBoxGubun>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[i];

                SelectBoxGubun temp;

                temp = new SelectBoxGubun(row, false);

                list.Add(temp);
            }

            return Request.CreateResponse(DataSourceLoader.Load(list, loadOptions));
        }
    }
}
