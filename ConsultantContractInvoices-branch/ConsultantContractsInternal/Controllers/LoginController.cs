using ConsultantContractsInternal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsultantContractsInternal.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Index(LoginViewModel model)
        {
            return View();
        }
    }
}
