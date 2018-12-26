using ConsultantContractsInternal.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsultantContractsInternal.Security
{
    internal static class Roles
    {
        //public Roles()
        //{
        //    using(var context = new ConsultantContractsInternal.Models.Use())
        //    {

        //    }
        //}
        internal const string Developer = "Developer";
        internal const string Admin = "Admin";
        //restricted by division in actions
        internal const string Approver = "Approver";
        //restricted by division in actions
        internal const string Recommend = "Recommend";
        //restricted by division in actions
        internal const string DataEntry = "DataEntry";
        internal const string Audit = "Audit";
        internal const string Read = "Read";

        internal const string ApproverRoles = "Developer, Approver,Administrator,Admin";
        internal const string RecommendRoles = "Developer, Admin, Recommend";
        internal const string InvoiceEntryRoles = "Admin, Developer, Recommend, DataEntry";
        internal const string Devministrator = "Developer, Administrator, Admin";
        internal const string ContractEntryRoles = "Developer, Admin, Recommend";
        internal const string RejectRoles = ApproverRoles + ", " + RecommendRoles;

        internal const string AllRoles = "Admin, Developer, Approver, Recommend, DataEntry, Audit, Read";

        internal static IList<ArDOT_UserProv.Client.API.Role> GetRoles()
        {
            return UserProvHelpers.GetUserSecurity().Select(p => p.Role).ToList();
        }

        internal static string[] GetAllRoleDisplayNames()
        {
            List<string> roles = new List<string> {Admin, Developer, Approver, Recommend, DataEntry, Audit, Read};

            return roles.ToArray();
        }
        public static string GetRoleDisplayName(string role)
        {
            if (String.IsNullOrEmpty(role))
                return String.Empty;
            return role;
        }
    }
}
