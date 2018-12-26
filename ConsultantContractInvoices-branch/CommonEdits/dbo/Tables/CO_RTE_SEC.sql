CREATE TABLE [dbo].[CO_RTE_SEC] (
    [COUNTY]      CHAR (2) NOT NULL,
    [ROUTE]       CHAR (3) NOT NULL,
    [SECTION_NUM] CHAR (2) NOT NULL,
    [ALT_SECTION] CHAR (1) NOT NULL,
    [CODE]        CHAR (1) NOT NULL,
    [BUDGET]      CHAR (3) NOT NULL,
    CONSTRAINT [PK_CO_RTE_SEC] PRIMARY KEY CLUSTERED ([COUNTY] ASC, [ROUTE] ASC, [SECTION_NUM] ASC, [ALT_SECTION] ASC, [BUDGET] ASC)
);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[CO_RTE_SEC] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[CO_RTE_SEC] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[dbo].[CO_RTE_SEC] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[dbo].[CO_RTE_SEC] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[CO_RTE_SEC] TO [VoucherVendors]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[CO_RTE_SEC] TO [JournalEntry]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[CO_RTE_SEC] TO [DozerPool]
    AS [dbo];

