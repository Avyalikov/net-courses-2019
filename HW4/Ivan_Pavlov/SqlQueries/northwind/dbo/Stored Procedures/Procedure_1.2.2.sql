CREATE PROCEDURE [dbo].[Procedure_1.2.2]
AS
	SELECT ContactName, Country 
	FROM Customers
	WHERE Country NOT IN ('USA', 'Canada')
	ORDER BY ContactName