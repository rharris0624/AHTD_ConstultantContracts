CREATE TABLE [dbo].[SalaryDetail] (
    [ContractCode]   INT           NOT NULL,
    [InvoiceNo]      INT           NOT NULL,
    [ItemNo]         INT           NOT NULL,
    [Amount(calc)]   MONEY         NOT NULL,
    [JobTitle]       NCHAR (10)    NULL,
    [Rate]           NCHAR (10)    NULL,
    [Hours]          NCHAR (10)    NULL,
    [RateMin]        NCHAR (10)    NULL,
    [RateMax]        NCHAR (10)    NULL,
    [TitleI]         BIT           NOT NULL,
    [LastUpdateDate] DATETIME2 (7) NOT NULL,
    [LastUpdateUser] NCHAR (10)    NULL,
    CONSTRAINT [PK_SalaryDetail] PRIMARY KEY CLUSTERED ([ContractCode] ASC, [InvoiceNo] ASC, [ItemNo] ASC)
);



