using System;
using System.Web;

using Microsoft.IdentityModel.Protocols.WSFederation.Metadata;

namespace AHTD.Security.Web
{
	/// <summary>
	/// Handles requests for federation metadata XML by generating it on the fly.
	/// </summary>
	public class FederationMetadataHandler : IHttpHandler
	{
		/// <summary>
		/// Enables processing of HTTP Web requests by a custom HttpHandler
		/// that implements the <see cref="T:System.Web.IHttpHandler"/>
		/// interface.
		/// </summary>
		/// <param name="context">An <see cref="T:System.Web.HttpContext"/>
		/// object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
		public void ProcessRequest(HttpContext context)
		{
			MetadataBase metadata = Common.GetFederationMetadata();
			MetadataSerializer serializer = new MetadataSerializer();
			serializer.WriteMetadata(context.Response.OutputStream, metadata);
			context.Response.ContentType = "text/xml";
		}

		/// <summary>
		/// Gets a value indicating whether another request can use the
		/// <see cref="T:System.Web.IHttpHandler"/> instance.
		/// </summary>
		/// <value></value>
		/// <returns>true if the <see cref="T:System.Web.IHttpHandler"/>
		/// instance is reusable; otherwise, false.
		/// </returns>
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}