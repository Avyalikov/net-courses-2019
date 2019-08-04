CREATE PROCEDURE [dbo].[Proc_1_2_1]

AS
	SELECT ContactName, Country
	FROM Customers
	WHERE Country IN ('USA','Canada')
	ORDER BY ContactName, Country
RETURN 0
