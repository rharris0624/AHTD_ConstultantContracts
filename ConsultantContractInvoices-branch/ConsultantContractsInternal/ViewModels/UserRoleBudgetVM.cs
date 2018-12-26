using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConsultantContractsInternal.Models;
using System.ComponentModel;

namespace ConsultantContractsInternal.ViewModels
{
    public class UserRoleBudgetVM
    {
        public string ApplicationId { get; set; }
        public string UserId { get; set; }
        [DisplayName("Role")]
        public string RoleId { get; set; }
        public string PermissionId { get; set; }
        public string ResourceId { get; set; }
        [DisplayName("Budget")]
        public string ResourceName { get; set; }
        [DisplayName("Description")]
        public string ResourceDesc { get; set; }
        public string UserName { get; set; }
        public int Sequence { get; set; }
        public List<ItemVM> UserList { get; set; }
        public List<ItemVM> RoleList { get; set; }
        public List<IdNameDescVM> BudgetList { get; set; }
        public List<LegacySecurity> LegacySecurities { get; set; }
        public List<string> SelectedBudgets { get; set; }
        public IEnumerable<Role> AvailableRoles { get; set; }
    }
}