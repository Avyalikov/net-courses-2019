-- Task 2.2 #1 Test Get Count of all orders
CREATE PROCEDURE Test_FullOrders
AS
SELECT 
	COUNT(OrderDate) As 'Total'
FROM 
	dbo.Orders 