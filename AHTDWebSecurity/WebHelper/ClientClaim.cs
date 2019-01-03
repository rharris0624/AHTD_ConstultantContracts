using System;

namespace AHTD.Security.Common
{
	/// <summary>
	/// Represents claim data usable by an app client.
	/// </summary>
	[Serializable]
	public class ClientClaim
	{
		/// <summary>
		/// Gets or sets the type of the claim.
		/// </summary>
		public string ClaimType { get; set; }
		/// <summary>
		/// Gets or sets the value of the claim.
		/// </summary>
		public string Value { get; set; }
		/// <summary>
		/// Gets or sets the type of the value.
		/// </summary>
		public string ValueType { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ClientClaim"/> class.
		/// </summary>
		public ClientClaim()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ClientClaim"/> class.
		/// </summary>
		/// <param name="claimType">Type of the claim.</param>
		/// <param name="value">The value.</param>
		public ClientClaim(string claimType, string value)
			: this()
		{
			ClaimType = claimType;
			Value = value;
			ValueType = value.GetType().ToString();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ClientClaim"/> class.
		/// </summary>
		/// <param name="claimType">Type of the claim.</param>
		/// <param name="value">The value.</param>
		/// <param name="valueType">Type of the value.</param>
		public ClientClaim(string claimType, string value, string valueType)
			: this(claimType, value)
		{
			ValueType = valueType;
		}
	}
}