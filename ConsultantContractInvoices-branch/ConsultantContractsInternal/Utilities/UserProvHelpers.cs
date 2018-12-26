using ArDOT_UserProv.Client.API;
using ConsultantContractsInternal.Models;
using ConsultantContractsInternal.Security;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Diagnostics;
using System.Configuration;

namespace ConsultantContractsInternal.Utilities
{
    public static class UserProvHelpers
    {
        public static string sSource = "UserProvHelpers";
        public static string sLog = "Application";
        public static string sEvent = "GetUserRole";

        private static IList<ArDOT_UserProv.Client.API.Security> _Securities;

        public static IList<ArDOT_UserProv.Client.API.Security> Securities
        {
            get
            {
                if (_Securities != null && _Securities.Count() > 0)
                    return _Securities;

                try
                {
                    var userProvHelper = new UserProvHelper(ConfigurationManager.AppSettings["UserProvUrl"], ConfigurationManager.AppSettings["UserProvApplicationId"]);
                    var userName = System.Web.HttpContext.Current.User.Identity.Name;
                    userName = userName.Replace("AHTD\\", "");

                    var securities = userProvHelper.GetApplicationSecuritiesForUser(userName);
                    if (securities != null && securities.Count() > 0)
                    {
                        _Securities = securities;
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
                }
                return _Securities;
            }
        }

        public static IList<int> AvailableDivisions(ConsultantContractsEntities context, string userName)
        {
            IList<int> response = null;
            try
            {
                //if (HttpContext.Current.Cache["AvailableDivs"] != null)
                if(HttpContext.Current.Session != null && HttpContext.Current.Session["AvailableDivs"] != null)
                    return (IList<int>)HttpContext.Current.Session["AvailableDivs"];
                IEnumerable<string> budgets = from security in Securities
                                              where security.Resource != null &&
                                                  security.Resource.ResourceName.Contains("Budget")
                                              select security.Resource.ResourceId;
                var divs = context.ResponsibleDivisions.Where(e => budgets.Contains(e.Budget)).Select(o => o.DivisionId);
                if (divs != null)
                {
                    response = divs.ToList();
                }
                if(HttpContext.Current.Session != null)
                    HttpContext.Current.Session["AvailableDivs"] = response;
            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
            }
            return response;
        }

        public static IList<ArDOT_UserProv.Client.API.Security> GetUserSecurity()
        {
            return Securities;
        }

        public static ArDOT_UserProv.Client.API.Role GetUserRole()
        {
            return Securities.FirstOrDefault(p => p.Role != null).Role;
        }

        public static IList<ArDOT_UserProv.Client.API.Role> GetUserRoles()
        {
            var result = new List<ArDOT_UserProv.Client.API.Role>();
            var securities = Securities.Select(p => p.Role != null);
            foreach (var security in Securities)
            {
                result.Add(security.Role);
            }

            var newResult = ListUtil.Distinct(result, (p, p1) => p.RoleId == p1.RoleId && p.RoleName == p1.RoleName);

            return newResult.ToList();
        }

        public static List<Domain.ContractPermissionBudget> GetContractPermissions(ConsultantContractsEntities context, string userName, IEnumerable<int> availableDivs)
        {
            var response = new List<Domain.ContractPermissionBudget>();
            try
            {
                if (HttpContext.Current.Cache["ContractPermissions"] != null)
                    return (List<Domain.ContractPermissionBudget>)HttpContext.Current.Cache["ContractPermissions"];

                IEnumerable<string> budgets = from security in Securities
                                              where security.Resource != null &&
                                                  security.Resource.ResourceName.Contains("Budget")
                                              select security.Resource.ResourceId;
                
                IEnumerable<ArDOT_UserProv.Client.API.Security> securities = from sec in Securities
                                              where sec.Resource != null &&
                                                  sec.Resource.ResourceName.Contains("Budget") &&
                                                  sec.Application.ApplicationId.Equals("CONS_CNTRT")
                                              select sec;
                IQueryable<Domain.ContractPermissionBudget> contractPermissions = from division in context.ResponsibleDivisions
                                join contract in context.Contracts on division.DivisionId equals contract.ResponsibleDivisionId
                                                                                  where availableDivs.Contains(division.DivisionId)
                                select new Domain.ContractPermissionBudget{ContractId = contract.ContractCode, Budget = division.Budget};

                if (contractPermissions != null)
                {
                    string permissionId = null;
                    foreach (var item in contractPermissions )
                    {
                        var security = securities.Where(p => p.Resource.ResourceName.Contains("Budget")
                            && p.Application.ApplicationId.Equals("CONS_CNTRT")
                            && p.Resource.ResourceId.Equals(item.Budget)).FirstOrDefault();
                        if (security != null && security.Permission != null )
                        {
                            permissionId = security.Permission.PermissionId;
                            response.Add(new Domain.ContractPermissionBudget { PermissionId = permissionId, ContractId = item.ContractId, Budget = item.Budget });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
            }
            HttpContext.Current.Cache["ContractPermissions"] = response;
            return response;
        }
    }
}