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
    
    public partial class LocationType
    {
        public LocationType()
        {
            this.Locations = new HashSet<Location>();
        }
    
        public int LocationTypeId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public System.DateTime DateEnabled { get; set; }
        public Nullable<System.DateTime> DateDisabled { get; set; }
        public System.DateTime LastChangeDate { get; set; }
        public string LastChangeUser { get; set; }
    
        public virtual ICollection<Location> Locations { get; set; }
    }
}
