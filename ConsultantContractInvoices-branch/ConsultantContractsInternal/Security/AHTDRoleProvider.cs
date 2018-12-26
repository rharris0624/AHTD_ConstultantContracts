using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}