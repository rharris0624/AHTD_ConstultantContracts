CREATE TABLE [dbo].[SuppServiceRate] (
    [ContractCode]   INT             NOT NULL,
    [SuppNo]         VARCHAR (15)    NOT NULL,
    [ServiceName]    NVARCHAR (200)  NOT NULL,
    [RateMin]        DECIMAL (18, 2) NOT NULL,
    [RateMax]        DECIMAL (18, 2) NOT NULL,
    [Deleted]        BIT             CONSTRAINT [DF_SuppServiceRate_Deleted] DEFAULT ((0)) NOT NULL,
    [LastUpdateDate] DATETIME2 (7)   NOT NULL,
    [LastUpdateUser] NCHAR (7)       NOT NULL,
    CONSTRAINT [PK_SuppServiceRate] PRIMARY KEY CLUSTERED ([ContractCode] ASC, [SuppNo] ASC, [ServiceName] ASC),
    CONSTRAINT [FK_SuppServiceRate_SuppAgreement] FOREIGN KEY ([ContractCode], [SuppNo]) REFERENCES [dbo].[SuppAgreement] ([ContractCode], [SuppNo])
);



