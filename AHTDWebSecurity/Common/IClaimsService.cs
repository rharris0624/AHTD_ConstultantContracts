using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AHTD.Security.Common
{
	/// <summary>
	/// Defines the claims service contract interface.
	/// </summary>
	[ServiceContract(Namespace=Constants.Namespaces.ServiceContractNS)]
	public interface IClaimsService
	{
		/// <summary>
		/// Gets client claims data for active clients.
		/// </summary>
		/// <returns>A generic list of client claims.</returns>
		[OperationContract]
		List<ClientClaim> GetClaims();
	}
}
