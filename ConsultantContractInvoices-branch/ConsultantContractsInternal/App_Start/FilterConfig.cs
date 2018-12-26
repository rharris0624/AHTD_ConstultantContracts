using ConsultantContractsInternal.Security.Attributes;
using Elmah;
using System.Web;
using System.Web.Mvc;

namespace ConsultantContractsInternal
{
    //public class FilterConfig
    //{
    //    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    //    {
    //        filters.Add(new HandleErrorAttribute());
    //    }
    //}
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ElmahHandledErrorLoggerFilter());
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AHTDAuthorizeAttribute());
        }
    }
    //For some reason needed to make elmah handle the already handled errors make by custom error pages.
    //Link is where the code was found.
    //http://stackoverflow.com/a/5936867/3712712
    public class ElmahHandledErrorLoggerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Log only handled exceptions, because all other will be caught by ELMAH anyway.
            if (context.ExceptionHandled)
                ErrorSignal.FromCurrentContext().Raise(context.Exception);
        }
    }

}