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
    
    public partial class AppRequest
    {
        public AppRequest()
        {
            this.Messages = new HashSet<Message>();
        }
    
        public int AppRequestId { get; set; }
        public int AccessRequestId { get; set; }
        public int AppId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> BudgetId { get; set; }
        public Nullable<int> LocationId { get; set; }
        public Nullable<bool> RequestApproved { get; set; }
        public bool AllowAccess { get; set; }
        public string Notes { get; set; }
        public Nullable<int> ProcessedBy { get; set; }
        public Nullable<System.DateTime> ProcessDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
    
        public virtual AccessRequest AccessRequest { get; set; }
        public virtual Application Application { get; set; }
        public virtual Budget Budget { get; set; }
        public virtual Directory Directory { get; set; }
        public virtual Location Location { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
