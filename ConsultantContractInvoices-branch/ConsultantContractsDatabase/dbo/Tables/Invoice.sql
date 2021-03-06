﻿CREATE TABLE [dbo].[Invoice] (
    [InvoiceId]                   INT            IDENTITY (1, 1) NOT NULL,
    [ContractCode]                INT            NOT NULL,
    [InvoiceNo]                   INT            NOT NULL,
    [RevisionNo]                  INT            CONSTRAINT [DF_Invoice_RevisionNo] DEFAULT ((0)) NOT NULL,
    [InvoiceDate]                 DATE           NOT NULL,
    [SubmittedDate]               DATE           NULL,
    [IsSimple]                    BIT            NOT NULL,
    [BeginDate]                   DATE           NOT NULL,
    [EndDate]                     DATE           NOT NULL,
    [ConsultantInvoiceNo]         NVARCHAR (50)  NULL,
    [SubConsultantId]             INT            NULL,
    [PrimeInvoice]                BIT            NOT NULL,
    [PrimeApproval]               DATE           NULL,
    [PrimeApprovalBy]             NVARCHAR (50)  NULL,
    [LumpSumPercentage]           DECIMAL (4, 2) NULL,
    [ContractCeiling]             MONEY          NULL,
    [T1SvcsCeiling]               MONEY          NULL,
    [T1Svcs]                      MONEY          NULL,
    [T1FixedFee]                  MONEY          NULL,
    [T1FixedFeeMax]               MONEY          NULL,
    [HomeOfficeOverheadRate]      DECIMAL (7, 4) NULL,
    [HomeOfficeOverheadRateMax]   DECIMAL (7, 4) NULL,
    [FCCM]                        DECIMAL (7, 4) NULL,
    [T2SvcsCeiling]               MONEY          NULL,
    [T2Svcs]                      MONEY          NULL,
    [T2FixedFee]                  MONEY          NULL,
    [T2FixedFeeMax]               MONEY          NULL,
    [FieldServiceOverheadRate]    DECIMAL (7, 4) NULL,
    [FieldServiceOverheadRateMax] DECIMAL (7, 4) NULL,
    [Multiplier]                  DECIMAL (4, 2) NULL,
    [PhysicalAddress]             NVARCHAR (50)  NOT NULL,
    [City]                        NVARCHAR (50)  NOT NULL,
    [State]                       NVARCHAR (50)  NOT NULL,
    [CountryCode]                 NVARCHAR (50)  NOT NULL,
    [PostalCode]                  NVARCHAR (50)  NOT NULL,
    [EmailAddress]                NVARCHAR (50)  NULL,
    [Remarks]                     NVARCHAR (MAX) NULL,
    [RemarkLastEditDate]          DATETIME2 (7)  NULL,
    [RemarkLastEditUser]          NCHAR (7)      NULL,
    [LastUpdateDate]              DATETIME2 (7)  NOT NULL,
    [LastUpdateUser]              NCHAR (10)     NOT NULL,
    [ConsultantJobNo]             NCHAR (25)     NULL,
    CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED ([InvoiceId] ASC),
    CONSTRAINT [FK_Invoice_Consultant] FOREIGN KEY ([SubConsultantId]) REFERENCES [dbo].[Consultant] ([ConsultantId]),
    CONSTRAINT [FK_Invoice_Contracts] FOREIGN KEY ([ContractCode]) REFERENCES [dbo].[Contracts] ([ContractCode])
);



