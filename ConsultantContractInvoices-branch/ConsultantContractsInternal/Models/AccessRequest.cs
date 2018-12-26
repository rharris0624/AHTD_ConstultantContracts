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
    
    public partial class AccessRequest
    {
        public AccessRequest()
        {
            this.AppRequests = new HashSet<AppRequest>();
            this.Messages = new HashSet<Message>();
        }
    
        public int AccessRequestId { get; set; }
        public int SecurityInstanceId { get; set; }
        public System.DateTime RequestDate { get; set; }
        public int OriginatorId { get; set; }
        public Nullable<System.DateTime> SupervisorApproveDate { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public int AccessRequestActionId { get; set; }
    
        public virtual AccessRequestAction AccessRequestAction { get; set; }
        public virtual Directory Directory { get; set; }
        public virtual SecurityInstance SecurityInstance { get; set; }
        public virtual ICollection<AppRequest> AppRequests { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
