using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Permissions;
using System.Text;
using System.Web;

using AHTD.Security.Common;

namespace AHTD.Security.Web
{
	/// <summary>
	/// Provides static methods to facilitate answering authorization questions
	/// in web code. Manages the retrieval, caching, and searching of client
	/// claims data.
	/// </summary>
	public static class Authorization
	{
		private static readonly List<ClientClaim> EmptyClaims = new List<ClientClaim>();

		private static List<ClientClaim> _clientClaims
		{
			get
			{
				if (HttpContext.Current == null || HttpContext.Current.Session == null)
					return EmptyClaims;

				if (HttpContext.Current.Session["ClientClaims"] == null
					|| HttpContext.Current.Session["ClientClaims"].GetType() != typeof(List<ClientClaim>))
				{
					_clientClaims = new List<ClientClaim>();
				}

				return (HttpContext.Current.Session["ClientClaims"] as List<ClientClaim>);
			}
			set
			{
				HttpContext.Current.Session["ClientClaims"] = value;
			}
		}
		private static string _actualRole
		{
			get
			{
				if (HttpContext.Current == null || HttpContext.Current.Session == null)
					return String.Empty;

				if (HttpContext.Current.Session["ActualRole"] == null
					|| HttpContext.Current.Session["ActualRole"].GetType() != typeof(string))
				{
					_actualRole = String.Empty;
				}

				return (string)HttpContext.Current.Session["ActualRole"];
			}
			set
			{
				HttpContext.Current.Session["ActualRole"] = value;
			}
		}
		private static string _spoofedRole
		{
			get
			{
				if (HttpContext.Current == null || HttpContext.Current.Session == null)
					return String.Empty;

				if (HttpContext.Current.Session["SpoofedRole"] == null
					|| HttpContext.Current.Session["SpoofedRole"].GetType() != typeof(string))
				{
					_spoofedRole = String.Empty;
				}

				return (string)HttpContext.Current.Session["SpoofedRole"];
			}
			set
			{
				HttpContext.Current.Session["SpoofedRole"] = value;
			}
		}

		/// <summary>
		/// Gets a static cached list of client claims.
		/// </summary>
		public static ReadOnlyCollection<ClientClaim> ClientClaims
		{
			get
			{
				return _clientClaims.AsReadOnly();
			}
		}

		/// <summary>
		/// Gets a value indicating whether or not the user has the
		/// specified windows account name.
		/// </summary>
		/// <param name="userID">A windows account name to check. Account names are not case-sensitive.</param>
		/// <returns>A <see cref="T:System.Boolean"/>.</returns>
		public static bool IsUserID(string userID)
		{
			bool found = false;

			foreach (ClientClaim claim in ClientClaims)
			{
				if (claim.ClaimType == StandardClaimTypes.WindowsAccountName
					&& String.Equals(claim.Value, userID, StringComparison.OrdinalIgnoreCase))
				{
					found = true;
					break;
				}
			}

			return found;
		}
		/// <summary>
		/// Gets a value indicating whether or not the user is in the
		/// specified role.
		/// </summary>
		/// <param name="roleName">The name of the role to check. Role names are case-sensitive.</param>
		/// <returns>A <see cref="T:System.Boolean"/>.</returns>
		public static bool IsInRole(string roleName)
		{
			bool found = false;

			foreach (ClientClaim claim in ClientClaims)
			{
				if (claim.ClaimType == StandardClaimTypes.Role
					&& claim.Value == roleName)
				{
					found = true;
					break;
				}
			}

			return found;
		}
		/// <summary>
		/// Gets a value indicating whether or not the user has the
		/// specified AHP unit number.
		/// </summary>
		/// <param name="unit">An AHP unit number to check.</param>
		/// <returns>A <see cref="T:System.Boolean"/>.</returns>
		public static bool IsAHPUnit(string unit)
		{
			bool found = false;

			foreach (ClientClaim claim in ClientClaims)
			{
				if (claim.ClaimType == AHPClaimTypes.Unit
					&& String.Equals(claim.Value, unit, StringComparison.OrdinalIgnoreCase))
				{
					found = true;
					break;
				}
			}

			return found;
		}
		/// <summary>
		/// Gets a value indicating whether or not the user has the
		/// specified employee number.
		/// </summary>
		/// <param name="employeeID">An employee number to check.</param>
		/// <returns>A <see cref="T:System.Boolean"/>.</returns>
		public static bool IsEmployeeID(string employeeID)
		{
			bool found = false;

			foreach (ClientClaim claim in ClientClaims)
			{
				if (claim.ClaimType == AHTDClaimTypes.EmpID
					&& String.Equals(claim.Value, employeeID, StringComparison.OrdinalIgnoreCase))
				{
					found = true;
					break;
				}
			}

			return found;
		}
		/// <summary>
		/// Gets a value indicating whether or not the user is in the
		/// specified budget number.
		/// </summary>
		/// <param name="budget">A budget number to check.</param>
		/// <returns>A <see cref="T:System.Boolean"/>.</returns>
		public static bool IsInBudget(string budget)
		{
			bool found = false;

			foreach (ClientClaim claim in ClientClaims)
			{
				if (claim.ClaimType == AHTDClaimTypes.Budget
					&& String.Equals(claim.Value, budget, StringComparison.OrdinalIgnoreCase))
				{
					found = true;
					break;
				}
			}

			return found;
		}
		/// <summary>
		/// Gets a value indicating whether or not the user is in the
		/// specified budget number within the specified location scope.
		/// </summary>
		/// <param name="budget">A budget number to check.</param>
		/// <param name="location">A location to scope the check within.</param>
		/// <returns>A <see cref="T:System.Boolean"/>.</returns>
		public static bool IsInBudget(string budget, string location)
		{
			bool found = false;

			foreach (ClientClaim claim in ClientClaims)
			{
				if (claim.ClaimType == AHTDClaimTypes.BudgetLocation
					&& String.Equals(claim.Value, AHTDClaimTypes.GetBudgetLocation(budget, location), StringComparison.OrdinalIgnoreCase))
				{
					found = true;
					break;
				}
			}

			return found;
		}
		/// <summary>
		/// Gets a value indicating whether or not the user is in the
		/// specified budget number within the specified location scope
		/// and the specified role.
		/// </summary>
		/// <param name="budget">A budget number to check.</param>
		/// <param name="location">A location to scope the check within.</param>
		/// <param name="roleName">A role name to scope the check for.</param>
		/// <returns>A <see cref="T:System.Boolean"/>.</returns>
		public static bool IsInBudget(string budget, string location, string roleName)
		{
			bool found = false;

			foreach (ClientClaim claim in ClientClaims)
			{
				if (claim.ClaimType == AHTDClaimTypes.RoleBudgetLocation
					&& String.Equals(claim.Value, AHTDClaimTypes.GetRoleBudgetLocation(roleName, budget, location), StringComparison.OrdinalIgnoreCase))
				{
					found = true;
					break;
				}
			}

			return found;
		}
		/// <summary>
		/// Gets a value indicating whether or not the user is in the
		/// specified location.
		/// </summary>
		/// <param name="location">A location to check.</param>
		/// <returns>A <see cref="T:System.Boolean"/>.</returns>
		public static bool IsInLocation(string location)
		{
			bool found = false;

			foreach (ClientClaim claim in ClientClaims)
			{
				if (claim.ClaimType == AHTDClaimTypes.Location
					&& String.Equals(claim.Value, location, StringComparison.OrdinalIgnoreCase))
				{
					found = true;
					break;
				}
			}

			return found;
		}

		/// <summary>
		/// Gets the value of the first claim that matches the given claim type
		/// or null if not found.
		/// </summary>
		/// <param name="claimType">Type of the claim.</param>
		/// <returns>The value of the matched claim.</returns>
		public static string GetSpecificClaimValue(string claimType)
		{
			ClientClaim specificClaim = null;

			foreach (ClientClaim claim in ClientClaims)
			{
				if (claim.ClaimType == claimType)
				{
					specificClaim = claim;
					break;
				}
			}

			if (specificClaim == null)
				return null;
			else
				return specificClaim.Value;
		}
		/// <summary>
		/// Loads the provided claims.
		/// </summary>
		/// <param name="claimsData">The claims data to use.</param>
		/// <remarks>
		/// Using this form of initialization is potentially unsafe but useful
		/// for offline or unit testing purposes. It is recommended not to use
		/// this method in production code.
		/// </remarks>
		public static void LoadClaims(IEnumerable<ClientClaim> claimsData)
		{
			_clientClaims.Clear();

			// Use assignment so that it is saved to the session
			_clientClaims = new List<ClientClaim>(claimsData);

			_actualRole = String.Empty;
			_spoofedRole = String.Empty;
		}
		///// <summary>
		///// Sets a custom Windows account name to spoof when retrieving claims.
		///// Pass null to turn off spoofing.
		///// </summary>
		///// <param name="userID">A Windows account name to spoof.</param>
		///// <remarks>
		///// <para>
		///// Calling this method will reset all claims data and cause
		///// <see cref="P:IsInitialized"/> to return false. If you previously
		///// called one of the Initialize methods, you will need to do so again
		///// or else the default deferred loading will occur.
		///// </para>
		///// <para>
		///// Calling this method requires permission to spoof users.
		///// </para>
		///// </remarks>
		//[PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = "CSD Applications")]
		//public static void SpoofUserID(string userID)
		//{
		//    _clientClaims.Clear();

		//    spoofedUserID = userID;
		//}
		/// <summary>
		/// Sets a custom role to spoof.
		/// Pass null or your original role to turn off spoofing.
		/// </summary>
		/// <param name="role">A role to spoof.</param>
		/// <remarks>
		/// <para>
		/// Calling this method requires permission to spoof roles.
		/// </para>
		/// </remarks>
		[PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = "CSD Applications")]
		public static void SpoofRole(string role)
		{
			if (String.IsNullOrEmpty(role) || role == _actualRole)
			{
				ModifyRoleClaim(_spoofedRole, _actualRole);
				_spoofedRole = String.Empty;
			}
			else
			{
				// If this is the first time spoofing, cache their actual role
				if (String.IsNullOrEmpty(_actualRole) && String.IsNullOrEmpty(_spoofedRole))
				{
					_actualRole = GetSpecificClaimValue(StandardClaimTypes.Role);
				}

				// Only make the change if it's actually different
				if (String.IsNullOrEmpty(_spoofedRole))
				{
					ModifyRoleClaim(_actualRole, role);
					_spoofedRole = role;
				}
				else if (_spoofedRole != role)
				{
					ModifyRoleClaim(_spoofedRole, role);
					_spoofedRole = role;
				}
			}
		}

		private static void ModifyRoleClaim(string oldRole, string newRole)
		{
			foreach (ClientClaim claim in ClientClaims)
			{
				if (claim.ClaimType == StandardClaimTypes.Role
					&& claim.Value == oldRole)
				{
					claim.Value = newRole;
				}
			}
		}
	}
}
