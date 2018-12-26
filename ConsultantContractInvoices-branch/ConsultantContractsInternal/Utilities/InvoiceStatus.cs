using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantContractsInternal.Utilities
{
    public class InvoiceStatus
    {
        public const string InvoiceCreated = "Review";
        public const string InvoiceSubmitted = "Submitted";
        public const string InvoiceRecommended = "Recommended";
        public const string InvoiceApproval = "Approved";
        public const string InvoiceWaiting = "Waiting";
        public const string InvoicePaid = "Paid";
        public const string InvoiceRejected = "Rejected";
    }
}