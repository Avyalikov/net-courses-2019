-- Task 2.2 #5
CREATE PROCEDURE FindCustomersInCity
AS
SELECT 
	Customers.ContactName
FROM 
	dbo.Customers
WHERE Customers.City IN 
		(SELECT 
			Customers.City
		FROM 
			dbo.Customers
		GROUP BY 
			Customers.City
		HAVING 
			COUNT(Customers.CustomerID) >= 2)
ORDER BY Customers.ContactName
