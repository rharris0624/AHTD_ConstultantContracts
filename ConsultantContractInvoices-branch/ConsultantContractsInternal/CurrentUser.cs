using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

using ConsultantContractsInternal.Models;
using System.Configuration;
using ConsultantContractsInternal.Properties;
using ConsultantContractsInternal.Utilities;
using System.Web.Http;
using System.Web.SessionState;

namespace ConsultantContractsInternal.Security
{
    /// <summary>
    /// Provides info related to the current user's claims data.
    /// </summary>
    public class CurrentUser //: UserInfoBase
    {

        private string sSource = "CurrentUser";
        private string sLog = "Application";
        private string sEvent = "GetClientClaims";
        private string _currentRole;
        private string _windowsAccountName;
        private List<ClientClaim> _clientClaims;
        private EventLog _eventLog;

        public CurrentUser()
        {
            _eventLog = new EventLog();
            _eventLog.Source = "ConsultantContracts.CurrentUser";
            _windowsAccountName = HttpContext.Current.User.Identity.Name;
            _clientClaims = new List<ClientClaim>();
            _clientClaims.Add(new ClientClaim(ClaimType.WindowsAccountName, UserName));
            var roles = UserProvHelpers.GetUserRoles();
            foreach (var role in roles)
            {
                _clientClaims.Add(new ClientClaim(ClaimType.Role, role.RoleName));
            }
        }
        // This code will add claims data support when running locally in Visual Studio

        public IEnumerable<ClientClaim> GetClientClaims()
        {
            return _clientClaims;
        }

        public IEnumerable<ClientClaim> ClientClaims 
        {
            get { return _clientClaims; } 
        }

        /// <summary>                                                                                                   
        /// Gets the current user's security role.
        /// </summary>
        public string Role
        {
            get
            {
                if (HttpContext.Current != null)
                    if (HttpContext.Current.Cache != null)
                    {
                        if (HttpContext.Current.Cache["UserRole"] == null)
                            HttpContext.Current.Cache["UserRole"] = _clientClaims.Where(p => p.ClaimType.Equals(ClaimType.Role)).FirstOrDefault().Name;
                        _currentRole = HttpContext.Current.Cache["UserRole"] as string;
                        return _currentRole;
                    }
                    else
                    {
                        _eventLog.WriteEntry("HttpContext.Current.Cache is null");
                        throw new Exception("HttpContext.Current.Cache is null");
                    }
                else
                {
                    _eventLog.WriteEntry("HttpContext.Current is null");
                    throw new Exception("HttpContext.Current is null");
                }
            }
            set
            {
                _currentRole = value;
            }
        }

        public string WindowsAccountName
        {
            get { return _windowsAccountName; }
        }

        public string CurrentRole { get { return _currentRole; } set { _currentRole = value; } }

        /// <summary>
        /// Gets the current user's UserName
        /// </summary>
        public virtual string UserName
        {
            get
            {
                return AHTD.Text.CommonText.StripNTDomain(_windowsAccountName);
            }
        }
    }
}
