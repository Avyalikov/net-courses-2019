CREATE PROCEDURE [dbo].[232]
AS
	SELECT ContactName, Count(Orders.OrderID) as Amount
	FROM Customers
	LEFT JOIN Orders ON Customers.CustomerID = Orders.CustomerID
	GROUP BY ContactName
	ORDER BY Amount
RETURN 0
