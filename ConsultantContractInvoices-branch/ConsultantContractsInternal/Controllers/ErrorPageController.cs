using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsultantContractsInternal.Controllers
{
    public class ErrorPageController : Controller
    {
        //
        // GET: /ErrorPage/

        public ActionResult ErrorMessage(string status, string message)
        {
            ViewBag.status = status;
            ViewBag.message = message;
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}
