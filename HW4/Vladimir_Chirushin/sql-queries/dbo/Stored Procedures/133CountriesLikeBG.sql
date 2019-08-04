-- Task 1.3 #3
CREATE PROCEDURE CountriesLikeBG
AS
SELECT 
	CustomerID, 
	Country
FROM 
	dbo.Customers
WHERE 
	Country LIKE '[B-G]%'
ORDER BY 
	Country