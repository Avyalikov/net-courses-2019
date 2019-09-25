CREATE PROCEDURE [dbo].[_232CustomersOrdersAmount]
AS
	SELECT c.ContactName, 
		COUNT(o.OrderID) as Amount
    FROM Customers as c LEFT JOIN Orders as o
    ON c.CustomerID=o.CustomerID
    GROUP BY ContactName
	ORDER BY Amount
