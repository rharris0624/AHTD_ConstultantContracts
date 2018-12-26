using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using AHTD.Security.Common;
using AHTD.Security.Web;
using System.Configuration;
using ConsultantContractsInternal.Properties;
using ConsultantContractsInternal.Utilities;

namespace ConsultantContractsInternal.Security
{
    /// <summary>
    /// Provides info related to the current user's claims data.
    /// </summary>
    public class CurrentUser : UserInfoBase
    {
        private string sSource = "UserProvHelpers";
        private string sLog = "Application";
        private string sEvent = "GetUserRole";
        // This code will add claims data support when running locally in Visual Studio

        protected override IEnumerable<ClientClaim> GetClientClaims()
        {
            if (ConfigurationManager.AppSettings["RuntimeEnvironment"].Equals("local"))
            {
                List<ClientClaim> claims = new List<ClientClaim>();
                
                claims.Add(new ClientClaim(StandardClaimTypes.WindowsAccountName, HttpContext.Current.User.Identity.Name));

                if (!EventLog.SourceExists(sSource))
                {
                    EventLog.CreateEventSource(sSource, sLog);
                }
                EventLog.WriteEntry(sSource, "The value of Identity.Name is " + HttpContext.Current.User.Identity.Name, EventLogEntryType.Warning);
                var role = UserProvHelpers.GetUserRole();
                if(role != null)
                    claims.Add(new ClientClaim(StandardClaimTypes.Role, role.RoleName));
                else
                    claims.Add(new ClientClaim(StandardClaimTypes.Role, Roles.Developer));

                return claims;
            }

            return base.GetClientClaims();
        }


        /// <summary>                                                                                                   
        /// Gets the current user's security role.
        /// </summary>
        public string Role
        {
            get
            {
                return GetSpecificClaimValue(StandardClaimTypes.Role);
            }
        }

        /// <summary>
        /// Gets the current user's UserName
        /// </summary>
        public virtual string UserName
        {
            get
            {
                return AHTD.Text.CommonText.StripNTDomain(WindowsAccountName);
            }
        }
    }
}
