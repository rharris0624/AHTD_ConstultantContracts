using System.Web.Mvc;

namespace ConsultantContractsInternal.Security
{
    public class AHTDAuthorizeAttribute : AuthorizeAttribute
    {
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
