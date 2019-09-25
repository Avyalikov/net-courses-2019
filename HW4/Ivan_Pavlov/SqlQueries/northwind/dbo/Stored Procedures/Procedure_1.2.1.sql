CREATE PROCEDURE [dbo].[Procedure_1.2.1]
AS
	SELECT ContactName, Country 
	FROM Customers
	WHERE Country IN ('USA', 'Canada')
	ORDER BY ContactName, Address
