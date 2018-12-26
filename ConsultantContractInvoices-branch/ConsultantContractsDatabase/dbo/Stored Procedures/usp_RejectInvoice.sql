CREATE PROCEDURE [dbo].[usp_RejectInvoice]
	-- Add the parameters for the stored procedure here
	@InvoiceId int, 
	@Reason nvarchar(1000) = '',
	@UserId NCHAR(10)
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	DECLARE @newInvoiceId INT = 0



	UPDATE [dbo].[InvoiceApproval]
		SET	[StatusDate] = GETDATE()
			,[RejectedReason] = @Reason
			,UserID = @UserId
		WHERE InvoiceId = @InvoiceId

	INSERT INTO dbo.Invoice
	        ( ContractCode ,
	          InvoiceNo ,
	          RevisionNo ,
	          InvoiceDate ,
	          BeginDate ,
	          EndDate ,
			  IsSimple,
	          ConsultantInvoiceNo ,
	          SubConsultantId ,
	          PrimeInvoice ,
	          PrimeApproval ,
	          PrimeApprovalBy ,
	          LumpSumPercentage ,
	          ContractCeiling ,
	          T1SvcsCeiling ,
	          T1Svcs ,
	          T1FixedFee ,
	          T1FixedFeeMax ,
	          HomeOfficeOverheadRate ,
	          HomeOfficeOverheadRateMax ,
	          FCCM ,
	          T2SvcsCeiling ,
	          T2Svcs ,
	          T2FixedFee ,
	          T2FixedFeeMax ,
	          FieldServiceOverheadRate ,
	          FieldServiceOverheadRateMax ,
	          Multiplier ,
	          PhysicalAddress ,
	          City ,
	          State ,
	          CountryCode ,
	          PostalCode ,
	          EmailAddress ,
	          Remarks ,
	          RemarkLastEditDate ,
	          RemarkLastEditUser ,
	          LastUpdateDate ,
	          LastUpdateUser
	        )
	SELECT ContractCode ,
           InvoiceNo ,
           RevisionNo + 1,
           InvoiceDate ,
           BeginDate ,
           EndDate ,
		   IsSimple,
           ConsultantInvoiceNo ,
           SubConsultantId ,
           PrimeInvoice ,
           PrimeApproval ,
           PrimeApprovalBy ,
           LumpSumPercentage ,
           ContractCeiling ,
           T1SvcsCeiling ,
           T1Svcs ,
           T1FixedFee ,
           T1FixedFeeMax ,
           HomeOfficeOverheadRate ,
           HomeOfficeOverheadRateMax ,
           FCCM ,
           T2SvcsCeiling ,
           T2Svcs ,
           T2FixedFee ,
           T2FixedFeeMax ,
           FieldServiceOverheadRate ,
           FieldServiceOverheadRateMax ,
           Multiplier ,
           PhysicalAddress ,
           City ,
           State ,
           CountryCode ,
           PostalCode ,
           EmailAddress ,
           Remarks ,
           RemarkLastEditDate ,
           RemarkLastEditUser ,
           GETDATE() ,
           @UserId
	FROM dbo.Invoice
	WHERE InvoiceId = @InvoiceId

	SET @newInvoiceId =  SCOPE_IDENTITY()

	INSERT INTO dbo.InvoiceApproval
	        ( InvoiceId)
	VALUES  ( @newInvoiceId)

	INSERT INTO dbo.InvoicePayments
	        ( InvoiceId ,
	          Func ,
	          FAP ,
	          Amount ,
	          ObjectCode
	        )
	SELECT @newInvoiceId ,
           Func ,
           FAP ,
           Amount ,
           ObjectCode
	FROM dbo.InvoicePayments
	WHERE InvoiceId = @InvoiceId

	COMMIT TRANSACTION

	RETURN @newInvoiceId

END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
    ROLLBACK TRANSACTION

    DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
    SELECT @ErrMsg = ERROR_MESSAGE(),
           @ErrSeverity = ERROR_SEVERITY()

    RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
END