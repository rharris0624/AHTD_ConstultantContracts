using ConsultantContractsInternal.Models;
using ConsultantContractsInternal.Security;
using ConsultantContractsInternal.Utilities;
using ConsultantContractsInternal.ViewModels;
using ConsultantContractsInternal.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elmah;
using ConsultantContractsInternal.Security.Attributes;

namespace ConsultantContractsInternal.Controllers
{
    //[AHTDAuthorize(Roles="Developer,Admin")]
    public class ApplicationSecurityController : Controller
    {
        //private ArDOT_UserProvEntities1 context = new ArDOT_UserProvEntities1();
        //
        // GET: /ApplicationSecurity/
        [AHTDAuthorize(Roles= Roles.Devministrator)]
        public ActionResult Index(string userId)
        {
            try
            {
                using (var context = new ArDOT_UserProvEntities1())
                {
                    //var applicationUsers = context.ApplicationSecurities.Join(context.Users,
                    //                                u => new { u.UserId },
                    //                                s => new { s.UserId }, (u, s) => u).Where(u => u.ApplicationId.Equals("CONS_CNTRT"));
                    var applicationUsers = context.Users.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).Select(s => new {s.UserId, s.LastName, s.FirstName});
                    var roleListVm = new UserRoleVm()
                    {
                        UserList = applicationUsers.Select(s => new ItemVM { ID = s.UserId, Name = s.LastName + ", " + s.FirstName }).Distinct().OrderBy(f => f.Name).ToList(),
                        ApplicationId = "CONS_CNTRT",
                        UserId = userId,
                    };
                    return View(roleListVm);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
            }
            finally
            {
            }
            return View();
        }

        public ActionResult Details(string userId, string roleId)
        {
            using (var context = new ArDOT_UserProvEntities1())
            {
                ApplicationSecurity applicationSecurity = context.ApplicationSecurities.FirstOrDefault(p => p.ApplicationId.Equals("CONS_CNTRT") &&
                                                                                                            p.UserId.Equals(userId) &&
                                                                                                            p.RoleId.Equals(roleId));
                if (roleId == null)
                {
                    return HttpNotFound();
                }
                return View(applicationSecurity);
            }
        }

        [HttpPost]
        public ActionResult IndexDetails(string id)
        {
            ReturnArgs r = new ReturnArgs();
            bool isAllowed = CheckPermissions();

            using (var context = new ArDOT_UserProvEntities1())
            {
                try
                {
                    IEnumerable<ApplicationSecurity> applicationSecurities = context.ApplicationSecurities.Where(p => p.ApplicationId.Equals("CONS_CNTRT") &&
                                                                                                                    p.UserId.Equals(id));
                    //if (applicationSecurities == null || applicationSecurities.Count() == 0)
                    //{
                    //    return HttpNotFound();
                    //}
                    if (isAllowed)
                    {
                        r.Status = 200;
                        r.ViewString = RazorViewToString.RenderViewToString(this, "IndexDetails", applicationSecurities);
                    }
                    else
                    {
                        r.Status = 300;
                        r.ViewString = RazorViewToString.RenderViewToString(this, "_Default", null);
                    }
                    return Json(r);
                }
                catch (Exception ex)
                {
                    ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
                }
            }
            return null;
        }

        private bool CheckPermissions()
        {
            return true;
        }

        [HttpGet]
        public ActionResult Create(string userId)
        {
            using (var context = new ArDOT_UserProvEntities1())
            {
                IEnumerable<Role> roles = context.Roles.Where(p => p.ApplicationId.Equals(Globals.APPNAME));
                var currentRoles = context.ApplicationSecurities.Where(p => p.ApplicationId.Equals(Globals.APPNAME)
                    && p.UserId.Equals(!string.IsNullOrEmpty(userId) ? userId : "")).Select(p => p.RoleId);
                roles = from role in roles where !currentRoles.Contains(role.RoleId) select role;
                IEnumerable<User> users = context.Users;
                UserRoleVm roleListVm = new UserRoleVm
                {
                    ApplicationId = Globals.APPNAME
                    ,
                    UserId = userId != null ? userId : ""
                    ,
                    RoleList = roles.Select(p => new ItemVM() { ID = p.RoleId, Name = p.RoleName }).ToList()
                    ,
                    UserList = context.Users.Select(p => new ItemVM() { ID = p.UserId, Name = p.LastName + ", " + p.FirstName }).ToList()
                };

                return View(roleListVm);
            }
        }

        [HttpGet]
        public ActionResult GetAvailableRoles(string userId)
        {
            using (var context = new ArDOT_UserProvEntities1())
            {
                List<string> currentRoleIds = context.ApplicationSecurities.Where(p => p.ApplicationId == Globals.APPNAME &&
                                                                p.UserId.Equals(userId)).Select(d => d.RoleId).ToList();
                List<ItemVM> roles = context.Roles.Where(a => a.ApplicationId.Equals(Globals.APPNAME) && !currentRoleIds.Contains(a.RoleId))
                                                .Select(m => new ItemVM { ID = m.RoleId, Name = m.RoleName }).ToList();
                var availableRoles = new SelectList(roles, "ID", "Name");
                return Json(availableRoles, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserRoleVm roleList)
        {
            ApplicationSecurity appSecurity = null;
            try
            {
                if (ModelState.IsValid)
                {
                    appSecurity = new ApplicationSecurity
                    {
                        ApplicationId = Globals.APPNAME
                        ,
                        UserId = roleList.UserId
                        ,
                        RoleId = roleList.RoleId
                    };
                    using (var context = new ArDOT_UserProvEntities1())
                    {
                        context.ApplicationSecurities.Add(appSecurity);
                        context.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
            }
            
            return View(appSecurity);
        }

        public ActionResult Delete(string applicationId, string userId, string roleId)
        {
            using (var context = new ArDOT_UserProvEntities1())
            {
                ApplicationSecurity appSecurity = context.ApplicationSecurities.FirstOrDefault(p => p.ApplicationId.Equals(applicationId) &&
                                                                                                    p.UserId.Equals(userId) &&
                                                                                                    p.RoleId.Equals(roleId));
                if (appSecurity == null)
                {
                    return HttpNotFound();
                }
                return View(appSecurity);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string applicationId, string roleId, string userId)
        {
            using (var context = new ArDOT_UserProvEntities1())
            {
                try
                {
                    IEnumerable<LegacySecurity> legacySecurites = context.LegacySecurities.Where(p => p.UserId.Equals(userId) && p.ApplicationId.Equals(Globals.APPNAME) && p.RoleId.Equals(roleId)).ToList();
                    context.LegacySecurities.RemoveRange(legacySecurites);
                    ApplicationSecurity appSecurity = context.ApplicationSecurities.FirstOrDefault(p => p.ApplicationId.Equals(applicationId) &&
                                                                                                        p.UserId.Equals(userId) &&
                                                                                                        p.RoleId.Equals(roleId));
                    context.ApplicationSecurities.Remove(appSecurity);
                    context.SaveChanges();

                }
                catch (Exception ex)
                {
                    ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
                }
            }
            return RedirectToAction("Index");
        }
    }
}
