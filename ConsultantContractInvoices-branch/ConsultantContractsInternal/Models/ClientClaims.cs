using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantContractsInternal.Models
{
    public class ClientClaim
    {
        private ClaimType _claimType;
        private string _name;

        public ClientClaim() { }
        public ClientClaim(ClaimType claimType, string name)
        {
            _claimType = claimType;
            _name = name;
        }

        public ClaimType ClaimType { get { return _claimType; } set { _claimType = value; } }
        public string Name { get { return _name; } set { _name = value; } }
    }

    public enum ClaimType
    { 
        WindowsAccountName,
        Role
    }
}