CREATE TABLE [dbo].[ContractAllotments] (
    [ContractCode]    INT            NOT NULL,
    [Func]            NCHAR (4)      NOT NULL,
    [FAP]             NCHAR (11)     NOT NULL,
    [FundingPriority] DECIMAL (2)    NULL,
    [FedPct]          DECIMAL (5, 4) NOT NULL,
    [StatePct]        DECIMAL (5, 4) NOT NULL,
    [StateAidPct]     DECIMAL (5, 4) NOT NULL,
    [OtherPct]        DECIMAL (5, 4) NOT NULL,
    [AllottedAmt]     MONEY          NOT NULL,
    [AllottedAmtOrig] MONEY          NOT NULL,
    [LastUpdateDate]  DATETIME2 (7)  NOT NULL,
    [LastUpdateUser]  NCHAR (7)      NOT NULL,
    CONSTRAINT [PK_ContractDetail] PRIMARY KEY CLUSTERED ([ContractCode] ASC, [Func] ASC, [FAP] ASC),
    CONSTRAINT [FK_ContractDetail_Contracts] FOREIGN KEY ([ContractCode]) REFERENCES [dbo].[Contracts] ([ContractCode])
);

