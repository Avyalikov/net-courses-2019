CREATE PROCEDURE [query_1_2_1]
AS
	SELECT ContactName, Country
	FROM Customers
	WHERE Country IN ('USA', 'Canada')
	ORDER BY ContactName, Address
