using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using ConsultantContractsInternal.Controllers;
using Elmah;

namespace ConsultantContractsInternal
{
	public static class Globals
	{
		public static string APPNAME = "CONS_CNTRT";
	}

	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			HtmlHelper.ClientValidationEnabled = true;
			HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
		}

		protected void Application_EndRequest()
		{
			if (!ConfigurationManager.AppSettings["RuntimeEnvironment"].Equals("local"))
			{
				if (Context.Response.StatusCode == 404)
				{
					Response.Clear();

					var rd = new RouteData();
					rd.Values["controller"] = "Shared";
					rd.Values["action"] = "PageNotFound";

					IController c = new SharedController();
					c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
				}
				else if (Context.Response.StatusCode == 500)
				{
					Response.Clear();

					var rd = new RouteData();
					rd.Values["controller"] = "Shared";
					rd.Values["action"] = "Error";

					IController c = new SharedController();
					c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
				}
			}
		}
		protected void Application_Error(object sender, EventArgs e)
		{
			Exception ex = Server.GetLastError();
			var rawData = Request.RawUrl;
			ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));


		}
	}
}