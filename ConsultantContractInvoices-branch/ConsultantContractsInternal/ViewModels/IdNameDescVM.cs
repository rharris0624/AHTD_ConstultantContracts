using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantContractsInternal.ViewModels
{
    public class IdNameDescVM
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; }
    }
}