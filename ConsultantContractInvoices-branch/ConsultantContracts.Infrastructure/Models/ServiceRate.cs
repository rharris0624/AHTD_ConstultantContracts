//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsultantContracts.Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServiceRate
    {
        public int ContractCode { get; set; }
        public string ServiceName { get; set; }
        public decimal RateMin { get; set; }
        public decimal RateMinOrig { get; set; }
        public decimal RateMax { get; set; }
        public decimal RateMaxOrig { get; set; }
        public System.DateTime LastUpdateDate { get; set; }
        public string LastUpdateUser { get; set; }
    
        public virtual Contract Contract { get; set; }
    }
}
