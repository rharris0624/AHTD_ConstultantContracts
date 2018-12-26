CREATE TABLE [dbo].[SuppSalaryRate] (
    [ContractCode]   INT             NOT NULL,
    [SuppNo]         VARCHAR (15)    NOT NULL,
    [JobTitle]       NVARCHAR (200)  NOT NULL,
    [RateMin]        DECIMAL (18, 2) NOT NULL,
    [RateMax]        DECIMAL (18, 2) NOT NULL,
    [Deleted]        BIT             CONSTRAINT [DF_SuppSalaryRate_Deleted] DEFAULT ((0)) NOT NULL,
    [LastUpdateDate] DATETIME2 (7)   NOT NULL,
    [LastUpdateUser] NCHAR (7)       NOT NULL,
    CONSTRAINT [PK_SuppSalaryRate_1] PRIMARY KEY CLUSTERED ([ContractCode] ASC, [SuppNo] ASC, [JobTitle] ASC),
    CONSTRAINT [FK_SuppSalaryRate_SuppAgreement] FOREIGN KEY ([ContractCode], [SuppNo]) REFERENCES [dbo].[SuppAgreement] ([ContractCode], [SuppNo])
);



