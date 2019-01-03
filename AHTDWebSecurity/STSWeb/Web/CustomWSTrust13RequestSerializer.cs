using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

using Microsoft.IdentityModel.Protocols.WSTrust;

using AHTD.Security.Common;

namespace AHTD.Security.Web
{
	/// <summary>
	/// A custom implementation of the WSTrust13RequestSerializer class that
	/// supports reading CustomRequestSecurityToken RST objects.
	/// </summary>
	public class CustomWSTrust13RequestSerializer : WSTrust13RequestSerializer
	{
		/// <summary>
		/// Creates the request security token as a CustomRequestSecurityToken.
		/// </summary>
		/// <returns>A CustomRequestSecurityToken.</returns>
		public override RequestSecurityToken CreateRequestSecurityToken()
		{
			return new CustomRequestSecurityToken();
		}
		/// <summary>
		/// Overrides the ReadXmlElement method by adding support for
		/// CustomRequestSecurityToken properties.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="rst">The RST object.</param>
		/// <param name="context">The serialization context.</param>
		public override void ReadXmlElement(XmlReader reader, RequestSecurityToken rst, WSTrustSerializationContext context)
		{
			if (reader == null)
				throw new ArgumentNullException("reader");
			if (rst == null)
				throw new ArgumentNullException("rst");
			if (context == null)
				throw new ArgumentNullException("context");

			if (rst is CustomRequestSecurityToken
				&& reader.IsStartElement(Constants.LocalNames.RSTAppName, Constants.Namespaces.RSTNS))
			{
				try
				{
					(rst as CustomRequestSecurityToken).AppName = reader.ReadElementContentAsString();
				}
				catch (FormatException)
				{
					// ignore the bad/null value
				}
				catch (InvalidCastException)
				{
					// ignore the bad/null value
				}
				catch (XmlException)
				{
					// ignore the bad/null value
				}
			}
			if (rst is CustomRequestSecurityToken
				&& reader.IsStartElement(Constants.LocalNames.RSTUserID, Constants.Namespaces.RSTNS))
			{
				try
				{
					(rst as CustomRequestSecurityToken).UserID = reader.ReadElementContentAsString();
				}
				catch (FormatException)
				{
					// ignore the bad/null value
				}
				catch (InvalidCastException)
				{
					// ignore the bad/null value
				}
				catch (XmlException)
				{
					// ignore the bad/null value
				}
			}
			else
			{
				base.ReadXmlElement(reader, rst, context);
			}
		}
	}
}