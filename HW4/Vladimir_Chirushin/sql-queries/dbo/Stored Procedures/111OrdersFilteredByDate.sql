-- Task 1.1 #1
CREATE PROCEDURE OrdersFilteredByDate
AS
DECLARE @ShippedDateLimit datetime = '06-may-1998'
SELECT 
	OrderID, 
	ShippedDate, 
	ShipVia
FROM 
	dbo.Orders
WHERE 
	ShippedDate >= CAST(@ShippedDateLimit AS DATE) AND 
	ShipVia >= 2