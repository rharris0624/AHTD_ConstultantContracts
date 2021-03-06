﻿CREATE TABLE [dbo].[ACCOUNT_HEADER] (
    [ACCT_NO]   CHAR (4)  NOT NULL,
    [ACCT_DESC] CHAR (22) NOT NULL,
    CONSTRAINT [PK_ACCOUNT_HEADER] PRIMARY KEY CLUSTERED ([ACCT_NO] ASC)
);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[ACCOUNT_HEADER] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[ACCOUNT_HEADER] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[dbo].[ACCOUNT_HEADER] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[dbo].[ACCOUNT_HEADER] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[ACCOUNT_HEADER] TO [VoucherVendors]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[ACCOUNT_HEADER] TO [BillboardAdvertising]
    AS [dbo];

