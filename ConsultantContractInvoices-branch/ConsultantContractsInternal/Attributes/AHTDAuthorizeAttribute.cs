using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace ConsultantContractsInternal.Security.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AHTDAuthorizeAttribute : AuthorizeAttribute
    {
        private AHTDRoleProvider _roleProvider;
        private List<string> _currentUsersRoles;
        private EventLog _eventLog;

        public string ViewName { get; set; }

        public AHTDAuthorizeAttribute()
        {
            _roleProvider = new AHTDRoleProvider();
            _currentUsersRoles = new List<string>();
            _eventLog = new EventLog("Application", Environment.MachineName, "ConsultantContracts::AHTDAuthorize.AuthorizeCore");
            
            ViewName = "AccessDenied";
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (HttpContext.Current.Cache["isAuthorized"] == null)
                HttpContext.Current.Cache["isAuthorized"] = base.AuthorizeCore(httpContext);
            bool isAuthorized = (bool)HttpContext.Current.Cache["isAuthorized"];

            
            if (!isAuthorized)
                return false;

            if (string.IsNullOrEmpty(Roles))
                return true;

            if(_currentUsersRoles.Count <= 0)
                _currentUsersRoles.AddRange(_roleProvider.GetRolesForUser(httpContext.User.Identity.Name));

            if (_currentUsersRoles.Count <= 0)
                return false;
            
            foreach(var role in _currentUsersRoles)
            {
                if (Roles.Split(',').Contains(role))
                    return true;
            }
            return false;
        }
        

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result = new ViewResult { ViewName = "AccessDenied" };
            }
        }
    }
}
