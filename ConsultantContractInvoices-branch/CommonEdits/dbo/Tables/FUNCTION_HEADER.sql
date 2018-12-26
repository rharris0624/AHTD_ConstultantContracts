CREATE TABLE [dbo].[FUNCTION_HEADER] (
    [FUNCTION]           CHAR (4)  NOT NULL,
    [FUNCTION_DESCRIPTI] CHAR (20) NOT NULL
);


GO
GRANT SELECT
    ON OBJECT::[dbo].[FUNCTION_HEADER] TO [VoucherVendors]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[FUNCTION_HEADER] TO [JournalEntry]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[FUNCTION_HEADER] TO [BillboardAdvertising]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[FUNCTION_HEADER] TO [LogoAdvertising]
    AS [dbo];

