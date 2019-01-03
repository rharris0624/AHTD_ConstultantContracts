using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

using AHTD.Security.Common;

namespace AHTD.Security.Web
{
	/// <summary>
	/// A base class for info related to a user's claims data.
	/// </summary>
	/// <remarks>
	/// Inherit from this class in your web app and extend with values that are
	/// relavent, such as perhaps employee number or role.
	/// </remarks>
	public abstract class UserInfoBase
	{
		private const string LocalHostName = "localhost";

		/// <summary>
		/// Gets a value indicating whether this app is running locally.
		/// </summary>
		protected static bool IsRunningLocally
		{
			get
			{
				if (HttpContext.Current == null || HttpContext.Current.Request == null)
					return false;
				return HttpContext.Current.Request.Url.Host.Equals(LocalHostName, StringComparison.OrdinalIgnoreCase)
					&& HttpContext.Current.Request.ServerVariables["SERVER_NAME"].Equals(LocalHostName, StringComparison.OrdinalIgnoreCase);
			}
		}

		/// <summary>
		/// Gets the current user's Windows account name.
		/// </summary>
		public static string WindowsAccountName
		{
			get
			{
				return GetSpecificClaimValue(StandardClaimTypes.WindowsAccountName);
			}
		}

		/// <summary>
		/// Gets a value for the specific claim.
		/// </summary>
		/// <param name="claimType">Type of the claim.</param>
		/// <returns></returns>
		protected static string GetSpecificClaimValue(string claimType)
		{
			ClientClaim specificClaim = null;

			foreach (ClientClaim claim in Authorization.ClientClaims)
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
	}
}
