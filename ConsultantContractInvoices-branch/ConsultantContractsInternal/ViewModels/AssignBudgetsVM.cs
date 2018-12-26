using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConsultantContractsInternal.Models;
using Newtonsoft.Json;


namespace ConsultantContractsInternal.ViewModels
{
    public class AssignBudgetsVM
    {
        [JsonProperty("ApplicationId")]
        public string ApplicationId { get; set; }
        [JsonProperty("UserId")]
        public string UserId { get; set; }
        [JsonProperty("RoleId")]
        public string RoleId { get; set; }
        public List<string> SelectedBudgets { get; set; }
    }
}