using ConsultantContractsInternal.Models;
using Elmah;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace ConsultantContractsInternal.Security
{
    public class AHTDRoleProvider : System.Web.Security.RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            var currentUser = new CurrentUser();

            if (username.Equals(currentUser.WindowsAccountName))
                return new string[] { currentUser.Role };

            return null;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return GetRolesForUser(username).Any(r => r.Equals(roleName));
        }

        public bool IsCurrentUserInRole(string roleName)
        {
            var currentUser = new CurrentUser();
            return GetRolesForUser(currentUser.WindowsAccountName).Any(r => r.Equals(roleName));
        }

        #region Unused members

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion Unused members
    }
    //private DbContext _context;

    //public AHTDRoleProvider() { }
    //public AHTDRoleProvider(DbContext context)
    //{
    //    _context = context;
    //}

    //public override string[] GetRolesForUser(string username)
    //{
    //    using (var context = new ArDOT_UserProvEntities1())
    //    {
    //        username = username.Substring(username.LastIndexOf('\\') + 1);
    //        var roles = context.ApplicationSecurities.Where(p => p.ApplicationId.Equals(Globals.APPNAME) && p.UserId.Equals(username)).Select(p => p.RoleId);
    //        if (roles != null)
    //            return roles.ToArray();
    //    }

    //    return null;
    //}

    //public override bool IsUserInRole(string username, string roleName)
    //{
    //    return GetRolesForUser(username).Any(r => r.Equals(roleName));
    //}

    //public bool IsCurrentUserInRole(string roleName)
    //{
    //    var currentUser = new CurrentUser();
    //    return GetRolesForUser(currentUser.WindowsAccountName).Any(r => r.Equals(roleName));
    //}

    //#region Unused members

    //public override string[] FindUsersInRole(string roleName, string usernameToMatch)
    //{
    //    var results = new string[0];
    //    try
    //    {
    //        using (var context = new ArDOT_UserProvEntities1())
    //        {
    //            var role = context.Roles.Where(p => p.ApplicationId.Equals(Globals.APPNAME)&&p.RoleName.Equals(roleName)).FirstOrDefault();
    //            return context.ApplicationSecurities.Where(p => p.ApplicationId.Equals(Globals.APPNAME) &&
    //                                                            p.RoleId.Equals(role.RoleId) &&
    //                                                            p.UserId.Contains(usernameToMatch)).Select(p => p.UserId).ToArray();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
    //    }
    //    return results;
    //}

    //public override string[] GetAllRoles()
    //{
    //    var results = new string[0];
    //    try
    //    {
    //        using (var context = new ArDOT_UserProvEntities1())
    //        {
    //            return context.Roles.Where(p => p.ApplicationId.Equals(Globals.APPNAME)).Select(p => p.RoleName).ToArray();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
    //    }
    //    return results;
    //}

    //public override string[] GetUsersInRole(string roleName)
    //{
    //    var results = new string[0];
    //    try
    //    {
    //        using (var context = new ArDOT_UserProvEntities1())
    //        {
    //            var role = context.Roles.Where(p => p.ApplicationId.Equals(Globals.APPNAME)&&p.RoleName.Equals(roleName)).FirstOrDefault();
    //            var usersInRole = context.ApplicationSecurities.Where(p => p.ApplicationId.Equals(roleName) && 
    //                                                                        p.RoleId.Equals(role.RoleId)).Select(p=>p.UserId);
    //            return usersInRole.ToArray();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
    //    }
    //    return results;
    //}

    //public override void AddUsersToRoles(string[] usernames, string[] roleNames)
    //{
    //    try
    //    {
    //        using (var context = new ArDOT_UserProvEntities1())
    //        {
    //            foreach (var userName in usernames)
    //            {
    //                var user = context.Users.Where(p => p.UserId.Equals(userName)).FirstOrDefault();
    //                foreach (var roleName in roleNames)
    //                {
    //                    var role = context.Roles.Where(p => p.ApplicationId.Equals(Globals.APPNAME)&&p.RoleName.Equals(roleName)).FirstOrDefault();
    //                    context.ApplicationSecurities.Add(
    //                            new ApplicationSecurity
    //                            {
    //                                RoleId = role.RoleId,
    //                                UserId = user.UserId,
    //                                ApplicationId = Globals.APPNAME
    //                            }
    //                        );
    //                    context.SaveChanges();
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
    //    }
    //}

    //public override string ApplicationName
    //{
    //    get
    //    {
    //        return Globals.APPNAME;
    //    }
    //    set
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public override void CreateRole(string roleName)
    //{
    //    using(var context = new ArDOT_UserProvEntities1())
    //    {
    //        Role role = new Role
    //        {
    //            ApplicationId = Globals.APPNAME,
    //            RoleName = roleName,
    //            RoleDesc = roleName
    //        };
    //        context.Roles.Add(role);
    //        context.SaveChanges();
    //    }
    //}

    //public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
    //{
    //    try
    //    {
    //        using (var context = new ArDOT_UserProvEntities1())
    //        {
    //            var roles = context.Roles.Where(p => p.RoleName.Equals(roleName) && p.ApplicationId.Equals(Globals.APPNAME));
    //            context.Roles.Remove(roles.First());
    //            context.SaveChanges();

    //            return true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
    //    }
    //    return false;
    //}

    //public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    //{
    //    try
    //    {
    //        using (var context = new ArDOT_UserProvEntities1())
    //        {
    //            for (var i = 0; i > usernames.Length; i++)
    //            {
    //                var appSecurities = context.ApplicationSecurities.Where(p=>p.User.UserId.Equals(usernames[i])&&p.Role.RoleName.Equals(roleNames[i]));
    //                context.ApplicationSecurities.RemoveRange(appSecurities);
    //                context.SaveChanges();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
    //    }
    //}

    //public override bool RoleExists(string roleName)
    //{
    //    try
    //    {
    //        using (var context = new ArDOT_UserProvEntities1())
    //        {
    //            return context.Roles.Any(p => p.RoleName.Equals(roleName) && p.ApplicationId.Equals(Globals.APPNAME));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
    //    }
    //    return false;
    //}

    //#endregion Unused members
}