CREATE TABLE [dbo].[AgreementType] (
    [AgreementTypeId]   INT           IDENTITY (1, 1) NOT NULL,
    [AgreementTypeName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_AgreementType] PRIMARY KEY CLUSTERED ([AgreementTypeId] ASC)
);

