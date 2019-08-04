CREATE PROCEDURE [query_2_2_4]
AS	
	SELECT CompanyName as 'Company', FirstName as 'Name'
	FROM Employees, Customers
	WHERE Employees.City = Customers.City