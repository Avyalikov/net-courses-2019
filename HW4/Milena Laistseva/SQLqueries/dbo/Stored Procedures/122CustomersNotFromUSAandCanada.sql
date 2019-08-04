CREATE PROCEDURE [dbo].[122CustomersNotFromUSAandCanada]
AS
	SELECT ContactName, Country
FROM Customers
WHERE Country NOT IN ('USA', 'Canada') 
ORDER BY ContactName
