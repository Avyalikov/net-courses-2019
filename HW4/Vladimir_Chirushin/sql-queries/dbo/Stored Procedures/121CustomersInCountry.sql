-- Task 1.2 #1
CREATE PROCEDURE CustomersInCountry
AS
SELECT 
	ContactName, 
	Country
FROM 
	dbo.Customers
WHERE
	Country IN ('USA', 'Canada')
ORDER BY
	Customers.Country
