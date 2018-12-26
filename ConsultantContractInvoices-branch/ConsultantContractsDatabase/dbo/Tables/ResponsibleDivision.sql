CREATE TABLE [dbo].[ResponsibleDivision] (
    [DivisionId]   INT           NOT NULL,
    [DivisionName] NVARCHAR (60) NOT NULL,
    [ShortName]    NVARCHAR (30) CONSTRAINT [DF_ResponsibleDivision_ShortName] DEFAULT (N'test') NOT NULL,
    [Budget]       CHAR (3)      NOT NULL,
    CONSTRAINT [PK_ResponsibleDivision] PRIMARY KEY CLUSTERED ([DivisionId] ASC)
);

