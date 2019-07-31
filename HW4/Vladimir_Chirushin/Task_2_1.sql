-- Task 2.1 #1
SELECT SUM(
	Quantity * UnitPrice * (1 - Discount)
	) Totals
FROM dbo.[Order Details]

-- Task 2.1 #2
SELECT COUNT(*) - COUNT (ShippedDate)
FROM dbo.Orders

--Task 2.1 #3
SELECT COUNT( DISTINCT CustomerID )
FROM dbo.Orders
