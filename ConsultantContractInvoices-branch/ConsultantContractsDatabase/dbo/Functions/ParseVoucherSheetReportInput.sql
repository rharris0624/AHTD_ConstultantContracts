-- =============================================
-- Author:		Jason Ellis
-- Create date: 9/1/2015
-- Description:	Parse string from website so voucher report can get specific invoices
-- =============================================
CREATE FUNCTION [dbo].[ParseVoucherSheetReportInput] 
(	
	-- Add the parameters for the function here
	@input varchar(max)
)
RETURNS @Invoices TABLE 
	(
	ContractCode INT,
	InvoiceNo INT
	)
AS

BEGIN
	DECLARE @FoundIndex INT
	DECLARE @ReturnValue VARCHAR(MAX)
	DECLARE @TupleIndex int
	
	SET @FoundIndex = CHARINDEX(',', @input)

	WHILE(@FoundIndex <> 0)
	BEGIN
		SET @ReturnValue = SUBSTRING(@input, 0, @FoundIndex)
		SET @TupleIndex = CHARINDEX('-',@ReturnValue)
		
		INSERT @Invoices
		        ( ContractCode, InvoiceNo )
		VALUES  ( SUBSTRING(@ReturnValue, 0, @TupleIndex),
		          SUBSTRING(@ReturnValue, @TupleIndex + 1, LEN(@ReturnValue) - @TupleIndex)
		          )

		SET @input = SUBSTRING(@input, @FoundIndex + 1, LEN(@input) - @FoundIndex)
		SET @FoundIndex = CHARINDEX(',', @input)
	END
		
	INSERT @Invoices
		    ( ContractCode, InvoiceNo )
	VALUES  ( SUBSTRING(@input, 0, @TupleIndex),
		        SUBSTRING(@input, @TupleIndex + 1, LEN(@input) - @TupleIndex)
		    )
	RETURN 
END