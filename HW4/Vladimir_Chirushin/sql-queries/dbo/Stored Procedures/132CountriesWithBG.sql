-- Task 1.3 #2
CREATE PROCEDURE CountriesWithBG
AS
SELECT 
	CustomerID, Country
FROM 
	dbo.Customers
WHERE 
	LEFT(Country,1) BETWEEN 'B' AND 'G'
ORDER BY 
	Country