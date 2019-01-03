using System;

namespace AHTD.Security.Common
{
	/// <summary>
	/// Provides constants for AHTD custom claim types.
	/// </summary>
	public static class AHTDClaimTypes
	{
		/// <summary>
		/// Directory ID
		/// </summary>
		public const string DirectoryID = "http://schemas.arkansashighways.com/ws/identity/claims/directoryid";
		/// <summary>
		/// Employee ID
		/// </summary>
		public const string EmpID = "http://schemas.arkansashighways.com/ws/identity/claims/empid";
		/// <summary>
		/// Budget #
		/// </summary>
		public const string Budget = "http://schemas.arkansashighways.com/ws/identity/claims/budget";
		/// <summary>
		/// Location, such as district or division
		/// </summary>
		public const string Location = "http://schemas.arkansashighways.com/ws/identity/claims/location";
		/// <summary>
		/// Budget # under a location
		/// </summary>
		public const string BudgetLocation = "http://schemas.arkansashighways.com/ws/identity/claims/budgetlocation";
		/// <summary>
		/// Budget # under a location for a given role
		/// </summary>
		public const string RoleBudgetLocation = "http://schemas.arkansashighways.com/ws/identity/claims/rolebudgetlocation";

        /// <summary>
        /// Supervisory Code
        /// <example>A, B, C, D</example>
        /// </summary>
        public const string SupervisoryCode = "http://schemas.arkansashighways.com/ws/identity/claims/supervisorycode";
		/// <summary>
		/// Combines a budget and location to form a standardized value to
		/// compare against a value for claims of type
		/// <see cref="P:BudgetLocation"/>.
		/// </summary>
		/// <param name="budget">A budget number.</param>
		/// <param name="location">A location.</param>
		/// <returns>A <see cref="T:System.String"/>.</returns>
		public static string GetBudgetLocation(string budget, string location)
		{
			// Location:Budget
			return String.Format("{1}:{0}", budget, location);
		}
		/// <summary>
		/// Combines a role, budget, and location to form a standardized value
		/// to compare against a value for claims of type
		/// <see cref="P:RoleBudgetLocation"/>.
		/// </summary>
		/// <param name="roleName">A role name.</param>
		/// <param name="budget">A budget number.</param>
		/// <param name="location">A location.</param>
		/// <returns>A <see cref="T:System.String"/>.</returns>
		public static string GetRoleBudgetLocation(string roleName, string budget, string location)
		{
			// Role:Location:Budget
			return String.Format("{0}:{2}:{1}", roleName, budget, location);
		}
	}
	/// <summary>
	/// Provides constants for AHP custom claim types.
	/// </summary>
	public static class AHPClaimTypes
	{
		/// <summary>
		/// AHP Rank
		/// </summary>
		public const string Rank = "http://schemas.arkansashighways.com/ws/identity/claims/ahprank";
		/// <summary>
		/// AHP Rank abbreviation
		/// </summary>
		public const string RankAbbrev = "http://schemas.arkansashighways.com/ws/identity/claims/ahprankabbr";
		/// <summary>
		/// AHP District
		/// </summary>
		public const string District = "http://schemas.arkansashighways.com/ws/identity/claims/ahpdistrict";
		/// <summary>
		/// AHP Unit
		/// </summary>
		public const string Unit = "http://schemas.arkansashighways.com/ws/identity/claims/ahpunit";
	}
	/// <summary>
	/// Provides constants for industry standard claim types.
	/// </summary>
	public static class StandardClaimTypes
	{
		/// <summary>
		/// Actor
		/// </summary>
		public const string Actor = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor";
		/// <summary>
		/// Anonymous
		/// </summary>
		public const string Anonymous = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous";
		/// <summary>
		/// Authentication
		/// </summary>
		public const string Authentication = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authentication";
		/// <summary>
		/// AuthenticationInstant
		/// </summary>
		public const string AuthenticationInstant = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant";
		/// <summary>
		/// AuthenticationMethod
		/// </summary>
		public const string AuthenticationMethod = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod";
		/// <summary>
		/// AuthorizationDecision
		/// </summary>
		public const string AuthorizationDecision = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision";
		/// <summary>
		/// ClaimType2005Namespace
		/// </summary>
		public const string ClaimType2005Namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";
		/// <summary>
		/// ClaimType2009Namespace
		/// </summary>
		public const string ClaimType2009Namespace = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims";
		/// <summary>
		/// ClaimTypeNamespace
		/// </summary>
		public const string ClaimTypeNamespace = "http://schemas.microsoft.com/ws/2008/06/identity/claims";
		/// <summary>
		/// CookiePath
		/// </summary>
		public const string CookiePath = "http://schemas.microsoft.com/ws/2008/06/identity/claims/cookiepath";
		/// <summary>
		/// Country
		/// </summary>
		public const string Country = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country";
		/// <summary>
		/// DateOfBirth
		/// </summary>
		public const string DateOfBirth = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth";
		/// <summary>
		/// DenyOnlyPrimaryGroupSid
		/// </summary>
		public const string DenyOnlyPrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid";
		/// <summary>
		/// DenyOnlyPrimarySid
		/// </summary>
		public const string DenyOnlyPrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid";
		/// <summary>
		/// DenyOnlySid
		/// </summary>
		public const string DenyOnlySid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid";
		/// <summary>
		/// Dns
		/// </summary>
		public const string Dns = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns";
		/// <summary>
		/// Dsa
		/// </summary>
		public const string Dsa = "http://schemas.microsoft.com/ws/2008/06/identity/claims/dsa";
		/// <summary>
		/// Email
		/// </summary>
		public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
		/// <summary>
		/// Expiration
		/// </summary>
		public const string Expiration = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration";
		/// <summary>
		/// Expired
		/// </summary>
		public const string Expired = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expired";
		/// <summary>
		/// Gender
		/// </summary>
		public const string Gender = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender";
		/// <summary>
		/// GivenName
		/// </summary>
		public const string GivenName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
		/// <summary>
		/// GroupSid
		/// </summary>
		public const string GroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid";
		/// <summary>
		/// Hash
		/// </summary>
		public const string Hash = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash";
		/// <summary>
		/// HomePhone
		/// </summary>
		public const string HomePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone";
		/// <summary>
		/// IsPersistent
		/// </summary>
		public const string IsPersistent = "http://schemas.microsoft.com/ws/2008/06/identity/claims/ispersistent";
		/// <summary>
		/// Locality
		/// </summary>
		public const string Locality = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality";
		/// <summary>
		/// MobilePhone
		/// </summary>
		public const string MobilePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone";
		/// <summary>
		/// Name
		/// </summary>
		public const string Name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
		/// <summary>
		/// NameIdentifier
		/// </summary>
		public const string NameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
		/// <summary>
		/// OtherPhone
		/// </summary>
		public const string OtherPhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone";
		/// <summary>
		/// PostalCode
		/// </summary>
		public const string PostalCode = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode";
		/// <summary>
		/// PPID
		/// </summary>
		public const string PPID = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/privatepersonalidentifier";
		/// <summary>
		/// PrimaryGroupSid
		/// </summary>
		public const string PrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid";
		/// <summary>
		/// PrimarySid
		/// </summary>
		public const string PrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid";
		/// <summary>
		/// Role
		/// </summary>
		public const string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
		/// <summary>
		/// Rsa
		/// </summary>
		public const string Rsa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";
		/// <summary>
		/// SerialNumber
		/// </summary>
		public const string SerialNumber = "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber";
		/// <summary>
		/// Sid
		/// </summary>
		public const string Sid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid";
		/// <summary>
		/// Spn
		/// </summary>
		public const string Spn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn";
		/// <summary>
		/// StateOrProvince
		/// </summary>
		public const string StateOrProvince = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince";
		/// <summary>
		/// StreetAddress
		/// </summary>
		public const string StreetAddress = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress";
		/// <summary>
		/// Surname
		/// </summary>
		public const string Surname = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
		/// <summary>
		/// System
		/// </summary>
		public const string System = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system";
		/// <summary>
		/// Thumbprint
		/// </summary>
		public const string Thumbprint = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint";
		/// <summary>
		/// Upn
		/// </summary>
		public const string Upn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn";
		/// <summary>
		/// Uri
		/// </summary>
		public const string Uri = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri";
		/// <summary>
		/// UserData
		/// </summary>
		public const string UserData = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata";
		/// <summary>
		/// Version
		/// </summary>
		public const string Version = "http://schemas.microsoft.com/ws/2008/06/identity/claims/version";
		/// <summary>
		/// Webpage
		/// </summary>
		public const string Webpage = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage";
		/// <summary>
		/// WindowsAccountName
		/// </summary>
		public const string WindowsAccountName = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname";
		/// <summary>
		/// X500DistinguishedName
		/// </summary>
		public const string X500DistinguishedName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname";
	}
}