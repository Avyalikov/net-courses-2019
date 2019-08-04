CREATE PROCEDURE [dbo].[121CustomersFromUSAandCanada]
AS
	SELECT ContactName, Country
FROM Customers
WHERE Country IN ('USA', 'Canada') 
ORDER BY ContactName, Country
