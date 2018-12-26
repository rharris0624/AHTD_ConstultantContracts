CREATE TABLE [dbo].[PARKS] (
    [PARKNO]     CHAR (6) NOT NULL,
    [DISTNO]     CHAR (2) NULL,
    [COUNTYCODE] CHAR (2) NULL
);


GO
GRANT SELECT
    ON OBJECT::[dbo].[PARKS] TO [VoucherVendors]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[PARKS] TO [JournalEntry]
    AS [dbo];

