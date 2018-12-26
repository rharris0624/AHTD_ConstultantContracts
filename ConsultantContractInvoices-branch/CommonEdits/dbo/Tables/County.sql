CREATE TABLE [dbo].[County] (
    [CountyCode]    CHAR (2)  NOT NULL,
    [FedCountyCode] CHAR (2)  NOT NULL,
    [CountyName]    CHAR (14) NOT NULL,
    [District]      CHAR (2)  NOT NULL,
    CONSTRAINT [PK_COUNTIES] PRIMARY KEY CLUSTERED ([CountyCode] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_COUNTIES]
    ON [dbo].[County]([CountyCode] ASC);


GO
GRANT SELECT
    ON OBJECT::[dbo].[County] TO [VoucherVendors]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[County] TO [JournalEntry]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[County] TO [BillboardAdvertising]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[County] TO [LogoAdvertising]
    AS [dbo];

