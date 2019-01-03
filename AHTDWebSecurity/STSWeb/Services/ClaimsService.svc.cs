using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Microsoft.IdentityModel.Claims;

using AHTD.Security.Common;

namespace AHTD.Security.Web.Services
{
	/// <summary>
	/// Provides a means for active clients to get client claims data.
	/// </summary>
	[ServiceBehavior(
		InstanceContextMode=InstanceContextMode.PerCall,
		ConcurrencyMode=ConcurrencyMode.Multiple,
		UseSynchronizationContext=false)]
	public class ClaimsService : IClaimsService
	{
		/// <summary>
		/// Gets client claims data for active clients.
		/// </summary>
		/// <returns>A generic list of client claims.</returns>
		public List<ClientClaim> GetClaims()
		{
			List<ClientClaim> clientClaims = new List<ClientClaim>();

			IClaimsPrincipal p = System.Threading.Thread.CurrentPrincipal as IClaimsPrincipal;
			foreach (IClaimsIdentity identity in p.Identities)
			{
				if (identity.Claims != null)
				{
					foreach (Claim c in identity.Claims)
					{
						clientClaims.Add(NewClientClaimFromClaim(c));
					}
				}
			}

			return clientClaims;
		}

		private ClientClaim NewClientClaimFromClaim(Claim claim)
		{
			if (claim == null)
				return null;
			return new ClientClaim(claim.ClaimType, claim.Value, claim.ValueType);
		}
	}
}
