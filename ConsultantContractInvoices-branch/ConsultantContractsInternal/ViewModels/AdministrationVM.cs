using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantContractsInternal.ViewModels
{
    public class AdministrationVM
    {
        public AdministrationVM() { }

        public string ApplicationUserName { get; set; }
        public string[] Users { get; set; }
        public string[] AssignedBudgets { get; set; }
        public string[] AvailableBudgets { get; set; }
        public string UserRole { get; set; }
    }
}