﻿CREATE TABLE [dbo].[BUD_FUNCTN] (
    [BUDGET]          CHAR (3)  NOT NULL,
    [FUNCTION]        CHAR (4)  NOT NULL,
    [FUNCTION_DESC]   CHAR (20) NOT NULL,
    [PAHR_FUNCTION]   CHAR (1)  NOT NULL,
    [TIMER_FUNCTION]  CHAR (1)  NOT NULL,
    [PAYR_START_DATE] CHAR (8)  NOT NULL,
    [PAYR_END_DATE]   CHAR (8)  NOT NULL,
    [MTH_START_DATE]  CHAR (8)  NOT NULL,
    [MTH_END_DATE]    CHAR (8)  NOT NULL,
    [LAST_UPD_DATE]   CHAR (8)  NOT NULL,
    [LAST_UPD_TIME]   CHAR (6)  NOT NULL,
    [LAST_UPD_USERID] CHAR (7)  NOT NULL,
    [LAST_UPD_TERMID] CHAR (4)  NOT NULL,
    CONSTRAINT [PK_BUD_FUNCTN] PRIMARY KEY CLUSTERED ([BUDGET] ASC, [FUNCTION] ASC)
);


GO
GRANT SELECT
    ON OBJECT::[dbo].[BUD_FUNCTN] TO [ROWVehiclePool]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[BUD_FUNCTN] TO [VoucherVendors]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[BUD_FUNCTN] TO [JournalEntry]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[BUD_FUNCTN] TO [BillboardAdvertising]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[BUD_FUNCTN] TO [LogoAdvertising]
    AS [dbo];

