using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantContractsInternal.ViewModels
{
    public class AllotmentBalanceVm
    {
        public string FAP { get; set; }
        public string Func { get; set; }
        public decimal? FundingPriority { get; set; }
        public decimal AllottedAmt { get; set; }
        public decimal Total { get; set; }
    }
}