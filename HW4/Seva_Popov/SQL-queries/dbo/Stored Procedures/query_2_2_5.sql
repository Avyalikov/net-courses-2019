CREATE PROCEDURE [query_2_2_5]
AS	
	SELECT ContactName
	FROM Customers
	WHERE City IN 
			(SELECT City
			FROM Customers
			GROUP BY City
			HAVING COUNT(CustomerID) >= 2)
	ORDER BY ContactName