CREATE TABLE [dbo].[PLANNING_JOB] (
    [JOB_NUM]         CHAR (6)        NOT NULL,
    [FUNC]            CHAR (4)        NOT NULL,
    [BUDGET]          CHAR (3)        NOT NULL,
    [PCT_PARTICIPATE] NUMERIC (5, 2)  NOT NULL,
    [ALLOT_AMT]       NUMERIC (11, 2) NOT NULL,
    [DESCRIPTION]     CHAR (30)       NOT NULL,
    [ALLOT_DATE]      CHAR (6)        NOT NULL,
    [ALLOT_GRANT1]    CHAR (2)        NOT NULL,
    [ALLOT_GRANT2]    CHAR (2)        NOT NULL,
    [ALLOT_GRANT3]    CHAR (4)        NOT NULL,
    [TO_DATE_EXPENSE] NUMERIC (10, 2) NOT NULL,
    [TO_DATE_BILLED]  NUMERIC (10, 2) NOT NULL,
    [CURR_EXPENSE]    NUMERIC (10, 2) NOT NULL,
    [JOB_TYPE]        CHAR (1)        NOT NULL,
    [JOB_STATUS]      CHAR (1)        NOT NULL
);


GO
GRANT SELECT
    ON OBJECT::[dbo].[PLANNING_JOB] TO [ConsultantContracts]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[PLANNING_JOB] TO [ROWVehiclePool]
    AS [dbo];


GO
GRANT UPDATE
    ON OBJECT::[dbo].[PLANNING_JOB] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[PLANNING_JOB] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[dbo].[PLANNING_JOB] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[dbo].[PLANNING_JOB] TO [JournalEntry_DOTNETAD]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[PLANNING_JOB] TO [VoucherVendors]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[PLANNING_JOB] TO [BillboardAdvertising]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[PLANNING_JOB] TO [LogoAdvertising]
    AS [dbo];

