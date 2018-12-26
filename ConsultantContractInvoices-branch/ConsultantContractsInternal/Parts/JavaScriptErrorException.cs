using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantContractsInternal.Parts
{
    [Serializable]
    public class JavaScriptErrorException: Exception
    {
        public JavaScriptErrorException(string message) : base(message) { }
    }
}