CREATE TABLE [dbo].[ContractWorkTypes] (
    [WorkTypeId]   INT NOT NULL,
    [ContractCode] INT NOT NULL,
    CONSTRAINT [PK_ContractWorkTypes] PRIMARY KEY CLUSTERED ([WorkTypeId] ASC, [ContractCode] ASC),
    CONSTRAINT [FK_ContractWorkTypes_Contracts] FOREIGN KEY ([ContractCode]) REFERENCES [dbo].[Contracts] ([ContractCode]),
    CONSTRAINT [FK_ContractWorkTypes_WorkType] FOREIGN KEY ([WorkTypeId]) REFERENCES [dbo].[WorkType] ([WorkTypeId])
);

