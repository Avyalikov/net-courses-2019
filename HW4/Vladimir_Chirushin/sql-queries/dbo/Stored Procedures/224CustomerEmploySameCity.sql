-- Task 2.2 #4
CREATE PROCEDURE CustomerEmploySameCity
AS
SELECT 
	CONCAT(Employees.LastName, ' ', Employees.FirstName) AS 'Seller', 
	Customers.ContactName AS 'Customer'
FROM
	dbo.Employees,
	dbo.Customers
WHERE
	Employees.City = Customers.City
