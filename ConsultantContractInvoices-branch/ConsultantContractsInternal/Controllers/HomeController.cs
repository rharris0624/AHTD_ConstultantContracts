using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Security;
using System.Web.Mvc;
using AHTD.Web.Mvc;
using Elmah;
using ConsultantContractsInternal.Parts;

namespace ConsultantContractsInternal.Controllers
{
	public class HomeController : SecuredController
	{
		private string sLog;
		private string sEvent = "Index";
		private string sSource = "HomeController";
		//
		// GET: /Home/
		[Compress]
		public ActionResult Index()
		{
			var userName = User.Identity.Name;
			return View("AdminHome");
		}
		[HttpPost]
		public void LogJavaScriptError(string message)
		{
			ErrorSignal.FromCurrentContext().Raise(new JavaScriptErrorException(message));
		}
	}
}
