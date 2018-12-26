CREATE TABLE [dbo].[ContractTypes] (
    [TypeId]        INT           NOT NULL,
    [TypeShortName] NVARCHAR (20) NOT NULL,
    [TypeName]      NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ContractTypes] PRIMARY KEY CLUSTERED ([TypeId] ASC)
);

