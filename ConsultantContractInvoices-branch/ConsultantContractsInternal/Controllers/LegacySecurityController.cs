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
using ConsultantContractsInternal.Security.Attributes;
using ConsultantContractsInternal.Attributes;
using Elmah;

namespace ConsultantContractsInternal.Controllers
{
    public class LegacySecurityController : Controller
    {
        //private ArDOT_UserProvEntities1 context = new ArDOT_UserProvEntities1();
        //
        // GET: /LegacySecurity/

        [AHTDAuthorize(Roles = Roles.Devministrator)]
        public ActionResult Index(string userId, string roleId)
        {
            UserRoleBudgetVM userRoleBudgetVm = null;
            try
            {
                using( var context = new ArDOT_UserProvEntities1())
                {
                    var applicationUsers = context.ApplicationSecurities.Join(context.Users,
                                                    u => new { u.UserId },
                                                    s => new { s.UserId }, (u, s) => u).Where(u => u.ApplicationId.Equals(Globals.APPNAME));
                    var applicationRoles = context.Roles.Where(u => u.ApplicationId.Equals(Globals.APPNAME));
                    userRoleBudgetVm = new UserRoleBudgetVM()
                    {
                        UserList = applicationUsers.Select(s => new ItemVM { ID = s.UserId, Name = s.User.LastName + ", " + s.User.FirstName }).Distinct().ToList(),
                        RoleList = applicationRoles.Select(s => new ItemVM { ID = s.RoleId, Name = s.RoleName }).ToList(),
                        ApplicationId = Globals.APPNAME,
                        UserId = userId,
                        RoleId = roleId
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
            return View("Index", userRoleBudgetVm);
        }

        [HttpGet]
        public ActionResult GetUsersInRoles(string userId)
        {
            using( var context = new ArDOT_UserProvEntities1())
            {
                IEnumerable<ItemVM> applicationSecurities = context.ApplicationSecurities
                                                                .Join(context.Roles, r => r.RoleId,
                                                                        s => s.RoleId, (r, s) => new { r, s })
                                                                .Where(s => s.r.UserId.Equals(userId) &&
                                                                s.r.ApplicationId.Equals(Globals.APPNAME) &&
                                                                s.s.ApplicationId.Equals(Globals.APPNAME))
                                                                .Select(m => new ItemVM { ID = m.r.RoleId, Name = m.s.RoleName });
                List<ItemVM> appSecurities = applicationSecurities.ToList();
                var userInRoles = new SelectList(appSecurities, "ID", "Name");
                return Json(userInRoles, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetAssignedBudgets(string userId, string roleId)
        {
            ReturnArgs ra = new ReturnArgs();
            
            IEnumerable<UserRoleBudgetVM> budgets = null;
            using( var context = new ArDOT_UserProvEntities1())
            {
                budgets = context.LegacySecurities
                                .Join(context.Resources, r => new { r.ApplicationId, r.ResourceId }, s => new { s.ApplicationId, s.ResourceId },
                                                                                                                (r, s) => new { r, s })
                                .Join(context.Users, m => m.r.UserId, b => b.UserId, (m, b) => new { m, b })
                                .Where(s => s.b.UserId.Equals(userId) &&
                                    s.m.s.ApplicationId.Equals(Globals.APPNAME) &&
                                    s.m.r.RoleId.Equals(roleId) &&
                                    s.m.s.ResourceName.Contains("Budget"))
                                .Select(m => new UserRoleBudgetVM
                                {
                                    ApplicationId = m.m.r.ApplicationId,
                                    Sequence = m.m.r.Sequence,
                                    RoleId = m.m.r.RoleId,
                                    UserId = m.m.r.UserId,
                                    PermissionId = m.m.r.PermissionId,
                                    ResourceId = m.m.r.ResourceId,
                                    ResourceName = m.m.s.ResourceName,
                                    ResourceDesc = m.m.s.ResourceDesc,
                                    UserName = m.b.LastName + ", " + m.b.FirstName
                                });
                if (budgets == null || budgets.Count() == 0)
                {
                    ra.Status = 400;
                    return HttpNotFound();
                }
                ra.Status = 200;
                ra.ViewString = RazorViewToString.RenderViewToString(this, "LegacySecurityDetails", budgets);

                return Json(ra,JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Create(string userId, string roleId)
        {
            UserRoleBudgetVM availableResources = null;

            if (userId == null || roleId == null)
                return View();
            
            using( var context = new ArDOT_UserProvEntities1())
            {
                var users = context.Users.Where(p => p.UserId.Equals(userId));
                var roles = context.Roles.Where(p => p.RoleId.Equals(roleId));
                var usedResources = context.LegacySecurities.Where(p => p.ApplicationId.Equals(Globals.APPNAME) &&
                                                                    p.UserId.Equals(userId) &&
                                                                    p.RoleId.Equals(roleId)).Select(p => p.ResourceId);
                var resources = context.Resources.Where(p => p.ApplicationId.Equals(Globals.APPNAME) && 
                                                                p.ResourceName.Contains("Budget") &&
                                                                 !usedResources.Contains(p.ResourceId))
                                                                .Select(m => new IdNameDescVM { ID = m.ResourceId, Name = m.ResourceName, Description = m.ResourceDesc });

                availableResources = 
                new UserRoleBudgetVM
                {
                    RoleId = roleId,
                    UserId = userId,
                    ApplicationId = Globals.APPNAME,
                    PermissionId = "CONS_FULL",
                    RoleList = roles.Select(p => new ItemVM { ID = p.RoleId, Name = p.RoleName }).ToList(),
                    BudgetList = resources.ToList(),
                    UserList = users.Select(g => new ItemVM { ID = g.UserId, Name = g.LastName + ", " + g.FirstName }).ToList()
                };
                return View("Create", availableResources);
            }
        }
        
        public ActionResult Delete(string applicationId, string userId, string roleId, int sequence, string resourceId)
        {
            using( var context = new ArDOT_UserProvEntities1())
            {
                LegacySecurity legacySecurity = context.LegacySecurities.FirstOrDefault(p => p.ApplicationId.Equals(Globals.APPNAME) &&
                                                                                                    p.UserId.Equals(userId) &&
                                                                                                    p.RoleId.Equals(roleId) &&
                                                                                                    p.ResourceId.Equals(resourceId) &&
                                                                                                    p.Sequence.Equals(sequence));
                if (legacySecurity == null)
                {
                    return HttpNotFound();
                }

                return View(legacySecurity);
            }
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string applicationId, string roleId, string userId, int sequence)
        {
            using( var context = new ArDOT_UserProvEntities1())
            {
                LegacySecurity legacySecurity = context.LegacySecurities.FirstOrDefault(p => p.ApplicationId.Equals(Globals.APPNAME) &&
                                                                                                    p.UserId.Equals(userId) &&
                                                                                                    p.RoleId.Equals(roleId) &&
                                                                                                    p.Sequence.Equals(sequence));
                if (legacySecurity != null)
                {
                    context.LegacySecurities.Remove(legacySecurity);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index", new { userId = userId, roleId = roleId });
        }

        [AHTDAuthorize(Roles = Roles.Devministrator)]
        //[ValidateAntiForgeryToken()]
        [HttpPost, ValidateJsonAntiForgeryToken]
        public JsonResult Create(UserRoleBudgetVM userRoleBudget)
        {
            ReturnArgs ra = new ReturnArgs();

            if (ModelState.IsValid)
            {
                if (userRoleBudget.LegacySecurities != null &&
                    userRoleBudget.LegacySecurities.Count() > 0)
                {
                    try
                    {
                        using( var context = new ArDOT_UserProvEntities1())
                        {
                            var legacySecuritiestoAdd = userRoleBudget.LegacySecurities
                                                .Select(p => new LegacySecurity{
                                                                    ApplicationId = userRoleBudget.ApplicationId,
                                                                    UserId = userRoleBudget.UserId,
                                                                    RoleId = userRoleBudget.RoleId,
                                                                    PermissionId = userRoleBudget.PermissionId,
                                                                    ResourceId = p.ResourceId
                                                });

                            List<LegacySecurity> legacySecuritiesToAddList = legacySecuritiestoAdd.ToList();
                            Int32[] sequencesUsed = context.LegacySecurities.Where(p => p.ApplicationId.Equals(userRoleBudget.ApplicationId) &&
                                                                                    p.RoleId.Equals(userRoleBudget.RoleId) &&
                                                                                    p.UserId.Equals(userRoleBudget.UserId)).Select(p => p.Sequence).ToArray();
                            var availableSequences = SequenceFinder.FindSequences(sequencesUsed, legacySecuritiesToAddList.Count());
                            var index = 0;
                            foreach (var legacySecurity in legacySecuritiesToAddList)
                            {
                                legacySecurity.Sequence = availableSequences[index++];//++newSequence;
                            }
                            context.LegacySecurities.AddRange(legacySecuritiesToAddList);
                            context.SaveChanges();
                            ra.Status = 200;
                            //return RedirectToAction("Index", new { userId = userRoleBudget.UserId, roleId = userRoleBudget.RoleId });
                        }
                    }
                    catch (Exception ex)
                    {
                        ra.Status = 500;
                        ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
                    }
                    finally
                    {
                    }
                }
            }
            return Json(new {userId= userRoleBudget.UserId, roleId= userRoleBudget.RoleId, returnArgs = ra});
        }


        [HttpPost]
        [AHTDAuthorize(Roles = Roles.Devministrator)]
        [ValidateJsonAntiForgeryToken()]
        public JsonResult DeleteRange(UserRoleBudgetVM userRoleBudget)
        {
            if (ModelState.IsValid)
            {
                if (userRoleBudget.LegacySecurities != null &&
                    userRoleBudget.LegacySecurities.Count() > 0)
                {
                    using (var context = new ArDOT_UserProvEntities1())
                    {
                        try
                        {
                            foreach (var legacySecurity in userRoleBudget.LegacySecurities)
                            {
                                var deleteCandidate = context.LegacySecurities.Where(p => p.ApplicationId.Equals(legacySecurity.ApplicationId) &&
                                                                                    p.UserId.Equals(legacySecurity.UserId) &&
                                                                                    p.RoleId.Equals(legacySecurity.RoleId) &&
                                                                                    p.ResourceId.Equals(legacySecurity.ResourceId));
                                if (deleteCandidate != null)
                                {
                                    context.LegacySecurities.RemoveRange(deleteCandidate);
                                }
                            }
                            context.SaveChanges();
                            //return RedirectToAction("Index", userRoleBudget.LegacySecurities[0].UserId, userRoleBudget.LegacySecurities[0]);
                            return Json(new { Status = 200, userId = userRoleBudget.LegacySecurities[0].UserId, roleId = userRoleBudget.LegacySecurities[0].RoleId });
                        }
                        catch (Exception ex)
                        {
                            return Json(new { Status = 300, msg = ex.Message });
                        }
                    }
                }
            }
            return Json(new { Status = 400, msg = "Error deleting range" });
        }
        
        [HttpGet]
        public ActionResult GetBudgetData(string userId, string roleId)
        {
            //select budgets that aren't already assigned to that user
            using( var context = new ArDOT_UserProvEntities1())
            {
                IEnumerable<ItemVM> availableBudgetList = context.Resources.Where( p => p.ApplicationId.Equals(Globals.APPNAME) && 
                                                                                    p.ResourceName.Contains("Budget")).Select(g => new ItemVM{ID =g.ResourceId, Name = g.ResourceName});
                IEnumerable<string> currentBudgetList = context.LegacySecurities.Where( p => p.ApplicationId.Equals(Globals.APPNAME) &&
                                                                                        p.UserId.Equals(userId) &&
                                                                                        p.RoleId.Equals(roleId) ).Select(g => g.ResourceId);
            

                if (availableBudgetList != null && availableBudgetList.Count() > 0 && currentBudgetList != null &&  currentBudgetList.Count() > 0)
                {
                    availableBudgetList = availableBudgetList.Where(p => !currentBudgetList.Contains(p.ID));
                }
                List<ItemVM> availableBudgets = availableBudgetList.ToList();
                var budgets = new SelectList(availableBudgets, "ID", "Name");

                return Json(budgets, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
