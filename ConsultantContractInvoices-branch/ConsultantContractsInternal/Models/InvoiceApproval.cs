//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsultantContractsInternal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InvoiceApproval
    {
        public int InvoiceApprovalID { get; set; }
        public int InvoiceId { get; set; }
        public string UserID { get; set; }
        public string Status { get; set; }
        public System.DateTime StatusDate { get; set; }
        public bool ActiveStatus { get; set; }
        public bool AuditReview { get; set; }
        public Nullable<System.DateTime> VoucherDate { get; set; }
        public string VoucherNo { get; set; }
        public string RejectedReason { get; set; }
    
        public virtual Invoice Invoice { get; set; }
    }
}
