using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable StringCompareToIsCultureSpecific

// ReSharper disable once CheckNamespace
namespace ConsultantContractsInternal.Models
{
    public partial class InvoicePayment
    {
        private readonly static string[] CertainFunctionCodes = {"3610","3611","3612", 
                                                "3710","3711","3712",
                                                "3810","3811","3812"};

        public bool IsT1()
        {
            return (Func.CompareTo("3010") >= 0 && Func.CompareTo("3200") < 0) ||
                   (Func.CompareTo("3400") >= 0 && Func.CompareTo("3499") <= 0) ||
                   (Func.CompareTo("3900") >= 0 && Func.CompareTo("3990") < 0) ||
                   CertainFunctionCodes.Contains(Func);
        }

        public bool IsT2()
        {
            return (Func.CompareTo("3202") >= 0 && Func.CompareTo("3400") < 0) ||
                   (Func.CompareTo("3499") > 0 && Func.CompareTo("3900") < 0)
                   && !CertainFunctionCodes.Contains(Func);
        }
    }
}