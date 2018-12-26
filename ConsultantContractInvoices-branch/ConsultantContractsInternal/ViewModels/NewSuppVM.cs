using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ConsultantContractsInternal.Models;

namespace ConsultantContractsInternal.ViewModels
{
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
	public class NewSuppVM
	{
		public DateTime SuppAgreementDate { get; set; }
		public DateTime CompletionDate { get; set; }
		public decimal? ContractCeiling { get; set; }
		public int ContractCode { get; set; }
		public string SuppNo { get; set; }
		public decimal? T1SvcsCeiling { get; set; }
		public decimal? T1FixedFeeMax { get; set; }
		public decimal? HomeOfficeOverheadRateMax { get; set; }
		public decimal? FCCM { get; set; }
		public decimal? T2SvcsCeiling { get; set; }
		public decimal? T2FixedFeeMax { get; set; }
		public decimal? FieldServiceOverheadRateMax { get; set; }
		public decimal? Multiplier { get; set; }
		public string Remarks { get; set; }

		public bool ContractCeilingChanged { get; set; }
		public bool T1SvcsCeilingChanged { get; set; }
		public bool T1FixedFeeMaxChanged { get; set; }
		public bool HomeOfficeOverheadRateMaxChanged { get; set; }
		public bool FCCMChanged { get; set; }
		public bool T2SvcsCeilingChanged { get; set; }
		public bool T2FixedFeeMaxChanged { get; set; }
		public bool FieldServiceOverheadRateMaxChanged { get; set; }
		public bool MultiplierChanged { get; set; }

		public List<AllotmentVM> T1Allotments { get; set; }
		public List<AllotmentVM> T2Allotments { get; set; }


		public List<SalaryRateVM> SalaryRates { get; set; }
		public List<ServiceRateVM> ServiceRates { get; set; }
		public List<SuppSubConsultantVM> SubConsultants { get; set; }

		public class SuppSubConsultantVM
		{
			public int ConsultantId { get; set; }
			public decimal? ContractCeiling { get; set; }
			public decimal? T1Services { get; set; }
			public decimal? T1FixedFee { get; set; }
			public decimal? HomeOfficeOverhead { get; set; }
			public decimal? FCCM { get; set; }
			public decimal? T2Services { get; set; }
			public decimal? T2FixedFee { get; set; }
			public decimal? FieldServiceOverhead { get; set; }
			public decimal? Multiplier { get; set; }

			public bool ContractCeilingChanged { get; set; }
			public bool T1ServicesChanged { get; set; }
			public bool T1FixedFeeChanged { get; set; }
			public bool HomeOfficeOverheadChanged { get; set; }
			public bool T2ServicesChanged { get; set; }
			public bool T2FixedFeeChanged { get; set; }
			public bool FieldServiceOverheadChanged { get; set; }
			public bool MultiplierChanged { get; set; }
			public bool FCCMChanged { get; set; }

			public List<SalaryRateVM> SalaryRates { get; set; }
			public List<ServiceRateVM> ServiceRates { get; set; }
			public ContractSubConsultant ToContractSubConsultant()
			{
				var c = new ContractSubConsultant
				{
					SubConsultantId = ConsultantId,
					ContractCeiling = ContractCeiling,
					ContractCeilingOrig = ContractCeiling,
					T1SvcsCeiling = T1Services,
					T1SvcsCeilingOrig = T1Services,
					T1FixedFeeMax = T1FixedFee,
					T1FixedFeeMaxOrig = T1FixedFee,
					HomeOfficeOverheadRateMax = HomeOfficeOverhead,
					HomeOfficeOverheadRateMaxOrig = HomeOfficeOverhead,
					FCCM = FCCM,
					FCCMOrig = FCCM,
					T2SvcsCeiling = T2Services,
					T2SvcsCeilingOrig = T2Services,
					T2FixedFeeMax = T2FixedFee,
					T2FixedFeeMaxOrig = T2FixedFee,
					FieldServiceOverheadRateMax = FieldServiceOverhead,
					FieldServiceOverheadRateMaxOrig = FieldServiceOverhead,
					Multiplier = Multiplier,
					MultiplierOrig = Multiplier
				};

				return c;
			}
			public SuppSubConsultantInfo ToSuppSubConsultant()
			{
				var c = new SuppSubConsultantInfo
				{
					SubConsultantId = ConsultantId,
					ContractCeiling = ContractCeilingChanged ? ContractCeiling : null,
					T1SvcsCeiling = T1ServicesChanged ? T1Services : null,
					T1FixedFeeMax = T1FixedFeeChanged ? T1FixedFee : null,
					HomeOfficeOverheadRateMax = HomeOfficeOverheadChanged ? HomeOfficeOverhead : null,
					FCCM = FCCMChanged ? FCCM : null,
					T2SvcsCeiling = T2ServicesChanged ? T2Services : null,
					T2FixedFeeMax = T2FixedFeeChanged ? T2FixedFee : null,
					FieldServiceOverheadRateMax = FieldServiceOverheadChanged ? FieldServiceOverhead : null,
					Multiplier = MultiplierChanged ? Multiplier : null
				};

				return c;
			}
		}
		public class SalaryRateVM
		{
			public string JobTitle { get; set; }
			public decimal RateMin { get; set; }
			public decimal RateMax { get; set; }
			public bool existingRecord { get; set; }
			public bool PendingDelete { get; set; }

			public bool RateMinChanged { get; set; }
			public bool RateMaxChanged { get; set; }

			public SuppSalaryRate ToSalaryRate()
			{
				var s = new SuppSalaryRate();


				s.JobTitle = JobTitle;
				s.RateMin = RateMin;
				s.RateMax = RateMax;
				s.Deleted = PendingDelete;
				return s;
			}

			public SuppSubConsultantSalaryRate ToSubConSalaryRate()
			{
				var s = new SuppSubConsultantSalaryRate();
		
				s.JobTitle = JobTitle;
				s.RateMin = RateMin;
				s.RateMax = RateMax;
				s.Deleted = PendingDelete;

				return s;
			}
		}

		public class ServiceRateVM
		{
			public string ServiceName { get; set; }
			public decimal RateMin { get; set; }
			public decimal RateMax { get; set; }
			public bool existingRecord { get; set; }
			public bool PendingDelete { get; set; }

			public bool RateMinChanged { get; set; }
			public bool RateMaxChanged { get; set; }

			public SuppServiceRate ToServiceRate()
			{
				var s = new SuppServiceRate();
				
				s.ServiceName = ServiceName;
				s.RateMin = RateMin;
				s.RateMax = RateMax;
				s.Deleted = PendingDelete;
				return s;
			}

			public SuppSubConsultantServiceRate ToSubConServiceRate()
			{
				var s = new SuppSubConsultantServiceRate();
				
				s.ServiceName = ServiceName;
				s.RateMin = RateMin;
				s.RateMax = RateMax;
				s.Deleted = PendingDelete;

				return s;
			}
		}

		public class AllotmentVM
		{
			public string Func { get; set; }
			public string FAP { get; set; }
			public int? FundingPriority { get; set; }
			public decimal AllottedAmount { get; set; }
			public bool AllottedAmountChanged { get; set; }
			//public bool EditValue { get; set; }

			public SuppAllotment ToAllotment()
			{
				var a = new SuppAllotment();
				a.Func = Func;
				a.FAP = FAP;
				a.AllottedAmt = AllottedAmount;

				return a;
			}
		}


		internal SuppAgreement PopulateSuppAgreement(SuppAgreement s)
		{
			s.SuppAgreementDate = SuppAgreementDate;
			s.CompletionDate = CompletionDate;
			s.ContractCode = ContractCode;
			s.SuppNo = SuppNo;
			if (ContractCeilingChanged)
			{
				s.ContractCeiling = ContractCeiling;
			}
			if (T1SvcsCeilingChanged)
			{
				s.T1SvcsCeiling = T1SvcsCeiling;
			}
			if (T1FixedFeeMaxChanged)
			{
				s.T1FixedFeeMax = T1FixedFeeMax;
			}
			if (HomeOfficeOverheadRateMaxChanged)
			{
				s.HomeOfficeOverheadRateMax = HomeOfficeOverheadRateMax;
			}
			if (FCCMChanged)
			{
				s.FCCM = FCCM;
			}
			if (T2SvcsCeilingChanged)
			{
				s.T2SvcsCeiling = T2SvcsCeiling;
			}
			if (T2FixedFeeMaxChanged)
			{
				s.T2FixedFeeMax = T2FixedFeeMax;
			}
			if (FieldServiceOverheadRateMaxChanged)
			{
				s.FieldServiceOverheadRateMax = FieldServiceOverheadRateMax;
			}
			if (MultiplierChanged)
			{
				s.Multiplier = Multiplier;
			}
			s.Remarks = Remarks;

			return s;
		}

			
	}
}