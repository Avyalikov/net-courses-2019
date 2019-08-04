CREATE PROCEDURE [dbo].[Procedure_1.2.3]
AS
	SELECT DISTINCT Country 
	FROM Customers
	ORDER BY Country DESC
