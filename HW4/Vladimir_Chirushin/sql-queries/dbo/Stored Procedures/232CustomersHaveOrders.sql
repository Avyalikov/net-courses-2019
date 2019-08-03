-- Task 2.3 #2
CREATE PROCEDURE CustomersHaveOrders
AS
SELECT
	Customers.ContactName, 
	COUNT(Orders.CustomerID) AS 'Orders'
FROM
	dbo.Customers
LEFT OUTER JOIN dbo.Orders ON
	Customers.CustomerID = Orders.CustomerID
GROUP BY
	Customers.ContactName
ORDER BY 'Orders' ASC