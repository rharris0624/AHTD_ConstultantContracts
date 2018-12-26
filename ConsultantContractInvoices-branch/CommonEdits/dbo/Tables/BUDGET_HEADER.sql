CREATE TABLE [dbo].[BUDGET_HEADER] (
    [BUDGET]             CHAR (3)  NOT NULL,
    [BUDGET_FILLER]      CHAR (1)  NOT NULL,
    [BUDGET_DESCRIPTION] CHAR (20) NOT NULL
);


GO
GRANT SELECT
    ON OBJECT::[dbo].[BUDGET_HEADER] TO [MiscInventory]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[BUDGET_HEADER] TO [ROWVehiclePool]
    AS [dbo];


GO
GRANT UPDATE
    ON OBJECT::[dbo].[BUDGET_HEADER] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[BUDGET_HEADER] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[dbo].[BUDGET_HEADER] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[dbo].[BUDGET_HEADER] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[BUDGET_HEADER] TO [VoucherVendors]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[BUDGET_HEADER] TO [JournalEntry]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[BUDGET_HEADER] TO [BillboardAdvertising]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[BUDGET_HEADER] TO [LogoAdvertising]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[BUDGET_HEADER] TO [DozerPool]
    AS [dbo];

