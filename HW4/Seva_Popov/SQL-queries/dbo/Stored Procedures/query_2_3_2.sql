CREATE PROCEDURE [query_2_3_2]
AS	
	SELECT ContactName, COUNT(OrderID) as Amount
	FROM Customers 
	LEFT JOIN Orders ON Customers.CustomerID = Orders.CustomerID
	GROUP BY ContactName
	ORDER BY Amount
