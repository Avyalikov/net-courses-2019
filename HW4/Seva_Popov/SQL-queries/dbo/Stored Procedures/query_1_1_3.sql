CREATE PROCEDURE [query_1_1_3]
AS

SELECT OrderID as 'Order Number', 
CASE 
WHEN ShippedDate IS NULL 
THEN 'Not Shipped' 
ELSE CAST(ShippedDate as nvarchar) 
END as 'Shipped Date'
FROM Orders
WHERE ShippedDate>'1998-05-06' OR ShippedDate IS NULL
