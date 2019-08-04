CREATE PROCEDURE [query_1_2_3]
AS
	SELECT DISTINCT Country
    	FROM Customers
    	ORDER BY Country DESC
