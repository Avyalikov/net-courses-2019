-- Task 2.2 #5
CREATE PROCEDURE FindCustomersInCity
AS
SELECT  
	DISTINCT Customers.City, 
	Customers.ContactName AS 'Customer'
FROM 
	dbo.Customers 
GROUP BY 
	Customers.ContactName, 
	Customers.City
ORDER BY 
	Customers.City