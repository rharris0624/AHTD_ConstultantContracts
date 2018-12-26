CREATE TABLE [dbo].[SuppSubConsultantInfo] (
    [ContractCode]                INT            NOT NULL,
    [SuppNo]                      VARCHAR (15)   NOT NULL,
    [SubConsultantId]             INT            NOT NULL,
    [ContractCeiling]             MONEY          NULL,
    [T1SvcsCeiling]               MONEY          NULL,
    [T1FixedFeeMax]               MONEY          NULL,
    [HomeOfficeOverheadRateMax]   DECIMAL (7, 4) NULL,
    [FCCM]                        DECIMAL (7, 4) NULL,
    [T2SvcsCeiling]               MONEY          NULL,
    [T2FixedFeeMax]               MONEY          NULL,
    [FieldServiceOverheadRateMax] DECIMAL (7, 4) NULL,
    [Multiplier]                  DECIMAL (4, 2) NULL,
    [LastUpdateDate]              DATETIME2 (7)  NOT NULL,
    [LastUpdateUser]              NCHAR (10)     NOT NULL,
    CONSTRAINT [PK_SuppSubConsultantInfo] PRIMARY KEY CLUSTERED ([ContractCode] ASC, [SuppNo] ASC, [SubConsultantId] ASC),
    CONSTRAINT [FK_SuppSubConsultantInfo_Consultant] FOREIGN KEY ([SubConsultantId]) REFERENCES [dbo].[Consultant] ([ConsultantId]),
    CONSTRAINT [FK_SuppSubConsultantInfo_SuppAgreement] FOREIGN KEY ([ContractCode], [SuppNo]) REFERENCES [dbo].[SuppAgreement] ([ContractCode], [SuppNo])
);



