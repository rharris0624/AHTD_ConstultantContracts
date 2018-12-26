using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantContractsInternal.Domain
{
    public class ContractPermissionBudget
    {
        public int ContractId { get; set; }
        public string PermissionId { get; set; }
        public string Budget { get; set; }
    }
}