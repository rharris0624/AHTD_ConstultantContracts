//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsultantContractsInternal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AppClaim
    {
        public int AppClaimId { get; set; }
        public int AppId { get; set; }
        public int ClaimId { get; set; }
        public bool Enabled { get; set; }
        public System.DateTime DateEnabled { get; set; }
        public Nullable<System.DateTime> DateDisabled { get; set; }
        public System.DateTime LastChangeDate { get; set; }
        public string LastChangeUser { get; set; }
    
        public virtual Application Application { get; set; }
        public virtual Claim Claim { get; set; }
    }
}
