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
    
    public partial class SuppSalaryRate
    {
        public string SuppNo { get; set; }
        public string JobTitle { get; set; }
        public decimal RateMin { get; set; }
        public decimal RateMax { get; set; }
        public System.DateTime LastUpdateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public int ContractCode { get; set; }
        public bool Deleted { get; set; }
    
        public virtual SuppAgreement SuppAgreement { get; set; }
    }
}
