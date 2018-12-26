CREATE TABLE [dbo].[SubConsultantServiceRates] (
    [ContractCode]    INT             NOT NULL,
    [SubConsultantId] INT             NOT NULL,
    [ServiceName]     NVARCHAR (200)  NOT NULL,
    [RateMin]         DECIMAL (18, 2) NOT NULL,
    [RateMinOrig]     DECIMAL (18, 2) NOT NULL,
    [RateMax]         DECIMAL (18, 2) NOT NULL,
    [RateMaxOrig]     DECIMAL (18, 2) NOT NULL,
    [LastUpdateDate]  DATETIME2 (7)   NOT NULL,
    [LastUpdateUser]  NCHAR (7)       NOT NULL,
    CONSTRAINT [PK_SubConsultantServiceRates] PRIMARY KEY CLUSTERED ([ContractCode] ASC, [SubConsultantId] ASC, [ServiceName] ASC),
    CONSTRAINT [FK_SubConsultantServiceRates_ContractSubConsultants] FOREIGN KEY ([ContractCode], [SubConsultantId]) REFERENCES [dbo].[ContractSubConsultants] ([ContractCode], [SubConsultantId])
);

