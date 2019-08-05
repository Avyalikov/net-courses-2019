CREATE PROCEDURE [dbo].[224]
AS
	SELECT LastName + ' ' + FirstName
	FROM Customers, Employees
	WHERE Customers.City = Employees.City
	UNION
	SELECT CompanyName
	FROM Customers, Employees
	WHERE Customers.City = Employees.City
RETURN 0
