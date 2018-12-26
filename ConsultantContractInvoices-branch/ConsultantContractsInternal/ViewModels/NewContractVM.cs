using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ConsultantContractsInternal.Models;

namespace ConsultantContractsInternal.ViewModels
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class NewContractVM
    {
        public string JobNo { get; set; }
        public int ConsultantId { get; set; }
        public int? RemitTo { get; set; }
        public int ContractType { get; set; }
        public string ContractStatus { get; set; }
        public DateTime? AgreementDate { get; set; }
        public DateTime? NoticeProceedDate { get; set; }
        public int AgreementType { get; set; }
        public string TaskOrderNo { get; set; }
        public int ResponsibleDivision { get; set; }
        public decimal? ContractCeiling { get; set; }
        public decimal? T1SvcsCeiling { get; set; }
        public decimal? T1FixedFeeMax { get; set; }
        public decimal? HomeOfficeOverheadRateMax { get; set; }
        public decimal? FCCM { get; set; }
        public decimal? T2SvcsCeiling { get; set; }
        public decimal? T2FixedFeeMax { get; set; }
        public decimal? FieldServiceOverheadRateMax { get; set; }
        public decimal? Multiplier { get; set; }
        public decimal? ContractCeilingOrig { get; set; }
        public decimal? T1SvcsCeilingOrig { get; set; }
        public decimal? T1FixedFeeMaxOrig { get; set; }
        public decimal? HomeOfficeOverheadRateMaxOrig { get; set; }
        public decimal? FCCMOrig { get; set; }
        public decimal? T2SvcsCeilingOrig { get; set; }
        public decimal? T2FixedFeeMaxOrig { get; set; }
        public decimal? FieldServiceOverheadRateMaxOrig { get; set; }
        public decimal? MultiplierOrig { get; set; }
        public DateTime? ScheduledCompletionDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Remarks { get; set; }

        public List<AllotmentVM> T1Allotments { get; set; }
        public List<AllotmentVM> T2Allotments { get; set; }


        public List<SalaryRateVM> SalaryRates { get; set; }
        public List<ServiceRateVM> ServiceRates { get; set; }
        public List<ContractSubConsultantVM> SubConsultants { get; set; }
        public List<WorkTypeVM> WorkTypes { get; set; }

        public class ContractSubConsultantVM
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
        }
        public class SalaryRateVM
        {
            public string JobTitle { get; set; }
            public decimal RateMin { get; set; }
            public decimal RateMax { get; set; }

            public SalaryRate ToSalaryRate()
            {
                var s = new SalaryRate
                {
                    JobTitle = JobTitle,
                    RateMin = RateMin,
                    RateMinOrig = RateMin,
                    RateMax = RateMax,
                    RateMaxOrig = RateMax
                };

                return s;
            }

            public SubConsultantSalaryRate ToSubConSalaryRate()
            {
                var s = new SubConsultantSalaryRate
                {
                    JobTitle = JobTitle,
                    RateMin = RateMin,
                    RateMinOrig = RateMin,
                    RateMax = RateMax,
                    RateMaxOrig = RateMax
                };

                return s;
            }
        }

        public class ServiceRateVM
        {
            public string ServiceName { get; set; }
            public decimal RateMin { get; set; }
            public decimal RateMax { get; set; }

            public ServiceRate ToServiceRate()
            {
                var s = new ServiceRate()
                {
                    ServiceName = ServiceName,
                    RateMin = RateMin,
                    RateMinOrig = RateMin,
                    RateMax = RateMax,
                    RateMaxOrig = RateMax
                };

                return s;
            }

            public SubConsultantServiceRate ToSubConServiceRate()
            {
                var s = new SubConsultantServiceRate
                {
                    ServiceName = ServiceName,
                    RateMin = RateMin,
                    RateMinOrig = RateMin,
                    RateMax = RateMax,
                    RateMaxOrig = RateMax
                };

                return s;
            }
        }

        public class AllotmentVM
        {
            public string Func { get; set; }
            public string FAP { get; set; }
            public int? FundingPriority { get; set; }
            public decimal FedPct { get; set; }
            public decimal StatePct { get; set; }
            public decimal StateAidPct { get; set; }
            public decimal OtherPct { get; set; }
            public decimal AllottedAmount { get; set; }

            public ContractAllotment ToAllotment()
            {
                var a = new ContractAllotment();
                a.Func = Func;
                a.FAP = FAP;
                a.FundingPriority = FundingPriority;
                a.FedPct = FedPct;
                a.StatePct = StatePct;
                a.StateAidPct = StateAidPct;
                a.OtherPct = OtherPct;
                a.AllottedAmt = AllottedAmount;
                a.AllottedAmtOrig = AllottedAmount;

                return a;
            }
        }

        public class WorkTypeVM
        {
            public int WorkTypeId { get; set; }
            public string WorktTypeName { get; set; }
        }

        internal Contract PopulateContract(Contract c)
        {
            c.JobNo = JobNo;
            c.ConsultantId = ConsultantId;
            c.ContractTypeId = ContractType;
            c.ContractStatus = ContractStatus;
            c.AgreementDate = AgreementDate;
            c.NoticeProceedDate = NoticeProceedDate;
            c.AgreementTypeId = AgreementType;
            c.RemitTo = RemitTo;
            c.ResponsibleDivisionId = ResponsibleDivision;
            c.ContractCeiling = ContractCeiling;
            c.T1SvcsCeiling = T1SvcsCeiling;
            c.T1FixedFeeMax = T1FixedFeeMax;
            c.HomeOfficeOverheadRateMax = HomeOfficeOverheadRateMax;
            c.FCCM = FCCM;
            c.T2SvcsCeiling = T2SvcsCeiling;
            c.T2FixedFeeMax = T2FixedFeeMax;
            c.FieldServiceOverheadRateMax = FieldServiceOverheadRateMax;
            c.Multiplier = Multiplier;
            c.ContractCeilingOrig = ContractCeilingOrig;
            c.T1SvcsCeilingOrig = T1SvcsCeilingOrig;
            c.T1FixedFeeMaxOrig = T1FixedFeeMaxOrig;
            c.HomeOfficeOverheadRateMaxOrig = HomeOfficeOverheadRateMaxOrig;
            c.FCCMOrig = FCCMOrig;
            c.T2SvcsCeilingOrig = T2SvcsCeilingOrig;
            c.T2FixedFeeMaxOrig = T2FixedFeeMaxOrig;
            c.FieldServiceOverheadRateMaxOrig = FieldServiceOverheadRateMaxOrig;
            c.MultiplierOrig = MultiplierOrig;
            c.ScheduledCompletionDate = ScheduledCompletionDate;
            c.CompletionDate = CompletionDate;
            c.Remarks = Remarks;

            return c;
        }

            
    }
}