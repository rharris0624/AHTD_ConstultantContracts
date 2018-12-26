CREATE TABLE [dbo].[SuppAllotments] (
    [ContractCode]   INT           NOT NULL,
    [SuppNo]         VARCHAR (15)  NOT NULL,
    [Func]           NCHAR (4)     NOT NULL,
    [FAP]            NCHAR (11)    NOT NULL,
    [AllottedAmt]    MONEY         NULL,
    [LastUpdateDate] DATETIME2 (7) NOT NULL,
    [LastUpdateUser] NCHAR (7)     NOT NULL,
    CONSTRAINT [PK_SuppAllotments_1] PRIMARY KEY CLUSTERED ([ContractCode] ASC, [SuppNo] ASC, [Func] ASC, [FAP] ASC),
    CONSTRAINT [FK_SuppAllotments_SuppAgreement] FOREIGN KEY ([ContractCode], [SuppNo]) REFERENCES [dbo].[SuppAgreement] ([ContractCode], [SuppNo])
);

