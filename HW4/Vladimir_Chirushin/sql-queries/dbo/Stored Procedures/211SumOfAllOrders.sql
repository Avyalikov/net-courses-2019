-- Task 2.1 #1
CREATE PROCEDURE SumOfAllOrders
AS
SELECT 
	SUM(
		Quantity * UnitPrice * (1 - Discount)
	) Totals
FROM 
	dbo.[Order Details]
