-- Task 1.1 #1
DECLARE @ShippedDateLimit datetime = '06-may-1998'
SELECT OrderID, ShippedDate, ShipVia
FROM dbo.Orders
WHERE ShippedDate >= CAST(@ShippedDateLimit as date) 
AND ShipVia >= 2 


-- Task 1.1 #2
SELECT OrderID, 
CASE 
 WHEN ShippedDate IS NULL 
 THEN 'Not Shipped' 
 END ShippedDate
FROM dbo.Orders
WHERE ShippedDate IS NULL
	

-- Task 1.1 #3
SELECT OrderID AS 'Order_Number',
CASE 
 WHEN ShippedDate IS NULL 
 THEN 'Not Shipped' 
 END ShippedDate
FROM dbo.Orders
WHERE ShippedDate IS NULL OR 
ShippedDate > CAST(@ShippedDateLimit as date) 