using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using ConsultantContractsInternal.Models;

namespace ConsultantContractsInternal.ViewModels
{
    public class ContractEditVM
    {
        public ContractEditVM()
        {
            
        }

        public ContractEditVM(ConsultantContractsEntities context, int contractCode)
        {

            var c = context.Contracts.Single(con => con.ContractCode == contractCode);

            ContractCode = c.ContractCode;

            JobNo = c.JobNo;
            ConsultantId = c.ConsultantId;
            ContractTypeId = c.ContractTypeId;
            ContractStatus = c.ContractStatus;
            TitlePhase = c.TitlePhase;
            AgreementDate = c.AgreementDate;
            NoticeProceedDate = c.NoticeProceedDate;
            ConsultantContractNo = c.ConsultantContractNo;
            AgreementTypeId = c.AgreementTypeId;
            TaskOrderNo = c.TaskOrderNo;
            ResponsibleDivisionId = c.ResponsibleDivisionId;
            ScheduledCompletionDate = c.ScheduledCompletionDate;
            CompletionDate = c.CompletionDate;

            //JL40985  2/9/2017  Added these 4 properties.
            HomeOfficeOverheadRateMax = c.HomeOfficeOverheadRateMax;
            FieldServiceOverheadRateMax = c.FieldServiceOverheadRateMax;
            FCCM = c.FCCM;
            Remarks = c.Remarks;

            //JL40985  3/28/2017  Added these 6 properties.
            Multiplier = c.Multiplier;
            ContractCeiling = c.ContractCeiling;
            T1SvcsCeiling = c.T1SvcsCeiling;
            T1FixedFeeMax = c.T1FixedFeeMax;
            T2SvcsCeiling = c.T2SvcsCeiling;
            T2FixedFeeMax = c.T2FixedFeeMax;

            //rh41200 5/22/2017 Added these 2 properties
            RemitToId = c.RemitTo;
        }

        public short TitlePhase { get; set; }

        public DateTime? CompletionDate { get; set; }
        public int ContractCode { get; set; }
        public string JobNo { get; set; }
        public int ConsultantId { get; set; }
        //rh41200 5/22/2017
        public int? RemitToId { get; set; }
        public string RemitToName { get; set; }
        public int ContractTypeId { get; set; }
        public string ContractStatus { get; set; }
        public DateTime? AgreementDate { get; set; }
        public DateTime? NoticeProceedDate { get; set; }
        public string ConsultantContractNo { get; set; }
        public int AgreementTypeId { get; set; }
        public string TaskOrderNo { get; set; }
        public int ResponsibleDivisionId { get; set; }
        public DateTime? ScheduledCompletionDate { get; set; }

        //JL40985  2/9/2017  Added these 4 properties.
        public decimal? HomeOfficeOverheadRateMax { get; set; }
        public decimal? FieldServiceOverheadRateMax { get; set; }
        public decimal? FCCM { get; set; }
        public string Remarks { get; set; }

        //JL40985  3/28/2017  Added these 6 properties.
        public decimal? Multiplier { get; set; }
        public decimal? ContractCeiling { get; set; }
        public decimal? T1SvcsCeiling { get; set; }
        public decimal? T1FixedFeeMax { get; set; }
        public decimal? T2SvcsCeiling { get; set; }
        public decimal? T2FixedFeeMax { get; set; }

        //rh41200 5/17/2017
        public string ConsultantName { get; set; }
    }
}