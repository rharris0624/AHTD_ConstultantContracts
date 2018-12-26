CREATE TABLE [dbo].[InvoiceApproval] (
    [InvoiceApprovalID] INT             IDENTITY (1, 1) NOT NULL,
    [InvoiceId]         INT             NOT NULL,
    [UserID]            VARCHAR (15)    NOT NULL,
    [Status]            VARCHAR (15)    NOT NULL,
    [StatusDate]        DATETIME        NOT NULL,
    [ActiveStatus]      BIT             NOT NULL,
    [AuditReview]       BIT             CONSTRAINT [DF_InvoiceApproval_AuditReview] DEFAULT ((0)) NOT NULL,
    [VoucherDate]       DATE            NULL,
    [VoucherNo]         NCHAR (10)      NULL,
    [RejectedReason]    NVARCHAR (1000) NULL,
    CONSTRAINT [PK_InvoiceApproval] PRIMARY KEY CLUSTERED ([InvoiceApprovalID] ASC),
    CONSTRAINT [FK_InvoiceApproval_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice] ([InvoiceId])
);



