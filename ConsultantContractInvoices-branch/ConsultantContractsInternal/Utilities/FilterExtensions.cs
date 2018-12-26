using System.Linq;
using ConsultantContractsInternal.Models;

namespace ConsultantContractsInternal.Utilities
{
    static class FilterExtensions
    {
        public static IQueryable<Invoice> RevisionFilter(this IQueryable<Invoice> invoice)
        {
            //return invoice.Where(i => i.SubmittedDate.HasValue && !i.InvoiceApproval.RejectedDate.HasValue);
            return invoice.Where(i => i.SubmittedDate.HasValue && i.InvoiceApproval.Where(p => p.InvoiceId == i.InvoiceId && p.Status != InvoiceStatus.InvoiceRejected && p.ActiveStatus == false).Count() ==0);
        }
    }
}