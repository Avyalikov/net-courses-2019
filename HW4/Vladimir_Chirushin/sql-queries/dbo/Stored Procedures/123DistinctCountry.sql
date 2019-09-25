-- Task 1.2 #3
CREATE PROCEDURE DistinctCountry
AS
SELECT 
	DISTINCT Country 
FROM 
	dbo.Customers
ORDER BY 
	Country DESC