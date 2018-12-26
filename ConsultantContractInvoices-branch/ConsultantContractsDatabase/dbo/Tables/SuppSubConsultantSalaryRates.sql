CREATE TABLE [dbo].[SuppSubConsultantSalaryRates] (
    [ConractCode]     INT             NOT NULL,
    [SuppNo]          VARCHAR (15)    NOT NULL,
    [SubConsultantId] INT             NOT NULL,
    [JobTitle]        NVARCHAR (200)  NOT NULL,
    [RateMin]         DECIMAL (18, 2) NOT NULL,
    [RateMax]         DECIMAL (18, 2) NOT NULL,
    [LastUpdateDate]  DATETIME2 (7)   NOT NULL,
    [LastUpdateUser]  NCHAR (7)       NOT NULL,
    [Deleted]         BIT             CONSTRAINT [DF_SuppSubConsultantSalaryRates_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SuppSubConsultantSalaryRates] PRIMARY KEY CLUSTERED ([ConractCode] ASC, [SuppNo] ASC, [SubConsultantId] ASC, [JobTitle] ASC),
    CONSTRAINT [FK_SuppSubConsultantSalaryRates_SuppSubConsultantInfo] FOREIGN KEY ([ConractCode], [SuppNo], [SubConsultantId]) REFERENCES [dbo].[SuppSubConsultantInfo] ([ContractCode], [SuppNo], [SubConsultantId])
);

