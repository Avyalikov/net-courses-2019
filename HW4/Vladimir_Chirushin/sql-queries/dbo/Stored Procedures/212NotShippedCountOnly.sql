-- Task 2.1 #2
CREATE PROCEDURE NotShippedCountOnly
AS
SELECT 
	COUNT(*) - COUNT (ShippedDate)
FROM 
	dbo.Orders