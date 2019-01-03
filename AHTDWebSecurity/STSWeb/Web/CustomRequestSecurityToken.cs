using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.IdentityModel.Protocols.WSTrust;

using AHTD.Security.Common;

namespace AHTD.Security.Web
{
	/// <summary>
	/// A custom implementation of the RequestSecurityToken class that
	/// supports additional custom properties.
	/// </summary>
	public class CustomRequestSecurityToken : RequestSecurityToken
	{
		internal const string DefaultAppNameValue = "DEFAULT";
		internal const string DefaultUserIDValue = "DEFAULT";

		/// <summary>
		/// Gets or sets the Application Name context for this RST.
		/// </summary>
		public string AppName { get; set; }
		/// <summary>
		/// Gets or sets the User ID context for this RST.
		/// </summary>
		public string UserID { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomRequestSecurityToken"/> class.
		/// </summary>
		public CustomRequestSecurityToken()
			: base()
		{
			AppName = DefaultAppNameValue;
			UserID = DefaultUserIDValue;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomRequestSecurityToken"/> class.
		/// </summary>
		/// <param name="appName">The application name context value to send with the request.</param>
		public CustomRequestSecurityToken(string appName)
			: this()
		{
			AppName = appName;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomRequestSecurityToken"/> class.
		/// </summary>
		/// <param name="appName">The application name context value to send with the request.</param>
		/// <param name="userID">The user ID to send with the request, typically for spoofing.</param>
		public CustomRequestSecurityToken(string appName, string userID)
			: this(appName)
		{
			UserID = userID;
		}
	}
}