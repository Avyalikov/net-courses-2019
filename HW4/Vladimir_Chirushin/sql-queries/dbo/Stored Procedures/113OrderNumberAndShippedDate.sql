-- Task 1.1 #3
CREATE PROCEDURE OrderNumberAndShippedDate
AS
DECLARE @ShippedDateLimit datetime = '06-may-1998'
SELECT 
	OrderID AS 'Order Number',
	CASE 
	 WHEN ShippedDate IS NULL 
	 THEN 'Not Shipped' 
	 END ShippedDate
FROM 
	dbo.Orders
WHERE 
	ShippedDate IS NULL OR 
	ShippedDate > CAST(@ShippedDateLimit as date) 