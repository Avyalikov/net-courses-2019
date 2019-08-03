-- Task 1.1 #2
CREATE PROCEDURE OrdersThatNotShipped
AS
SELECT 
	OrderID, 
	CASE 
	 WHEN ShippedDate IS NULL 
	 THEN 'Not Shipped' 
	 END ShippedDate
FROM 
	dbo.Orders
WHERE 
	ShippedDate IS NULL