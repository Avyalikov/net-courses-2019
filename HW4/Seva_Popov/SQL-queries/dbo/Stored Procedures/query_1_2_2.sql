CREATE PROCEDURE [query_1_2_2]
AS
	SELECT ContactName, Country 
	FROM Customers
	WHERE Country NOT IN ('USA', 'Canada')
	ORDER BY ContactName
