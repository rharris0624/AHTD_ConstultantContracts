CREATE TABLE [dbo].[SuppAgreement] (
    [ContractCode]                INT            NOT NULL,
    [SuppNo]                      VARCHAR (15)   NOT NULL,
    [ContractCeiling]             MONEY          NULL,
    [T1SvcsCeiling]               MONEY          NULL,
    [T1FixedFeeMax]               MONEY          NULL,
    [HomeOfficeOverheadRateMax]   DECIMAL (7, 4) NULL,
    [FCCM]                        DECIMAL (7, 4) NULL,
    [T2SvcsCeiling]               MONEY          NULL,
    [T2FixedFeeMax]               MONEY          NULL,
    [FieldServiceOverheadRateMax] DECIMAL (7, 4) NULL,
    [Multiplier]                  DECIMAL (4, 2) NULL,
    [SuppAgreementDate]           DATETIME2 (7)  NULL,
    [CompletionDate]              DATETIME2 (7)  NULL,
    [RemarkLastEditUser]          NCHAR (7)      NULL,
    [Remarks]                     NVARCHAR (MAX) NULL,
    [RemarkLastEditDate]          DATETIME2 (7)  NULL,
    [LastUpdateDate]              DATETIME2 (7)  NOT NULL,
    [LastUpdateUser]              NCHAR (7)      NOT NULL,
    CONSTRAINT [PK_SuppAgreement_1] PRIMARY KEY CLUSTERED ([ContractCode] ASC, [SuppNo] ASC),
    CONSTRAINT [FK_SuppAgreement_Contracts] FOREIGN KEY ([ContractCode]) REFERENCES [dbo].[Contracts] ([ContractCode])
);



