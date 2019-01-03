using System;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Web.Configuration;
using System.Xml;

using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Protocols.WSFederation;
using Microsoft.IdentityModel.Protocols.WSFederation.Metadata;
using Microsoft.IdentityModel.Protocols.WSIdentity;
using Microsoft.IdentityModel.SecurityTokenService;

using AHTD.Security.Common;

namespace AHTD.Security.Web
{
	/// <summary>
	/// Summary description for Common
	/// </summary>
	public static class Common
	{
		/// <summary>
		/// ServerName configuration setting property name.
		/// </summary>
		public const string ServerName = "ServerName";
		/// <summary>
		/// IssuerName configuration setting property name.
		/// </summary>
		public const string IssuerName = "IssuerName";
		/// <summary>
		/// SigningCertificateName configuration setting property name.
		/// </summary>
		public const string SigningCertificateName = "SigningCertificateName";
		/// <summary>
		/// EncryptingCertificateName configuration setting property name.
		/// </summary>
		public const string EncryptingCertificateName = "EncryptingCertificateName";

		/// <summary>
		/// Generate a sample MetadataBase.
		/// </summary>
		/// <remarks>
		/// In a production system this would be generated from the STS configuration.
		/// </remarks>
		public static MetadataBase GetFederationMetadata()
		{
			string serverHostName = WebConfigurationManager.AppSettings[ServerName];
			string endpointId = String.Format("http://{0}/stsweb", serverHostName);
			EntityDescriptor metadata = new EntityDescriptor();
			metadata.EntityId = new EntityId(endpointId);

			// Define the signing key
			string signingCertificateName = WebConfigurationManager.AppSettings[SigningCertificateName];
			X509Certificate2 cert = CertificateUtil.GetCertificate(StoreName.My, StoreLocation.LocalMachine, signingCertificateName);
			
			metadata.SigningCredentials = new X509SigningCredentials(cert);

			// Create role descriptor for security token service
			SecurityTokenServiceDescriptor stsRole = new SecurityTokenServiceDescriptor();
			stsRole.ProtocolsSupported.Add(new Uri(WSFederationMetadataConstants.Namespace));
			metadata.RoleDescriptors.Add(stsRole);

			// Add contact names
			ContactPerson person;
			person = new ContactPerson(ContactType.Administrative);
			person.GivenName = "Lesa";
			person.Surname = "Frymark";
			person.Company = "AHTD";
            person.EmailAddresses.Add("Lesa.Frymark@ahtd.ar.gov");
			person.TelephoneNumbers.Add("501-569-2491");
			stsRole.Contacts.Add(person);
			person = new ContactPerson(ContactType.Technical);
			person.GivenName = "Ben";
			person.Surname = "Meadors";
			person.Company = "AHTD";
			person.EmailAddresses.Add("Ben.Meadors@ahtd.ar.gov");
			person.TelephoneNumbers.Add("501-569-2042");
			stsRole.Contacts.Add(person);

			// Include key identifier for signing key in metadata
			SecurityKeyIdentifierClause clause = new X509RawDataKeyIdentifierClause(cert);
			SecurityKeyIdentifier ski = new SecurityKeyIdentifier(clause);
			KeyDescriptor signingKey = new KeyDescriptor(ski);
			signingKey.Use = KeyType.Signing;
			stsRole.Keys.Add(signingKey);

			// Add endpoints
			EndpointAddress endpointAddress = null;
			string activeSTSUrl = String.Format("http://{0}/stsweb/Services/STSService.svc", serverHostName);
			string passiveSTSUrl = String.Format("http://{0}/stsweb", serverHostName);
			endpointAddress = new EndpointAddress(new Uri(activeSTSUrl),
													null,
													null, GetMetadataReader(activeSTSUrl), null);
			stsRole.SecurityTokenServiceEndpoints.Add(endpointAddress);
			endpointAddress = new EndpointAddress(new Uri(passiveSTSUrl),
													null,
													null, GetMetadataReader(passiveSTSUrl), null);
			stsRole.SecurityTokenServiceEndpoints.Add(endpointAddress);

			// Add a collection of offered claims
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(ClaimTypes.WindowsAccountName, "WindowsAccountName", "The Windows logon domain and user ID of the subject."));
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(ClaimTypes.Role, "Role", "Any roles the subject falls under within the scope of the relying party."));
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(ClaimTypes.GivenName, "GivenName", "First name of the subject."));
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(ClaimTypes.Surname, "Surname", "Last name of the subject."));
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(ClaimTypes.Email, "Email", "Email address of the subject."));
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(AHPClaimTypes.District, "AHPDistrict", "The AHP district for the subject."));
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(AHPClaimTypes.Rank, "AHPRank", "The rank name for the subject."));
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(AHPClaimTypes.RankAbbrev, "AHPRankAbbrev", "The rank abbreviation for the subject."));
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(AHPClaimTypes.Unit, "AHPUnit", "The unit number for the subject."));
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(AHTDClaimTypes.Budget, "Budget", "The budget number for the subject."));
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(AHTDClaimTypes.DirectoryID, "DirectoryID", "The directory id number for the subject."));
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(AHTDClaimTypes.EmpID, "EmpID", "The employee number for the subject."));
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(AHTDClaimTypes.Location, "Location", "The location of the subject."));
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(AHTDClaimTypes.BudgetLocation, "BudgetLocation", "The budget number under a location for the subject."));
			stsRole.ClaimTypesOffered.Add(new DisplayClaim(AHTDClaimTypes.RoleBudgetLocation, "RoleBudgetLocation", "The budget number under a location for a given role of the subject."));

			return metadata;
		}

		/// <summary>
		/// Create a reader to provide simulated Metadata endpoint configuration element
		/// </summary>
		/// <param name="url">The endpoint URL.</param>
		static XmlDictionaryReader GetMetadataReader(string url)
		{
			MetadataSet metadata = new MetadataSet();
			MetadataReference mexReferece = new MetadataReference(new EndpointAddress(url + "/mex"), AddressingVersion.WSAddressing10);
			MetadataSection refSection = new MetadataSection(MetadataSection.MetadataExchangeDialect, null, mexReferece);
			metadata.MetadataSections.Add(refSection);

			byte[] metadataSectionBytes;
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				using (XmlTextWriter textWriter = new XmlTextWriter(stringWriter))
				{
					metadata.WriteTo(textWriter);
					textWriter.Flush();
					stringWriter.Flush();
					metadataSectionBytes = stringWriter.Encoding.GetBytes(stringBuilder.ToString());
				}
			}

			return XmlDictionaryReader.CreateTextReader(metadataSectionBytes, XmlDictionaryReaderQuotas.Max);
		}
	} 
}
