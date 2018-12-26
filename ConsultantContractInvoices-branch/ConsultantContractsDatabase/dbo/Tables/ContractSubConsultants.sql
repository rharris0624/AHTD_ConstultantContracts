CREATE TABLE [dbo].[ContractSubConsultants] (
    [ContractCode]                    INT            NOT NULL,
    [SubConsultantId]                 INT            NOT NULL,
    [ContractCeiling]                 MONEY          NULL,
    [ContractCeilingOrig]             MONEY          NULL,
    [T1SvcsCeiling]                   MONEY          NULL,
    [T1SvcsCeilingOrig]               MONEY          NULL,
    [T1FixedFeeMax]                   MONEY          NULL,
    [T1FixedFeeMaxOrig]               MONEY          NULL,
    [HomeOfficeOverheadRateMax]       DECIMAL (7, 4) NULL,
    [HomeOfficeOverheadRateMaxOrig]   DECIMAL (7, 4) NULL,
    [FCCM]                            DECIMAL (7, 4) NULL,
    [FCCMOrig]                        DECIMAL (7, 4) NULL,
    [T2SvcsCeiling]                   MONEY          NULL,
    [T2SvcsCeilingOrig]               MONEY          NULL,
    [T2FixedFeeMax]                   MONEY          NULL,
    [T2FixedFeeMaxOrig]               MONEY          NULL,
    [FieldServiceOverheadRateMax]     DECIMAL (7, 4) NULL,
    [FieldServiceOverheadRateMaxOrig] DECIMAL (7, 4) NULL,
    [Multiplier]                      DECIMAL (4, 2) NULL,
    [MultiplierOrig]                  DECIMAL (4, 2) NULL,
    [LastUpdateDate]                  DATETIME2 (7)  NOT NULL,
    [LastUpdateUser]                  NCHAR (10)     NOT NULL,
    CONSTRAINT [PK_ContractSubConsultants] PRIMARY KEY CLUSTERED ([ContractCode] ASC, [SubConsultantId] ASC),
    CONSTRAINT [FK_ContractSubConsultants_Consultant] FOREIGN KEY ([SubConsultantId]) REFERENCES [dbo].[Consultant] ([ConsultantId]),
    CONSTRAINT [FK_ContractSubConsultants_Contracts] FOREIGN KEY ([ContractCode]) REFERENCES [dbo].[Contracts] ([ContractCode])
);



