USE Northwind;
SELECT OrderID, ShippedDate =
      CASE
	     WHEN ShippedDate IS NULL THEN 'Not Shipped'  
      END
FROM Orders
WHERE ShippedDate IS NULL;
