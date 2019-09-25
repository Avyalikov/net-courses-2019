CREATE PROCEDURE [query_1_3_3]
AS
	SELECT CustomerID, Country
    	FROM Customers
    	WHERE Country LIKE '[B-G]%'
    	ORDER BY Country
