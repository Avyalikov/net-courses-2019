CREATE PROCEDURE [dbo].[122]
AS
	SELECT ContactName, Country
	FROM Customers
	WHERE Country NOT IN ('USA','Canada')
	ORDER BY ContactName
RETURN 0
