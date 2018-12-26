CREATE TABLE [dbo].[STATES] (
    [STATE_CODE] CHAR (2)  NOT NULL,
    [STATE_NAME] CHAR (30) NOT NULL,
    [USA_FLAG]   CHAR (1)  NOT NULL,
    CONSTRAINT [PK_STATES] PRIMARY KEY CLUSTERED ([STATE_CODE] ASC)
);


GO
GRANT SELECT
    ON OBJECT::[dbo].[STATES] TO [ConsultantContracts]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[STATES] TO [VoucherVendors]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[STATES] TO [JournalEntry]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[STATES] TO [BillboardAdvertising]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[STATES] TO [LogoAdvertising]
    AS [dbo];

