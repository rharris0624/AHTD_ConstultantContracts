using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsultantContractsInternal.Models
{
    using System;
    using System.Collections.Generic;

    public partial class UnpaidInvoice
    {
        public int ContractCode { get; set; }
        public int ConsultantId { get; set; }
        public string ConsultantName { get; set; }
        public int InvoiceNo { get; set; }
        public string FAP { get; set; }
        public decimal? Amount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal? T1FixedFee { get; set; }
        public decimal? T1Svcs { get; set; }
        public decimal? T2FixedFee { get; set; }
        public string Status { get; set; }
    }
}
