CREATE TABLE [dbo].[SalaryRates] (
    [ConractCode]    INT             NOT NULL,
    [JobTitle]       NVARCHAR (200)  NOT NULL,
    [RateMin]        DECIMAL (18, 2) NOT NULL,
    [RateMinOrig]    DECIMAL (18, 2) NOT NULL,
    [RateMax]        DECIMAL (18, 2) NOT NULL,
    [RateMaxOrig]    DECIMAL (18, 2) NOT NULL,
    [LastUpdateDate] DATETIME2 (7)   NOT NULL,
    [LastUpdateUser] NCHAR (7)       NOT NULL,
    CONSTRAINT [PK_SalaryRates] PRIMARY KEY CLUSTERED ([ConractCode] ASC, [JobTitle] ASC),
    CONSTRAINT [FK_SalaryRates_Contracts] FOREIGN KEY ([ConractCode]) REFERENCES [dbo].[Contracts] ([ContractCode])
);



