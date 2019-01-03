using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Web;
using AHTD.Logging;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Protocols.WSTrust;
using Microsoft.IdentityModel.SecurityTokenService;

using AHTD.Security.Common;

namespace AHTD.Security.Web
{
	/// <summary>
	/// A custom implementation of the ClaimsIdentity class that automates
	/// building claims info.
	/// </summary>
	public class CustomClaimsIdentity : ClaimsIdentity
	{
        private static Dictionary<string,string> config = new Dictionary<string, string>() { { "applicationName", ConfigurationManager.AppSettings["AppName"] } };
        private readonly IAppLoggingService ahtdLog = new AHTD.Logging.AHTDErrorLog(config) as IAppLoggingService;

		private IClaimsPrincipal principal;
		private RequestSecurityToken request;
		private Scope scope;
		private string requestingAppName;
		private string requestingUser;
		private string requestingUserId;
		private int? requestingAppId;
		private AHTD.Security.Web.Model.HighwayPoliceEntities ahpContext;
		private AHTD.Security.Web.Model.UserProvEntities usrprovContext;

		/// <summary>
		/// An AHP DB data context.
		/// </summary>
		protected AHTD.Security.Web.Model.HighwayPoliceEntities AHPContext
		{
			get
			{
				if (ahpContext == null)
					ahpContext = new AHTD.Security.Web.Model.HighwayPoliceEntities();
				return ahpContext;
			}
		}
		/// <summary>
		/// A UsrProv DB data context.
		/// </summary>
		protected AHTD.Security.Web.Model.UserProvEntities UserProvContext
		{
			get
			{
				if (usrprovContext == null)
					usrprovContext = new AHTD.Security.Web.Model.UserProvEntities();
				return usrprovContext;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomClaimsIdentity"/> class.
		/// </summary>
		public CustomClaimsIdentity(IClaimsPrincipal principal, RequestSecurityToken request, Scope scope)
			: base()
		{
			this.principal = principal;
			this.request = request;
			this.scope = scope;

			Initialize();

			try
			{
				FillClaims();
			}
			catch (Exception ex)
			{
                ahtdLog.LogException(ex, false);   
			}
		}

		private void Initialize()
		{
			if (request != null)
			{
				if (request is CustomRequestSecurityToken)
				{
					this.requestingAppName = (request as CustomRequestSecurityToken).AppName;
					try
					{
						if (String.IsNullOrEmpty((request as CustomRequestSecurityToken).UserID)
							|| (request as CustomRequestSecurityToken).UserID == CustomRequestSecurityToken.DefaultUserIDValue)
						{
							this.requestingUser = this.principal.Identity.Name;
						}
						else
						{
							PrincipalPermission pPerm = new PrincipalPermission(null, "CSD Applications", true);
							pPerm.Demand();
							this.requestingUser = "AHTD\\" + (request as CustomRequestSecurityToken).UserID;
						}
					}
					catch
					{
						this.requestingUser = this.principal.Identity.Name;
					}
				}
				else
				{
					if (request.AppliesTo != null)
					{
						this.requestingAppName = request.AppliesTo.Uri.AbsolutePath.Replace("/", "");
					}
					this.requestingUser = this.principal.Identity.Name;
				}

				try
				{
					this.requestingAppId = GetAppID();
				}
				catch
				{
					this.requestingAppId = null;
                    ahtdLog.LogException(new ApplicationException(String.Format("Could not find requesting application, attempted to find matches for '{0}'.", requestingAppName) + Environment.NewLine +
                                                                  "Check the Microsoft.IdentityModel section of the application config " + 
                                                                  "to ensure that the application name matches the name in the User provisioning database."), false);   
				}
			}

			requestingUserId = AHTD.Text.CommonText.StripNTDomain(requestingUser);
		}
		private void FillClaims()
		{
			// Passive clients (i.e. an ASP.NET web app) will rely solely on AppClaims
			// Active clients using the ClaimsService will also need AppClaims to
			//define their custom claim needs beyond what the service requires by default
			IEnumerable<string> claimsTypes = GetClaimTypes();

			if (claimsTypes != null)
			{
				foreach (string claimType in claimsTypes)
				{
					try
					{
						this.Claims.AddRange(FulfillClaimRequest(claimType));
					}
					catch
					{
                        ahtdLog.LogException(new ApplicationException(String.Format("Could not find the requested claim type: '{0}' for the application: '{1}'.", claimType, requestingAppName) + Environment.NewLine +
                                                                                    "Check the Application Claims in User Provisioning to ensure that the claim type is valid" + 
                                                                                    "for this application."), false);   
					}
				}
			}

			// The ClaimsService requests their WindowsAccountName, so add that if we
			//haven't already (most apps probably require it anyway, though)
			// Also, if no claims were added, this will cause a SAML Assertion error
			//further on if we allow things to continue, so let's also add it if empty
			if (this.Claims.Count == 0 || !this.Claims.Any(c => c.ClaimType == ClaimTypes.WindowsAccountName))
			{
				this.Claims.Add(new Claim(ClaimTypes.WindowsAccountName, principal.Identity.Name));
			}
		}

		private List<Claim> FulfillClaimRequest(RequestClaim requestedClaim)
		{
		    if (requestedClaim == null)
		        return null;

		    return FulfillClaimRequest(requestedClaim.ClaimType);
		}
		private List<Claim> FulfillClaimRequest(string claimType)
		{
			if (String.IsNullOrEmpty(claimType))
				return null;

            if (!GetClaimTypes().Any(ct => ct == claimType))
            {
                ahtdLog.LogException(new ApplicationException(String.Format("Could not find the requested claim type: '{0}' for the application: '{1}'.", claimType, requestingAppName) + Environment.NewLine +
                                                                                    "Check the Application Claims in User Provisioning to ensure that the claim type is valid" +
                                                                                    "for this application."), false); 
            }
            
			List<Claim> claims = new List<Claim>();

			switch (claimType)
			{
				case ClaimTypes.WindowsAccountName:
					claims.Add(GetWindowsAccountName());
					break;
                case ClaimTypes.Upn:
                    claims.Add(GetUpn());
                    break;
				case ClaimTypes.Role:
					claims.AddRange(GetRoles());
					break;
				case ClaimTypes.GivenName:
					claims.Add(GetGivenName());
					break;
				case ClaimTypes.Surname:
					claims.Add(GetSurname());
					break;
				case ClaimTypes.Email:
					claims.Add(GetEmail());
					break;
				case AHPClaimTypes.District:
					claims.Add(GetAHPDistrict());
					break;
				case AHPClaimTypes.Rank:
					claims.Add(GetAHPRank());
					break;
				case AHPClaimTypes.RankAbbrev:
					claims.Add(GetAHPRankAbbrev());
					break;
				case AHPClaimTypes.Unit:
					claims.Add(GetAHPUnit());
					break;
				case AHTDClaimTypes.Budget:
					claims.AddRange(GetBudgets());
					break;
				case AHTDClaimTypes.DirectoryID:
					claims.Add(GetDirectoryID());
					break;
				case AHTDClaimTypes.EmpID:
					claims.Add(GetEmpID());
					break;
				case AHTDClaimTypes.Location:
					claims.AddRange(GetLocations());
					break;
				case AHTDClaimTypes.BudgetLocation:
					claims.AddRange(GetBudgetLocations());
					break;
				case AHTDClaimTypes.RoleBudgetLocation:
					claims.AddRange(GetRoleBudgetLocations());
					break;
                case AHTDClaimTypes.SupervisoryCode:
                    claims.Add(GetSupervisoryCode());
                    break;
			}

			return claims;
		}
        private Claim GetUpn()
        {
            //This accidentally works
            //Don't tell anyone
            return GetEmail();
        }
		private Claim GetWindowsAccountName()
		{
			return new Claim(ClaimTypes.WindowsAccountName, requestingUser);
		}
		private IEnumerable<Claim> GetRoles()
		{
			if (requestingAppId == null)
				return null;

			return (from role in UserProvContext.Roles
					join access in UserProvContext.GrantedAccesses
					on role.RoleId equals access.RoleId
					where role.Enabled && role.AppId == requestingAppId
						&& access.AppId == requestingAppId
						&& access.SecurityInstance.Directory.UserId == requestingUserId
					select role)
					.AsEnumerable()
					.Select(role => new Claim(ClaimTypes.Role, role.RoleName));
		}
		private Claim GetGivenName()
		{
			return (from employee in UserProvContext.Directories
					where employee.EmployeeStatus == AHTD.Security.Web.Model.EmployeeStatus.Active
						&& employee.UserId == requestingUserId
					select employee)
					.AsEnumerable()
					.Select(employee => new Claim(StandardClaimTypes.GivenName, employee.FirstName))
					.DefaultIfEmpty(null)
					.FirstOrDefault();
		}
		private Claim GetSurname()
		{
			return (from employee in UserProvContext.Directories
					where employee.EmployeeStatus == AHTD.Security.Web.Model.EmployeeStatus.Active
						&& employee.UserId == requestingUserId
					select employee)
					.AsEnumerable()
					.Select(employee => new Claim(StandardClaimTypes.Surname, employee.LastName))
					.DefaultIfEmpty(null)
					.FirstOrDefault();
		}
		private Claim GetEmail()
		{
			return (from employee in UserProvContext.Directories
					where employee.EmployeeStatus == AHTD.Security.Web.Model.EmployeeStatus.Active
						&& employee.UserId == requestingUserId
					select employee)
					.AsEnumerable()
					.Select(employee => new Claim(StandardClaimTypes.Email, employee.Email))
					.DefaultIfEmpty(null)
					.FirstOrDefault();
		}
		private Claim GetAHPDistrict()
		{
			return (from roster in AHPContext.Rosters
					where roster.Active && roster.UserID == requestingUserId
					select roster)
					.AsEnumerable()
					.Select(roster => new Claim(AHPClaimTypes.District, roster.District))
					.DefaultIfEmpty(null)
					.FirstOrDefault();
		}
		private Claim GetAHPRank()
		{
			return (from roster in AHPContext.Rosters.Include("Rank")
					where roster.Active && roster.UserID == requestingUserId
					select roster)
					.AsEnumerable()
					.Select(roster => new Claim(AHPClaimTypes.Rank, roster.Rank.rankname))
					.DefaultIfEmpty(null)
					.FirstOrDefault();
		}
		private Claim GetAHPRankAbbrev()
		{
			return (from roster in AHPContext.Rosters.Include("Rank")
					where roster.Active && roster.UserID == requestingUserId
					select roster)
					.AsEnumerable()
					.Select(roster => new Claim(AHPClaimTypes.RankAbbrev, roster.Rank.rankabbrev))
					.DefaultIfEmpty(null)
					.FirstOrDefault();
		}
		private Claim GetAHPUnit()
		{
			return (from roster in AHPContext.Rosters
					where roster.Active && roster.UserID == requestingUserId
					select roster)
					.AsEnumerable()
					.Select(roster => new Claim(AHPClaimTypes.Unit, roster.Unit))
					.DefaultIfEmpty(null)
					.FirstOrDefault();
		}
		private IEnumerable<Claim> GetBudgets()
		{
			if (requestingAppId == null)
				return null;

			return (from budget in UserProvContext.Budgets
					join access in UserProvContext.GrantedAccesses
						on budget.BudgetId equals access.BudgetId
					where budget.Enabled
						&& access.AppId == requestingAppId
						&& access.SecurityInstance.Directory.UserId == requestingUserId
					select budget)
					.AsEnumerable()
					.Select(budget => new Claim(AHTDClaimTypes.Budget, budget.Number));
		}
		private Claim GetDirectoryID()
		{
			return (from employee in UserProvContext.Directories
					where employee.EmployeeStatus == AHTD.Security.Web.Model.EmployeeStatus.Active
						&& employee.UserId == requestingUserId
					select employee)
					.AsEnumerable()
					.Select(employee => new Claim(AHTDClaimTypes.DirectoryID, employee.DirectoryId.ToString()))
					.DefaultIfEmpty(null)
					.FirstOrDefault();
		}
		private Claim GetEmpID()
		{
			return (from employee in UserProvContext.Directories
					where employee.EmployeeStatus == AHTD.Security.Web.Model.EmployeeStatus.Active
						&& employee.UserId == requestingUserId
					select employee)
					.AsEnumerable()
					.Select(employee => new Claim(AHTDClaimTypes.EmpID, employee.EmployeeNumber))
					.DefaultIfEmpty(null)
					.FirstOrDefault();
		}

        private Claim GetSupervisoryCode()
        {
            var emp = (from employee in UserProvContext.Directories
                             where employee.EmployeeStatus == AHTD.Security.Web.Model.EmployeeStatus.Active
                                   && employee.UserId == requestingUserId
                             select employee).FirstOrDefault();
            if (emp == null)
                return null;

            return UserProvContext.SupervisorLookups.Where(s => s.EmployeeNumber == emp.EmployeeNumber)
                    .AsEnumerable()
					.Select(s => new Claim(AHTDClaimTypes.SupervisoryCode, s.SupervisoryCode))
					.DefaultIfEmpty(null)
					.FirstOrDefault();
		}
		private IEnumerable<Claim> GetLocations()
		{
			if (requestingAppId == null)
				return null;

			return (from location in UserProvContext.Locations
					join access in UserProvContext.GrantedAccesses
						on location.LocationId equals access.LocationId
					where location.Enabled
						&& access.AppId == requestingAppId
						&& access.SecurityInstance.Directory.UserId == requestingUserId
					select location)
					.AsEnumerable()
					.Select(location => new Claim(AHTDClaimTypes.Location, location.ShortDescription));
		}
		private IEnumerable<Claim> GetBudgetLocations()
		{
			if (requestingAppId == null)
				return null;

			return (from access in UserProvContext.GrantedAccesses
					join budget in UserProvContext.Budgets
						on access.BudgetId equals budget.BudgetId
					join location in UserProvContext.Locations
						on access.LocationId equals location.LocationId
					where budget.Enabled && location.Enabled
						&& access.AppId == requestingAppId
						&& access.SecurityInstance.Directory.UserId == requestingUserId
					select new { BudgetNumber = budget.Number, LocationName = location.ShortDescription })
					.AsEnumerable()
					.Select(budgetloc => new Claim(AHTDClaimTypes.BudgetLocation,
					   AHTDClaimTypes.GetBudgetLocation(budgetloc.BudgetNumber, budgetloc.LocationName)));
		}
		private IEnumerable<Claim> GetRoleBudgetLocations()
		{
			if (requestingAppId == null)
				return null;

			return (from access in UserProvContext.GrantedAccesses
					join role in UserProvContext.Roles
						on access.RoleId equals role.RoleId
					join budget in UserProvContext.Budgets
						on access.BudgetId equals budget.BudgetId
					join location in UserProvContext.Locations
						on access.LocationId equals location.LocationId
					where role.Enabled && budget.Enabled && location.Enabled
						&& access.AppId == requestingAppId
						&& access.SecurityInstance.Directory.UserId == requestingUserId
					select new { RoleName = role.RoleName, BudgetNumber = budget.Number, LocationName = location.ShortDescription })
					.AsEnumerable()
					.Select(rolebudgetloc => new Claim(AHTDClaimTypes.RoleBudgetLocation,
					   AHTDClaimTypes.GetRoleBudgetLocation(rolebudgetloc.RoleName, rolebudgetloc.BudgetNumber, rolebudgetloc.LocationName)));
		}

		private int? GetAppID()
		{
			if (String.IsNullOrEmpty(requestingAppName))
				return null;

			return (from application in UserProvContext.Applications
					where application.Enabled && application.AppName == requestingAppName
					select application)
					.Select(application => application.AppId)
					.Single();
		}
		private IEnumerable<string> GetClaimTypes()
		{
			if (requestingAppId == null)
				return null;

			return from claim in UserProvContext.AppClaims
				   where claim.Enabled && claim.AppId == requestingAppId
				   select claim.Claim.ClaimType;
		}
	}
}