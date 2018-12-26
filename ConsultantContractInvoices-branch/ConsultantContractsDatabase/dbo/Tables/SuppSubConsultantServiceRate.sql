CREATE TABLE [dbo].[SuppSubConsultantServiceRate] (
    [ContractCode]    INT             NOT NULL,
    [SuppNo]          VARCHAR (15)    NOT NULL,
    [SubConsultantId] INT             NOT NULL,
    [ServiceName]     NVARCHAR (200)  NOT NULL,
    [RateMin]         DECIMAL (18, 2) NOT NULL,
    [RateMax]         DECIMAL (18, 2) NOT NULL,
    [Deleted]         BIT             CONSTRAINT [DF_SuppSubConsultantServiceRate_Deleted] DEFAULT ((0)) NOT NULL,
    [LastUpdateDate]  DATETIME2 (7)   NOT NULL,
    [LastUpdateUser]  NCHAR (7)       NOT NULL,
    CONSTRAINT [PK_SuppSubConsultantServiceRate] PRIMARY KEY CLUSTERED ([ContractCode] ASC, [SuppNo] ASC, [SubConsultantId] ASC, [ServiceName] ASC),
    CONSTRAINT [FK_SuppSubConsultantServiceRate_SuppSubConsultantInfo] FOREIGN KEY ([ContractCode], [SuppNo], [SubConsultantId]) REFERENCES [dbo].[SuppSubConsultantInfo] ([ContractCode], [SuppNo], [SubConsultantId])
);

