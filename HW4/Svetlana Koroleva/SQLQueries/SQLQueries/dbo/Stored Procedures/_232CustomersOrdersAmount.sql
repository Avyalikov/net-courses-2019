CREATE PROCEDURE [dbo].[_232CustomersOrdersAmount]
AS
	SELECT c.ContactName, COUNT(od.Quantity) as Amount
    FROM Customers as c LEFT JOIN Orders as o
    ON c.CustomerID=o.CustomerID
    JOIN [Order Details] as od
    ON o.OrderID=od.OrderID
    GROUP BY ContactName
