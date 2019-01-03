using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHTD.Security.Web.Model
{
	public static class EmployeeStatus
	{
		public const string Active = "AC";
		public const string Terminated = "TM";
		public const string Retired = "RT";
		public const string RetireeBeneficiary = "RB";
		public const string Deferred = "DF";
		public const string RetirementAnnuityStopped = "RS";
		public const string AlternatePayee = "AP";
	}
}