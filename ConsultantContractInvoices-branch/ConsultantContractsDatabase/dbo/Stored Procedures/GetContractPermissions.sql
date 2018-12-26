CREATE PROCEDURE [dbo].[GetContractPermissions]
	@userId VARCHAR(25),
	@application VARCHAR(10)
AS
	SELECT rd.Budget
	FROM Contracts co
	INNER JOIN ResponsibleDivision rd ON co.ResponsibleDivisionId = rd.DivisionId
	INNER JOIN [ArDot_UserProv].[dbo].[LegacySecurities] ls ON ls.ResourceId = rd.Budget
	WHERE ls.UserId = @userId AND ls.ApplicationId = @application

GRANT EXECUTE ON getcontractpermissions TO [ahtd\rh41200]