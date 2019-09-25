CREATE PROCEDURE [dbo].[232CustomersOrdersAmount]
AS
	SELECT Customers.ContactName, COUNT(Orders.OrderID) AS Amount
FROM Customers
LEFT JOIN Orders ON Customers.CustomerID = Orders.CustomerID
GROUP BY ContactName
ORDER BY Amount