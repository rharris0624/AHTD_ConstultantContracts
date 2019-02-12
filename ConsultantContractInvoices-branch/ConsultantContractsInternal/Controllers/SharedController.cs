using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using ConsultantContractsInternal.Security;
using System.Web.Caching;
using ConsultantContractsInternal.Security.Attributes;
using ConsultantContractsInternal.Models;
using AHTD.Security.Common;

namespace ConsultantContractsInternal.Controllers
{
    public class SharedController : SecuredController
    {
        private string sLog = "";
        private string sSource = "SharedControllerClaims";
        //
        // GET: /Shared/Claims

        public ActionResult Claims()
        {
            ViewBag.ClientClaims = CurrentUser.ClientClaims.Where(c => c.ClaimType.Equals(StandardClaimTypes.Role) || c.ClaimType.Equals(StandardClaimTypes.WindowsAccountName));
            ViewBag.CurrentUserRole = CurrentUser.Role;

            return PartialView();
        }
        [AHTDAuthorizeAttribute(Roles = Roles.Devministrator)]
        public ActionResult ChangeRole(string newRole)
        {
            Session["UserRole"] = newRole;
            //CurrentUser.Role = newRole;
            return View("Index","Home");
        }
        public JsonResult GetCurrentRole(string userName)
        {
            var role = CurrentUser.Role;
            return Json(new {role = CurrentUser.Role},JsonRequestBehavior.AllowGet);
        }
        public ActionResult Error()
        {
            ViewBag.Title = "500 Error";
            return View();
        }
        public ActionResult PageNotFound()
        {
            ViewBag.Title = "404 Not found";
            return View();
        }
        public ActionResult AccessDenied()
        {
            ViewBag.Title = "401 Unauthorized";
            return View();
        }
    }
}
