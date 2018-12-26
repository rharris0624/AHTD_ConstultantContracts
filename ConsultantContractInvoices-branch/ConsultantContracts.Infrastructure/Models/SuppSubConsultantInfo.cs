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
    
    public partial class SuppSubConsultantInfo
    {
        public SuppSubConsultantInfo()
        {
            this.SuppSubConsultantSalaryRates = new HashSet<SuppSubConsultantSalaryRate>();
            this.SuppSubConsultantServiceRates = new HashSet<SuppSubConsultantServiceRate>();
        }
    
        public int ContractCode { get; set; }
        public string SuppNo { get; set; }
        public Nullable<decimal> ContractCeiling { get; set; }
        public Nullable<decimal> T1SvcsCeiling { get; set; }
        public Nullable<decimal> T1FixedFeeMax { get; set; }
        public Nullable<decimal> T2SvcsCeiling { get; set; }
        public Nullable<decimal> T2FixedFeeMax { get; set; }
        public System.DateTime LastUpdateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public Nullable<decimal> HomeOfficeOverheadRateMax { get; set; }
        public Nullable<decimal> FCCM { get; set; }
        public Nullable<decimal> FieldServiceOverheadRateMax { get; set; }
        public Nullable<decimal> Multiplier { get; set; }
        public int SubConsultantId { get; set; }
    
        public virtual SuppAgreement SuppAgreement { get; set; }
        public virtual ICollection<SuppSubConsultantSalaryRate> SuppSubConsultantSalaryRates { get; set; }
        public virtual ICollection<SuppSubConsultantServiceRate> SuppSubConsultantServiceRates { get; set; }
        public virtual Consultant SubConsultant { get; set; }
    }
}
