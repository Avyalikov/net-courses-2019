CREATE PROCEDURE [dbo].[Procedure_2.2.5]
AS
	SELECT ContactName
	FROM Customers
	WHERE City IN 
		(SELECT City
		FROM Customers
		GROUP BY City
		HAVING COUNT(CustomerID) >= 2)
	ORDER BY ContactName

