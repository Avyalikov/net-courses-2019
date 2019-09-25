-- Task 1.2 #2
CREATE PROCEDURE CustomersNotInCountry
AS
SELECT 
	ContactName, 
	Country as 'Country'
FROM 
	dbo.Customers
WHERE
	Country NOT IN ('USA', 'Canada')
ORDER BY 
	ContactName