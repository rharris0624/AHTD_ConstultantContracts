CREATE TABLE [dbo].[InvoicePayments] (
    [InvoiceId]  INT       NOT NULL,
    [Func]       CHAR (4)  NOT NULL,
    [FAP]        CHAR (11) NOT NULL,
    [Amount]     MONEY     NOT NULL,
    [ObjectCode] CHAR (3)  CONSTRAINT [DF_InvoicePayments_ObjectCode] DEFAULT ((281)) NOT NULL,
    CONSTRAINT [PK_InvoicePayments_1] PRIMARY KEY CLUSTERED ([InvoiceId] ASC, [Func] ASC, [FAP] ASC),
    CONSTRAINT [FK_InvoicePayments_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice] ([InvoiceId])
);



