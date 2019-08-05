CREATE PROCEDURE [dbo].[121]
AS
	SELECT ContactName, Country
	FROM Customers
	WHERE Country IN ('USA','Canada')
	ORDER BY ContactName, Country
RETURN 0
