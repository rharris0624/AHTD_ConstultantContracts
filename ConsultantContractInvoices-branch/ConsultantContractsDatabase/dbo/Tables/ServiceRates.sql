CREATE TABLE [dbo].[ServiceRates] (
    [ContractCode]   INT             NOT NULL,
    [ServiceName]    NVARCHAR (200)  NOT NULL,
    [RateMin]        DECIMAL (18, 2) NOT NULL,
    [RateMinOrig]    DECIMAL (18, 2) NOT NULL,
    [RateMax]        DECIMAL (18, 2) NOT NULL,
    [RateMaxOrig]    DECIMAL (18, 2) NOT NULL,
    [LastUpdateDate] DATETIME2 (7)   NOT NULL,
    [LastUpdateUser] NCHAR (7)       NOT NULL,
    CONSTRAINT [PK_ServiceRates] PRIMARY KEY CLUSTERED ([ContractCode] ASC, [ServiceName] ASC),
    CONSTRAINT [FK_ServiceRates_Contracts] FOREIGN KEY ([ContractCode]) REFERENCES [dbo].[Contracts] ([ContractCode])
);



