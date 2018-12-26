using ConsultantContractsInternal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultantContractsInternal.ViewModels
{
    public class UserRoleVm
    {
        public string ApplicationId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public List<ItemVM> UserList{ get; set; }
        public List<ItemVM> RoleList { get; set; }
        public string SelectedUserId { get; set; }
        public IEnumerable<ApplicationSecurity> ApplicationSecurities { get; set; }
    }
}