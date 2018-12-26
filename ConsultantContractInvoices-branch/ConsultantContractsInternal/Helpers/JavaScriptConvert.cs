using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ConsultantContractsInternal.Helpers
{
    public static class JavaScriptConvert
    {
        public static IHtmlString SerializeObject(object value)
        {
            using( var stringWriter = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                var serializer = new JsonSerializer
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                jsonWriter.QuoteName = false;
                serializer.Serialize(jsonWriter, value);

                return new HtmlString(stringWriter.ToString());
            }
        }
    }
}
