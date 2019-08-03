-- Task 1.3 #1
CREATE PROCEDURE OrdersWithProducts
AS
SELECT 
	DISTINCT OrderID
FROM 
	dbo.[Order Details]
WHERE 
	Quantity BETWEEN 3 AND 10
