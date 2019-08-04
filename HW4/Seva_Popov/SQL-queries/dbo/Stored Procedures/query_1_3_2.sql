CREATE PROCEDURE [query_1_3_2]
AS
	SELECT CustomerID, Country
	FROM Customers
	WHERE Country BETWEEN  'b%' AND  'g%'
	ORDER BY Country
