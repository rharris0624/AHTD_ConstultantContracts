CREATE TABLE [dbo].[SubConsultantSalaryRates] (
    [ConractCode]     INT             NOT NULL,
    [SubConsultantId] INT             NOT NULL,
    [JobTitle]        NVARCHAR (200)  NOT NULL,
    [RateMin]         DECIMAL (18, 2) NOT NULL,
    [RateMinOrig]     DECIMAL (18, 2) NOT NULL,
    [RateMax]         DECIMAL (18, 2) NOT NULL,
    [RateMaxOrig]     DECIMAL (18, 2) NOT NULL,
    [LastUpdateDate]  DATETIME2 (7)   NOT NULL,
    [LastUpdateUser]  NCHAR (7)       NOT NULL,
    CONSTRAINT [PK_SubConsultantSalaryRates] PRIMARY KEY CLUSTERED ([ConractCode] ASC, [SubConsultantId] ASC, [JobTitle] ASC),
    CONSTRAINT [FK_SubConsultantSalaryRates_ContractSubConsultants] FOREIGN KEY ([ConractCode], [SubConsultantId]) REFERENCES [dbo].[ContractSubConsultants] ([ContractCode], [SubConsultantId])
);

