using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

using ConsultantContractsInternal.Security;

namespace ConsultantContractsInternal.Controllers
{
    public class SecuredController : Controller
    {
        private readonly Lazy<CurrentUser> _currentUser = new Lazy<CurrentUser>(() => new CurrentUser());
        protected CurrentUser CurrentUser
        {
            get { return _currentUser.Value; }
        }

        public class JsonNetResult : JsonResult
        {
            public static readonly JsonSerializerSettings DefaultSettings = new JsonSerializerSettings 
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore, 
                DateFormatHandling = DateFormatHandling.IsoDateFormat 
            };

            public override void ExecuteResult(ControllerContext context)
            {
                if (context == null)
                    throw new ArgumentNullException("context");

                var response = context.HttpContext.Response;

                response.ContentType = !String.IsNullOrEmpty(ContentType)
                                           ? ContentType
                                           : "application/json";

                if (ContentEncoding != null)
                    response.ContentEncoding = ContentEncoding;

                // If you need special handling, you can call another form of SerializeObject below
                var serializedObject = JsonConvert.SerializeObject(Data, Formatting.Indented, DefaultSettings);
                response.Write(serializedObject);
            }
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult() { Data = data, ContentEncoding = contentEncoding, ContentType = contentType };
        }
    }
}