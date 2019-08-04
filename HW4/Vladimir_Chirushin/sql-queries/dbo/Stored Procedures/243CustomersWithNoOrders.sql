-- Task 2.4 #3
CREATE PROCEDURE CustomersWithNoOrders
AS
SELECT
	Customers.ContactName
FROM	
	dbo.Customers
WHERE 
	NOT EXISTS(
		(SELECT 
			Orders.CustomerID
		FROM 
			dbo.Orders
		WHERE 
			Orders.CustomerID = Customers.CustomerID))